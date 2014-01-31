using ProtoBuf;

[ProtoContract]
public enum CollisionFlagsSerializer
{
    None,
    Sides,
    Above,
    Below,
    CollidedSides,
    CollidedAbove,
    CollidedBelow
}