using UnityEngine;
using System.Collections;

namespace AdventureGame.CaveGenerator
{
	/*
	 * When you hit the play button in the editor, all the objects in the active scene are serialized and saved, 
	 * so that unity can deserialize and return them to their original state when you stop the execution in the editor. 
	 * Unity also creates copies of all objects in the scene, so the changes you do during play mode change the copies, 
	 * not the original objects in the scene. During this copy process it deserializes the objects with the data it saved 
	 * just before copying, so no visible change is done on the objects. But not in your case!
	 * 
	 * The problem in your case is the object that you are creating(HiddenObject) is not serializable. 
	 * So, when you hit play, unity serializes the gameobject in you scene, which has the Inventory script on it, 
	 * but the hiddenObject member of your Inventory class is not serializable, so it's not saved. And obviously 
	 * it can't be deserialized either, since it wasn't saved in the first place. So, your hiddenObject is set to 
	 * null whenever unity deserializes your Inventory class, which is when you hit play or stop. Your object will 
	 * also be set to null when you save your scene, close the scene and open it again.
	 */

	public class DungeonEditorGeneratorFix : MonoBehaviour
	{
		public GameObject dungeonGenerator;

		void Start ()
		{
			dungeonGenerator.GetComponentInParent<GridManager> ().ReGenerate ();

			var detailsPlacement = dungeonGenerator.GetComponentsInParent<DetailsPlacement> ();

			foreach(var detail in detailsPlacement)
			{
				detail.Generate ();
			}
		}

	}
}