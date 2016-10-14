using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace AdventureGame
{
	public class ObjectPool : MonoBehaviour
	{

		/// <summary>
		/// The object prefabs which the pool can handle.
		/// </summary>
		public ObjectPoolItem[] objectPoolItems;

		/// <summary>
		/// The pooled objects currently available.
		/// </summary>
		public List<GameObject>[] pooledObjects;

		/// <summary>
		/// The container object that we will keep unused pooled objects so we dont clog up the editor with objects.
		/// </summary>
		private GameObject m_ContainerObject;


		void Awake ()
		{
			Initialise ();
		}

		private void Initialise ()
		{
			m_ContainerObject = gameObject;

			pooledObjects = new List<GameObject>[objectPoolItems.Length];
			
			int i = 0;
			foreach (ObjectPoolItem item in objectPoolItems) {
				pooledObjects [i] = new List<GameObject> (); 

				for (int n = 0; n < item.BufferAmount; n++) {
					GameObject newObj = Instantiate (item.ObjectPrefab) as GameObject;
					newObj.name = item.ObjectPrefab.name;
					newObj.SetActive (false);
					PoolObject (newObj);
				}

				i++;
			}
		}

		/// <summary>
		/// Gets a new object for the name type provided.  If no object type exists or if onlypooled is true and there is no objects of that type in the pool
		/// then null will be returned.
		/// </summary>
		/// <returns>
		/// The object for type.
		/// </returns>
		/// <param name='objectType'>
		/// Object type.
		/// </param>
		/// <param name='onlyPooled'>
		/// If true, it will only return an object if there is one currently pooled.
		/// </param>
		public GameObject GetObjectForType (string objectType, bool onlyPooled)
		{
			
			for (int i = 0; i < objectPoolItems.Length; i++) {
				var prefab = objectPoolItems [i].ObjectPrefab;
				if (prefab.name == objectType) {
					if (pooledObjects [i].Count > 0) {
						GameObject pooledObject = pooledObjects [i] [0];
												
						if (pooledObject) {
							pooledObjects [i].RemoveAt (0);
							pooledObject.transform.SetParent (null, false);
						} else {
							Debug.LogError (objectType + ": not found in pool");
						}
											
						return pooledObject;

					} else if (!onlyPooled) {
						Debug.Log("Creating object of type: " + objectType);
						GameObject newObj = (GameObject)Instantiate (prefab);
						newObj.name = prefab.name;
						return newObj;
					}

					break;

				}
			}

			// No object of the specified type or none were left in the pool with onlyPooled set to true
			return null;
		}

		/// <summary>
		/// Pools the object specified.  Will not be pooled if there is no prefab of that type.
		/// </summary>
		/// <param name='obj'>
		/// Object to be pooled.
		/// </param>
		public void PoolObject (GameObject obj)
		{

			for (int i = 0; i < objectPoolItems.Length; i++) {
				if (objectPoolItems [i].ObjectPrefab.name == obj.name) {
					obj.SetActive(false);
					obj.transform.SetParent(m_ContainerObject.transform, true);
					pooledObjects [i].Add (obj);
					return;
				}
			}

						
			Debug.LogError (obj.name + ": not setup to use object pool");
		}

		public bool SetupToUseObjectPool(string objName)
		{
			for (int i = 0; i < objectPoolItems.Length; i++) {
				if (objectPoolItems [i].ObjectPrefab.name == objName) {
					return true;
				}
			}

			return false;
		}
		
		public bool DestroyPooledObjects ()
		{
			bool destroyed = transform.childCount > 0;
			
			transform.ClearImmediate ();
			
			return destroyed;
		}


	}

	[System.Serializable]
	public class ObjectPoolItem
	{
		public GameObject ObjectPrefab;
		public int BufferAmount = 1;
	}

}
