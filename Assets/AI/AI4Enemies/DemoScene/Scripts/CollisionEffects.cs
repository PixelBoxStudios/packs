using UnityEngine;
using System.Collections;

public class CollisionEffects : MonoBehaviour {

	// Use this for initialization
    public GameObject particleSystem;
    public GameObject body;
    private Component emitter;
	
    void Awake()
    { 
        Destroy(gameObject,2);
    }
		
    void OnCollisionEnter(Collision collisionInfo) 
    {
       
        StartCoroutine(End());

    }
    IEnumerator End()
    {
        gameObject.rigidbody.velocity = Vector3.zero;
        yield return new WaitForSeconds(0.2f);       
        Destroy(gameObject);        
    }
    
    
}
