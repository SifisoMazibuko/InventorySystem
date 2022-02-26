using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Exceptions
{
    public class ProductException : Exception
    {
        public ProductException(string message) : base(message) { }
        public ProductException(string message, Exception exception) : base(message, exception) { }
    }
}
