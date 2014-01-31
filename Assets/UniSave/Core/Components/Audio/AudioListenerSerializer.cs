using UnityEngine;
using ProtoBuf;

[ProtoContract]
public sealed class AudioListenerSerializer
{
    [ProtoMember(1)] public bool Enabled { get; set; }
	[ProtoMember(2)] public float Volume { get; set; }
	[ProtoMember(3)] public bool Pause { get; set; }
	[ProtoMember(4)] public AudioVelocityUpdateModeSerializer VelocityUpdateMode { get; set; }

    public AudioListenerSerializer(GameObject gameObject, AudioListenerSerializer component)
    {
        var audioListener = gameObject.GetComponent<AudioListener>();

        if (audioListener == null)
            audioListener = gameObject.AddComponent<AudioListener>();

        audioListener.enabled = component.Enabled;
        AudioListener.volume = component.Volume;
        AudioListener.pause = component.Pause;
        audioListener.velocityUpdateMode = (AudioVelocityUpdateMode) component.VelocityUpdateMode;
    }

    public AudioListenerSerializer(GameObject gameObject)
    {
        var audioListener = gameObject.GetComponent<AudioListener>();

        Enabled = audioListener.enabled;
        Volume = AudioListener.volume;
        Pause = AudioListener.pause;
        VelocityUpdateMode = (AudioVelocityUpdateModeSerializer) audioListener.velocityUpdateMode;
    }

    // Empty constructor required for Protobuf
    private AudioListenerSerializer()
    {
    }
}
