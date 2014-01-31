using UnityEngine;
using System;
using ProtoBuf;

[ProtoContract]
public sealed class CameraSerializer
{
    [ProtoMember(1)] public float FOV { get; set; }
    [ProtoMember(2)] public float Near { get; set; }
    [ProtoMember(3)] public float Far { get; set; }
    [ProtoMember(4)] public float FieldOfView { get; set; }
    [ProtoMember(5)] public float NearClipPlane { get; set; }
    [ProtoMember(6)] public float FarClipPlane { get; set; }
    [ProtoMember(7)] public RenderingPathSerializer RenderingPath { get; set; }
    [ProtoMember(8)] public bool HDR { get; set; }
    [ProtoMember(9)] public float OrthographicSize { get; set; }
    [ProtoMember(10)] public bool Orthographic { get; set; }
    [ProtoMember(11)] public bool IsOrthoGraphic { get; set; }
    [ProtoMember(12)] public float Depth { get; set; }
    //[ProtoMember(13)] public float Aspect { get; set; }
    [ProtoMember(14)] public int CullingMask { get; set; }
    [ProtoMember(15)] public ColorSerializer BackgroundColor { get; set; }
    [ProtoMember(16)] public RectSerializer Rect { get; set; }
    //[ProtoMember(17)] public RectSerializer PixelRect { get; set; }
    [ProtoMember(18)] public string TargetTextureName { get; set; }
    [ProtoMember(19)] public Matrix4x4Serializer WorldToCameraMatrix { get; set; }
    [ProtoMember(20)] public Matrix4x4Serializer ProjectionMatrix { get; set; }
    [ProtoMember(21)] public CameraClearFlagsSerializer ClearFlags { get; set; }
    [ProtoMember(22)] public bool UseOcclusionCulling { get; set; }
    [ProtoMember(23)] public float[] LayerCullDistances { get; set; }
    [ProtoMember(24)] public bool LayerCullSpherical { get; set; }
    [ProtoMember(25)] public DepthTextureModeSerializer DepthTextureMode { get; set; }
    [ProtoMember(26)] public bool Enabled { get; set; }
    [ProtoMember(27)] public TransparencySortModeSerializer TransparencySortMode;

    public CameraSerializer(GameObject gameObject, CameraSerializer component)
    {
        var camera = gameObject.GetComponent<Camera>();

        if (camera == null)
            camera = gameObject.AddComponent<Camera>();

        camera.fov = component.FOV;
        camera.near = component.Near;
        camera.far = component.Far;
        camera.fieldOfView = component.FieldOfView;
        camera.nearClipPlane = component.NearClipPlane;
        camera.farClipPlane = component.FarClipPlane;
        camera.renderingPath = (RenderingPath) component.RenderingPath;
        camera.orthographicSize = component.OrthographicSize;
        camera.orthographic = component.Orthographic;
        camera.isOrthoGraphic = component.IsOrthoGraphic;
        camera.depth = component.Depth;
        //camera.aspect = component.Aspect;
        camera.cullingMask = component.CullingMask;
        camera.backgroundColor = (Color) component.BackgroundColor;
        camera.rect = (Rect) component.Rect;
        //camera.pixelRect = (Rect) component.PixelRect;
        camera.hdr = component.HDR;
        camera.layerCullSpherical = component.LayerCullSpherical;
        camera.useOcclusionCulling = component.UseOcclusionCulling;

        if (!String.IsNullOrEmpty(component.TargetTextureName))
            camera.targetTexture = (RenderTexture) UniSave.TryLoadResource(component.TargetTextureName);

        camera.worldToCameraMatrix = (Matrix4x4) component.WorldToCameraMatrix;
        camera.ResetWorldToCameraMatrix();
        camera.projectionMatrix = (Matrix4x4) component.ProjectionMatrix;
        camera.ResetProjectionMatrix();
        camera.clearFlags = (CameraClearFlags) component.ClearFlags;
        camera.layerCullDistances = component.LayerCullDistances;
        camera.depthTextureMode = (DepthTextureMode) component.DepthTextureMode;
        camera.enabled = component.Enabled;

        #if UNITY_3_5_3

            camera.transparencySortMode = (TransparencySortMode) component.TransparencySortMode;

        #endif
    }

    public CameraSerializer(GameObject gameObject)
    {
        var camera = gameObject.GetComponent<Camera>();

        FOV = camera.fov;
        Near = camera.near;
        Far = camera.far;
        FieldOfView = camera.fieldOfView;
        NearClipPlane = camera.nearClipPlane;
        FarClipPlane = camera.farClipPlane;
        RenderingPath = (RenderingPathSerializer) camera.renderingPath;
        HDR = camera.hdr;
        LayerCullSpherical = camera.layerCullSpherical;
        UseOcclusionCulling = camera.useOcclusionCulling;
        OrthographicSize = camera.orthographicSize;
        Orthographic = camera.orthographic;
        IsOrthoGraphic = camera.isOrthoGraphic;
        Depth = camera.depth;
        //Aspect = camera.aspect;
        CullingMask = camera.cullingMask;
        BackgroundColor = (ColorSerializer) camera.backgroundColor;
        Rect = (RectSerializer) camera.rect;
        //PixelRect = (RectSerializer) camera.pixelRect;

        if (camera.targetTexture != null)
            TargetTextureName = camera.targetTexture.name;

        WorldToCameraMatrix = (Matrix4x4Serializer) camera.worldToCameraMatrix;
        ProjectionMatrix = (Matrix4x4Serializer) camera.projectionMatrix;
        ClearFlags = (CameraClearFlagsSerializer) camera.clearFlags;
        LayerCullDistances = camera.layerCullDistances;
        DepthTextureMode = (DepthTextureModeSerializer) camera.depthTextureMode;
        Enabled = camera.enabled;

        #if UNITY_3_5_3

            TransparencySortMode = (TransparencySortModeSerializer) camera.transparencySortMode;

        #endif
    }

    // Empty constructor required for ProtoBuf
    private CameraSerializer()
    {
    }
}
