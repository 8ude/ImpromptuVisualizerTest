using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaptureCenterPixel : MonoBehaviour {
    // Script Inputs
    public bool m_shouldCaptureOnNextFrame = false;
    public Color m_lastCapturedColor = Color.green;
	public Vector3 targetPos;

    // Privates
    Texture2D m_centerPixTex;
	Camera cam;
    
	void Start()
    {
		cam = GetComponent<Camera>();
        m_centerPixTex = new Texture2D(1, 1, TextureFormat.RGBA32, false);
    }

    void OnPostRender()
    {
        if (m_shouldCaptureOnNextFrame)
        {
			

			Resolution res = Screen.currentResolution;


			Vector3 screenPos = cam.WorldToScreenPoint (targetPos);

			#if UNITY_EDITOR
			int x = Screen.width / 2 - 1;
			int y = Screen.height / 2 - 1;
			#else
			int x = res.width / 2 - 1;
			int y = res.height / 2 - 1;
			#endif

            m_lastCapturedColor = GetRenderedColorAt(x, y);
            m_shouldCaptureOnNextFrame = false;
        }
    }

    // Helpers
    Color GetRenderedColorAt(int x, int y)
    {
        m_centerPixTex.ReadPixels(new Rect(x, y, 1, 1), 0, 0);
        m_centerPixTex.Apply();
        
        return m_centerPixTex.GetPixels()[0];
    }
}
