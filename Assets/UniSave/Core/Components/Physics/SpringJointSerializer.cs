using System;
using System.Linq;
using UnityEngine;
using ProtoBuf;

[ProtoContract]
public sealed class SpringJointSerializer
{
    [ProtoMember(1)] public string ConnectedBodyName { get; set; }
    [ProtoMember(2)] public Vector3Serializer Axis { get; set; }
    [ProtoMember(3)] public Vector3Serializer Anchor { get; set; }
    [ProtoMember(4)] public float BreakForce { get; set; }
    [ProtoMember(5)] public float BreakTorque { get; set; }
	[ProtoMember(6)] public float Spring { get; set; }
	[ProtoMember(7)] public float Damper { get; set; }
	[ProtoMember(8)] public float MinDistance { get; set; }
	[ProtoMember(9)] public float MaxDistance { get; set; }

    public SpringJointSerializer(GameObject gameObject, SpringJointSerializer component)
	{
        var springJoint = gameObject.GetComponent<SpringJoint>();

        if (springJoint == null)
            springJoint = gameObject.AddComponent<SpringJoint>();

        if (!String.IsNullOrEmpty(component.ConnectedBodyName))
        {
            var rigidBodies = GameObject.FindObjectsOfType(typeof(Rigidbody)) as Rigidbody[];

            if (rigidBodies != null)
                springJoint.connectedBody = rigidBodies.FirstOrDefault(rigidBody => rigidBody.name == component.ConnectedBodyName);
        }

        springJoint.axis = (Vector3) component.Axis;
        springJoint.anchor = (Vector3) component.Anchor;
        springJoint.breakForce = component.BreakForce;
        springJoint.breakTorque = component.BreakTorque;
        springJoint.spring = component.Spring;
        springJoint.damper = component.Damper;
        springJoint.minDistance = component.MinDistance;
        springJoint.maxDistance = component.MaxDistance;
	}
	
	public SpringJointSerializer(GameObject gameObject)
	{
        var springJoint = gameObject.GetComponent<SpringJoint>();

        if (springJoint.connectedBody != null)
            ConnectedBodyName = springJoint.connectedBody.name;

        Axis = (Vector3Serializer) springJoint.axis;
        Anchor = (Vector3Serializer) springJoint.anchor;
        BreakForce = springJoint.breakForce;
        BreakTorque = springJoint.breakTorque;
	    Spring = springJoint.spring;
	    Damper = springJoint.damper;
	    MinDistance = springJoint.minDistance;
	    MaxDistance = springJoint.maxDistance;
	}

    // Empty constructor required for ProtoBuf
    private SpringJointSerializer()
    {
    }
}
