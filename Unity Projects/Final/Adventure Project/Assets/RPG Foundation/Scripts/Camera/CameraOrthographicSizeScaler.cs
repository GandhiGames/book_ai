using UnityEngine;
using System.Collections;

namespace AdventureGame
{
	[RequireComponent(typeof(Camera))]
	public class CameraOrthographicSizeScaler : MonoBehaviour 
	{
		public float sizeScale = 0.8f;
		public bool continousScale = false;

		private Camera m_camera;

		void Awake()
		{
			m_camera = GetComponent<Camera>();
		}
			
		void Start () 
		{
			m_camera.orthographicSize = (Screen.height * 0.01f) / sizeScale;
		}

		void Update () 
		{
			if (continousScale) 
			{
				m_camera.orthographicSize = (Screen.height * 0.01f) / sizeScale;
			}
		}
	}
}
