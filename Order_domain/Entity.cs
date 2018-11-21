using System;

namespace Order_domain
{
    public abstract class Entity
    {
        public Guid Id { get; private set; }

        protected Entity(Guid id)
        {
            Id = id;
        }

        public void GenerateId()
        {
            if (Id != Guid.Empty)
            {
                //IllegalStateException
                throw new Exception("Generating an ID for a customer that already has " +
                                                "an ID (" + Id + ") is not allowed.");
            }

            Id = Guid.NewGuid();
        }
    }
}
