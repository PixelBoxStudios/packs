using UnityEngine;
using ProtoBuf;

[ProtoContract]
public struct Vector2Serializer
{
    [ProtoMember(1)] public float X;
    [ProtoMember(2)] public float Y;
	
	public Vector2Serializer(Vector2 data)
	{
		X = data.x;
		Y = data.y;
	}
	
	public static explicit operator Vector2(Vector2Serializer data)
	{
		return new Vector2(data.X, data.Y);
	}
	
	public static explicit operator Vector2Serializer(Vector2 data)
	{
		return new Vector2Serializer(data);
	}
}
