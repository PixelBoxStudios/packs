using UnityEngine;
using System;
using ProtoBuf;

[ProtoContract]
public sealed class MeshColliderSerializer
{
    [ProtoMember(1)] public bool Enabled { get; set; }
    [ProtoMember(2)] public bool IsTrigger { get; set; }
    [ProtoMember(3)] public string MaterialName { get; set; }
	[ProtoMember(4)] public MeshSerializer SharedMesh { get; set; }
	[ProtoMember(5)] public bool Convex { get; set; }
	[ProtoMember(6)] public bool SmoothSphereCollisions { get; set; }

    public MeshColliderSerializer(GameObject gameObject, MeshColliderSerializer component)
    {
        var meshCollider = gameObject.GetComponent<MeshCollider>();

        if (meshCollider == null)
            meshCollider = gameObject.AddComponent<MeshCollider>();

        meshCollider.enabled = component.Enabled;
        meshCollider.isTrigger = component.IsTrigger;

        if (!String.IsNullOrEmpty(component.MaterialName))
            meshCollider.material = (PhysicMaterial)UniSave.TryLoadResource(component.MaterialName);

        if (component.SharedMesh != null)
        {
            meshCollider.sharedMesh = (Mesh) component.SharedMesh;
            meshCollider.sharedMesh.name = component.SharedMesh.MeshName;
        }

        meshCollider.convex = component.Convex;
        SmoothSphereCollisions = meshCollider.smoothSphereCollisions;
    }

    public MeshColliderSerializer(GameObject gameObject)
    {
        var meshCollider = gameObject.GetComponent<MeshCollider>();

        Enabled = meshCollider.enabled;
        IsTrigger = meshCollider.isTrigger;

        if (meshCollider.material != null)
            MaterialName = meshCollider.material.name;

        if (meshCollider.sharedMesh != null)
        {
            SharedMesh = (MeshSerializer) meshCollider.sharedMesh;
            SharedMesh.MeshName = meshCollider.sharedMesh.name;
        }

        Convex = meshCollider.convex;
        SmoothSphereCollisions = meshCollider.smoothSphereCollisions;
    }

    // Empty constructor required for ProtoBuf
    private MeshColliderSerializer()
    {
    }
}
