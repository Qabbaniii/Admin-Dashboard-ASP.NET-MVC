using Dashboard.DAL.Contexts;
using Dashboard.DAL.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard.DAL.Repositories.GenericRepo
{
    public class GenericRepository<IEntity> : IGenericRepository<IEntity> where IEntity : BaseEntity
    {
        private readonly ApplicationDbContext _context;

        public GenericRepository(ApplicationDbContext context)
        {
            this._context = context;
        }
        public IEnumerable<IEntity> GetAll(bool WithTracking = false)
        {
            if (WithTracking)
            {
                return _context.Set<IEntity>().Where(d => d.IsDeleted == false).ToList();
            }
            else
            {
                return _context.Set<IEntity>().Where(d => d.IsDeleted == false).AsNoTracking().ToList();
            }
        }
        public void Add(IEntity Item)
        {
            _context.Set<IEntity>().Add(Item);
        }
        public void Update(IEntity Item)
        {
            _context.Set<IEntity>().Update(Item);
        }



        public IEntity GetByID(int id)
        {
            var Item = _context.Set<IEntity>().Find(id);
            return Item;
        }
        public void Delete(int id)
        {
            var Item = _context.Set<IEntity>().Find(id);
            _context.Set<IEntity>().Remove(Item);
        }


        public void softDelete(int id)
        {
            var Item = _context.Set<IEntity>().Find(id);
            Item.IsDeleted = true;
            _context.Update(Item);
        }


    }
}
