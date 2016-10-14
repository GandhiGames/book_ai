using UnityEngine;
using System.Collections;

namespace AdventureGame
{
	[RequireComponent (typeof(Light))]
	public class LightFlicker : MonoBehaviour
	{
		// possible values: sin, tri(angle), sqr(square), saw(tooth), inv(verted sawtooth), noise (random)
		public enum FlickerType
		{
			Sin,
			Tri,
			Sqr,
			Saw,
			Inv,
			Noise
		}

		public FlickerType flickerType = FlickerType.Noise;
	
		public float _base = 0.0f;
		// start
		public float amplitude = 1.0f;
		// amplitude of the wave
		public float phase = 0.0f;
		// start point inside on wave cycle
		public float frequency = 0.5f;
		// cycle frequency per second

		// Keep a copy of the original color
		private Color m_OriginalColor;
		private Light m_Light;

	
		void Awake()
		{
			m_Light = GetComponent<Light> ();
		}

		void Start ()
		{
			m_OriginalColor = m_Light.color;
		}

		void Update ()
		{
			m_Light.color = m_OriginalColor * EvalWave ();
		}

		private float EvalWave ()
		{
			float x = (Time.time + phase) * frequency;
			float y = 1f;

			x = x - Mathf.Floor (x); // normalized value (0..1)

			switch (flickerType) {
			case FlickerType.Sin:
				y = Mathf.Sin (x * 2 * Mathf.PI);
				break;
			case FlickerType.Tri:
				if (x < 0.5)
					y = 4.0f * x - 1.0f;
				else
					y = -4.0f * x + 3.0f;  
				break;
			case FlickerType.Sqr:
				if (x < 0.5f)
					y = 1.0f;
				else
					y = -1.0f;  
				break;
			case FlickerType.Saw:
				y = x;
				break;
			case FlickerType.Inv:
				y = 1.0f - x;
				break;
			case FlickerType.Noise:
				y = 1f - (Random.value * 2f);
				break;
			}

			return (y * amplitude) + _base;    
		}
	}
}