using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Xml;

class XMLHelper
{
	XmlDocument xd;
    string XMLFile;
	public XMLHelper()
	{
		xd = new XmlDocument();
	}
	public XMLHelper(string XMLFile)
	{
		xd = new XmlDocument();
		xd.Load(XMLFile);
        this.XMLFile = XMLFile;
	}

	public void Load(string XMLFile)
	{
        xd.Load(XMLFile);
        this.XMLFile = XMLFile;
	}

	public string GetXmlPropertityValue(string PropertityName, string AttributeName)
	{
		try
		{
			XmlNodeList xmlNoteList = xd.GetElementsByTagName(PropertityName);
			XmlElement item = (XmlElement)xmlNoteList[0];
			string PropertityValue = item.GetAttribute(AttributeName);
			return PropertityValue;
		}
		catch (Exception ex)
		{
			throw ex;
		}
	}

	public void SetXmlPropertityValue(string PropertityName, string AttributeName, string PropertityValue)
	{
		try
		{
			XmlNodeList xmlNoteList = xd.GetElementsByTagName(PropertityName);
			XmlElement item = (XmlElement)xmlNoteList[0];
			item.SetAttribute(AttributeName, PropertityValue);
		}
		catch (Exception ex)
		{
			throw ex;
		}
	}

    public void Save()
    {
        xd.Save(XMLFile);
    }
}