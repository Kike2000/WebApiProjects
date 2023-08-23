using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TasksSystem.Model;

namespace TasksSystem.DAL.Repositories.Contracts
{
    public interface ITaskRepository : IGenericRepository<Model.Task>
    {
        Task<Model.Task> Register(Model.Task task);
    }
}
