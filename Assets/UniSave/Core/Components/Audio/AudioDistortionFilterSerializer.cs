using UnityEngine;
using ProtoBuf;

[ProtoContract]
public sealed class AudioDistortionFilterSerializer
{
	[ProtoMember(1)] public float DistortionLevel { get; set; }
    [ProtoMember(2)] public bool Enabled { get; set; }

    public AudioDistortionFilterSerializer(GameObject gameObject, AudioDistortionFilterSerializer component)
    {
        var audioDistortionFilter = gameObject.GetComponent<AudioDistortionFilter>();

        if (audioDistortionFilter == null)
            audioDistortionFilter = gameObject.AddComponent<AudioDistortionFilter>();

        audioDistortionFilter.distortionLevel = component.DistortionLevel;
        audioDistortionFilter.enabled = component.Enabled;
    }

    public AudioDistortionFilterSerializer(GameObject gameObject)
    {
        var audioDistortionFilter = gameObject.GetComponent<AudioDistortionFilter>();

        DistortionLevel = audioDistortionFilter.distortionLevel;
        Enabled = audioDistortionFilter.enabled;
    }

    // Empty constructor required for Protobuf
    private AudioDistortionFilterSerializer()
    {
    }
}
