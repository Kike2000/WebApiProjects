using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TasksSystem.DAL.Repositories.Contracts;
using TasksSystem.DAL.DBContext;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace TasksSystem.DAL.Repositories
{
    public class GenericRepository<TModel> : IGenericRepository<TModel> where TModel : class
    {
        //Mediante la propiedad de tipo ApplicationDbContext estamos creando una instancia de esta.
        //ApplicationDbContext es una clase creada por EntityFramework que representa una base de datos y sus Entidades.
        //Contiene las propiedades DBSet para cada entidad
        private readonly ApplicationDbContext _context;

        
        //En el contructor estoy haciendo la inyeccion de dependencia del contexto de EntityFramework para poder acceder a la base de datos
        //Aplicando modularidad
        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IQueryable<TModel>> Consult(Expression<Func<TModel, bool>> filter = null)
        {
            try
            {

                IQueryable<TModel> queryModel = filter == null ? _context.Set<TModel>() : _context.Set<TModel>().Where(filter);
                return queryModel;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<TModel> Create(TModel model)
        {
            try
            {
                _context.Set<TModel>().Add(model);
                await _context.SaveChangesAsync();
                return model;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> Delete(TModel model)
        {
            try
            {

                _context.Set<TModel>().Remove(model);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<TModel> Get(Expression<Func<TModel, bool>> filter)
        {
            try
            {
                TModel model = await _context.Set<TModel>().FirstOrDefaultAsync(filter);
                return model;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> Update(TModel model)
        {
            try
            {

                _context.Set<TModel>().Update(model);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
