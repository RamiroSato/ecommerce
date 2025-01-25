using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Models
{
    public class AwsCognitoSettings
    {
        public string Region { get; set; }
        public string UserPoolId { get; set; }
        public string AppClientId { get; set; }

    }
}
