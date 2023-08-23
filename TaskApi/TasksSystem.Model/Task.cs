using System;
using System.Collections.Generic;

namespace TasksSystem.Model;

public partial class Task
{
    public Guid Id { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public bool? IsCompleted { get; set; }

    public Guid? UserId { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public DateTime? CreationTaskDate { get; set; }

    public Guid? CreatorId { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual User? Creator { get; set; }

    public virtual User? User { get; set; }
}
