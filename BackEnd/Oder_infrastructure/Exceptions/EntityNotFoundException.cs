using System;

namespace Oder_infrastructure.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException(string additionalContext, string className, Guid id)
        :base("During " + additionalContext + ", the following entity was not found: "+ className + " with id = " + id.ToString("N"))
        {
        }
    }
}
