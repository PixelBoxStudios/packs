using UnityEngine;
using ProtoBuf;

[ProtoContract]
public sealed class AudioHighPassFilterSerializer
{
	[ProtoMember(1)] public float CutoffFrequency { get; set; }
	[ProtoMember(2)] public float HighpassResonaceQ { get; set; }
    [ProtoMember(3)] public bool Enabled { get; set; }

    public AudioHighPassFilterSerializer(GameObject gameObject, AudioHighPassFilterSerializer component)
    {
        var audioHighPassFilter = gameObject.GetComponent<AudioHighPassFilter>();

        if (audioHighPassFilter == null)
            audioHighPassFilter = gameObject.AddComponent<AudioHighPassFilter>();

        audioHighPassFilter.cutoffFrequency = component.CutoffFrequency;
        audioHighPassFilter.highpassResonaceQ = component.HighpassResonaceQ;
        audioHighPassFilter.enabled = component.Enabled;
    }

    public AudioHighPassFilterSerializer(GameObject gameObject)
    {
        var audioHighPassFilter = gameObject.GetComponent<AudioHighPassFilter>();

        CutoffFrequency = audioHighPassFilter.cutoffFrequency;
        HighpassResonaceQ = audioHighPassFilter.highpassResonaceQ;
        Enabled = audioHighPassFilter.enabled;
    }

    // Empty constructor required for Protobuf
    private AudioHighPassFilterSerializer()
    {
    }
}
