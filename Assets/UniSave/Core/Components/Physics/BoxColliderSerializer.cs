using UnityEngine;
using System;
using ProtoBuf;

[ProtoContract]
public sealed class BoxColliderSerializer
{
    [ProtoMember(1)] public bool Enabled { get; set; }
    [ProtoMember(2)] public bool IsTrigger { get; set; }
    [ProtoMember(3)] public string MaterialName { get; set; }
	[ProtoMember(4)] public Vector3Serializer Center { get; set; }
	[ProtoMember(5)] public Vector3Serializer Size { get; set; }
	[ProtoMember(6)] public Vector3Serializer Extents { get; set; }

    public BoxColliderSerializer(GameObject gameObject, BoxColliderSerializer component)
    {
        var boxCollider = gameObject.GetComponent<BoxCollider>();

        if (boxCollider == null)
            boxCollider = gameObject.AddComponent<BoxCollider>();

        boxCollider.enabled = component.Enabled;
        boxCollider.isTrigger = component.IsTrigger;

        if (!String.IsNullOrEmpty(component.MaterialName))
            boxCollider.material = (PhysicMaterial) UniSave.TryLoadResource(component.MaterialName);

        boxCollider.center = (Vector3) component.Center;
        boxCollider.size = (Vector3) component.Size;
        boxCollider.extents = (Vector3) component.Extents;
    }

    public BoxColliderSerializer(GameObject gameObject)
    {
        var boxCollider = gameObject.GetComponent<BoxCollider>();

        Enabled = boxCollider.enabled;
        IsTrigger = boxCollider.isTrigger;

        if (boxCollider.material != null)
            MaterialName = boxCollider.material.name;

        Center = (Vector3Serializer) boxCollider.center;
        Size = (Vector3Serializer) boxCollider.size;
        Extents = (Vector3Serializer) boxCollider.extents;
    }

    // Empty constructor required for ProtoBuf
    private BoxColliderSerializer()
    {
    }
}
