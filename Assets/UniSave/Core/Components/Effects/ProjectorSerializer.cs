using System;
using UnityEngine;
using ProtoBuf;

[ProtoContract]
public sealed class ProjectorSerializer
{
    [ProtoMember(1)]  public float NearClipPlane { get; set; }
    [ProtoMember(2)]  public float FarClipPlane { get; set; }
    [ProtoMember(3)]  public float FieldOfView { get; set; }
    [ProtoMember(4)]  public float AspectRatio { get; set; }
    [ProtoMember(5)]  public bool IsOrthoGraphic { get; set; }
    [ProtoMember(6)]  public bool Orthographic { get; set; }
    [ProtoMember(7)]  public float OrthographicSize { get; set; }
    [ProtoMember(8)]  public float OrthoGraphicSize { get; set; }
    [ProtoMember(9)]  public int IgnoreLayers { get; set; }
    [ProtoMember(10)] public string MaterialName { get; set; }
    [ProtoMember(11)] public bool Enabled { get; set; }

    public ProjectorSerializer(GameObject gameObject, ProjectorSerializer component)
    {
        var projector = gameObject.GetComponent<Projector>();

        if (projector == null)
            projector = gameObject.AddComponent<Projector>();

        projector.nearClipPlane = component.NearClipPlane;
        projector.farClipPlane = component.FarClipPlane;
        projector.fieldOfView = component.FieldOfView;
        projector.aspectRatio = component.AspectRatio;
        projector.isOrthoGraphic = component.IsOrthoGraphic;
        projector.orthographic = component.Orthographic;
        projector.orthographicSize = component.OrthographicSize;
        projector.orthoGraphicSize = component.OrthoGraphicSize;
        projector.ignoreLayers = component.IgnoreLayers;

        if (!String.IsNullOrEmpty(component.MaterialName))
            projector.material = (Material) UniSave.TryLoadResource(component.MaterialName);

        projector.enabled = component.Enabled;
    }

    public ProjectorSerializer(GameObject gameObject)
    {
        var projector = gameObject.GetComponent<Projector>();

        NearClipPlane = projector.nearClipPlane;
        FarClipPlane = projector.farClipPlane;
        FieldOfView = projector.fieldOfView;
        AspectRatio = projector.aspectRatio;
        IsOrthoGraphic = projector.isOrthoGraphic;
        Orthographic = projector.orthographic;
        OrthographicSize = projector.orthographicSize;
        OrthoGraphicSize = projector.orthoGraphicSize;
        IgnoreLayers = projector.ignoreLayers;

        if (projector.material != null)
            MaterialName = projector.material.name;

        Enabled = projector.enabled;
    }

    // Empty constructor required for ProtoBuf
    private ProjectorSerializer()
    {
    }
}
