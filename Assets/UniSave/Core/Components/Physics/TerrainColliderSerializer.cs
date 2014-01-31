using UnityEngine;
using System;
using ProtoBuf;

[ProtoContract]
public sealed class TerrainColliderSerializer
{
    [ProtoMember(1)] public bool Enabled { get; set; }
    [ProtoMember(2)] public bool IsTrigger { get; set; }
    [ProtoMember(3)] public string MaterialName { get; set; }
    [ProtoMember(4)] public string TerrainDataName { get; set; }

    public TerrainColliderSerializer(GameObject gameObject, TerrainColliderSerializer component)
    {
        var terrainCollider = gameObject.GetComponent<TerrainCollider>();

        if (terrainCollider == null)
            terrainCollider = gameObject.AddComponent<TerrainCollider>();

        terrainCollider.enabled = component.Enabled;
        terrainCollider.isTrigger = component.IsTrigger;

        if (!String.IsNullOrEmpty(component.MaterialName))
            terrainCollider.material = (PhysicMaterial) UniSave.TryLoadResource(component.MaterialName);

        if (!String.IsNullOrEmpty(component.TerrainDataName))
            terrainCollider.terrainData = (TerrainData) UniSave.TryLoadResource(component.TerrainDataName);
    }

    public TerrainColliderSerializer(GameObject gameObject)
    {
        var terrainCollider = gameObject.GetComponent<TerrainCollider>();

        Enabled = terrainCollider.enabled;
        IsTrigger = terrainCollider.isTrigger;

        if (terrainCollider.material != null)
            MaterialName = terrainCollider.material.name;

        if (terrainCollider.terrainData != null)
            TerrainDataName = terrainCollider.terrainData.name;
    }

    // Empty constructor required for ProtoBuf
    private TerrainColliderSerializer()
    {
    }
}
