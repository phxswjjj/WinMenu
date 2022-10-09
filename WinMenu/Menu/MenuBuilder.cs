using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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

        internal MenuBuilder LoadXML(string path)
        {
            var doc = XDocument.Load(path);
            this.Document = doc;

            var xsd = Path.ChangeExtension(path, "xsd");
            if (File.Exists(xsd))
            {
                var validateEventHandler = new ValidationEventHandler((o, e) =>
                {
                    switch (e.Severity)
                    {
                        case XmlSeverityType.Error:
                        case XmlSeverityType.Warning:
                            throw new Exception(e.Message);
                    }
                });

                var reader = new XmlTextReader("Menu.xsd");
                var schema = XmlSchema.Read(reader, validateEventHandler);
                var schemas = new XmlSchemaSet();
                schemas.Add(schema);

                doc.Validate(schemas, validateEventHandler);
            }

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
            else
            {
                var typeString = rootNode.Attribute("Type")?.Value;
                if (!string.IsNullOrEmpty(typeString))
                {
                    if (typeString.StartsWith("."))
                    {
                        var a = Assembly.GetCallingAssembly();
                        var s = a.EntryPoint.DeclaringType.Namespace;
                        typeString = $"{s}.Menu{typeString}";
                    }
                    var t = Type.GetType(typeString);
                    var obj = Activator.CreateInstance(t);
                    if (obj is IMenuItem)
                    {
                        var mu = (IMenuItem)obj;
                        rootItem.Click += mu.OnClick;
                    }
                    else
                        throw new Exception($"{typeString} 必須實作 IMenuItem");
                }
            }
            return rootItem;
        }
    }
}
