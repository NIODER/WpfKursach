using System;
using System.Collections.Generic;

namespace ClinicDatabaseModel.Database;

/// <summary>
/// РЎРїРµС†РёР°Р»СЊРЅРѕСЃС‚СЊ
/// </summary>
public partial class Speciality
{
    public long SpecilityId { get; set; }

    /// <summary>
    /// РЎРїРµС†РёР°Р»СЊРЅРѕСЃС‚СЊ
    /// </summary>
    public string SpecialityName { get; set; } = null!;

    public virtual ICollection<Attachment> Attachments { get; } = new List<Attachment>();

    public virtual ICollection<Employee> Employees { get; } = new List<Employee>();
}
