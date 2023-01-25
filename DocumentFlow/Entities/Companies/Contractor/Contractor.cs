//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 20.09.2021
//
// Версия 2023.1.25
//  - добвавлены поля subject и person_id и соответствующая инфраструктура
//    для работы с subject
//
//-----------------------------------------------------------------------

using DocumentFlow.Data.Core;

using Humanizer;

namespace DocumentFlow.Entities.Companies;

public enum SubjectsCivilLow { Person, LegalEntity }

[Description("Контрагент")]
public class Contractor : Company
{
    public static readonly Guid CompanyGroup = new("{AEE39994-7BFE-46C0-828B-AC6296103CD1}");
    public static readonly Guid PersonGroup = new("{A9799032-2C6A-46DA-AB8A-CF6423E3BEB6}");

    public static readonly Dictionary<SubjectsCivilLow, string> subjects = new()
    {
        [SubjectsCivilLow.Person] = "Физическое лицо",
        [SubjectsCivilLow.LegalEntity] = "Юридическое лицо"
    };

    [EnumType("subjects_civil_low")]
    public string? subject { get; set; }

    public Guid? person_id { get; set; }

    public SubjectsCivilLow? SubjectCivilLow
    {
        get { return subject == null ? null : Enum.Parse<SubjectsCivilLow>(subject.Dehumanize()); }
        protected set { subject = value?.ToString().Humanize(LetterCasing.LowerCase); }
    }

    public static IReadOnlyDictionary<SubjectsCivilLow, string> Subjects => subjects;
}
