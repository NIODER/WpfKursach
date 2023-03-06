using System;
using System.Collections.Generic;

namespace ClinicDatabaseModel.Database;

/// <summary>
/// Р РµС†РµРїС‚С‹
/// </summary>
public partial class Recipe
{
    public long RecipeId { get; set; }

    /// <summary>
    /// РЎСЂРѕРє СЂР°Р±РѕС‚С‹ СЂРµС†РµРїС‚Р°
    /// </summary>
    public TimeSpan Period { get; set; }

    /// <summary>
    /// Р РµС†РµРїС‚
    /// </summary>
    public string Recipe1 { get; set; } = null!;

    /// <summary>
    /// РќР°С‡Р°Р»Рѕ СЃСЂРѕРєР° СЂРµС†РµРїС‚Р°
    /// </summary>
    public DateOnly ActiveSince { get; set; }

    public long PatientId { get; set; }

    public long EmployeeId { get; set; }

    public virtual Employee Employee { get; set; } = null!;

    public virtual Patient Patient { get; set; } = null!;
}
