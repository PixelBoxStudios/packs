using UnityEngine;
using ProtoBuf;

[ProtoContract]
public struct JointSpringSerializer
{
	[ProtoMember(1)] public float Spring;
	[ProtoMember(2)] public float Damper;
	[ProtoMember(3)] public float TargetPosition;

    public JointSpringSerializer(JointSpring data)
    {
        Spring = data.spring;
        Damper = data.damper;
        TargetPosition = data.targetPosition;
    }

    public static explicit operator JointSpring(JointSpringSerializer data)
    {
        var jointSpring = new JointSpring()
        {
            spring = data.Spring, 
            damper = data.Damper, 
            targetPosition = data.TargetPosition
        };

        return jointSpring;
    }

    public static explicit operator JointSpringSerializer(JointSpring data)
	{
        return new JointSpringSerializer(data);
	}
}
