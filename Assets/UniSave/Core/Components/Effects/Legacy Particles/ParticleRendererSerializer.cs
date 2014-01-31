using System.Linq;
using UnityEngine;
using System;
using ProtoBuf;

[ProtoContract]
public class ParticleRendererSerializer
{
    [ProtoMember(1)] public bool Enabled { get; set; }
    [ProtoMember(2)] public bool CastShadows { get; set; }
    [ProtoMember(3)] public bool ReceiveShadows { get; set; }
    [ProtoMember(4)] public string[] MaterialNames { get; set; }
    [ProtoMember(5)] public int LightmapIndex { get; set; }
    [ProtoMember(6)] public Vector4Serializer LightmapTilingOffset { get; set; }
    [ProtoMember(7)] public bool UseLightProbes { get; set; }
    [ProtoMember(8)] public string LightProbeAnchorName { get; set; }
	[ProtoMember(9)] public ParticleRenderModeSerializer ParticleRenderMode { get; set; }
	[ProtoMember(10)] public float LengthScale { get; set; }
	[ProtoMember(11)] public float VelocityScale { get; set; }
	[ProtoMember(12)] public float CameraVelocityScale { get; set; }
	[ProtoMember(13)] public float MaxParticleSize { get; set; }
	[ProtoMember(14)] public int UvAnimationXTile { get; set; }
	[ProtoMember(15)] public int UvAnimationYTile { get; set; }
	[ProtoMember(16)] public float UvAnimationCycles { get; set; }
	[ProtoMember(17)] public float MaxPartileSize { get; set; }
	[ProtoMember(18)] public RectSerializer[] UvTiles { get; set; }

    public ParticleRendererSerializer(GameObject gameObject, ParticleRendererSerializer component)
    {
        var particleRenderer = gameObject.GetComponent<ParticleRenderer>();

        if (particleRenderer == null)
            particleRenderer = gameObject.AddComponent<ParticleRenderer>();

        particleRenderer.enabled = component.Enabled;
        particleRenderer.castShadows = component.CastShadows;
        particleRenderer.receiveShadows = component.ReceiveShadows;

        particleRenderer.materials = (from materialName in component.MaterialNames
                                      select (Material) UniSave.TryLoadResource(materialName)).ToArray();
        
        particleRenderer.lightmapIndex = component.LightmapIndex;
        particleRenderer.lightmapTilingOffset = (Vector4) component.LightmapTilingOffset;

        particleRenderer.useLightProbes = component.UseLightProbes;

        if (!String.IsNullOrEmpty(component.LightProbeAnchorName))
            particleRenderer.lightProbeAnchor.name = component.LightProbeAnchorName;

        particleRenderer.particleRenderMode = (ParticleRenderMode) component.ParticleRenderMode;
        particleRenderer.lengthScale = component.LengthScale;
        particleRenderer.velocityScale = component.VelocityScale;
        particleRenderer.cameraVelocityScale = component.CameraVelocityScale;
        particleRenderer.maxParticleSize = component.MaxParticleSize;
        particleRenderer.uvAnimationXTile = component.UvAnimationXTile;
        particleRenderer.uvAnimationYTile = component.UvAnimationYTile;
        particleRenderer.maxPartileSize = component.MaxPartileSize;

        if (component.UvTiles != null)
            particleRenderer.uvTiles = Array.ConvertAll(component.UvTiles, element => (Rect) element);
    }

    public ParticleRendererSerializer(GameObject gameObject)
    {
        var particleRenderer = gameObject.GetComponent<ParticleRenderer>();

        Enabled = particleRenderer.enabled;
        CastShadows = particleRenderer.castShadows;
        ReceiveShadows = particleRenderer.receiveShadows;

        MaterialNames = (from material in particleRenderer.materials
                         select material.name).ToArray();

        LightmapIndex = particleRenderer.lightmapIndex;
        LightmapTilingOffset = (Vector4Serializer) particleRenderer.lightmapTilingOffset;

        UseLightProbes = particleRenderer.useLightProbes;

        if (particleRenderer.lightProbeAnchor != null)
            LightProbeAnchorName = particleRenderer.lightProbeAnchor.name;

        ParticleRenderMode = (ParticleRenderModeSerializer) particleRenderer.particleRenderMode;
        LengthScale = particleRenderer.lengthScale;
        VelocityScale = particleRenderer.velocityScale;
        CameraVelocityScale = particleRenderer.cameraVelocityScale;
        MaxParticleSize = particleRenderer.maxParticleSize;
        UvAnimationXTile = particleRenderer.uvAnimationXTile;
        UvAnimationYTile = particleRenderer.uvAnimationYTile;
        MaxPartileSize = particleRenderer.maxPartileSize;

        if (particleRenderer.uvTiles != null)
            UvTiles = Array.ConvertAll(particleRenderer.uvTiles, element => (RectSerializer) element);
    }

    // Empty constructor required for Protobuf
    private ParticleRendererSerializer()
    {
    }
}
