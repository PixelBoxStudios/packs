using UnityEngine;
using ProtoBuf;

[ProtoContract]
public struct SoftJointLimitSerializer
{
	[ProtoMember(1)] public float Limit { get; set; }
	[ProtoMember(2)] public float Spring { get; set; }
	[ProtoMember(3)] public float Damper { get; set; }
	[ProtoMember(4)] public float Bounciness { get; set; }

    public SoftJointLimitSerializer(SoftJointLimit data) : this()
    {
        Limit = data.limit;
        Spring = data.spring;
        Damper = data.damper;
        Bounciness = data.bounciness;
    }

    public static explicit operator SoftJointLimit(SoftJointLimitSerializer data)
    {
        var softJointLimit = new SoftJointLimit()
        {
            limit = data.Limit,
            spring = data.Spring,
            damper = data.Damper,
            bounciness = data.Bounciness
        };

        return softJointLimit;
    }

    public static explicit operator SoftJointLimitSerializer(SoftJointLimit data)
	{
        return new SoftJointLimitSerializer(data);
	}
}
