//-----------------------------------------------------------------------
// Copyright © 2010-2019 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 08.03.2019
// Time: 17:13
//-----------------------------------------------------------------------

using System;
using System.Text.RegularExpressions;

namespace DocumentFlow.Data.Core
{
    public class Document : DocumentInfo, IDocument, IComparable, IComparable<Document>
    {
        public string document_name { get; protected set; }
        public DateTime doc_date { get; set; }
        public int doc_year { get; set; }
        public string doc_number { get; set; }
        public Guid organization_id { get; set; }
        
        public int CompareTo(object obj)
        {
            if (obj is Document other)
            {
                return CompareTo(other);
            }

            throw new Exception($"{obj} должен быть типа {GetType().Name}");
        }

        public int CompareTo(Document other)
        {
            long sec_current = doc_date.Ticks / 10000000;
            long sec_other = other.doc_date.Ticks / 10000000;
            int res = sec_current.CompareTo(sec_other);
            if (res == 0)
            {
                Regex regex_current = new Regex(@"^\D*(\d+)");
                MatchCollection matches_current = regex_current.Matches(doc_number);

                Regex regex_other = new Regex(@"^\D*(\d+)");
                MatchCollection matches_other = regex_other.Matches(other.doc_number);

                if (matches_current.Count == 0 || matches_other.Count == 0)
                {
                    res = doc_number.CompareTo(other.doc_number);
                }
                else
                {
                    long number_current = Convert.ToInt64(matches_current[0].Value);
                    long number_other = Convert.ToInt64(matches_other[0].Value);
                    res = number_current.CompareTo(number_other);
                }
            }

            return res;
        }
    }
}
