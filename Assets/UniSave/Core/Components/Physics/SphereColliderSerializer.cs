using UnityEngine;
using System;
using ProtoBuf;

[ProtoContract]
public sealed class SphereColliderSerializer
{
    [ProtoMember(1)] public bool Enabled { get; set; }
    [ProtoMember(2)] public bool IsTrigger { get; set; }
    [ProtoMember(3)] public string MaterialName { get; set; }
	[ProtoMember(4)] public Vector3Serializer Center { get; set; }
	[ProtoMember(5)] public float Radius { get; set; }

    public SphereColliderSerializer(GameObject gameObject, SphereColliderSerializer component)
    {
        var sphereCollider = gameObject.GetComponent<SphereCollider>();

        if (sphereCollider == null)
            sphereCollider = gameObject.AddComponent<SphereCollider>();

        sphereCollider.enabled = component.Enabled;
        sphereCollider.isTrigger = component.IsTrigger;

        if (!String.IsNullOrEmpty(component.MaterialName))
            sphereCollider.material = (PhysicMaterial) UniSave.TryLoadResource(component.MaterialName);

        sphereCollider.center = (Vector3) component.Center;
        sphereCollider.radius = component.Radius;
    }

    public SphereColliderSerializer(GameObject gameObject)
    {
        var sphereCollider = gameObject.GetComponent<SphereCollider>();

        Enabled = sphereCollider.enabled;
        IsTrigger = sphereCollider.isTrigger;

        if (sphereCollider.material != null)
            MaterialName = sphereCollider.material.name;

        Center = (Vector3Serializer)sphereCollider.center;
        Radius = sphereCollider.radius;
    }

    // Empty constructor required for ProtoBuf
    private SphereColliderSerializer()
    {
    }
}
