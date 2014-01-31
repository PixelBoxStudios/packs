using UnityEngine;
using ProtoBuf;

[ProtoContract]
public struct Matrix4x4Serializer
{
    [ProtoMember(1)]
	public float M00;
    [ProtoMember(2)]
	public float M10;
    [ProtoMember(3)]
	public float M20;
    [ProtoMember(4)]
	public float M30;
    [ProtoMember(5)]
	public float M01;
    [ProtoMember(6)]
	public float M11;
    [ProtoMember(7)]
	public float M21;
    [ProtoMember(8)]
	public float M31;
    [ProtoMember(9)]
	public float M02;
    [ProtoMember(10)]
	public float M12;
    [ProtoMember(11)]
	public float M22;
    [ProtoMember(12)]
	public float M32;
    [ProtoMember(13)]
	public float M03;
    [ProtoMember(14)]
	public float M13;
    [ProtoMember(15)]
	public float M23;
    [ProtoMember(16)]
	public float M33;
	
	public Matrix4x4Serializer(Matrix4x4 data)
	{
		M00 = data.m00;
		M10 = data.m10;
		M20 = data.m20;
		M30 = data.m30;
		M01 = data.m01;
		M11 = data.m11;
		M21 = data.m21;
		M31 = data.m31;
		M02 = data.m02;
		M12 = data.m12;
		M22 = data.m22;
		M32 = data.m32;
		M03 = data.m03;
		M13 = data.m13;
		M23 = data.m23;
		M33 = data.m33;
	}
	
	public static explicit operator Matrix4x4(Matrix4x4Serializer data)
	{
		var matrix4x4 = new Matrix4x4();
		
		matrix4x4.m00 = data.M00;
		matrix4x4.m10 = data.M10;
		matrix4x4.m20 = data.M20;
		matrix4x4.m30 = data.M30;
		matrix4x4.m01 = data.M01;
		matrix4x4.m11 = data.M11;
		matrix4x4.m21 = data.M21;
		matrix4x4.m31 = data.M31;
		matrix4x4.m02 = data.M02;
		matrix4x4.m12 = data.M12;
		matrix4x4.m22 = data.M22;
		matrix4x4.m32 = data.M32;
		matrix4x4.m03 = data.M03;
		matrix4x4.m13 = data.M13;
		matrix4x4.m23 = data.M23;
		matrix4x4.m33 = data.M33;
		
		return matrix4x4;
	}
	
	public static explicit operator Matrix4x4Serializer(Matrix4x4 data)
	{
		return new Matrix4x4Serializer(data);
	}
}
