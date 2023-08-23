using System;
using System.Collections.Generic;

namespace TasksSystem.Model;

public partial class Comment
{
    public Guid Id { get; set; }

    public string? Comment1 { get; set; }

    public Guid? TaskId { get; set; }

    public virtual Task? Task { get; set; }
}
