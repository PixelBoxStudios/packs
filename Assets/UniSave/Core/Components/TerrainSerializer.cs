using UnityEngine;
using System;
using ProtoBuf;

[ProtoContract]
public sealed class TerrainSerializer
{
    [ProtoMember(1)] public bool Enabled { get; set; }
    [ProtoMember(2)] public string TerrainDataName { get; set; }
    [ProtoMember(3)] public float TreeDistance { get; set; }
    [ProtoMember(4)] public float TreeBillboardDistance { get; set; }
    [ProtoMember(5)] public float TreeCrossFadeLength { get; set; }
    [ProtoMember(6)] public int TreeMaximumFullLODCount { get; set; }
    [ProtoMember(7)] public float DetailObjectDistance { get; set; }
    [ProtoMember(8)] public float DetailObjectDensity { get; set; }
    [ProtoMember(9)] public float HeightmapPixelError { get; set; }
    [ProtoMember(10)] public int HeightmapMaximumLOD { get; set; }
    [ProtoMember(11)] public float BasemapDistance { get; set; }
    [ProtoMember(12)] public int LightmapIndex { get; set; }
    [ProtoMember(13)] public bool CastShadows { get; set; }
    [ProtoMember(14)] public TerrainRenderFlagsSerializer EditorRenderFlags { get; set; }

    public TerrainSerializer(GameObject gameObject, TerrainSerializer component)
    {
        var terrain = gameObject.GetComponent<Terrain>();

        if (terrain == null)
            terrain = gameObject.AddComponent<Terrain>();

        if (!String.IsNullOrEmpty(component.TerrainDataName))
            terrain.terrainData = (TerrainData) UniSave.TryLoadResource(component.TerrainDataName);

        terrain.enabled = component.Enabled;
        terrain.treeDistance = component.TreeDistance;
        terrain.treeBillboardDistance = component.TreeBillboardDistance;
        terrain.treeCrossFadeLength = component.TreeCrossFadeLength;
        terrain.treeMaximumFullLODCount = component.TreeMaximumFullLODCount;
        terrain.detailObjectDistance = component.DetailObjectDistance;
        terrain.detailObjectDensity = component.DetailObjectDensity;
        terrain.heightmapPixelError = component.HeightmapPixelError;
        terrain.heightmapMaximumLOD = component.HeightmapMaximumLOD;
        terrain.basemapDistance = component.BasemapDistance;
        terrain.lightmapIndex = component.LightmapIndex;
        terrain.castShadows = component.CastShadows;
        terrain.editorRenderFlags = (TerrainRenderFlags) component.EditorRenderFlags;
    }

    public TerrainSerializer(GameObject gameObject)
    {
        var terrain = gameObject.GetComponent<Terrain>();

        if (terrain.terrainData != null)
            TerrainDataName = terrain.terrainData.name;

        Enabled = terrain.enabled;
        TreeDistance = terrain.treeDistance;
        TreeBillboardDistance = terrain.treeBillboardDistance;
        TreeCrossFadeLength = terrain.treeCrossFadeLength;
        TreeMaximumFullLODCount = terrain.treeMaximumFullLODCount;
        DetailObjectDistance = terrain.detailObjectDistance;
        DetailObjectDensity = terrain.detailObjectDensity;
        HeightmapPixelError = terrain.heightmapPixelError;
        HeightmapMaximumLOD = terrain.heightmapMaximumLOD;
        BasemapDistance = terrain.basemapDistance;
        LightmapIndex = terrain.lightmapIndex;
        CastShadows = terrain.castShadows;
        EditorRenderFlags = (TerrainRenderFlagsSerializer) terrain.editorRenderFlags;
    }

    // Empty constructor required for ProtoBuf
    private TerrainSerializer()
    {
    }
}