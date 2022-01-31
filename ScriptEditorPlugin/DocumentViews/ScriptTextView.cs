using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

using SphereStudio.Base;
using SphereStudio.Components;
using SphereStudio.Properties;

using ScintillaNET;

namespace SphereStudio.DocumentViews
{
    enum ScriptType
    {
        Sphere,
        Cellscript,
    }

    public partial class ScriptTextView : TextView, IStyleAware
    {
        private int activeLine = 0;
        private int errorLine = 0;
        private bool showCurrentLine = true;
        private int lastCaretPosition = Scintilla.InvalidPosition;
        private string lastKnownFileName = "";
        private int lineMarginWidth = -1;
        private PluginMain plugin;
        private QuickFinder quickFinder;
        private Scintilla scintilla = new Scintilla();
        private bool useAutoComplete;

        public ScriptTextView(PluginMain plugin, bool highlight = false)
        {
            InitializeComponent();

            Icon = Icon.FromHandle(Resources.ScriptIcon.GetHicon());

            this.plugin = plugin;
            quickFinder = new QuickFinder(this, scintilla);

            scintilla.BorderStyle = BorderStyle.None;
            scintilla.Dock = DockStyle.Fill;
            scintilla.Styles[Style.Default].Font = StyleManager.Style.FixedFont.Name;
            scintilla.Styles[Style.Default].SizeF = StyleManager.Style.FixedFont.Size;
            scintilla.Styles[Style.Default].ForeColor = StyleManager.Style.TextColor;
            scintilla.Styles[Style.Default].BackColor = StyleManager.Style.BackColor;
            scintilla.StyleClearAll();
            scintilla.CharAdded += codeBox_CharAdded;
            scintilla.InsertCheck += codeBox_InsertCheck;
            scintilla.KeyDown += codeBox_KeyDown;
            scintilla.MarginClick += codeBox_MarginClick;
            scintilla.SavePointLeft += codeBox_SavePointLeft;
            scintilla.SavePointReached += codeBox_SavePointReached;
            scintilla.TextChanged += codeBox_TextChanged;
            scintilla.UpdateUI += codeBox_UpdateUI;
            Controls.Add(scintilla);

            initializeAutoComplete();
            initializeMargins();
            if (highlight) {
                scintilla.Lexer = Lexer.Cpp;
                initializeHighlighting(string.Empty);
                initializeCodeFolding();
            }

            StyleManager.AutoStyle(this);
        }

        public override int ActiveLine
        {
            get
            {
                return activeLine;
            }
            set
            {
                if (activeLine > 0)
                    scintilla.Lines[activeLine - 1].MarkerDelete(1);
                activeLine = value;
                if (activeLine > 0)
                {
                    scintilla.Lines[activeLine - 1].MarkerAdd(1);
                    scintilla.GotoPosition(scintilla.Lines[activeLine - 1].Position);
                    var parentLine = scintilla.Lines[activeLine - 1].FoldParent;
                    if (parentLine >= 0 && !scintilla.Lines[parentLine].Expanded)
                        scintilla.Lines[parentLine].ToggleFold();
                }
            }
        }

        public override int[] BreakpointLines
        {
            get
            {
                return scintilla.Lines
                    .Where(line => (line.MarkerGet() & 0x01) != 0)
                    .Select(line => line.Index + 1)
                    .ToArray();
            }
            set
            {
                scintilla.MarkerDeleteAll(0);
                foreach (var lineNumber in value)
                    scintilla.Lines[lineNumber - 1].MarkerAdd(0);
            }
        }

        public override int ErrorLine
        {
            get
            {
                return errorLine;
            }
            set
            {
                if (errorLine > 0)
                    scintilla.Lines[errorLine - 1].MarkerDelete(2);
                errorLine = value;
                if (errorLine > 0)
                {
                    scintilla.Lines[errorLine - 1].MarkerAdd(2);
                    scintilla.GotoPosition(scintilla.Lines[errorLine - 1].Position);
                    var parentLine = scintilla.Lines[activeLine - 1].FoldParent;
                    if (parentLine >= 0 && !scintilla.Lines[parentLine].Expanded)
                        scintilla.Lines[parentLine].ToggleFold();
                }
            }
        }

