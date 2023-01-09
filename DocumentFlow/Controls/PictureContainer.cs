//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 09.01.2023
//-----------------------------------------------------------------------

using System.ComponentModel;
using System.Windows.Forms.Design;

namespace DocumentFlow.Controls;

[Designer(typeof(ParentControlDesigner))]
public class PictureContainer : PictureBox {}
