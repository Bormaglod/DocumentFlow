//-----------------------------------------------------------------------
// Copyright © 2010-2019 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 12.11.2014
// Time: 13:54
//-----------------------------------------------------------------------

namespace DocumentFlow.Core
{
    public static class StringExtension
    {
        public static string NullIfEmpty(this string source) => string.IsNullOrEmpty(source) ? null : source;
    }
}
