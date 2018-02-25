using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TouchEvent : MonoBehaviour {
	public UnityEvent onTouchBegan;
	public UnityEvent onTouchEnd;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
		{
			onTouchBegan.Invoke ();
		}

		if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
		{
			onTouchEnd.Invoke ();
		}	

		#if UNITY_EDITOR
		if(Input.GetMouseButtonDown(0)){
			onTouchBegan.Invoke();
		}
		if(Input.GetMouseButtonUp(0)){
			onTouchEnd.Invoke();
		}
		#endif
	}


}
