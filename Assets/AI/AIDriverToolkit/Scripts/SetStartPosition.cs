using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SetStartPosition : MonoBehaviour {

	public int startWaypoint = 0;
	public int heightOffset = 2;
	// Use this for initialization
	void Start () {
		if (startWaypoint > 0)
		{
			Vector3 startPos;
			AIDriverController aiDriverController;
			aiDriverController = gameObject.GetComponent("AIDriverController") as AIDriverController;
			
			if (aiDriverController.waypoints.Count > startWaypoint)
			{
				startPos =  aiDriverController.waypoints[startWaypoint].position;
				startPos.y += heightOffset;
				gameObject.transform.position = startPos;
				gameObject.transform.rotation = aiDriverController.waypoints[startWaypoint].rotation;
				
				if (aiDriverController.waypoints.Count > startWaypoint + 1)
				{
					aiDriverController.currentWaypoint = startWaypoint + 1; 
				}
				else
				{
					aiDriverController.currentWaypoint = 0;
				}
			}
			else
			{
				Debug.LogError("StartWaypoint number is to high. Maximum is" + (aiDriverController.waypoints.Count -1) + ").");
			}
		}
			                            
	}	
	
}
