using ProtoBuf;

[ProtoContract]
public enum WrapModeSerializer
{
    Once,
    Loop,
    PingPong,
    Default,
    ClampForever,
    Clamp
}
