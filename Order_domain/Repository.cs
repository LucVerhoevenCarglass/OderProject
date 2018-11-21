using System;
using System.Collections.Generic;
using System.Linq;

namespace Order_domain
{
    public abstract class Repository<T, U>
        where T : Entity
        where U : EntityDatabase<T>
    {

        public U Database { get; set; }

        protected Repository(U database)
        {
            Database = database;
        }

        public T Save(T entity)
        {
            entity.GenerateId();
            Database.Save(entity);
            return entity;
        }

        public T Update(T entity)
        {
            Database.Save(entity);
            return entity;
        }

        public Dictionary<Guid, T> GetAll()
        {
            return Database.GetAll();
        }

        public T Get(Guid entityId)
        {
            return Database.GetAll().SingleOrDefault(x => x.Key == entityId).Value;
        }

        /**
         * Since we don't use transactions yet, we need a way to reset the database
         * in the tests. We'll use this method. Obviously this is a method that should
         * never be available in production...
         */
        public void Reset()
        {
            Database.Reset();
        }
    }
}
