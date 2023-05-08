using System;
using System.Collections.Generic;

namespace Gradebook.Models.Gradebook;

public partial class Grade
{
    public int Id { get; set; }

    public int Grade1 { get; set; }

    public string Subject { get; set; } = null!;

    public int StudentId { get; set; }

    public virtual Student Student { get; set; } = null!;
}
