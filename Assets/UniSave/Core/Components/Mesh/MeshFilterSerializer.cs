using UnityEngine;
using ProtoBuf;

[ProtoContract]
public sealed class MeshFilterSerializer
{
    [ProtoMember(1)] public MeshSerializer Mesh { get; set; }
	
	public MeshFilterSerializer(GameObject gameObject, MeshFilterSerializer component)
	{
		var meshFilter = gameObject.GetComponent<MeshFilter>();
		
		if (meshFilter == null)
            meshFilter = gameObject.AddComponent<MeshFilter>();

        if (component.Mesh != null)
        {
            meshFilter.mesh = (Mesh) component.Mesh;
            meshFilter.mesh.name = component.Mesh.MeshName;
        }
	}
	
	public MeshFilterSerializer(GameObject gameObject)
	{
		var meshFilter = gameObject.GetComponent<MeshFilter>();

        if (meshFilter.mesh != null)
        {
            Mesh = (MeshSerializer) meshFilter.mesh;
            Mesh.MeshName = meshFilter.mesh.name;
        }
	}

    // Empty constructor required for ProtoBuf
    private MeshFilterSerializer()
    {
    }
}


