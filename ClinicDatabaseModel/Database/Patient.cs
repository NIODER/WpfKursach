using System;
using System.Collections.Generic;

namespace ClinicDatabaseModel.Database;

/// <summary>
/// РџР°С†РёРµРЅС‚С‹
/// </summary>
public partial class Patient
{
    public long PatientId { get; set; }

    public string Firstname { get; set; } = null!;

    /// <summary>
    /// Р¤Р°РјРёР»РёСЏ
    /// </summary>
    public string Lastname { get; set; } = null!;

    /// <summary>
    /// РћС‚С‡РµСЃС‚РІРѕ
    /// </summary>
    public string? Patronymic { get; set; }

    /// <summary>
    /// РџРѕР» : true - male
    /// </summary>
    public bool Gender { get; set; }

    /// <summary>
    /// Р”Р°С‚Р° СЂРѕР¶РґРµРЅРёСЏ
    /// </summary>
    public DateOnly BirthDate { get; set; }

    public decimal Snils { get; set; }

    /// <summary>
    /// РџРѕР»РёСЃ OMC
    /// </summary>
    public decimal Oms { get; set; }

    /// <summary>
    /// login
    /// </summary>
    public string Login { get; set; } = null!;

    /// <summary>
    /// password
    /// </summary>
    public string Password { get; set; } = null!;

    public virtual ICollection<Attachment> Attachments { get; } = new List<Attachment>();

    public virtual ICollection<Recipe> Recipes { get; } = new List<Recipe>();

    public virtual ICollection<Visit> Visits { get; } = new List<Visit>();
}
