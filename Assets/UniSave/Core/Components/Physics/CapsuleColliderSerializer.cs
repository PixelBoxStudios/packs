using UnityEngine;
using System;
using ProtoBuf;

[ProtoContract]
public sealed class CapsuleColliderSerializer
{
    [ProtoMember(1)] public bool Enabled { get; set; }
    [ProtoMember(2)] public bool IsTrigger { get; set; }
    [ProtoMember(3)] public string MaterialName { get; set; }
	[ProtoMember(4)] public Vector3Serializer Center { get; set; }
	[ProtoMember(5)] public float Radius { get; set; }
	[ProtoMember(6)] public float Height { get; set; }
	[ProtoMember(7)] public int Direction { get; set; }

    public CapsuleColliderSerializer(GameObject gameObject, CapsuleColliderSerializer component)
    {
        var capsuleCollider = gameObject.GetComponent<CapsuleCollider>();

        if (capsuleCollider == null)
            capsuleCollider = gameObject.AddComponent<CapsuleCollider>();

        capsuleCollider.enabled = component.Enabled;
        capsuleCollider.isTrigger = component.IsTrigger;

        if (!String.IsNullOrEmpty(component.MaterialName))
            capsuleCollider.material = (PhysicMaterial) UniSave.TryLoadResource(component.MaterialName);

        capsuleCollider.center = (Vector3) component.Center;
        capsuleCollider.radius = component.Radius;
        capsuleCollider.direction = component.Direction;
    }

    public CapsuleColliderSerializer(GameObject gameObject)
    {
        var capsuleCollider = gameObject.GetComponent<CapsuleCollider>();

        Enabled = capsuleCollider.enabled;
        IsTrigger = capsuleCollider.isTrigger;

        if (capsuleCollider.material != null)
            MaterialName = capsuleCollider.material.name;

        Center = (Vector3Serializer) capsuleCollider.center;
        Radius = capsuleCollider.radius;
        Direction = capsuleCollider.direction;
    }

    // Empty constructor required for ProtoBuf
    private CapsuleColliderSerializer()
    {
    }
}
