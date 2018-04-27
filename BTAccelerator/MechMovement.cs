using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BTAccelerator
{
    [DataContract]
    class MechMovement
    {
        [DataMember]
        public readonly MechMovementDescription Description = new MechMovementDescription();
        [DataMember]
        public double MaxWalkDistance;
        [DataMember]
        public double MaxSprintDistance;
        [DataMember]
        public double WalkVelocity;
        [DataMember]
        public double RunVelocity;
        [DataMember]
        public double SprintVelocity;
        [DataMember]
        public double LimpVelocity;
        [DataMember]
        public double WalkAcceleration;
        [DataMember]
        public double SprintAcceleration;
        [DataMember]
        public double MaxRadialVelocity;
        [DataMember]
        public double MaxRadialAcceleration;
        [DataMember]
        public double MaxJumpVel;
        [DataMember]
        public double MaxJumpAccel;
        [DataMember]
        public double MaxJumpVerticalAccel;
    }
}
