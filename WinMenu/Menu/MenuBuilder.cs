using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml.XPath;

namespace WinMenu.Menu
{
    internal class MenuBuilder
    {
        private XDocument Document;

        internal MenuBuilder LoadConfig(string path)
        {
            ValidationEventHandler validateEventHandler = new ValidationEventHandler((o, e) =>
            {
                switch (e.Severity)
                {
                    case XmlSeverityType.Error:
                    case XmlSeverityType.Warning:
                        throw new Exception(e.Message);
                }
            });

            XmlTextReader reader = new XmlTextReader("Menu.xsd");
            XmlSchema schema = XmlSchema.Read(reader, validateEventHandler);
            XmlSchemaSet schemas = new XmlSchemaSet();
            schemas.Add(schema);

            var doc = XDocument.Load(path);
            doc.Validate(schemas, validateEventHandler);

            this.Document = doc;
            return this;
        }

        internal ToolStripItem[] Build()
        {
            var items = new List<ToolStripMenuItem>();

            var doc = this.Document;
            foreach (var rootNode in doc.XPathSelectElements("/Menu/Node"))
            {
                var item = Create(rootNode);
                items.Add(item);
            }

            return items.ToArray();
        }

        private ToolStripMenuItem Create(XElement rootNode)
        {
            var rootItem = new ToolStripMenuItem();
            rootItem.Text = rootNode.Attribute("Title").Value;
            if (rootNode.HasElements)
            {
                foreach (var node in rootNode.Elements())
                {
                    var item = Create(node);
                    rootItem.DropDownItems.Add(item);
                }
            }
            return rootItem;
        }
    }
}
