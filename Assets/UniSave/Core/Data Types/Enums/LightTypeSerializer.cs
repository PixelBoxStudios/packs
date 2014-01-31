using ProtoBuf;

[ProtoContract]
public enum LightTypeSerializer
{
    Spot,
    Directional,
    Point,
    Area
}