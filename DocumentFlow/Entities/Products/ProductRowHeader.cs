﻿//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 22.05.2022
//-----------------------------------------------------------------------

using DocumentFlow.Data.Infrastructure;
using DocumentFlow.Properties;

namespace DocumentFlow.Entities.Products;

public class ProductRowHeader : IProductRowHeader
{
    public Image? Get(IDocumentInfo entity)
    {
        if (entity is Product product && product.thumbnails)
        {
            return product.deleted ? Resources.icons8_document_image_delete_16 : Resources.icons8_document_image_16;
        }

        return null;
    }
}
