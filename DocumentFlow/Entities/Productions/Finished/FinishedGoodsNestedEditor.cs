﻿//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 04.08.2022
//-----------------------------------------------------------------------

using DocumentFlow.Infrastructure;

namespace DocumentFlow.Entities.Productions.Finished;

public class FinishedGoodsNestedEditor : BaseFinishedGoodsEditor, IFinishedGoodsNestedEditor
{
    public FinishedGoodsNestedEditor(IFinishedGoodsRepository repository, IPageManager pageManager) 
        : base(repository, pageManager, true) { }
}