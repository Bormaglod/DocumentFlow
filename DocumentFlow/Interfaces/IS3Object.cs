﻿//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 24.09.2023
//-----------------------------------------------------------------------

using System.IO;

namespace DocumentFlow.Interfaces;

public interface IS3Object : IDisposable
{
    Task GetObject(string bucketName, string objectName, string fileName);
    Task GetObject(string bucketName, string objectName, Action<Stream> callbackStream);
    Task PutObject(string bucketName, string objectName, string fileName);
    Task RemoveObject(string bucketName, string objectName);
}
