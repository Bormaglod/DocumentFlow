//-----------------------------------------------------------------------
// Copyright © 2010-2019 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 20.04.2016
// Time: 21:16
//-----------------------------------------------------------------------

namespace DocumentFlow.Core
{
    using System;
    using System.IO;
    using System.Windows.Documents;
    using System.Windows.Markup;
    using System.Xml;
    
    public static class XamlExtension
    {
        /// <summary>
        /// Clones a table row
        /// </summary>
        /// <param name="row">original table row</param>
        /// <returns>cloned table row</returns>
        public static TableRow Clone(this TableRow row)
        {
            return row == null ? null : (TableRow)LoadXamlFromString(XamlWriter.Save(row));
        }

        /// <summary>
        /// Loads a XAML object from string
        /// </summary>
        /// <param name="s">string containing the XAML object</param>
        /// <returns>XAML object or null, if string was empty</returns>
        private static object LoadXamlFromString(string s)
        {
            if (String.IsNullOrEmpty(s)) return null;
            StringReader stringReader = new StringReader(s);
            XmlReader xmlReader = XmlReader.Create(stringReader, new XmlReaderSettings());
            return XamlReader.Load(xmlReader);
        }
    }
}
