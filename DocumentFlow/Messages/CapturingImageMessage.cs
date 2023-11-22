//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 15.11.2023
//-----------------------------------------------------------------------

namespace DocumentFlow.Messages;

public class CapturingImageMessage
{
    public CapturingImageMessage(Guid id, Image image) => (Id, Image) = (id, image);

    public Guid Id { get; }
    public Image Image {  get; }
}