using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Exceptions;

[Serializable]
public class ResourceAlreadyExistsException : Exception
{
    public ResourceAlreadyExistsException() { }
    public ResourceAlreadyExistsException(string message) : base(message) { }
    public ResourceAlreadyExistsException(string message, Exception inner) : base(message, inner) { }
}
