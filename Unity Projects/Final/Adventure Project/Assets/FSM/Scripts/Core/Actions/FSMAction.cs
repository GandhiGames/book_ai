using AdventureGame.ActionManagement;

namespace AdventureGame.StateManagement
{
	public abstract class FSMAction
	{
		public Command GetAction ()
		{
			if (OkToAct ()) {
				return BuildAction ();
			}

			return null;
		}


		public virtual void Enter ()
		{

		}

		public virtual void Exit ()
		{

		}

		protected virtual bool OkToAct ()
		{
			return true;
		}

		protected abstract Command BuildAction ();


	}
}
