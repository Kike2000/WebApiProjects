using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TasksSystem.DAL.DBContext;
using TasksSystem.DAL.Repositories.Contracts;
using TasksSystem.DAL.Repositories;


namespace TasksSystem.IOC
{
    public static class Dependency
    {
        //Método de extensión IServiceCollection pertenece al motor de ASP.NET Core
        public static void DependencyInjections(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("ConnectionSQL"));
            });
            
            //Preguntar a chat
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<ITaskRepository, TaskRepository>();

        }
    }
}
