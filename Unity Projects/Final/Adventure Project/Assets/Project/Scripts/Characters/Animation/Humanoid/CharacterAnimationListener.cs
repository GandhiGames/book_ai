using UnityEngine;
using System.Collections;

namespace AdventureGame
{
	public class CharacterAnimationListener : MonoBehaviour
	{
		private AnimatedCharacterWalkComponent[] m_walkListeners;
		private AnimatedCharacterIdleComponent[] m_idleListeners;
		private AnimatedCharacterSwingComponent[] m_swingListeners;
		private AnimatedCharacterShootComponent[] m_shootListeners;

		void Awake ()
		{
			m_walkListeners = GetComponentsInChildren<AnimatedCharacterWalkComponent> ();
			m_idleListeners = GetComponentsInChildren<AnimatedCharacterIdleComponent> ();
			m_swingListeners = GetComponentsInChildren<AnimatedCharacterSwingComponent> ();
			m_shootListeners = GetComponentsInChildren<AnimatedCharacterShootComponent> ();
		}

		public void WalkDown (int animationStep)
		{
			foreach (var component in m_walkListeners) {
				component.SetWalkDown (animationStep);
			}
		}

		public void WalkLeft (int animationStep)
		{
			foreach (var component in m_walkListeners) {
				component.SetWalkLeft (animationStep);
			}
		}

		public void WalkUp (int animationStep)
		{
			foreach (var component in m_walkListeners) {
				component.SetWalkUp (animationStep);
			}
		}

		public void WalkRight (int animationStep)
		{
			foreach (var component in m_walkListeners) {
				component.SetWalkRight (animationStep);
			}
		}

		public void IdleLeft ()
		{
			foreach (var component in m_idleListeners) {
				component.SetIdleLeft ();
			}
		}

		public void IdleRight ()
		{
			foreach (var component in m_idleListeners) {
				component.SetIdleRight ();
			}
		}

		public void IdleUp ()
		{
			foreach (var component in m_idleListeners) {
				component.SetIdleUp ();
			}
		}

		public void IdleDown ()
		{
			foreach (var component in m_idleListeners) {
				component.SetIdleDown ();
			}
		}

		public void SwingUp (int animationStep)
		{
			foreach (var component in m_swingListeners) {
				component.SetSwingUp (animationStep);
			}
		}

		public void SwingLeft (int animationStep)
		{
			foreach (var component in m_swingListeners) {
				component.SetSwingLeft (animationStep);
			}
		}

		public void SwingDown (int animationStep)
		{
			foreach (var component in m_swingListeners) {
				component.SetSwingDown (animationStep);
			}
		}

		public void SwingRight (int animationStep)
		{
			foreach (var component in m_swingListeners) {
				component.SetSwingRight (animationStep);
			}
		}

		public void RaiseProjectileUp (int animationStep)
		{
			foreach (var component in m_shootListeners) {
				component.SetRaiseProjectileUp (animationStep);
			}
		}

		public void RaiseProjectileLeft (int animationStep)
		{
			foreach (var component in m_shootListeners) {
				component.SetRaiseProjectileLeft (animationStep);
			}
		}

		public void RaiseProjectileDown (int animationStep)
		{
			foreach (var component in m_shootListeners) {
				component.SetRaiseProjectileDown (animationStep);
			}
		}

		public void RaiseProjectileRight (int animationStep)
		{
			foreach (var component in m_shootListeners) {
				component.SetRaiseProjectileRight (animationStep);
			}
		}

		public void HoldProjectileUp ()
		{
			foreach (var component in m_shootListeners) {
				component.SetHoldUp ();
			}
		}

		public void HoldProjectileLeft ()
		{
			foreach (var component in m_shootListeners) {
				component.SetHoldLeft ();
			}
		}

		public void HoldProjectileDown ()
		{
			foreach (var component in m_shootListeners) {
				component.SetHoldDown ();
			}
		}

		public void HoldProjectileRight ()
		{
			foreach (var component in m_shootListeners) {
				component.SetHoldRight ();
			}
		}

		public void ReleaseProjectileUp ()
		{
			foreach (var component in m_shootListeners) {
				component.ReleaseProjectileUp ();
			}
		}

		public void ReleaseProjectileLeft ()
		{
			foreach (var component in m_shootListeners) {
				component.ReleaseProjectileLeft ();
			}
		}

		public void ReleaseProjectileDown ()
		{
			foreach (var component in m_shootListeners) {
				component.ReleaseProjectileDown ();
			}
		}

		public void ReleaseProjectileRight ()
		{
			foreach (var component in m_shootListeners) {
				component.ReleaseProjectileRight ();
			}
		}

		public void GetNewProjectileUp (int index)
		{
			foreach (var component in m_shootListeners) {
				component.GetNewProjectileUp (index);
			}
		}

		public void GetNewProjectileLeft (int index)
		{
			foreach (var component in m_shootListeners) {
				component.GetNewProjectileLeft (index);
			}
		}

		public void GetNewProjectileDown (int index)
		{
			foreach (var component in m_shootListeners) {
				component.GetNewProjectileDown (index);
			}
		}

		public void GetNewProjectileRight (int index)
		{
			foreach (var component in m_shootListeners) {
				component.GetNewProjectileRight (index);
			}
		}

	}
}
