﻿//-----------------------------------------------------------------------
// Copyright © 2010-2020 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 07.10.2020
// Time: 19:51
//-----------------------------------------------------------------------

namespace DocumentFlow.Data
{
    public interface IDocument : IDocumentInfo
    {
        string document_name { get; }
    }
}