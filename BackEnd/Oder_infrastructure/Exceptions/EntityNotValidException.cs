using System;

namespace Oder_infrastructure.Exceptions
{
    public class EntityNotValidException : Exception
    {
        public EntityNotValidException(string additionalContext, object entity)
         :base("During " + additionalContext + ", the following entity was found to be invalid: " + entity)
        {
        }
    }
}
