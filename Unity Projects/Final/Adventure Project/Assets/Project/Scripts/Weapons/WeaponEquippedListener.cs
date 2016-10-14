
namespace AdventureGame
{
	public interface WeaponEquippedListener<T> where T : Weapon
	{
		void WeaponEquipped (T weapon);
	}
}
