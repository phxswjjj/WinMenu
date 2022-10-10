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
using Unity;

namespace WinMenu.Menu
{
    internal class SecurityMenuBuilder
    {
        private XDocument Document;
        private Action<object, ChangeSecurityStateEventArgs> ChangeSecurityStateHandler;

        internal SecurityMenuBuilder LoadXML(string path)
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

                var reader = new XmlTextReader(xsd);
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
            var container = UnityResolver.Create();
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
                        typeString = $"{s}{typeString}";
                    }
                    var t = Type.GetType(typeString);
                    if (t == null)
                        throw new Exception($"{typeString} not defined");
                    var obj = container.Resolve(t);
                    if (obj is MenuBase mb)
                    {
                        var viewModeStr = rootNode.Attribute("ViewMode")?.Value;
                        if (!string.IsNullOrEmpty(viewModeStr))
                        {
                            mb.ViewMode = (ViewModeType)Enum.Parse(typeof(ViewModeType), viewModeStr);
                            mb.AccessString = rootNode.Attribute("AccessString")?.Value;
                            if (!string.IsNullOrEmpty(mb.AccessString) && mb.AccessString.StartsWith(":"))
                                mb.AccessString = $"Func{mb.AccessString}";
                        }
                    }

                    if (obj is IMenuItem mu)
                    {
                        rootItem.Click += mu.OnClick;
                    }
                    else
                        throw new Exception($"{typeString} 必須實作 IMenuItem");

                    if (obj is ISecurityMeunItem smu)
                        smu.ChangeSecurityState += OnChangeSecurityState;

                    rootItem.Tag = obj;
                }
            }
            if (rootItem.Tag == null)
                rootItem.Tag = container.Resolve<DefaultMenuItem>();
            return rootItem;
        }

        private void OnChangeSecurityState(object sender, ChangeSecurityStateEventArgs e)
        {
            if (this.ChangeSecurityStateHandler != null)
                this.ChangeSecurityStateHandler.Invoke(sender, e);
        }

        internal SecurityMenuBuilder SetChangeSecurityStateAction(Action<object, ChangeSecurityStateEventArgs> changeSecurityStateAction)
        {
            this.ChangeSecurityStateHandler = changeSecurityStateAction;
            return this;
        }
    }
}
