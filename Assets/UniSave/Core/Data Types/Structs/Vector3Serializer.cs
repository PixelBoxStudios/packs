using UnityEngine;
using ProtoBuf;

[ProtoContract]
[System.Serializable]
public struct Vector3Serializer
{
    [ProtoMember(1)] public float X;
    [ProtoMember(2)] public float Y;
    [ProtoMember(3)] public float Z;
	
	public Vector3Serializer(Vector3 data)
	{
		X = data.x;
		Y = data.y;
		Z = data.z;
	}
	
	public static explicit operator Vector3(Vector3Serializer data)
	{
		return new Vector3(data.X, data.Y, data.Z);
	}
	
	public static explicit operator Vector3Serializer(Vector3 data)
	{
		return new Vector3Serializer(data);
	}
}
