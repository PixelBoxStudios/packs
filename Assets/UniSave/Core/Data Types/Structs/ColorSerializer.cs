using UnityEngine;
using ProtoBuf;

[ProtoContract]
public struct ColorSerializer
{
    [ProtoMember(1)] public float R;
    [ProtoMember(2)] public float G;
    [ProtoMember(3)] public float B;
    [ProtoMember(4)] public float A;
	
	public ColorSerializer(Color data)
	{
		R = data.r;
		G = data.g;
		B = data.b;
		A = data.a;
	}
	
	public static explicit operator Color(ColorSerializer data)
	{
		return new Color(data.R, data.G, data.B, data.A);
	}
	
	public static explicit operator ColorSerializer(Color data)
	{
		return new ColorSerializer(data);
	}
}
