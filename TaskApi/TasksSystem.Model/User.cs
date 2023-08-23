using System;
using System.Collections.Generic;

namespace TasksSystem.Model;

public partial class User
{
    public Guid Id { get; set; }

    public string? Name { get; set; }

    public string? Email { get; set; }

    public Guid? RolId { get; set; }

    public virtual Role? Rol { get; set; }

    public virtual ICollection<Task> TaskCreators { get; set; } = new List<Task>();

    public virtual ICollection<Task> TaskUsers { get; set; } = new List<Task>();
}
