//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 20.09.2021
//-----------------------------------------------------------------------

using DocumentFlow.Data.Tools;

using Humanizer;

using System.ComponentModel;

namespace DocumentFlow.Data.Models;

public enum SubjectsCivilLow 
{
    [Description("Физическое лицо")]
    Person,

    [Description("Юридическое лицо")]
    LegalEntity 
}

[EntityName("Контрагент")]
public class Contractor : Company
{
    public static readonly Guid CompanyGroup = new("{AEE39994-7BFE-46C0-828B-AC6296103CD1}");
    public static readonly Guid PersonGroup = new("{A9799032-2C6A-46DA-AB8A-CF6423E3BEB6}");

    private string? subject;
    private Guid? personId;

    [EnumType("subjects_civil_low")]
    public string? Subject
    {
        get => subject;
        set
        {
            if (subject != value) 
            { 
                subject = value;
                NotifyPropertyChanged();
            }
        }
    }

    public Guid? PersonId
    {
        get => personId;
        set 
        { 
            if (personId != value) 
            { 
                personId = value;
                NotifyPropertyChanged();
            }
        }
    }

    [Computed]
    public SubjectsCivilLow? SubjectCivilLow
    {
        get { return Subject == null ? null : Enum.Parse<SubjectsCivilLow>(Subject.Dehumanize()); }
        protected set { Subject = value?.ToString().Humanize(LetterCasing.LowerCase); }
    }

    public override string ToString() => Code;
}
