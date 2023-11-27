using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Primitives
{

    [DataContract]
    public class Response
    {
        [DataMember]
        public bool Ok { get; set; } = true;

        [DataMember]
        public string Error { get; set; } = string.Empty;

    }
}

