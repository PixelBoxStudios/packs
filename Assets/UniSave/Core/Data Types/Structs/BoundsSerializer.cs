using UnityEngine;
using ProtoBuf;

[ProtoContract]
public struct BoundsSerializer
{
    [ProtoMember(1)] public Vector3Serializer Center { get; set; }
    [ProtoMember(2)] public Vector3Serializer Size { get; set; }
    [ProtoMember(3)] public Vector3Serializer Extents { get; set; }
    [ProtoMember(4)] public Vector3Serializer Min { get; set; }
    [ProtoMember(5)] public Vector3Serializer Max { get; set; }

    public BoundsSerializer(Bounds data) : this()
    {
        Center = (Vector3Serializer)data.center;
        Size = (Vector3Serializer)data.size;
        Extents = (Vector3Serializer)data.extents;
        Min = (Vector3Serializer)data.min;
        Max = (Vector3Serializer)data.max;
    }

    public static explicit operator Bounds(BoundsSerializer data)
    {
        var bounds = new Bounds
        {
            center = (Vector3)data.Center,
            size = (Vector3)data.Size,
            extents = (Vector3)data.Extents,
            min = (Vector3)data.Min,
            max = (Vector3)data.Max
        };

        return bounds;
    }

    public static explicit operator BoundsSerializer(Bounds data)
    {
        return new BoundsSerializer(data);
    }
}