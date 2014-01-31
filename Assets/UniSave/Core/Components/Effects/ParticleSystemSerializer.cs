using UnityEngine;
using ProtoBuf;

[ProtoContract]
public sealed class ParticleSystemSerializer
{
    [ProtoMember(1)]  public float StartDelay { get; set; }
    [ProtoMember(2)]  public bool Loop { get; set; }
    [ProtoMember(3)]  public bool PlayOnAwake { get; set; }
    [ProtoMember(4)]  public float Time { get; set; }
    [ProtoMember(5)]  public float PlaybackSpeed { get; set; }
    [ProtoMember(6)]  public bool EnableEmission { get; set; }
    [ProtoMember(7)]  public float EmissionRate { get; set; }
    [ProtoMember(8)]  public float StartSpeed { get; set; }
    [ProtoMember(9)]  public float StartSize { get; set; }
    [ProtoMember(10)] public ColorSerializer StartColor { get; set; }
    [ProtoMember(11)] public float StartRotation { get; set; }
    [ProtoMember(12)] public float StartLifetime { get; set; }
    [ProtoMember(13)] public float GravityModifier { get; set; }

    public ParticleSystemSerializer (GameObject gameObject, ParticleSystemSerializer component)
    {
        var particleSystem = gameObject.GetComponent<ParticleSystem>();

        if (particleSystem == null)
            particleSystem = gameObject.AddComponent<ParticleSystem>();

        particleSystem.startDelay = component.StartDelay;
        particleSystem.loop = component.Loop;
        particleSystem.playOnAwake = component.PlayOnAwake;
        particleSystem.time = component.Time;
        particleSystem.playbackSpeed = component.PlaybackSpeed;
        particleSystem.enableEmission = component.EnableEmission;
        particleSystem.emissionRate = component.EmissionRate;
        particleSystem.startSpeed = component.StartSpeed;
        particleSystem.startSize = component.StartSize;
        particleSystem.startColor = (Color) component.StartColor;
        particleSystem.startRotation = component.StartRotation;
        particleSystem.startLifetime = component.StartLifetime;
        particleSystem.gravityModifier = component.GravityModifier;
    }

    public ParticleSystemSerializer(GameObject gameObject)
    {
        var particleSystem = gameObject.GetComponent<ParticleSystem>();

        StartDelay = particleSystem.startDelay;
        Loop = particleSystem.loop;
        PlayOnAwake = particleSystem.playOnAwake;
        Time = particleSystem.time;
        PlaybackSpeed = particleSystem.playbackSpeed;
        EnableEmission = particleSystem.enableEmission;
        EmissionRate = particleSystem.emissionRate;
        StartSpeed = particleSystem.startSpeed;
        StartSize = particleSystem.startSize;
        StartColor = (ColorSerializer) particleSystem.startColor;
        StartRotation = particleSystem.startRotation;
        StartLifetime = particleSystem.startLifetime;
        GravityModifier = particleSystem.gravityModifier;
    }

    // Empty constructor required for Protobuf
    private ParticleSystemSerializer()
    {
    }
}
