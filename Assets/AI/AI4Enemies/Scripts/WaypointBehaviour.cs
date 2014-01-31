using UnityEngine;
using System.Collections;

public class WaypointBehaviour : MonoBehaviour
{
  
    void Awake()
    {
        renderer.enabled = false;

        if (gameObject.collider != null)
        {
            Destroy(gameObject.collider);
        }
    }
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}   