        public override string[] FileExtensions
        {
            get => new[] { "js", "mjs", "ts", "json" };
        }

        public override bool ReadOnly
        {
            get => scintilla.ReadOnly;
            set => scintilla.ReadOnly = value;
        }

        public override string Text
        {
            get
            {
                return scintilla.Text;
            }
            set
            {
                scintilla.Text = value;
                scintilla.EmptyUndoBuffer();
            }
        }

        public override string ViewState
        {
            get
            {
                return $"{scintilla.CurrentPosition}|{scintilla.AnchorPosition}|{scintilla.FirstVisibleLine}";
            }
            set
            {
                var values = value.Split('|');
                scintilla.CurrentPosition = Convert.ToInt32(values[0]);
                scintilla.AnchorPosition = Convert.ToInt32(values[1]);
                scintilla.FirstVisibleLine = Convert.ToInt32(values[2]);
            }
        }

        public override void Activate()
        {
            plugin.ShowMenus(true);
        }

        public void ApplyStyle(UIStyle style)
        {
            scintilla.TabWidth = plugin.Settings.IndentWidth;
            scintilla.IndentWidth = scintilla.TabWidth;
            scintilla.UseTabs = plugin.Settings.PreferTabs;

            useAutoComplete = plugin.Settings.AutoComplete;
            showCurrentLine = plugin.Settings.ShowCurrentLine;

            var useFolding = plugin.Settings.EnableCodeFolding;
            scintilla.Margins[2].Width = useFolding ? 16 : 0;

            scintilla.CaretForeColor = StyleManager.Style.TextColor;
            scintilla.Styles[Style.Default].Font = StyleManager.Style.FixedFont.Name;
            scintilla.Styles[Style.Default].SizeF = StyleManager.Style.FixedFont.Size;
            scintilla.Styles[Style.Default].ForeColor = StyleManager.Style.TextColor;
            scintilla.Styles[Style.Default].BackColor = StyleManager.Style.BackColor;
            scintilla.StyleClearAll();
            initializeCodeFolding();
            initializeHighlighting(lastKnownFileName);
            initializeMargins();
        }

        public override void Copy()
        {
            scintilla.Copy();
        }

        public override void Cut()
        {
            scintilla.Cut();
        }

        public override void Deactivate()
        {
            plugin.ShowMenus(false);
        }

        public override void GoToLine(int lineNumber)
        {
            scintilla.GotoPosition(scintilla.Lines[lineNumber - 1].Position);
        }

        public override void Load(string fileName)
        {
            using (var fileReader = new StreamReader(fileName, true))
            {
                var extension = Path.GetExtension(fileName).StartsWith(".")
                    ? Path.GetExtension(fileName).Substring(1)
                    : string.Empty;
                scintilla.Lexer = FileExtensions.Contains(extension) || Path.GetFileNameWithoutExtension(fileName) == "Cellscript"
                    ? Lexer.Cpp : Lexer.Null;
                scintilla.Text = fileReader.ReadToEnd();
                scintilla.EmptyUndoBuffer();
                if (scintilla.Lexer == Lexer.Cpp)
                {
                    initializeHighlighting(fileName);
                    initializeCodeFolding();
                }

                if (PluginManager.Core.Project != null)
                    BreakpointLines = PluginManager.Core.Project.GetBreakpoints(fileName);
                else
                    BreakpointLines = new int[0];
            }

            scintilla.SetSavePoint();
        }

