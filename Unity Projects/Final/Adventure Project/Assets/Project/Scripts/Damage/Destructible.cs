using UnityEngine;
using System.Collections;

namespace AdventureGame
{
	public class Destructible : MonoBehaviour, DamageListener
	{
		public GameObject objectToSpawnOnDestroy;
		public int minToSpawn;
		public int maxToSpawn;
		[Range (0, 1)]
		public float spawnChance = 0.6f;

		public void ApplyDamage (float damageAmount, Vector2 force)
		{
			SpawnObjectsOnDestroy ();
					
			RemoveFromGame ();
		}

		private void SpawnObjectsOnDestroy ()
		{
			if (objectToSpawnOnDestroy == null) {
				return;
			}
			
			int numToSpawn = Random.Range (minToSpawn, maxToSpawn + 1);

			for (int i = 0; i < numToSpawn; i++) {
				ObjectManager.instance.GetObject (objectToSpawnOnDestroy, transform.position, Quaternion.identity, false);
			}
		}

		private void RemoveFromGame ()
		{
			gameObject.CleanName ();
			
			if (ObjectManager.instance.OkToPool (gameObject.name)) {
				ObjectManager.instance.RemoveObject (gameObject);
			} else {
				Destroy (gameObject);
			}
		}
	}
}
