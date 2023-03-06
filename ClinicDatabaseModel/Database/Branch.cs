using System;
using System.Collections.Generic;

namespace ClinicDatabaseModel.Database;

/// <summary>
/// Р¤РёР»РёР°Р»
/// </summary>
public partial class Branch
{
    public long BranchId { get; set; }

    /// <summary>
    /// РќР°Р·РІР°РЅРёРµ РѕС‚РґРµР»РµРЅРёСЏ
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// РђРґСЂРµСЃ С„РёР»РёР°Р»Р°
    /// </summary>
    public string Address { get; set; } = null!;

    /// <summary>
    /// РўРµР»РµС„РѕРЅ
    /// </summary>
    public string Phone { get; set; } = null!;

    /// <summary>
    /// email
    /// </summary>
    public string Email { get; set; } = null!;

    public virtual Attachment? Attachment { get; set; }

    public virtual ICollection<Shedule> Shedules { get; } = new List<Shedule>();
}
