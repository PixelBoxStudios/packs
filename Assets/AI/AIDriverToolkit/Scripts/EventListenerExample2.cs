using UnityEngine;
using System.Collections;
//using EnemyAI;

public class EventListenerExample2 : MonoBehaviour
{
   
    public GameObject respawnEffect;
    private string message;
    
    void OnEnable()
    {
        AIDriverController.onLastWaypoint += onLastWaypoint;        
        AIRespawnController.onRespawnWaypoint += onRespawnWaypoint;
    }

    void OnDisable()
    {       
        AIDriverController.onLastWaypoint -= onLastWaypoint;        
        AIRespawnController.onRespawnWaypoint -= onRespawnWaypoint;
    }
    	

    void onLastWaypoint(AIEventArgs2 e)
    {
        //Example:
        //message = e.name + " reached last Waypoint '" + e.currentWaypointName + "' (" + e.currentWaypointIndex +").";     
        //StartCoroutine(ShowMessage(message,1));
    }

    void onRespawnWaypoint(AIEventArgs2 e)
    {
        Instantiate(respawnEffect, e.position, e.rotation);		
        //Example:
        //message = e.name + " was respawned at '" + e.position.x + ";" + e.position.x + ";" + e.position.x +"'.";
        //StartCoroutine(ShowMessage(message, 1));
    } 

    IEnumerator ShowMessage(string text, float seconds)
    {
        message = text;       
        yield return new WaitForSeconds(seconds);
        message = "";
    }
    void OnGUI()
    {
        GUILayout.Space(20);
        GUILayout.Label(message);
    }
}
