using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBoundaries : MonoBehaviour {
	public CameraControl cc;
	public int level = 2;

	// Use this for initialization
	void Start () 
	{
	}

	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown(KeyCode.O))
		{
			LoadLevelBoundaries (--level);
		}
	}

	void LoadLevelBoundaries(int _level)
	{
		Vector3 v3 = transform.position;
		switch (_level)
		{
		case 1: 
			cc.SetCam (13.3f, -21.7f, 9.86f);
			cc.mapX = 38.44f;
			cc.mapY = 26.6f;
			break;
		case 2:
			cc.SetCam (23f, 0f, 0f);
			cc.mapX = 82f;
			cc.mapY = 46f;
			break;
		}
	}
}
