using ProtoBuf;

[ProtoContract]
public enum JointDriveModeSerializer
{
    None,
    Position,
    Velocity,
    PositionAndVelocity
}
