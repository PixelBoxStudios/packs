using System;
using System.Linq;
using UnityEngine;
using ProtoBuf;

[ProtoContract]
public sealed class FixedJointSerializer
{
    [ProtoMember(1)] public string ConnectedBodyName { get; set; }
    [ProtoMember(2)] public Vector3Serializer Axis { get; set; }
    [ProtoMember(3)] public Vector3Serializer Anchor { get; set; }
    [ProtoMember(4)] public float BreakForce { get; set; }
    [ProtoMember(5)] public float BreakTorque { get; set; }

    public FixedJointSerializer(GameObject gameObject, FixedJointSerializer component)
	{
        var fixedJoint = gameObject.GetComponent<FixedJoint>();

        if (fixedJoint == null)
            fixedJoint = gameObject.AddComponent<FixedJoint>();

        if (!String.IsNullOrEmpty(component.ConnectedBodyName))
        {
            var rigidBodies = GameObject.FindObjectsOfType(typeof(Rigidbody)) as Rigidbody[];

            if (rigidBodies != null)
                fixedJoint.connectedBody = rigidBodies.FirstOrDefault(rigidBody => rigidBody.name == component.ConnectedBodyName);
        }

        fixedJoint.axis = (Vector3)component.Axis;
        fixedJoint.anchor = (Vector3)component.Anchor;
        fixedJoint.breakForce = component.BreakForce;
        fixedJoint.breakTorque = component.BreakTorque;
	}
	
	public FixedJointSerializer(GameObject gameObject)
	{
        var fixedJoint = gameObject.GetComponent<FixedJoint>();

        if (fixedJoint.connectedBody != null)
            ConnectedBodyName = fixedJoint.connectedBody.name;

        Axis = (Vector3Serializer)fixedJoint.axis;
        Anchor = (Vector3Serializer)fixedJoint.anchor;
        BreakForce = fixedJoint.breakForce;
        BreakTorque = fixedJoint.breakTorque;
	}

    // Empty constructor required for ProtoBuf
    private FixedJointSerializer()
    {
    }
}
