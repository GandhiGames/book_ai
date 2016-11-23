using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Collections;
using System.Collections.Generic;

namespace AdventureGame
{
	[RequireComponent (typeof(ObjectPool))]
	public class ObjectManager : Singleton<ObjectManager>
	{
		private ObjectPool m_Pool;

		void Awake()
		{
			m_Pool = GetComponent<ObjectPool>();
		}

		public GameObject GetObject (GameObject prefab, Vector2 position, Quaternion rotation, bool onlyPooled)
		{
		
			GameObject obj = null;
			
			if (Application.isPlaying) {

				obj = m_Pool.GetObjectForType (prefab.name, onlyPooled);
			
				if (obj) {
					obj.transform.position = position;
					obj.transform.rotation = rotation;
					obj.SetActive (true);
				} 
			} else {
#if UNITY_EDITOR
				obj = (GameObject)Editor.Instantiate (prefab, position, rotation);
#endif
			}
			
			return obj;
		}

		public bool OkToPool(string objName)
		{
			return Application.isPlaying ? m_Pool.SetupToUseObjectPool (objName) : false;
		}

		public GameObject GetObject (GameObject prefab, Vector2 position)
		{
			return GetObject (prefab, position, Quaternion.identity, false);
		}

		public GameObject GetObject (GameObject prefab, Vector2 position, bool onlyPooledObjects)
		{
			return GetObject (prefab, position, Quaternion.identity, onlyPooledObjects);
		}

		public void RemoveObject (GameObject obj)
		{
			m_Pool.PoolObject (obj);
		}
	}
}
