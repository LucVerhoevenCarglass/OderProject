using System;

namespace Oder_infrastructure.Exceptions
{
    public class NotAuthorizedException : Exception
    {
        public NotAuthorizedException(string additionalContext)
         :base(additionalContext)
        {
        }
    }
}
