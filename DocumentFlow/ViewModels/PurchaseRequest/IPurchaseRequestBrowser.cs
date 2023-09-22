﻿//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 01.01.2022
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Interfaces;
using DocumentFlow.Data.Models;
using DocumentFlow.Tools;

using System.ComponentModel;

namespace DocumentFlow.ViewModels;

[Description("Заявка")]
[MenuItem(MenuDestination.Document, "Заявка на приобретение материалов", order: 10)]
[EntityEditor(typeof(IPurchaseRequestEditor))]
public interface IPurchaseRequestBrowser : IBrowser<PurchaseRequest>
{
}