using System;
using System.Collections.Generic;

namespace ClinicDatabaseModel.Database;

/// <summary>
/// РЎРѕС‚СЂСѓРґРЅРёРєРё
/// </summary>
public partial class Employee
{
    public long EmployeeId { get; set; }

    public string Firstname { get; set; } = null!;

    /// <summary>
    /// Р¤Р°РјРёР»РёСЏ
    /// </summary>
    public string Lastname { get; set; } = null!;

    /// <summary>
    /// РћС‚С‡РµСЃС‚РІРѕ
    /// </summary>
    public string? Patronymic { get; set; }

    public decimal Inn { get; set; }

    /// <summary>
    /// РџСЂРµРјРёСЏ
    /// </summary>
    public int? Award { get; set; }

    /// <summary>
    /// РљРѕРЅС‚Р°РєС‚РЅС‹Р№ РЅРѕРјРµСЂ С‚РµР»РµС„РѕРЅР° СЃРѕС‚СЂСѓРґРЅРёРєР°
    /// </summary>
    public string Phone { get; set; } = null!;

    public string Title { get; set; } = null!;

    public int Salary { get; set; }

    /// <summary>
    /// РљРѕР»РёС‡РµСЃС‚РІРѕ РѕС‚РїСѓСЃРєРЅС‹С… РґРЅРµР№
    /// </summary>
    public short VacationCount { get; set; }

    /// <summary>
    /// Р›РѕРіРёРЅ РґР»СЏ РІС…РѕРґР° РІ СЃРёСЃС‚РµРјСѓ
    /// </summary>
    public string Login { get; set; } = null!;

    public long? SpecilityId { get; set; }

    /// <summary>
    /// РџР°СЂРѕР»СЊ
    /// </summary>
    public string Password { get; set; } = null!;

    public virtual ICollection<Recipe> Recipes { get; } = new List<Recipe>();

    public virtual ICollection<Shedule> Shedules { get; } = new List<Shedule>();

    public virtual Speciality? Specility { get; set; }
}
