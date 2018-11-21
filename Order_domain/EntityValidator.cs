using System;

namespace Order_domain
{
    public abstract class EntityValidator<T> 
        where T:Entity
    {
        public bool IsValidForCreation(T entity)
        {
            return !IsAFieldEmptyOrNull(entity) && entity.Id == Guid.Empty;
        }

        public bool IsValidForUpdating(T entity)
        {
            return !IsAFieldEmptyOrNull(entity) && entity.Id != Guid.Empty;
        }

        protected abstract bool IsAFieldEmptyOrNull(T entity);

        protected bool IsEmptyOrNull(string attribute)
        {
            return attribute == null || string.IsNullOrWhiteSpace(attribute);
        }

        protected bool IsNull(object obj)
        {
            return obj == null;
        }

        protected bool IsNotNull(object obj)
        {
            return obj != null;
        }

        public void ThrowInvalidOperationException(T entity, string type)
        {
            throw new InvalidOperationException("Invalid " + (entity == null ? "NULL_ENTITY" : entity.GetType().Name)
                                                       + " provided for " + type + ". Provided object: " + (entity == null ? null : entity.ToString()));
        }
    }

}
