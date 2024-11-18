using System;
using System.Collections.Generic;

namespace Lab4_1.Models;

public partial class WorkingHour
{
    public int LocationId { get; set; }

    public TimeOnly WeekdaysStart { get; set; }

    public TimeOnly WeekdaysEnd { get; set; }

    public TimeOnly? SaturdayStart { get; set; }

    public TimeOnly? SaturdayEnd { get; set; }

    public TimeOnly? SundayStart { get; set; }

    public TimeOnly? SundayEnd { get; set; }

    public virtual Location Location { get; set; } = null!;
}
