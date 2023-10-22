using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlatformWell_AEM.Model;


public class Well
{
    public int Id { get; set; }

    public int? PlatformId { get; set; }

    public string UniqueName { get; set; } = null!;

    public double? Latitude { get; set; }

    public double? Longitude { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Platform? Platform { get; set; }
}
