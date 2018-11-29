using System;
using System.Collections.Generic;
using System.Linq;

namespace Order_domain
{
    public abstract class EntityDatabase<T>
        where T : Entity
    {
        private Dictionary<Guid, T> _entities;

        protected EntityDatabase()
        {
            _entities = new Dictionary<Guid, T>();
        }

        public void Populate(params T[] entities)
        {
            _entities = entities.ToDictionary(entity => entity.Id);
        }

        public Dictionary<Guid, T> GetAll()
        {
            return _entities;
        }

        public void Save(T entity)
        {
            if (_entities.ContainsKey(entity.Id))
            {
                _entities[entity.Id] = entity;
            }
            else
            {
                _entities.Add(entity.Id, entity);
            }
        }

        /**
         * Since we don't use transactions yet, we need a way to reset the database
         * in the tests. We'll use this method. Obviously this is a method that should
         * never be available in production...
         */
        public void Reset()
        {
            _entities = new Dictionary<Guid, T>();
        }
    }
}
