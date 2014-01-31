using UnityEngine;
using ProtoBuf;

[ProtoContract]
public struct JointLimitsSerializer
{
	[ProtoMember(1)] public float Min { get; set; }
	[ProtoMember(2)] public float MinBounce { get; set; }
	[ProtoMember(3)] public float Max { get; set; }
	[ProtoMember(4)] public float MaxBounce { get; set; }

    public JointLimitsSerializer(JointLimits data) : this()
    {
        Min = data.min;
        MinBounce = data.minBounce;
        Max = data.max;
        MaxBounce = data.maxBounce;
    }

    public static explicit operator JointLimits(JointLimitsSerializer data)
    {
        var jointLimits = new JointLimits()
        {
            min = data.Min,
            minBounce = data.MinBounce,
            max = data.Max,
            maxBounce = data.MaxBounce
        };

        return jointLimits;
    }

    public static explicit operator JointLimitsSerializer(JointLimits data)
    {
        return new JointLimitsSerializer(data);
    }
}
