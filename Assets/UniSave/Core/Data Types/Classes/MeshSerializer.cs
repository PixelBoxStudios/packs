using UnityEngine;
using System;
using ProtoBuf;

[ProtoContract]
public class MeshSerializer
{
    [ProtoMember(1)]  public Vector3Serializer[] Vertices { get; set; }
    [ProtoMember(2)]  public Vector3Serializer[] Normals { get; set; }
    [ProtoMember(3)]  public Vector4Serializer[] Tangents { get; set; }
    [ProtoMember(4)]  public Vector2Serializer[] Uv { get; set; }
    [ProtoMember(5)]  public Vector2Serializer[] Uv2 { get; set; }
    [ProtoMember(6)]  public Vector2Serializer[] Uv1 { get; set; }
    [ProtoMember(7)]  public BoundsSerializer Bounds { get; set; }
    [ProtoMember(8)]  public ColorSerializer[] Colors { get; set; }
    [ProtoMember(9)]  public int[] Triangles { get; set; }
    [ProtoMember(10)] public int SubMeshCount { get; set; }
    [ProtoMember(11)] public BoneWeightSerializer[] BoneWeights { get; set; }
    [ProtoMember(12)] public Matrix4x4Serializer[] Bindposes { get; set; }
    [ProtoMember(13)] public string MeshName { get; set; }
	
	public MeshSerializer(Mesh data)
	{
		Vertices = Array.ConvertAll(data.vertices, element => (Vector3Serializer)element);
		Normals = Array.ConvertAll(data.normals, element => (Vector3Serializer)element);
		Tangents = Array.ConvertAll(data.tangents, element => (Vector4Serializer)element);
		Uv = Array.ConvertAll(data.uv, element => (Vector2Serializer)element);
		Uv2 = Array.ConvertAll(data.uv2, element => (Vector2Serializer)element);
		Uv1 = Array.ConvertAll(data.uv1, element => (Vector2Serializer)element);
		Bounds = (BoundsSerializer)data.bounds;
		Colors = Array.ConvertAll(data.colors, element => (ColorSerializer)element);
		Triangles = data.triangles;
		SubMeshCount = data.subMeshCount;
		BoneWeights = Array.ConvertAll(data.boneWeights, element => (BoneWeightSerializer)element);
		Bindposes = Array.ConvertAll(data.bindposes, element => (Matrix4x4Serializer)element);
        MeshName = data.name ;
	}

    //Empty constructor required for Protobuf
    private MeshSerializer()
    {
    }
	
	public static explicit operator Mesh(MeshSerializer data)
	{
	    var mesh = new Mesh
	    {
	        vertices = Array.ConvertAll(data.Vertices, element => (Vector3) element),
	        normals = Array.ConvertAll(data.Normals, element => (Vector3) element),
	        tangents = Array.ConvertAll(data.Tangents, element => (Vector4) element),
	        uv = Array.ConvertAll(data.Uv, element => (Vector2) element),
	        uv2 = Array.ConvertAll(data.Uv2, element => (Vector2) element),
	        uv1 = Array.ConvertAll(data.Uv1, element => (Vector2) element),
	        bounds = (Bounds) data.Bounds,
	        triangles = data.Triangles,
	        subMeshCount = data.SubMeshCount,
	        name = data.MeshName
	    };

	    if (data.Colors != null)
            mesh.colors = Array.ConvertAll(data.Colors, element => (Color)element);

        if (data.BoneWeights != null)
		mesh.boneWeights = Array.ConvertAll(data.BoneWeights, element => (BoneWeight)element);

        if (data.Bindposes != null)
		mesh.bindposes = Array.ConvertAll(data.Bindposes, element => (Matrix4x4)element);

        return mesh;
	}
	
	public static explicit operator MeshSerializer(Mesh data)
	{
		return new MeshSerializer(data);
	}
}
