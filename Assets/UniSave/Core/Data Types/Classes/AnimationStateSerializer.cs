using UnityEngine;
using ProtoBuf;

[ProtoContract]
public sealed class AnimationStateSerializer
{
    [ProtoMember(1)] public bool Enabled { get; set; }
    [ProtoMember(2)] public float Weight { get; set; }
    [ProtoMember(3)] public WrapModeSerializer WrapMode { get; set; }
    [ProtoMember(4)] public float Time { get; set; }
    [ProtoMember(5)] public float NormalizedTime { get; set; }
    [ProtoMember(6)] public float Speed { get; set; }
    [ProtoMember(7)] public float NormalizedSpeed { get; set; }
    [ProtoMember(8)] public int Layer { get; set; }
    [ProtoMember(9)] public string Name { get; set; }
    [ProtoMember(10)] public AnimationBlendModeSerializer BlendMode { get; set; }
    [ProtoMember(11)] public string ClipName;

    public AnimationStateSerializer(AnimationState data)
    {
        Enabled = data.enabled;
        Weight = data.weight;
        WrapMode = (WrapModeSerializer) data.wrapMode;
        Time = data.time;
        NormalizedTime = data.normalizedTime;
        Speed = data.speed;
        NormalizedSpeed = data.normalizedSpeed;
        Layer = data.layer;
        Name = data.name;
        BlendMode = (AnimationBlendModeSerializer) data.blendMode;
        ClipName = data.clip.name;
    }

    //Empty constructor required for Protobuf
    private AnimationStateSerializer()
    {
    }

    public static explicit operator AnimationState(AnimationStateSerializer data)
    {
        var animationState = new AnimationState
        {
            enabled = data.Enabled,
            weight = data.Weight,
            wrapMode = (WrapMode) data.WrapMode,
            time = data.Time,
            normalizedTime = data.NormalizedTime,
            speed = data.Speed,
            normalizedSpeed = data.NormalizedSpeed,
            layer = data.Layer,
            name = data.Name,
            blendMode = (AnimationBlendMode) data.BlendMode,
        };

        return animationState;
    }

    public static explicit operator AnimationStateSerializer(AnimationState data)
    {
        return new AnimationStateSerializer(data);
    }
}