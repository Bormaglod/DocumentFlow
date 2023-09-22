//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 05.02.2022
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Interfaces;
using DocumentFlow.Data.Models;
using DocumentFlow.Tools;

using System.ComponentModel;

namespace DocumentFlow.ViewModels;

[Description("Распределение")]
[EntityEditor(typeof(IPostingPaymentsEditor))]
public interface IPostingPaymentsBrowser : IBrowser<PostingPayments>
{
}
