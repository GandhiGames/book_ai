using UnityEngine;
using System.Collections;

namespace AdventureGame.CaveGenerator
{
	public class DungeonDetailsPlacement : DetailsPlacement
	{
		public PathManager pathManager;

		public int minNumberToPlace = 0;
		public int maxNumberToPlace = 5;

		public CellPositionData[] positionData;

		protected override bool PlaceOnCurrentLevel ()
		{
			if (!GridManager.instance.initialised) {
				return false;
			}

			int numToSpawn = Random.Range (minNumberToPlace, maxNumberToPlace + 1);

			if(numToSpawn > 0){
				new CellPlacement (container, positionData, numToSpawn).Place ();
			}

			return true;
		}
			
	
	}
}
