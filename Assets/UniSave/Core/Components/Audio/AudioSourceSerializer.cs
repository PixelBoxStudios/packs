using UnityEngine;
using System;
using ProtoBuf;

[ProtoContract]
public sealed class AudioSourceSerializer
{
	[ProtoMember(1)] public float Volume { get; set; }
	[ProtoMember(2)] public float Pitch { get; set; }
	[ProtoMember(3)] public float Time { get; set; }
	[ProtoMember(4)] public int TimeSamples { get; set; }
	[ProtoMember(5)] public string ClipName { get; set; }
	[ProtoMember(6)] public bool Loop { get; set; }
	[ProtoMember(7)] public bool IgnoreListenerVolume { get; set; }
	[ProtoMember(8)] public bool PlayOnAwake { get; set; }
	[ProtoMember(9)] public AudioVelocityUpdateModeSerializer VelocityUpdateMode { get; set; }
	[ProtoMember(10)] public float PanLevel { get; set; }
	[ProtoMember(11)] public bool BypassEffects { get; set; }
	[ProtoMember(12)] public float DopplerLevel { get; set; }
	[ProtoMember(13)] public float Spread { get; set; }
	[ProtoMember(14)] public int Priority { get; set; }
	[ProtoMember(15)] public bool Mute { get; set; }
	[ProtoMember(16)] public float MinDistance { get; set; }
	[ProtoMember(17)] public float MaxDistance { get; set; }
	[ProtoMember(18)] public float Pan { get; set; }
	[ProtoMember(19)] public AudioRolloffModeSerializer RolloffMode { get; set; }
    [ProtoMember(20)] public bool Enabled { get; set; }

    public AudioSourceSerializer(GameObject gameObject, AudioSourceSerializer component)
    {
        var audioSource = gameObject.GetComponent<AudioSource>();

        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        audioSource.volume = component.Volume;
        audioSource.pitch = component.Pitch;
        audioSource.time = component.Time;
        audioSource.timeSamples = component.TimeSamples;
        
        if (!String.IsNullOrEmpty(component.ClipName))
        {
            audioSource.clip = (AudioClip) Resources.Load(component.ClipName);

            if (audioSource.clip == null)
                Debug.LogWarning("Asset \"" + component.ClipName + "\" could not be found. Are you sure that the Asset has been added to the Resources folder?");
        }

        audioSource.loop = component.Loop;
        audioSource.ignoreListenerVolume = component.IgnoreListenerVolume;
        audioSource.playOnAwake = component.PlayOnAwake;
        audioSource.velocityUpdateMode = (AudioVelocityUpdateMode) component.VelocityUpdateMode;
        audioSource.panLevel = component.PanLevel;
        audioSource.bypassEffects = component.BypassEffects;
        audioSource.dopplerLevel = component.DopplerLevel;
        audioSource.spread = component.Spread;
        audioSource.priority = component.Priority;
        audioSource.mute = component.Mute;
        audioSource.minDistance = component.MinDistance;
        audioSource.maxDistance = component.MaxDistance;
        audioSource.pan = component.Pan;
        audioSource.rolloffMode = (AudioRolloffMode) component.RolloffMode;
        audioSource.enabled = component.Enabled;
    }

    public AudioSourceSerializer(GameObject gameObject)
    {
        var audioSource = gameObject.GetComponent<AudioSource>();

        Volume = audioSource.volume;
        Pitch = audioSource.pitch;
        Time = audioSource.time;
        TimeSamples = audioSource.timeSamples;

        if (audioSource.clip != null)
        ClipName = audioSource.clip.name;

        Loop = audioSource.loop;
        IgnoreListenerVolume = audioSource.ignoreListenerVolume;
        PlayOnAwake = audioSource.playOnAwake;
        VelocityUpdateMode = (AudioVelocityUpdateModeSerializer) audioSource.velocityUpdateMode;
        PanLevel = audioSource.panLevel;
        BypassEffects = audioSource.bypassEffects;
        DopplerLevel = audioSource.dopplerLevel;
        Spread = audioSource.spread;
        Priority = audioSource.priority;
        Mute = audioSource.mute;
        MinDistance = audioSource.minDistance;
        MaxDistance = audioSource.maxDistance;
        Pan = audioSource.pan;
        RolloffMode = (AudioRolloffModeSerializer) audioSource.rolloffMode;
        Enabled = audioSource.enabled;
    }

    // Empty constructor required for Protobuf
    private AudioSourceSerializer()
    {
    }
}
