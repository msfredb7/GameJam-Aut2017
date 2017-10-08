using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {
	
	public float zoomSensivity = 2f;
	public const float dragSensivity = 20f;
	private Vector3 mouseOrigin;
	private Vector3 move;
	private Vector3 moveCorner;
	private Vector3 clamp;
	private Vector3 currentPosition;
	private Vector3 v3;
	private bool isDragging;
	private int _screenWidth;
	private int _screenHeight;
	private float clampVal;

	public float mapX = 82f;
	public float mapY = 46f;
	private float minX;
	private float maxX;
	private float minY;
	private float maxY;
	private float vertExtent;
	private float horzExtent;

	// Use this for initialization
	void Start () {
		SetCam (23f, 0f, 0f);
		_screenWidth = Screen.width;
		_screenHeight = Screen.height;
	}
	
	// Update is called once per frame
	void LateUpdate () {
		ZoomCamera ();

		vertExtent = Camera.main.orthographicSize;
		horzExtent = vertExtent * _screenWidth / _screenHeight;

		minX = (horzExtent - mapX / 2.0f) + v3.x;
		print (transform.position.x);
		maxX = (mapX / 2.0f - horzExtent) + v3.x;
		minY = (vertExtent - mapY / 2.0f) + v3.y;
		print (transform.position.y);
		maxY = (mapY / 2.0f - vertExtent) + v3.y;

		if (Input.GetMouseButtonDown (1)) 
		{
			mouseOrigin = Input.mousePosition;
		} 
		if (Input.GetMouseButton (1))
		{
			isDragging = true;
		}
		if (!Input.GetMouseButton (1))
		{
			isDragging = false;
		}
		if (isDragging) 
		{
			currentPosition = Camera.main.ScreenToViewportPoint (Input.mousePosition - mouseOrigin);
			move = new Vector3 (currentPosition.x * dragSensivity, currentPosition.y * dragSensivity, currentPosition.z * dragSensivity);
			transform.Translate (-move, Space.Self);
	
			mouseOrigin = Input.mousePosition;
		}
		if (!isDragging) 
		{
			float xValue = Input.GetAxis ("Horizontal");
			float yValue = Input.GetAxis ("Vertical");

			Camera.main.transform.Translate(new Vector3(xValue / 4, yValue / 4, 0.0f));

			if (Input.mousePosition.x > _screenWidth - 30) 
			{
				moveCorner = new Vector3 (dragSensivity * Time.deltaTime, 0, 0);
				transform.Translate (moveCorner, Space.Self);
			}
			if (Input.mousePosition.x < 0 + 30) 
			{
				moveCorner = new Vector3 (dragSensivity * Time.deltaTime, 0, 0);
				transform.Translate (-moveCorner, Space.Self);
			}
			if (Input.mousePosition.y > _screenHeight - 30) 
			{
				moveCorner = new Vector3 (0, dragSensivity * Time.deltaTime, 0);
				transform.Translate (moveCorner, Space.Self);
			}
			if (Input.mousePosition.y < 0 + 30) 
			{
				moveCorner = new Vector3 (0, dragSensivity * Time.deltaTime, 0);
				transform.Translate (-moveCorner, Space.Self);
			}
		}
		clamp = transform.position;
		clamp.x = Mathf.Clamp (clamp.x, minX, maxX);
		clamp.y = Mathf.Clamp (clamp.y, minY, maxY);
		transform.position = clamp;
	}

	void ZoomCamera()
	{
		Camera.main.orthographicSize += Input.GetAxis ("Mouse ScrollWheel") * zoomSensivity;
		if (Input.GetMouseButtonDown (0)) {
			Camera.main.orthographicSize -= 4.5f;
			Camera.main.orthographicSize = Mathf.Clamp (Camera.main.orthographicSize, 5f, clampVal);
		}
		if (Input.GetMouseButtonDown (1)) {
			Camera.main.orthographicSize += 4.5f;
			Camera.main.orthographicSize = Mathf.Clamp (Camera.main.orthographicSize, 5f, clampVal);
		}
	}

	public void SetCam(float val1, float val2, float val3){
		Camera.main.orthographicSize = val1;
		v3 = transform.position;
		v3.x = val2;
		v3.y = val3;
		clampVal = val1;
		transform.position = v3;
	}
}