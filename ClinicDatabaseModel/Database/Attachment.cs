using System;
using System.Collections.Generic;

namespace ClinicDatabaseModel.Database;

/// <summary>
/// РџСЂРёРєСЂРµРїР»РµРЅРёСЏ
/// </summary>
public partial class Attachment
{
    /// <summary>
    /// Р”Р°С‚Р° РїСЂРёРєСЂРµРїР»РµРЅРёСЏ
    /// </summary>
    public DateOnly Since { get; set; }

    public long BranchId { get; set; }

    public long PatientId { get; set; }

    public long SpecilityId { get; set; }

    public virtual Branch Branch { get; set; } = null!;

    public virtual Patient Patient { get; set; } = null!;

    public virtual Speciality Specility { get; set; } = null!;
}
