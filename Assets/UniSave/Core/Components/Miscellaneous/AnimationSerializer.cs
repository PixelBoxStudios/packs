using System.Linq;
using UnityEngine;
using System;
using ProtoBuf;

[ProtoContract]
public sealed class AnimationSerializer
{
    [ProtoMember(1)] public string ClipName { get; set; }
    [ProtoMember(2)] public bool PlayAutomatically { get; set; }
    [ProtoMember(3)] public WrapModeSerializer WrapMode { get; set; }
    [ProtoMember(4)] public bool AnimatePhysics { get; set; }
    [ProtoMember(5)] public AnimationCullingTypeSerializer CullingType { get; set; }
    [ProtoMember(6)] public BoundsSerializer LocalBounds { get; set; }
    [ProtoMember(7)] public AnimationStateSerializer[] Animations { get; set; }
    [ProtoMember(8)] public bool Enabled { get; set; }

    public AnimationSerializer(GameObject gameObject, AnimationSerializer component)
    {
        var animation = gameObject.GetComponent<Animation>();

        if (animation == null)
            animation = gameObject.AddComponent<Animation>();

        if (!String.IsNullOrEmpty(component.ClipName))
            animation.clip = (AnimationClip) UniSave.TryLoadResource(component.ClipName);

        animation.playAutomatically = component.PlayAutomatically;
        animation.wrapMode = (WrapMode) component.WrapMode;
        animation.animatePhysics = component.AnimatePhysics;
        animation.cullingType = (AnimationCullingType) component.CullingType;
        animation.localBounds = (Bounds) component.LocalBounds;

        foreach (AnimationStateSerializer animationState in component.Animations)
        {
            animation.AddClip((AnimationClip) UniSave.TryLoadResource(animationState.ClipName), animationState.Name);
            animation[animationState.Name].enabled = animationState.Enabled;
            animation[animationState.Name].weight = animationState.Weight;
            animation[animationState.Name].wrapMode = (WrapMode) animationState.WrapMode;
            animation[animationState.Name].time = animationState.Time;
            animation[animationState.Name].normalizedTime = animationState.NormalizedTime;
            animation[animationState.Name].speed = animationState.Speed;
            animation[animationState.Name].normalizedSpeed = animationState.NormalizedSpeed;
            animation[animationState.Name].layer = animationState.Layer;
            animation[animationState.Name].name = animationState.Name;
            animation[animationState.Name].blendMode = (AnimationBlendMode) animationState.BlendMode;
        }

        animation.enabled = component.Enabled;
    }

    public AnimationSerializer(GameObject gameObject)
    {
        var animation = gameObject.GetComponent<Animation>();

        if (animation.clip != null)
            ClipName = animation.clip.name;

        PlayAutomatically = animation.playAutomatically;
        WrapMode = (WrapModeSerializer) animation.wrapMode;
        AnimatePhysics = animation.animatePhysics;
        CullingType = (AnimationCullingTypeSerializer) animation.cullingType;
        LocalBounds = (BoundsSerializer) animation.localBounds;

        Animations = new AnimationStateSerializer[animation.GetClipCount()];
        int i = 0;

        foreach (AnimationState animationState in animation)
        {
            Animations[i] = (AnimationStateSerializer) animationState;
            i++;
        }

        Enabled = animation.enabled;
    }

    // Empty constructor required for ProtoBuf
    private AnimationSerializer()
    {
    }
}

