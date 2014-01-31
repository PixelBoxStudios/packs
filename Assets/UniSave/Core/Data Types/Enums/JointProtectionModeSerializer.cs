using ProtoBuf;

[ProtoContract]
public enum JointProjectionModeSerializer
{
    None,
    PositionAndRotation,
    PositionOnly
}