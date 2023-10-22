﻿using System;
using System.Collections.Generic;

namespace PlatformWell_AEM.Model;

public partial class PlatformWell
{
    public int Id { get; set; }

    public int? PlatformId { get; set; }

    public string UniqueName { get; set; } = null!;

    public double? Latitude { get; set; }

    public double? Longitude { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual PlatformTable? Platform { get; set; }
}
