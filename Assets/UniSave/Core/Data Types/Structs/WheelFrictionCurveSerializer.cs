using UnityEngine;
using ProtoBuf;

[ProtoContract]
public struct WheelFrictionCurveSerializer
{
	[ProtoMember(1)] public float ExtremumSlip { get; set; }
	[ProtoMember(2)] public float ExtremumValue { get; set; }
	[ProtoMember(3)] public float AsymptoteSlip { get; set; }
	[ProtoMember(4)] public float AsymptoteValue { get; set; }
	[ProtoMember(5)] public float Stiffness { get; set; }

    public WheelFrictionCurveSerializer(WheelFrictionCurve data) : this()
    {
        ExtremumSlip = data.extremumSlip;
        ExtremumValue = data.extremumValue;
        AsymptoteSlip = data.asymptoteSlip;
        AsymptoteValue = data.asymptoteValue;
        Stiffness = data.stiffness;
    }

    public static explicit operator WheelFrictionCurve(WheelFrictionCurveSerializer data)
    {
        var wheelFrictionCurve = new WheelFrictionCurve()
        {
            extremumSlip = data.ExtremumSlip,
            extremumValue = data.ExtremumValue,
            asymptoteSlip = data.AsymptoteSlip,
            asymptoteValue = data.AsymptoteValue,
            stiffness = data.Stiffness
        };

        return wheelFrictionCurve;
    }

    public static explicit operator WheelFrictionCurveSerializer(WheelFrictionCurve data)
	{
        return new WheelFrictionCurveSerializer(data);
	}
}
