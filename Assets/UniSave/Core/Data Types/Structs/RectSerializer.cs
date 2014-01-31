using UnityEngine;
using ProtoBuf;

[ProtoContract]
public struct RectSerializer
{
    [ProtoMember(1)] public float X { get; set; }
    [ProtoMember(2)] public float Y { get; set; }
    [ProtoMember(3)] public Vector2Serializer Center { get; set; }
    [ProtoMember(4)] public float Width { get; set; }
    [ProtoMember(5)] public float Height { get; set; }
    [ProtoMember(6)] public float XMin { get; set; }
    [ProtoMember(7)] public float YMin { get; set; }
    [ProtoMember(8)] public float XMax { get; set; }
    [ProtoMember(9)] public float YMax { get; set; }

    public RectSerializer(Rect data) : this()
    {
        X = data.x;
        Y = data.y;
        Center = (Vector2Serializer) data.center;
        Width = data.width;
        Height = data.height;
        XMin = data.xMin;
        YMin = data.yMin;
        XMax = data.xMax;
        YMax = data.yMax;
    }

    public static explicit operator Rect(RectSerializer data)
    {
        var rect = new Rect
        {
            x = data.X,
            y = data.Y,
            center = (Vector2) data.Center,
            width = data.Width,
            height = data.Height,
            xMin = data.XMin,
            yMin = data.YMin,
            xMax = data.XMax,
            yMax = data.YMax
        };

        return rect;
    }

    public static explicit operator RectSerializer(Rect data)
    {
        return new RectSerializer(data);
    }
}
