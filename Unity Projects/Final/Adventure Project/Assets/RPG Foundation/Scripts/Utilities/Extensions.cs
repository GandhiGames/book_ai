using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AdventureGame
{
	public static class Extensions
	{
		//private static System.Random rng = new System.Random ();

		public static bool IsNullOrEmpty (this IEnumerable[] t)
		{
			return t == null || t.Length == 0;
		}

		public static bool IsNullOrEmpty (this ICollection collection)
		{
			return collection == null || collection.Count == 0;
		}

		public static void ClearImmediate (this Transform transform)
		{
			var children = new List<GameObject> ();
			foreach (Transform child in transform) {
				children.Add (child.gameObject);
			}

			foreach (var child in children) {
				child.gameObject.CleanName ();
				if (ObjectManager.instance.OkToPool (child.name)) {
					ObjectManager.instance.RemoveObject (child);
				} else {
					if (Application.isPlaying) {
						GameObject.Destroy (child);
					} else {
						GameObject.DestroyImmediate (child);
					}
			
				}
			}

		}


		public static string[] MaskToNames (this LayerMask original)
		{
			var output = new List<string> ();

			for (int i = 0; i < 32; ++i) {
				int shifted = 1 << i;
				if ((original & shifted) == shifted) {
					string layerName = LayerMask.LayerToName (i);
					if (!string.IsNullOrEmpty (layerName)) {
						output.Add (layerName);
					}
				}
			}
			return output.ToArray ();
		}

		public static void Shuffle<T> (this IList<T> list)
		{  
			int n = list.Count;  
			while (n > 1) {  
				n--;  
				int k = Random.Range (0, n + 1);  
				T value = list [k];  
				list [k] = list [n];  
				list [n] = value;  
			}  
		}

		public static IList<T> Clone<T> (this IList<T> listToClone)
		{
			return new List<T> (listToClone); 
		}

		public static void CleanName(this GameObject gameObject)
		{
			gameObject.name = gameObject.name.Replace ("(Clone)", "");
		}

		public static List<T> ToList<T>(this T obj) 
		{
			var list = new List<T> ();
			list.Add (obj);
			return list;
		}

	}
}
