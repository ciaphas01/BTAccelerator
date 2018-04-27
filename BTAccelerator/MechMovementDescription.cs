using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BTAccelerator
{
    [DataContract]
    public class MechMovementDescription
    {
        [DataMember]
        public string Id;
        [DataMember]
        public string Name;
        [DataMember]
        public string Details;
        [DataMember]
        public string Icon;
    }
}
