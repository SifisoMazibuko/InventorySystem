using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Exceptions
{
    public class SupplierException : Exception
    {
        public SupplierException(string message) : base(message) { }
        public SupplierException(string message, Exception exception) : base(message, exception) { }
    }
}
