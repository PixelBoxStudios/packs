using UnityEngine;
using System.Collections;

public class SwitchOAMode : MonoBehaviour {
	public string tagName="Untagged";
	public bool switchUseOaTo = false;
	
	void OnTriggerEnter(Collider other) {
		
		if (other.gameObject.transform.root.gameObject.tag == tagName)
		{
			AIDriverController aIDriverController = other.gameObject.transform.root.gameObject.GetComponentInChildren<AIDriverController>();
			if (aIDriverController != null)
			{
				//aIDriverController.useObstacleAvoidance = switchUseOaTo;
				aIDriverController.SwitchOaMode(switchUseOaTo);
			}
		}
	}
}