        public override bool NewDocument()
        {
            scintilla.Text = string.Empty;
            scintilla.Lexer = Lexer.Cpp;
            initializeHighlighting(string.Empty);
            initializeCodeFolding();

            scintilla.EmptyUndoBuffer();
            scintilla.SetSavePoint();
            return true;
        }

        public override void Paste()
        {
            if (scintilla.CanPaste)
                scintilla.Paste();
        }

        public override void Save(string filename)
        {
            using (var writer = new StreamWriter(filename, false, new UTF8Encoding(false)))
            {
                writer.Write(scintilla.Text);
            }
            scintilla.SetSavePoint();
        }

        public override void Redo()
        {
            if (scintilla.CanRedo)
                scintilla.Redo();
        }

        public override void SelectAll()
        {
            scintilla.SelectAll();
        }

        public override void Undo()
        {
            if (scintilla.CanUndo)
                scintilla.Undo();
        }

        public override void ZoomIn()
        {
            scintilla.ZoomIn();
        }

        public override void ZoomOut()
        {
            scintilla.ZoomOut();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keys)
        {
            switch (keys)
            {
                case Keys.Escape:
                    if (quickFinder.Visible)
                    {
                        quickFinder.Close();
                        return true;
                    }
                    break;
                case Keys.F3:
                    quickFinder.FindNext();
                    return true;
                case Keys.Control | Keys.A:
                    if (scintilla.Focused)
                        scintilla.SelectAll();
                    return true;
                case Keys.Control | Keys.F:
                    quickFinder.Open();
                    return true;
                case Keys.Control | Keys.H:
                    quickFinder.Open(true);
                    return true;
            }
            return base.ProcessCmdKey(ref msg, keys);
        }

        private void initializeAutoComplete()
        {
            scintilla.AutoCCancelAtStart = true;
            scintilla.AutoCChooseSingle = false;
            scintilla.AutoCIgnoreCase = true;
            scintilla.AutoCSeparator = ';';
            scintilla.AutoCSetFillUps("");
            scintilla.AutoCStops("(");
        }

        private void initializeCodeFolding()
        {
            scintilla.SetProperty("fold", "1");
            scintilla.SetProperty("fold.compact", "1");
            scintilla.AutomaticFold = AutomaticFold.Show | AutomaticFold.Click | AutomaticFold.Change;
            scintilla.SetFoldFlags(FoldFlags.LineAfterContracted);
            scintilla.SetFoldMarginColor(true, StyleManager.Style.BackColor);
            scintilla.SetFoldMarginHighlightColor(true, StyleManager.Style.BackColor);
        }

