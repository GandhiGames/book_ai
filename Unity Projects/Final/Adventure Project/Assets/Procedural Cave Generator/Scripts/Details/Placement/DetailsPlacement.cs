using UnityEngine;
using System.Collections;

namespace AdventureGame.CaveGenerator
{
	public abstract class DetailsPlacement : MonoBehaviour 
	{
		public string containerName;

		protected GameObject container;

		protected virtual void Awake()
		{
			Initialise ();
		}

		public void EditorSetup()
		{
			Initialise ();
		}

		public bool Generate ()
		{
			Remove ();
			bool generated = PlaceOnCurrentLevel ();

			return generated;
		}

		public bool Remove ()
		{
			bool destroyed = DetailsGenerated ();

			container.transform.ClearImmediate ();

			return destroyed;
		}

		protected virtual void Initialise()
		{
			// Prefab null check ensures editor does not try to read name variable in editor.
			if(!ContainerInitialised ()){
				InitialiseContainer ();
			}
		}

		protected bool DetailsGenerated ()
		{
			return container.transform.childCount > 0;
		}

		protected void InitialiseContainer()
		{
			container = Utilities.instance.CreateContainer (gameObject, "Details" + "_" + containerName);
		}

		protected bool ContainerInitialised()
		{
			return container != null;
		}

		protected abstract bool PlaceOnCurrentLevel ();
	}
}
