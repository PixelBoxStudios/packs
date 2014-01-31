using System.Linq;
using UnityEngine;
using System;
using ProtoBuf;

[ProtoContract]
public sealed class HingeJointSerializer
{
    [ProtoMember(1)]  public string ConnectedBodyName { get; set; }
    [ProtoMember(2)]  public Vector3Serializer Axis { get; set; }
    [ProtoMember(3)]  public Vector3Serializer Anchor { get; set; }
    [ProtoMember(4)]  public float BreakForce { get; set; }
    [ProtoMember(5)]  public float BreakTorque { get; set; }
    [ProtoMember(6)]  public JointMotorSerializer Motor { get; set; }
    [ProtoMember(7)]  public JointLimitsSerializer Limits { get; set; }
    [ProtoMember(8)]  public JointSpringSerializer Spring { get; set; }
    [ProtoMember(9)]  public bool UseMotor { get; set; }
    [ProtoMember(10)] public bool UseLimits { get; set; }
    [ProtoMember(11)] public bool UseSpring { get; set; }

    public HingeJointSerializer(GameObject gameObject, HingeJointSerializer component)
	{
		var hingeJoint = gameObject.GetComponent<HingeJoint>();
		
		if (hingeJoint == null)
            hingeJoint = gameObject.AddComponent<HingeJoint>();


        if (!String.IsNullOrEmpty(component.ConnectedBodyName))
        {
            var rigidBodies = GameObject.FindObjectsOfType(typeof(Rigidbody)) as Rigidbody[];

            if (rigidBodies != null)
                hingeJoint.connectedBody = rigidBodies.FirstOrDefault(rigidBody => rigidBody.name == component.ConnectedBodyName);
        }


        hingeJoint.axis = (Vector3) component.Axis;
        hingeJoint.anchor = (Vector3) component.Anchor;
        hingeJoint.breakForce = component.BreakForce;
        hingeJoint.breakTorque = component.BreakTorque;
        hingeJoint.motor = (JointMotor) component.Motor;
        hingeJoint.limits = (JointLimits) component.Limits;
        hingeJoint.spring = (JointSpring) component.Spring;
        hingeJoint.useMotor = component.UseMotor;
        hingeJoint.useLimits = component.UseLimits;
        hingeJoint.useSpring = component.UseSpring;
	}
	
	public HingeJointSerializer(GameObject gameObject)
	{
		var hingeJoint = gameObject.GetComponent<HingeJoint>();

        if (hingeJoint.connectedBody != null)
            ConnectedBodyName = hingeJoint.connectedBody.name;

	    Axis = (Vector3Serializer) hingeJoint.axis;
	    Anchor = (Vector3Serializer) hingeJoint.anchor;
	    BreakForce = hingeJoint.breakForce;
	    BreakTorque = hingeJoint.breakTorque;
	    Motor = (JointMotorSerializer) hingeJoint.motor;
	    Limits = (JointLimitsSerializer) hingeJoint.limits;
	    Spring = (JointSpringSerializer) hingeJoint.spring;
	    UseMotor = hingeJoint.useMotor;
	    UseLimits = hingeJoint.useLimits;
	    UseSpring = hingeJoint.useSpring;
	}

    // Empty constructor required for ProtoBuf
    private HingeJointSerializer()
    {
    }
}
