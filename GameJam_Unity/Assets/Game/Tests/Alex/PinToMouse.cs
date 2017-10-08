using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinToMouse : MonoBehaviour
{
    public Vector3 worldOffset;
    private Camera cam;

    void Start()
    {
        Game.OnGameReady += () => cam = Camera.main;
    }

    void Update()
    {
        if (cam == null)
            return;

        transform.position = cam.WorldToScreenPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition) + worldOffset);
    }
}
