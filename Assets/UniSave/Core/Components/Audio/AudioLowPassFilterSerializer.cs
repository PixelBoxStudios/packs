using UnityEngine;
using ProtoBuf;

[ProtoContract]
public sealed class AudioLowPassFilterSerializer
{
	[ProtoMember(1)] public float CutoffFrequency { get; set; }
	[ProtoMember(2)] public float LowpassResonaceQ { get; set; }
    [ProtoMember(3)] public bool Enabled { get; set; }

    public AudioLowPassFilterSerializer(GameObject gameObject, AudioLowPassFilterSerializer component)
    {
        var audioLowPassFilter = gameObject.GetComponent<AudioLowPassFilter>();

        if (audioLowPassFilter == null)
            audioLowPassFilter = gameObject.AddComponent<AudioLowPassFilter>();

        audioLowPassFilter.cutoffFrequency = component.CutoffFrequency;
        audioLowPassFilter.lowpassResonaceQ = component.LowpassResonaceQ;
        audioLowPassFilter.enabled = component.Enabled;
    }

    public AudioLowPassFilterSerializer(GameObject gameObject)
    {
        var audioLowPassFilter = gameObject.GetComponent<AudioLowPassFilter>();

        CutoffFrequency = audioLowPassFilter.cutoffFrequency;
        LowpassResonaceQ = audioLowPassFilter.lowpassResonaceQ;
        Enabled = audioLowPassFilter.enabled;
    }

    // Empty constructor required for Protobuf
    private AudioLowPassFilterSerializer()
    {
    }
}
