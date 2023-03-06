using System;
using System.Collections.Generic;

namespace ClinicDatabaseModel.Database;

/// <summary>
/// РџСЂРёРµРјС‹
/// </summary>
public partial class Visit
{
    /// <summary>
    /// Р”Р°С‚Р° РїСЂРёРµРјР°
    /// </summary>
    public DateOnly VisitDate { get; set; }

    /// <summary>
    /// Р’СЂРµРјСЏ РїСЂРёРµРјР° (РїРѕ СЂР°СЃРїРёСЃР°РЅРёСЋ)
    /// </summary>
    public TimeOnly SheduledVisitTime { get; set; }

    public long WdId { get; set; }

    public long PatientId { get; set; }

    /// <summary>
    /// Р–Р°Р»РѕР±Р°
    /// </summary>
    public string Complaint { get; set; } = null!;

    public virtual Patient Patient { get; set; } = null!;

    public virtual Shedule Wd { get; set; } = null!;
}
