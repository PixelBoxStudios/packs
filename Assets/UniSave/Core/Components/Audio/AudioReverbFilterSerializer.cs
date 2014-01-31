using UnityEngine;
using ProtoBuf;

[ProtoContract]
public sealed class AudioReverbFilterSerializer
{
	[ProtoMember(1)] public AudioReverbPresetSerializer ReverbPreset { get; set; }
	[ProtoMember(2)] public float DryLevel { get; set; }
	[ProtoMember(3)] public float Room { get; set; }
	[ProtoMember(4)] public float RoomHF { get; set; }
	[ProtoMember(5)] public float RoomRolloff { get; set; }
	[ProtoMember(6)] public float DecayTime { get; set; }
	[ProtoMember(7)] public float DecayHFRatio { get; set; }
	[ProtoMember(8)] public float ReflectionsLevel { get; set; }
	[ProtoMember(9)] public float ReflectionsDelay { get; set; }
	[ProtoMember(10)] public float ReverbLevel { get; set; }
	[ProtoMember(11)] public float ReverbDelay { get; set; }
	[ProtoMember(12)] public float Diffusion { get; set; }
	[ProtoMember(13)] public float Density { get; set; }
	[ProtoMember(14)] public float HFReference { get; set; }
	[ProtoMember(15)] public float RoomLF { get; set; }
	[ProtoMember(16)] public float LFReference { get; set; }
    [ProtoMember(17)] public bool Enabled { get; set; }

    public AudioReverbFilterSerializer(GameObject gameObject, AudioReverbFilterSerializer component)
    {
        var audioReverbFilter = gameObject.GetComponent<AudioReverbFilter>();

        if (audioReverbFilter == null)
            audioReverbFilter = gameObject.AddComponent<AudioReverbFilter>();

        audioReverbFilter.reverbPreset = (AudioReverbPreset) component.ReverbPreset;
        audioReverbFilter.dryLevel = component.DryLevel;
        audioReverbFilter.room = component.Room;
        audioReverbFilter.roomHF = component.RoomHF;
        audioReverbFilter.roomRolloff = component.RoomRolloff;
        audioReverbFilter.decayTime = component.DecayTime;
        audioReverbFilter.decayHFRatio = component.DecayHFRatio;
        audioReverbFilter.reflectionsLevel = component.ReflectionsLevel;
        audioReverbFilter.reflectionsDelay = component.ReflectionsDelay;
        audioReverbFilter.reverbLevel = component.ReverbLevel;
        audioReverbFilter.reverbDelay = component.ReverbDelay;
        audioReverbFilter.diffusion = component.Diffusion;
        audioReverbFilter.density = component.Density;
        audioReverbFilter.hfReference = component.HFReference;
        audioReverbFilter.roomLF = component.RoomLF;
        audioReverbFilter.lFReference = component.LFReference;
        audioReverbFilter.enabled = component.Enabled;
    }

    public AudioReverbFilterSerializer(GameObject gameObject)
    {
        var audioReverbFilter = gameObject.GetComponent<AudioReverbFilter>();

        ReverbPreset = (AudioReverbPresetSerializer) audioReverbFilter.reverbPreset;
        DryLevel = audioReverbFilter.dryLevel;
        Room = audioReverbFilter.room;
        RoomHF = audioReverbFilter.roomHF;
        RoomRolloff = audioReverbFilter.roomRolloff;
        DecayTime = audioReverbFilter.decayTime;
        DecayHFRatio = audioReverbFilter.decayHFRatio;
        ReflectionsLevel = audioReverbFilter.reflectionsLevel;
        ReflectionsDelay = audioReverbFilter.reflectionsDelay;
        ReverbLevel = audioReverbFilter.reverbLevel;
        ReverbDelay = audioReverbFilter.reverbDelay;
        Diffusion = audioReverbFilter.diffusion;
        Density = audioReverbFilter.density;
        HFReference = audioReverbFilter.hfReference;
        RoomLF = audioReverbFilter.roomLF;
        LFReference = audioReverbFilter.lFReference;
        Enabled = audioReverbFilter.enabled;
    }

    // Empty constructor required for Protobuf
    private AudioReverbFilterSerializer()
    {
    }
}
