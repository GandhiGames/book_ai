using UnityEngine;

namespace AdventureGame
{
	public class RangedWeapon : Weapon
	{
		public GameObject projectilePrefab;

		public float shootForce = 200f;

		void Awake ()
		{
			Equip<RangedWeapon> (this);
		}

		public override void DoDamage (Vector2 heading)
		{
			var projectile = GetNewProjectile (heading);
			projectile.Initialise (heading, shootForce, range, knockbackForce, 
				hitMask, obstacleMask, damageAmount);
		}

		private Projectile GetNewProjectile (Vector2 heading)
		{

			float angle = Mathf.Atan2 (heading.y, heading.x) * Mathf.Rad2Deg;

			var projectileObj = ObjectManager.instance.GetObject (projectilePrefab, transform.position, 
				Quaternion.AngleAxis (angle, Vector3.forward), false);

			if(projectileObj == null)
			{
				Debug.LogError("Projectile not added to object pool.");
			}

			return projectileObj.GetComponent<Projectile> ();
		}
	}
}