        private void initializeHighlighting(string fileName)
        {
            if (fileName == null)
                return;

            scintilla.SetProperty("lexer.cpp.backquoted.strings", "1");

            // define colors for syntax highlighting.  the colors below were chosen to be very
            // similar to the Sphere 1.x editor.
            //_codeBox.SetSelectionForeColor(true, Styler.Style.TextColor);
            scintilla.SetSelectionBackColor(true, StyleManager.Style.HighlightColor);
            scintilla.Styles[Style.BraceLight].ForeColor = Color.Chartreuse;
            scintilla.Styles[Style.BraceLight].BackColor = Color.DarkGreen;
            scintilla.Styles[Style.BraceBad].ForeColor = Color.Orange;
            scintilla.Styles[Style.BraceBad].BackColor = Color.DarkRed;
            if (StyleManager.Style.BackColor.GetBrightness() < 0.5)
            {
                scintilla.Styles[Style.Cpp.Character].ForeColor = Color.DarkSalmon;
                scintilla.Styles[Style.Cpp.Comment].ForeColor = Color.OliveDrab;
                scintilla.Styles[Style.Cpp.CommentDoc].ForeColor = Color.OliveDrab;
                scintilla.Styles[Style.Cpp.CommentLine].ForeColor = Color.OliveDrab;
                scintilla.Styles[Style.Cpp.CommentLineDoc].ForeColor = Color.DimGray;
                scintilla.Styles[Style.Cpp.GlobalClass].ForeColor = Color.LightSeaGreen;
                scintilla.Styles[Style.Cpp.Number].ForeColor = Color.DarkSalmon;
                scintilla.Styles[Style.Cpp.Operator].ForeColor = Color.Plum;
                scintilla.Styles[Style.Cpp.Regex].ForeColor = Color.AntiqueWhite;
                scintilla.Styles[Style.Cpp.String].ForeColor = Color.Khaki;
                scintilla.Styles[Style.Cpp.StringEol].ForeColor = Color.Red;
                scintilla.Styles[Style.Cpp.StringEol].BackColor = StyleManager.Style.BackColor;
                scintilla.Styles[Style.Cpp.StringRaw].ForeColor = Color.Orchid;
                scintilla.Styles[Style.Cpp.Word].ForeColor = Color.CornflowerBlue;
                scintilla.Styles[Style.Cpp.Word2].ForeColor = Color.MediumSeaGreen;
            }
            else
            {
                scintilla.Styles[Style.Cpp.Character].ForeColor = Color.DarkRed;
                scintilla.Styles[Style.Cpp.Comment].ForeColor = Color.Green;
                scintilla.Styles[Style.Cpp.CommentDoc].ForeColor = Color.Green;
                scintilla.Styles[Style.Cpp.CommentLine].ForeColor = Color.Green;
                scintilla.Styles[Style.Cpp.CommentLineDoc].ForeColor = Color.DimGray;
                scintilla.Styles[Style.Cpp.GlobalClass].ForeColor = Color.DarkMagenta;
                scintilla.Styles[Style.Cpp.Number].ForeColor = Color.DarkRed;
                scintilla.Styles[Style.Cpp.Operator].ForeColor = Color.Gray;
                scintilla.Styles[Style.Cpp.Regex].ForeColor = Color.SteelBlue;
                scintilla.Styles[Style.Cpp.String].ForeColor = Color.Teal;
                scintilla.Styles[Style.Cpp.StringEol].ForeColor = Color.Red;
                scintilla.Styles[Style.Cpp.StringEol].BackColor = StyleManager.Style.BackColor;
                scintilla.Styles[Style.Cpp.StringRaw].ForeColor = Color.DarkOrchid;
                scintilla.Styles[Style.Cpp.Word].ForeColor = Color.Blue;
                scintilla.Styles[Style.Cpp.Word2].ForeColor = Color.DimGray;
            }

            // tell Scintilla about JS keywords.  a generic lexer is used for C-like languages
            // so this unfortunately isn't done for us.
            scintilla.SetKeywords(0, "as async await break case catch class const continue debugger default delete do else enum export extends false finally for from function get if implements import in instanceof interface let of new null package private protected public return set static super switch this throw true try typeof var void while with yield");
            scintilla.SetKeywords(1, "arguments eval exports global module require undefined Infinity NaN"
                + " Array ArrayBuffer Atomics Boolean DataView Date Error EvalError Float32Array Float64Array Function Int8Array Int16Array Int32Array JSON Map Math Number Object Promise Proxy RangeError ReferenceError Reflect RegExp Set SharedArrayBuffer String Symbol SyntaxError TypeError Uint8Array Uint8ClampedArray Uint16Array Uint32Array URIError WeakMap WeakSet"
                + " decodeURI decodeURIComponent encodeURI encodeURIComponent escape isFinite isNaN parseFloat parseInt unescape");

            // load Sphere API keywords
            try {
                lastKnownFileName = fileName;
                var dictionaryName = @"Dictionary\SphereAPI.txt";
                if (Path.GetFileNameWithoutExtension(fileName) == "Cellscript")
                    dictionaryName = @"Dictionary\CellscriptAPI.txt";
                var apiList = File.ReadAllLines(Path.Combine(Application.StartupPath, dictionaryName));
                scintilla.SetKeywords(3, string.Join(" ", from line in apiList
                                                          let keyword = line.Trim()
                                                          where keyword != "" && !keyword.StartsWith("#")
                                                          select keyword));
            }
            catch {
                // *MUNCH* //
            }
        }

