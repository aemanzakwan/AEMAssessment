using System;
using System.Collections.Generic;

namespace PlatformWell_AEM.Model;

public partial class PlatformTable
{
    public int Id { get; set; }

    public string UniqueName { get; set; } = null!;

    public double? Latitude { get; set; }

    public double? Longitude { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<PlatformWell> Well { get; set; } = new List<PlatformWell>();
}
