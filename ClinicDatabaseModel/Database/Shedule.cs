using System;
using System.Collections.Generic;

namespace ClinicDatabaseModel.Database;

/// <summary>
/// Р Р°СЃРїРёСЃР°РЅРёРµ
/// </summary>
public partial class Shedule
{
    public long WdId { get; set; }

    public long BranchId { get; set; }

    public long EmployeeId { get; set; }

    /// <summary>
    /// Р”РµРЅСЊ РЅРµРґРµР»Рё : Р§РёСЃР»Рѕ РѕС‚ 0 РґРѕ 6, РіРґРµ: 
    /// 0 - РІРѕСЃРєСЂРµСЃРµРЅРёРµ, 6 - СЃСѓР±Р±РѕС‚Р°
    /// </summary>
    public decimal Weekday { get; set; }

    /// <summary>
    /// РќР°С‡Р°Р»Рѕ СЂР°Р±РѕС‚С‹
    /// </summary>
    public TimeOnly StartWorking { get; set; }

    public virtual Branch Branch { get; set; } = null!;

    public virtual Employee Employee { get; set; } = null!;

    public virtual ICollection<Visit> Visits { get; } = new List<Visit>();
}
