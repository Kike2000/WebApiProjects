using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TasksSystem.DAL.DBContext;
using TasksSystem.DAL.Repositories.Contracts;
using TasksSystem.Model;


namespace TasksSystem.DAL.Repositories
{
    public class TaskRepository : GenericRepository<Model.Task>, ITaskRepository
    {
        private readonly ApplicationDbContext _context;
        public TaskRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Model.Task> Register(Model.Task task)
        {
            //Preguntar a chat
            try
            {
                Model.Task taskGenerated = new Model.Task
                {
                    Id = task.Id,
                    CreationTaskDate = DateTime.Now,
                    Comments = task.Comments,
                    Creator = task.Creator,
                    Description = task.Description,
                    Title = task.Title,
                    UserId = task.UserId,
                    CreatorId = task.CreatorId
                };
                _context.Tasks.Add(taskGenerated);
                await _context.SaveChangesAsync();
                return taskGenerated;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
