using System;
using System.Threading.Tasks;
using System.Windows.Forms;

using SphereStudio.Base;

using SphereStudio.Properties;
using SphereStudio.SsjKi;

namespace SphereStudio.Forms
{
    partial class JsObjectViewer : Form, IStyleAware
    {
        private Inferior debuggee;
        private KiAtom jsValue;

        public JsObjectViewer(Inferior inferior, string objectName, KiAtom value)
        {
            InitializeComponent();
            StyleManager.AutoStyle(this);

            treeIconImageList.Images.Add("object", Resources.StackIcon);
            treeIconImageList.Images.Add("prop", Resources.VisibleIcon);
            treeIconImageList.Images.Add("hiddenProp", Resources.InvisibleIcon);

            debuggee = inferior;
            jsValue = value;
            nameTextBox.Text = string.Format("eval('{0}') = {1};",
                objectName.Replace(@"\", @"\\").Replace("'", @"\'").Replace("\n", @"\n").Replace("\r", @"\r"),
                jsValue.ToString());
        }

        public void ApplyStyle(UIStyle style)
        {
            style.AsUIElement(this);
            style.AsTextView(nameTextBox);
            style.AsTextView(propListTreeView);
            style.AsAccent(closeButton);

        }

        private async void this_Load(object sender, EventArgs e)
        {
            propListTreeView.BeginUpdate();
            propListTreeView.Nodes.Clear();
            var trunk = propListTreeView.Nodes.Add(jsValue.ToString());
            trunk.ImageKey = "object";
            if (jsValue.Tag == KiTag.Ref)
                await PopulateTreeNode(trunk, (KiRef)jsValue);
            trunk.Expand();
            propListTreeView.EndUpdate();
        }

        private async void PropTree_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            if (e.Node.Tag == null || e.Node.Nodes[0].Text != "")
                return;

            PropDesc propDesc = (PropDesc)e.Node.Tag;
            if (propDesc.Value.Tag == KiTag.Ref)
                await PopulateTreeNode(e.Node, (KiRef)propDesc.Value);
        }

        private void PropTree_MouseMove(object sender, MouseEventArgs e)
        {
            var ht = propListTreeView.HitTest(e.Location);
            propListTreeView.Cursor = ht.Node != null && ht.Node.Bounds.Contains(e.Location)
                ? Cursors.Hand : Cursors.Default;
        }

        private async Task PopulateTreeNode(TreeNode node, KiRef handle)
        {
            propListTreeView.BeginUpdate();
            var props = await debuggee.InspectObject(handle, 0, int.MaxValue);
            foreach (var key in props.Keys) {
                if (props[key].Flags.HasFlag(PropFlags.Accessor)) {
                    KiAtom getter = props[key].Getter;
                    KiAtom setter = props[key].Setter;
                    var nodeText = string.Format("{0} = get: {1}, set: {2}", key,
                        getter.ToString(), setter.ToString());
                    var valueNode = node.Nodes.Add(nodeText);
                    valueNode.ImageKey = "prop";
                    valueNode.SelectedImageKey = valueNode.ImageKey;
                    valueNode.Tag = props[key];
                }
                else {
                    KiAtom value = props[key].Value;
                    var nodeText = string.Format("{0} = {1}", key, value.ToString());
                    var valueNode = node.Nodes.Add(nodeText);
                    valueNode.ImageKey = "prop";
                    valueNode.SelectedImageKey = valueNode.ImageKey;
                    valueNode.Tag = props[key];
                    if (value.Tag == KiTag.Ref) {
                        valueNode.Nodes.Add("");
                    }
                }
            }
            if (node.Nodes.Count > 0 && node.Nodes[0].Text == "")
                node.Nodes.RemoveAt(0);
            propListTreeView.EndUpdate();
        }
    }
}
