using System;
using System.ComponentModel;

namespace SphereStudio.Base
{
    /// <summary>
    /// Provides a base class for a text editing component.
    /// </summary>
    [ToolboxItem(false)]
    public class TextView : DocumentView
    {
        /// <summary>
        /// Fires when a breakpoint is added or removed.
        /// </summary>
        public event EventHandler<BreakpointChangedEventArgs> BreakpointChanged;
        
        /// <summary>
        /// Gets or sets the active line, used while debugging.
        /// </summary>
        public virtual int ActiveLine
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the line numbers of all breakpoints set in this script view.
        /// </summary>
        public virtual int[] BreakpointLines
        {
            get { return new int[0]; }
            set { }
        }

        /// <summary>
        /// Gets or sets the error line, used to highlight exceptions during debugging.
        /// </summary>
        public virtual int ErrorLine
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        /// <summary>
        /// Gets or sets the contents of the script view.
        /// </summary>
        public override string Text
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        /// <summary>
        /// Moves the cursor to a specified line number, used when debugging.
        /// </summary>
        /// <param name="lineNumber">The line number to navigate to.</param>
        public virtual void GoToLine(int lineNumber)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Fires a BreakpointChanged event for this ScriptView.
        /// </summary>
        /// <param name="e"></param>
        protected void OnBreakpointChanged(BreakpointChangedEventArgs e)
        {
            BreakpointChanged?.Invoke(this, e);
        }
    }

    /// <summary>
    /// Contains data for a BreakpointChanged event.
    /// </summary>
    public class BreakpointChangedEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes data for a BreakpointChanged event.
        /// </summary>
        /// <param name="lineNumber">The line number of the breakpoint being modified.</param>
        /// <param name="isActive">Whether the breakpoint is enabled or disabled.</param>
        public BreakpointChangedEventArgs(int lineNumber, bool isActive)
        {
            LineNumber = lineNumber;
            Active = isActive;
        }

        /// <summary>
        /// Whether the breakpoint is enabled or disabled.
        /// </summary>
        public bool Active { get; private set; }

        /// <summary>
        /// The line number containing the breakpoint.
        /// </summary>
        public int LineNumber { get; private set; }
    }
}
