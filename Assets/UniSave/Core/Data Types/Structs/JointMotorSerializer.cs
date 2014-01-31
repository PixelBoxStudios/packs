using UnityEngine;
using ProtoBuf;

[ProtoContract]
public struct JointMotorSerializer
{
	[ProtoMember(1)] public float TargetVelocity { get; set; }
	[ProtoMember(2)] public float Force { get; set; }
	[ProtoMember(3)] public bool FreeSpin { get; set; }

    public JointMotorSerializer(JointMotor data) : this()
    {
        TargetVelocity = data.targetVelocity;
        Force = data.force;
        FreeSpin = data.freeSpin;
    }

    public static explicit operator JointMotor(JointMotorSerializer data)
    {
        var jointMotor = new JointMotor()
        {
            targetVelocity = data.TargetVelocity,
            force = data.Force,
            freeSpin = data.FreeSpin
        };

        return jointMotor;
    }

    public static explicit operator JointMotorSerializer(JointMotor data)
    {
        return new JointMotorSerializer(data);
    }
}