        private void initializeMargins()
        {
            // initialize code folding icons
            scintilla.Markers[Marker.Folder].Symbol = MarkerSymbol.BoxPlus;
            scintilla.Markers[Marker.FolderOpen].Symbol = MarkerSymbol.BoxMinus;
            scintilla.Markers[Marker.FolderEnd].Symbol = MarkerSymbol.BoxPlusConnected;
            scintilla.Markers[Marker.FolderMidTail].Symbol = MarkerSymbol.TCorner;
            scintilla.Markers[Marker.FolderOpenMid].Symbol = MarkerSymbol.BoxMinusConnected;
            scintilla.Markers[Marker.FolderSub].Symbol = MarkerSymbol.VLine;
            scintilla.Markers[Marker.FolderTail].Symbol = MarkerSymbol.LCorner;
            for (var i = Marker.FolderEnd; i <= Marker.FolderOpen; ++i)
            {
                scintilla.Markers[i].SetForeColor(StyleManager.Style.BackColor);
                scintilla.Markers[i].SetBackColor(StyleManager.Style.TextColor);
            }

            // debugging margin
            scintilla.Margins[0].Type = MarginType.Symbol;
            scintilla.Margins[0].Mask = Marker.MaskAll & ~Marker.MaskFolders;
            scintilla.Margins[0].Width = 20;
            scintilla.Margins[0].Sensitive = true;
            scintilla.Markers[0].Symbol = MarkerSymbol.Circle;  // breakpoint marker
            scintilla.Markers[0].SetBackColor(Color.Red);
            scintilla.Markers[0].SetForeColor(Color.DarkRed);
            scintilla.Markers[1].Symbol = MarkerSymbol.ShortArrow;  // next line to execute
            scintilla.Markers[1].SetBackColor(Color.Yellow);
            scintilla.Markers[1].SetForeColor(Color.Black);
            scintilla.Markers[2].Symbol = MarkerSymbol.Background;  // error highlight
            scintilla.Markers[2].SetBackColor(Color.FromArgb(96, 48, 48));
            scintilla.Markers[3].Symbol = MarkerSymbol.Background;  // current line highlight
            scintilla.Markers[3].SetBackColor(StyleManager.Style.AccentColor);

            // line number margin - dynamically resized as content changes
            scintilla.Margins[1].Type = MarginType.Number;
            scintilla.Margins[1].Mask = 0x0;
            scintilla.Styles[Style.LineNumber].ForeColor = StyleManager.Style.HighlightColor;
            scintilla.Styles[Style.LineNumber].BackColor = StyleManager.Style.BackColor;

            // code folding margin
            scintilla.Margins[2].Type = MarginType.Symbol;
            scintilla.Margins[2].Mask = Marker.MaskFolders;
            scintilla.Margins[2].Width = 20;
            scintilla.Margins[2].Sensitive = true;
        }

        private void codeBox_CharAdded(object sender, CharAddedEventArgs e)
        {
            if (char.IsLetter((char)e.Char) && useAutoComplete && scintilla.Lexer == Lexer.Cpp)
            {
                var word = scintilla.GetWordFromPosition(scintilla.CurrentPosition).ToLower();
                var filter = string.Join(";", plugin.Functions
                    .Where(name => name.ToLower().Contains(word))
                    .Select(name => name.Replace(";", "")));
                if (filter.Length > 0)
                    scintilla.AutoCShow(word.Length, filter);
            }
            else if (e.Char == '}')
            {
                var lineIndex = scintilla.LineFromPosition(scintilla.CurrentPosition);
                if (scintilla.Lines[lineIndex].Text.Trim() == "}")
                    scintilla.Lines[lineIndex].Indentation -= scintilla.IndentWidth;
            }
        }

