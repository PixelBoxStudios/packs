using UnityEngine;
using ProtoBuf;

[ProtoContract]
public sealed class AudioEchoFilterSerializer
{
	[ProtoMember(1)] public float Delay { get; set; }
	[ProtoMember(2)] public float DecayRatio { get; set; }
	[ProtoMember(3)] public float DryMix { get; set; }
	[ProtoMember(4)] public float WetMix { get; set; }
    [ProtoMember(5)] public bool Enabled { get; set; }

    public AudioEchoFilterSerializer(GameObject gameObject, AudioEchoFilterSerializer component)
    {
        var audioEchoFilter = gameObject.GetComponent<AudioEchoFilter>();

        if (audioEchoFilter == null)
            audioEchoFilter = gameObject.AddComponent<AudioEchoFilter>();

        audioEchoFilter.delay = component.Delay;
        audioEchoFilter.decayRatio = component.DecayRatio;
        audioEchoFilter.dryMix = component.DryMix;
        audioEchoFilter.wetMix = component.WetMix;
        audioEchoFilter.enabled = component.Enabled;
    }

    public AudioEchoFilterSerializer(GameObject gameObject)
    {
        var audioEchoFilter = gameObject.GetComponent<AudioEchoFilter>();

        Delay = audioEchoFilter.delay;
        DecayRatio = audioEchoFilter.decayRatio;
        DryMix = audioEchoFilter.dryMix;
        WetMix = audioEchoFilter.wetMix;
        Enabled = audioEchoFilter.enabled;
    }

    // Empty constructor required for Protobuf
    private AudioEchoFilterSerializer()
    {
    }
}
