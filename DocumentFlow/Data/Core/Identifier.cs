//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 07.11.2014
//-----------------------------------------------------------------------

using DocumentFlow.Data.Infrastructure;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace DocumentFlow.Data.Core;

public abstract class Identifier<T> : INotifyPropertyChanged, IIdentifier<T>
    where T : struct, IComparable
{
    public event PropertyChangedEventHandler? PropertyChanged;

    [Exclude]
    [Display(AutoGenerateField = false)]
    public T id { get; set; }

    protected void NotifyPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    protected byte[] ImageToByteArray(Image imageIn)
    {
        MemoryStream ms = new();
        imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
        return ms.ToArray();
    }
}
