using UnityEngine;
using ProtoBuf;

[ProtoContract]
public struct JointDriveSerializer
{
	[ProtoMember(1)] public JointDriveModeSerializer Mode { get; set; }
	[ProtoMember(2)] public float PositionSpring { get; set; }
	[ProtoMember(3)] public float PositionDamper { get; set; }
	[ProtoMember(4)] public float MaximumForce { get; set; }

    public JointDriveSerializer(JointDrive data) : this()
    {
        Mode = (JointDriveModeSerializer) data.mode;
        PositionSpring = data.positionSpring;
        PositionDamper = data.positionDamper;
        MaximumForce = data.maximumForce;
    }

    public static explicit operator JointDrive(JointDriveSerializer data)
    {
        var jointDrive = new JointDrive()
        {
            mode = (JointDriveMode) data.Mode,
            positionSpring = data.PositionSpring,
            positionDamper = data.PositionDamper,
            maximumForce = data.MaximumForce
        };

        return jointDrive;
    }

    public static explicit operator JointDriveSerializer(JointDrive data)
    {
        return new JointDriveSerializer(data);
    }
}
