using System;
using ProtoBuf;

[ProtoContract][Flags]
public enum TerrainRenderFlagsSerializer
{
    heightmap,
    trees,
    details,
    all
}