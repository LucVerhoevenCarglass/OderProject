using System;
using System.Collections.Generic;
using System.Linq;
using Order_service.Data;


namespace Order_domain
{
   public abstract class Repository<T>
        where T : Entity
    {
        private readonly OrderContext _context;
        protected Repository()
        {
        }

        protected Repository(OrderContext context)
        {
            _context = context;
        }

        public T Save(T entity)
        {
            entity.GenerateId();
            _context.Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public T Update(T entity)
        {
            T getEntity = Get(entity.Id);
            _context.Attach(getEntity);
            getEntity = entity;
            //_context.Update(entity)
            _context.SaveChanges();
            return entity;
        }

        public List<T> GetAll()
        {
            //TODO : Just for the info dit returned en dictonary
            // return _context.Set<T>().ToDictionary(entityField => entityField.Id, entity => entity);
            return _context.Set<T>().ToList();
        }

        public T Get(Guid entityId)
        {
            return _context.Set<T>().SingleOrDefault(entity => entity.Id == entityId);
        }

        /**
         * Since we don't use transactions yet, we need a way to reset the database
         * in the tests. We'll use this method. Obviously this is a method that should
         * never be available in production...
         */
        public void Reset()
        {
            //Database.Reset();
        }
    }
}
