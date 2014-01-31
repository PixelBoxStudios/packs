using System;
using ProtoBuf;

[ProtoContract][Flags]
public enum RigidbodyConstraintsSerializer
{
    None,
    FreezePositionX,
    FreezePositionY,
    FreezePositionZ,
    FreezeRotationX,
    FreezeRotationY,
    FreezeRotationZ,
    FreezePosition,
    FreezeRotation,
    FreezeAll
}