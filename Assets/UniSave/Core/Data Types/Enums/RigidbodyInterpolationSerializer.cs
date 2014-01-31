using ProtoBuf;

[ProtoContract]
public enum RigidbodyInterpolationSerializer
{
    None,
    Interpolate,
    Extrapolate
}