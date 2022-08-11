//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 06.02.2022
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Infrastructure;

namespace DocumentFlow.Entities.PaymentOrders.Posting;

public interface IPostingPaymentsEditor : IEditor<PostingPayments, IPostingPaymentsBrowser>
{
}
