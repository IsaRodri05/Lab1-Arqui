using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab1App.Models.Entities;

public partial class Profesion
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string Nom { get; set; } = null!;

    public string? Des { get; set; }

    public virtual ICollection<Estudio> Estudios { get; set; } = new List<Estudio>();
}
