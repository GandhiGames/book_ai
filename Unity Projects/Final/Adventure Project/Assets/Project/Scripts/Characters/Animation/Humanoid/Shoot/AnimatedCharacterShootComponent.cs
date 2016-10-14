namespace AdventureGame
{
	public interface AnimatedCharacterShootComponent
	{
		void SetRaiseProjectileUp (int index);

		void SetRaiseProjectileLeft (int index);

		void SetRaiseProjectileDown (int index);

		void SetRaiseProjectileRight (int index);

		void SetHoldUp ();

		void SetHoldLeft ();

		void SetHoldDown ();

		void SetHoldRight ();

		void ReleaseProjectileUp ();

		void ReleaseProjectileLeft ();

		void ReleaseProjectileDown ();

		void ReleaseProjectileRight ();

		void GetNewProjectileUp (int index);

		void GetNewProjectileLeft (int index);

		void GetNewProjectileDown (int index);

		void GetNewProjectileRight (int index);
	}
}