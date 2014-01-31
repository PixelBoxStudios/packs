using System;
using System.Collections;
using UnityEngine;

public class Checkpoint : MonoBehaviour 
{
    private const string GizmoIcon = "../UniSave/Gizmos/UniSave_Checkpoint.png";
    private const string AutoSaveName = "Autosave";

    private Transform _player;
	private SphereCollider _sphereCollider;

    private void Awake()
	{
	    _player = GameObject.FindGameObjectWithTag("Player").transform;
		_sphereCollider = GetComponent<SphereCollider>();
	}
	
	private void Update()
	{
        if (Vector3.Distance(_player.transform.position, transform.position) <= _sphereCollider.radius)
        {
            Destroy(gameObject);
            UniSave.Save(AutoSaveName);
        }
	}
	
	private void OnDrawGizmos()
	{
		Gizmos.DrawIcon(transform.position, GizmoIcon);
	}
}
