using UnityEngine;
using ProtoBuf;

[ProtoContract]
public struct QuaternionSerializer 
{
    [ProtoMember(1)] public float X;
    [ProtoMember(2)] public float Y;
    [ProtoMember(3)] public float Z;
    [ProtoMember(4)] public float W;
	
	public QuaternionSerializer(Quaternion data)
	{
		X = data.x;
		Y = data.y;
		Z = data.z;
		W = data.w;
	}
	
	public static explicit operator Quaternion(QuaternionSerializer data)
	{
		return new Quaternion(data.X, data.Y, data.Z, data.W);
	}
	
	public static explicit operator QuaternionSerializer(Quaternion data)
	{
		return new QuaternionSerializer(data);
	}
}
