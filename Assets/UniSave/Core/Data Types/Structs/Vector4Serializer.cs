using UnityEngine;
using ProtoBuf;

[ProtoContract]
public struct Vector4Serializer
{
    [ProtoMember(1)] public float X;
    [ProtoMember(2)] public float Y;
    [ProtoMember(3)] public float Z;
    [ProtoMember(4)] public float W;
	
	public Vector4Serializer(Vector4 data)
	{
		X = data.x;
		Y = data.y;
		Z = data.z;
		W = data.w;
	}
	
	public static explicit operator Vector4(Vector4Serializer data)
	{
		return new Vector4(data.X, data.Y, data.Z, data.W);
	}
	
	public static explicit operator Vector4Serializer(Vector4 data)
	{
		return new Vector4Serializer(data);
	}
}
