using UnityEngine;


public class ExampleAttackComponent : MonoBehaviour
{
	public GameObject projectilePrefab;
	public Transform launchPointWeapon;

	public void MeleeAttack(AIBehaviors_AttackData attackData)
	{
		Debug.Log ("Melee attack");
		// Handle Melee attack behavior here...
	}


	public void RangedAttack(AIBehaviors_AttackData attackData)
	{
		GameObject projectile = GameObject.Instantiate(projectilePrefab, launchPointWeapon.position, transform.rotation) as GameObject;
		ExampleProjectile projectileComponent = projectile.GetComponent<ExampleProjectile>();
		projectileComponent.damage = attackData.damage;
	}
}