using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {

	public float minZoom = 0.5f;
	public float maxZoom =  1.4f;
	public float zoomSensivity = 0.05f;
	private const float dragSensivity = 2.4f;
	private Vector3 mouseOrigin;
	private Vector3 move;
	private Vector3 currentPosition;
	private bool isDragging;
	private int _screenWidth;
	private int _screenHeight;


	// Use this for initialization
	void Start () {
		_screenWidth = Screen.width;
		_screenHeight = Screen.height;
	}
	
	// Update is called once per frame
	void LateUpdate () {
		ZoomCamera ();
		MoveCameraDrag ();
		MoveCameraCorner ();
	}

	void ZoomCamera()
	{
		Camera.main.orthographicSize += Input.GetAxis ("Mouse ScrollWheel") * zoomSensivity;

	}

	void MoveCameraDrag()
	{
		if (Input.GetMouseButtonDown (1)) {
			mouseOrigin = Input.mousePosition;
			isDragging = true;
		} 
		if (!Input.GetMouseButton (1)) {
			isDragging = false;
		}
		if (isDragging) 
		{
			currentPosition = Camera.main.ScreenToViewportPoint (Input.mousePosition - mouseOrigin);

			move = new Vector3 (currentPosition.x * dragSensivity, currentPosition.y * dragSensivity, currentPosition.z * dragSensivity);

			Camera.main.transform.Translate (-move, Space.Self);

			mouseOrigin = Input.mousePosition;
		}
	}

	void MoveCameraCorner()
	{


		//1f = ortho * 0.50;
		//float width = height * Camera.main.aspect;

		if (Input.mousePosition.x > _screenWidth && transform.position.x < (0.50f / Camera.main.orthographicSize)) 
		{
			move = new Vector3 (dragSensivity * Time.deltaTime, 0, 0);
			transform.Translate (move, Space.Self);
		}

		if (Input.mousePosition.x < 0 && transform.position.x > (-0.50f / Camera.main.orthographicSize)) 
		{
			move = new Vector3 (-dragSensivity * Time.deltaTime, 0, 0);
			transform.Translate (move, Space.Self);
		}

		if (Input.mousePosition.y > _screenHeight && transform.position.y < 0.20f / Camera.main.orthographicSize) 
		{
			move = new Vector3 (0, dragSensivity * Time.deltaTime, 0);
			transform.Translate (move, Space.Self);
		}

		if (Input.mousePosition.y < 0 && transform.position.y > -0.20f / Camera.main.orthographicSize) 
		{
			move = new Vector3 (0, -dragSensivity * Time.deltaTime, 0);
			transform.Translate (move, Space.Self);
		}
	}
}
