using System.Configuration;
using System.Xml;
using System.Xml.Linq;

namespace gfoidl.ConfigBuilders
{
    public abstract class DiagnosticListenerPathConfigBuilder : ConfigurationBuilder
    {
        public override XmlNode ProcessRawXml(XmlNode rawXml)
        {
            XElement xml = rawXml.ToXElement();

            foreach (XElement listener in xml.Descendants("listeners"))
            {
                foreach (XElement add in listener.Descendants("add"))
                {
                    XAttribute initializeData = add.Attribute("initializeData");

                    if (initializeData == null) continue;

                    string path          = initializeData.Value;
                    initializeData.Value = this.ModifyPath(path);
                }
            }

            return xml.ToXmlNode();
        }
        //---------------------------------------------------------------------
        public abstract string ModifyPath(string path);
    }
}
