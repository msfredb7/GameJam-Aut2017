using System.Collections;
using System.Collections.Generic;
using CCC.Manager;
using UnityEngine;

public class William_TestScript : MonoBehaviour
{

    public static float MAP_SIZE_X = 10;
    public static float MAP_SIZE_Y = 10;

    public static int MIN_ORDER_TIMER = 1;
    public static int MAX_ORDER_TIMER = 5;

    public static Bounds MAP_BOUNDS = new Bounds(new Vector2(0,0), new Vector2(MAP_SIZE_X, MAP_SIZE_Y));

    // Use this for initialization
    void Start () {
		MasterManager.Sync();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

}
