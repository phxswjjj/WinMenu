using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace WinMenu.Menu
{
    internal class MenuBuilder
    {
        private XmlDocument Document;

        internal MenuBuilder LoadConfig(string path)
        {
            var doc = new XmlDocument();
            doc.Load(path);
            this.Document = doc;
            return this;
        }

        internal ToolStripItem[] Build()
        {
            var items = new List<ToolStripMenuItem>();

            var doc = this.Document;
            foreach (XmlNode rootNode in doc.SelectNodes("/Menu/Node"))
            {
                var item = Create(rootNode);
                items.Add(item);
            }

            return items.ToArray();
        }
        private static ToolStripMenuItem Create(XmlNode rootNode)
        {
            var rootItem = new ToolStripMenuItem();
            rootItem.Text = rootNode.Attributes["Title"].Value;
            if (rootNode.HasChildNodes)
            {
                foreach (XmlNode node in rootNode.ChildNodes)
                {
                    var item = Create(node);
                    rootItem.DropDownItems.Add(item);
                }
            }
            return rootItem;
        }
    }
}