        private void codeBox_InsertCheck(object sender, InsertCheckEventArgs e)
        {
            if (e.Text.EndsWith("\r") || e.Text.EndsWith("\n"))
            {
                var startPos = scintilla.Lines[scintilla.LineFromPosition(scintilla.CurrentPosition)].Position;
                var endPos = e.Position;
                var currentLineText = scintilla.GetTextRange(startPos, (endPos - startPos));
                var indent = Regex.Match(currentLineText, @"^[ \t]*");
                e.Text = $"{e.Text}{indent.Value}";
                if (Regex.IsMatch(currentLineText, @"{\s*$"))
                    e.Text = $"{e.Text}\t";
            }
        }

        private void codeBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F9 && e.Modifiers == 0x0)
            {
                e.Handled = true;
                var line = scintilla.Lines[scintilla.CurrentLine];
                var markers = line.MarkerGet();
                bool isSet = (markers & 0x01) == 0;
                OnBreakpointChanged(new BreakpointChangedEventArgs(line.Index + 1, isSet));
                if (isSet)
                    line.MarkerAdd(0);
                else
                    line.MarkerDelete(0);
            }
        }

        private void codeBox_MarginClick(object sender, MarginClickEventArgs e)
        {
            if (e.Margin == 0)
            {
                var line = scintilla.Lines[scintilla.LineFromPosition(e.Position)];
                bool isSet = (line.MarkerGet() & 0x01) == 0;
                OnBreakpointChanged(new BreakpointChangedEventArgs(line.Index + 1, isSet));
                if (isSet)
                    line.MarkerAdd(0);
                else
                    line.MarkerDelete(0);
            }
        }

        private void codeBox_SavePointLeft(object sender, EventArgs e)
        {
            Dirty = true;
        }

        private void codeBox_SavePointReached(object sender, EventArgs e)
        {
            Dirty = false;
        }

        private void codeBox_TextChanged(object sender, EventArgs e)
        {
            int lastLineNum = scintilla.Lines.Count - 1;
            int newMarginWidth = scintilla.TextWidth(Style.LineNumber, lastLineNum.ToString()) + 8;
            if (scintilla.Lexer == Lexer.Null)
                newMarginWidth = 0;
            if (newMarginWidth != lineMarginWidth)
                scintilla.Margins[1].Width = newMarginWidth;
            lineMarginWidth = newMarginWidth;
        }

        private void codeBox_UpdateUI(object sender, UpdateUIEventArgs e)
        {
            const string openBraceChars = "([{";
            const string closeBraceChars = ")]}";

            var caretPos = scintilla.CurrentPosition;
            if (caretPos != lastCaretPosition)
            {
                var currentLine = scintilla.LineFromPosition(caretPos);
                scintilla.MarkerDeleteAll(3);
                if (showCurrentLine)
                    scintilla.Lines[currentLine].MarkerAdd(3);
                var charBefore = (char)scintilla.GetCharAt(caretPos - 1);
                var charAfter = (char)scintilla.GetCharAt(caretPos);
                var brace1Pos = Scintilla.InvalidPosition;
                if (closeBraceChars.Contains(charBefore))
                    brace1Pos = caretPos - 1;
                else if (openBraceChars.Contains(charAfter))
                    brace1Pos = caretPos;
                if (brace1Pos != Scintilla.InvalidPosition)
                {
                    var brace2Pos = scintilla.BraceMatch(brace1Pos);
                    if (brace2Pos != Scintilla.InvalidPosition)
                        scintilla.BraceHighlight(brace1Pos, brace2Pos);
                    else
                        scintilla.BraceBadLight(brace1Pos);
                }
                else
                {
                    scintilla.BraceHighlight(Scintilla.InvalidPosition, Scintilla.InvalidPosition);
                }
                lastCaretPosition = caretPos;
            }
        }
    }
}
