using UnityEngine;
using ProtoBuf;

[ProtoContract]
public sealed class AudioReverbZoneSerializer
{
	[ProtoMember(1)] public float MinDistance { get; set; }
	[ProtoMember(2)] public float MaxDistance { get; set; }
	[ProtoMember(3)] public AudioReverbPresetSerializer ReverbPreset { get; set; }
	[ProtoMember(4)] public int Room { get; set; }
	[ProtoMember(5)] public int RoomHF { get; set; }
	[ProtoMember(6)] public int RoomLF { get; set; }
	[ProtoMember(7)] public float DecayTime { get; set; }
	[ProtoMember(8)] public float DecayHFRatio { get; set; }
	[ProtoMember(9)] public int Reflections { get; set; }
	[ProtoMember(10)] public float ReflectionsDelay { get; set; }
	[ProtoMember(11)] public int Reverb { get; set; }
	[ProtoMember(12)] public float ReverbDelay { get; set; }
	[ProtoMember(13)] public float HFReference { get; set; }
	[ProtoMember(14)] public float LFReference { get; set; }
	[ProtoMember(15)] public float RoomRolloffFactor { get; set; }
	[ProtoMember(16)] public float Diffusion { get; set; }
	[ProtoMember(17)] public float Density { get; set; }
    [ProtoMember(18)] public bool Enabled { get; set; }

    public AudioReverbZoneSerializer(GameObject gameObject, AudioReverbZoneSerializer component)
    {
        var audioReverbFilter = gameObject.GetComponent<AudioReverbZone>();

        if (audioReverbFilter == null)
            audioReverbFilter = gameObject.AddComponent<AudioReverbZone>();

        audioReverbFilter.minDistance = component.MinDistance;
        audioReverbFilter.maxDistance = component.MaxDistance;
        audioReverbFilter.reverbPreset = (AudioReverbPreset) component.ReverbPreset;
        audioReverbFilter.room = component.Room;
        audioReverbFilter.roomHF = component.RoomHF;
        audioReverbFilter.roomLF = component.RoomLF;
        audioReverbFilter.decayTime = component.DecayTime;
        audioReverbFilter.decayHFRatio = component.DecayHFRatio;
        audioReverbFilter.reflections = component.Reflections;
        audioReverbFilter.reflectionsDelay = component.ReflectionsDelay;
        audioReverbFilter.reverb = component.Reverb;
        audioReverbFilter.reverbDelay = component.ReverbDelay;
        audioReverbFilter.HFReference = component.HFReference;
        audioReverbFilter.LFReference = component.LFReference;
        audioReverbFilter.roomRolloffFactor = component.RoomRolloffFactor;
        audioReverbFilter.diffusion = component.Diffusion;
        audioReverbFilter.density = component.Density;
        audioReverbFilter.enabled = component.Enabled;
    }

    public AudioReverbZoneSerializer(GameObject gameObject)
    {
        var audioReverbFilter = gameObject.GetComponent<AudioReverbZone>();

        MinDistance = audioReverbFilter.minDistance;
        MaxDistance = audioReverbFilter.maxDistance;
        ReverbPreset = (AudioReverbPresetSerializer) audioReverbFilter.reverbPreset;
        Room = audioReverbFilter.room;
        RoomHF = audioReverbFilter.roomHF;
        RoomLF = audioReverbFilter.roomLF;
        DecayTime = audioReverbFilter.decayTime;
        DecayHFRatio = audioReverbFilter.decayHFRatio;
        Reflections = audioReverbFilter.reflections;
        ReflectionsDelay = audioReverbFilter.reflectionsDelay;
        Reverb = audioReverbFilter.reverb;
        ReverbDelay = audioReverbFilter.reverbDelay;
        HFReference = audioReverbFilter.HFReference;
        LFReference = audioReverbFilter.LFReference;
        RoomRolloffFactor = audioReverbFilter.roomRolloffFactor;
        Diffusion = audioReverbFilter.diffusion;
        Density = audioReverbFilter.density;
        Enabled = audioReverbFilter.enabled;
    }

    // Empty constructor required for Protobuf
    private AudioReverbZoneSerializer()
    {
    }
}