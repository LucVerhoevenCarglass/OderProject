using System;
using System.Collections.Generic;
using System.Text;

namespace Order.Domain
{
    public class OrderExeptions : Exception
    {
        public OrderExeptions(string message) : base(message)
        {
        }

    }
}
