using UnityEngine;
using System.Collections;
using AdventureGame.ActionManagement;

namespace AdventureGame.StateManagement
{
	public class IdleAction : FSMAction
	{	
		protected override Command BuildAction ()
		{
			return null;
		}

		protected override bool OkToAct ()
		{
			return false;
		}
			
	}
}
