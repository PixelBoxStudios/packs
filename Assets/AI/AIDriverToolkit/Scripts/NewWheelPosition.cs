using UnityEngine;
using System.Collections;

public class NewWheelPosition : MonoBehaviour {
    public WheelCollider WheelCol;
    private Vector3 newPos;


    void Update () {
	    RaycastHit hit;

	    if (Physics.Raycast(WheelCol.transform.position, -WheelCol.transform.up, out hit, WheelCol.suspensionDistance + WheelCol.radius))
		   
		    if (hit.collider.isTrigger)	
		    {               
                newPos =transform.position;
		    }	
		    else
		    {
		        newPos = hit.point + WheelCol.transform.up * WheelCol.radius;
		    }
    		
	    else
		    newPos = WheelCol.transform.position - (WheelCol.transform.up * WheelCol.suspensionDistance);    		
	    
	    transform.position = newPos;
    }
}
