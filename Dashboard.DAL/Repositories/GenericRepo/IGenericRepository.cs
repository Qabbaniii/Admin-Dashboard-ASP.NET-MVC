using Dashboard.DAL.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard.DAL.Repositories.GenericRepo
{
    public interface IGenericRepository<IEntity> where IEntity : BaseEntity
    {
        public IEnumerable<IEntity> GetAll(bool WithTracking = false);
        public IEntity GetByID(int id);
        public void Add(IEntity Item);
        public void Update(IEntity Item);
        public void Delete(int id);
        public void softDelete(int id);
    }
}
