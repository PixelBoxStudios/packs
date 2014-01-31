using UnityEngine;
using ProtoBuf;

[ProtoContract]
public sealed class AudioChorusFilterSerializer
{
    [ProtoMember(1)] public bool Enabled { get; set; }
    [ProtoMember(2)] public float DryMix { get; set; }
	[ProtoMember(3)] public float WetMix1 { get; set; }
	[ProtoMember(4)] public float WetMix2 { get; set; }
	[ProtoMember(5)] public float WetMix3 { get; set; }
	[ProtoMember(6)] public float Delay { get; set; }
	[ProtoMember(7)] public float Rate { get; set; }
	[ProtoMember(8)] public float Depth { get; set; }
	[ProtoMember(9)] public float Feedback { get; set; }

    public AudioChorusFilterSerializer(GameObject gameObject, AudioChorusFilterSerializer component)
    {
        var audioChorusFilter = gameObject.GetComponent<AudioChorusFilter>();

        if (audioChorusFilter == null)
            audioChorusFilter = gameObject.AddComponent<AudioChorusFilter>();

        audioChorusFilter.enabled = component.Enabled;
        audioChorusFilter.dryMix = component.DryMix;
        audioChorusFilter.wetMix1 = component.WetMix1;
        audioChorusFilter.wetMix2 = component.WetMix2;
        audioChorusFilter.wetMix3 = component.WetMix3;
        audioChorusFilter.delay = component.Delay;
        audioChorusFilter.rate = component.Rate;
        audioChorusFilter.depth = component.Depth;
        audioChorusFilter.feedback = component.Feedback;
    }

    public AudioChorusFilterSerializer(GameObject gameObject)
    {
        var audioChorusFilter = gameObject.GetComponent<AudioChorusFilter>();

        Enabled = audioChorusFilter.enabled;
        DryMix = audioChorusFilter.dryMix;
        WetMix1 = audioChorusFilter.wetMix1;
        WetMix2 = audioChorusFilter.wetMix2;
        WetMix3 = audioChorusFilter.wetMix3;
        Delay = audioChorusFilter.delay;
        Rate = audioChorusFilter.rate;
        Depth = audioChorusFilter.depth;
        Feedback = audioChorusFilter.feedback;
    }

    // Empty constructor required for Protobuf
    private AudioChorusFilterSerializer()
    {
    }
}
