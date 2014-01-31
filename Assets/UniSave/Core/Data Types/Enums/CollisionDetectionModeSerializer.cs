using ProtoBuf;

[ProtoContract]
public enum CollisionDetectionModeSerializer
{
    Discrete,
    Continuous,
    ContinuousDynamic
}