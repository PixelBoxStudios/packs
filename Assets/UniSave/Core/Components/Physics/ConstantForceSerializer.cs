using UnityEngine;
using ProtoBuf;

[ProtoContract]
public sealed class ConstantForceSerializer
{
    [ProtoMember(1)] public bool Enabled { get; set; }
	[ProtoMember(2)] public Vector3Serializer Force { get; set; }
	[ProtoMember(3)] public Vector3Serializer RelativeForce { get; set; }
	[ProtoMember(4)] public Vector3Serializer Torque { get; set; }
	[ProtoMember(5)] public Vector3Serializer RelativeTorque { get; set; }

    public ConstantForceSerializer(GameObject gameObject, ConstantForceSerializer component)
	{
        var constantForce = gameObject.GetComponent<ConstantForce>();

        if (constantForce == null)
            constantForce = gameObject.AddComponent<ConstantForce>();

        constantForce.enabled = component.Enabled;
        constantForce.force = (Vector3) component.Force;
        constantForce.relativeForce = (Vector3) component.RelativeForce;
        constantForce.torque = (Vector3) component.Torque;
        constantForce.relativeTorque = (Vector3) component.RelativeTorque;
	}
	
	public ConstantForceSerializer(GameObject gameObject)
	{
        var constantForce = gameObject.GetComponent<ConstantForce>();

	    Enabled = constantForce.enabled;
	    Force = (Vector3Serializer) constantForce.force;
	    RelativeForce = (Vector3Serializer) constantForce.relativeForce;
	    Torque = (Vector3Serializer) constantForce.torque;
	    RelativeTorque = (Vector3Serializer) constantForce.relativeTorque;
	}

    // Empty constructor required for ProtoBuf
    private ConstantForceSerializer()
    {
    }
}
