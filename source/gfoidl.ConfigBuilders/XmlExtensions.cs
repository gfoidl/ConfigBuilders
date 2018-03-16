using System.Xml;
using System.Xml.Linq;

namespace gfoidl.ConfigBuilders
{
    // Cf. https://stackoverflow.com/a/5399711
    internal static class XmlExtensions
    {
        public static XElement ToXElement(this XmlNode node)
        {
            //XElement xml = XElement.Load(rawXml.CreateNavigator().ReadSubtree());

            var xDoc = new XDocument();

            using (XmlWriter xmlWriter = xDoc.CreateWriter())
                node.WriteTo(xmlWriter);

            return xDoc.Root;
        }
        //---------------------------------------------------------------------
        public static XmlNode ToXmlNode(this XElement element)
        {
            using (XmlReader xmlReader = element.CreateReader())
            {
                var xmlDoc = new XmlDocument();
                xmlDoc.Load(xmlReader);

                return xmlDoc;
            }
        }
    }
}
