using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinToWorld : MonoBehaviour
{
    public Transform worldTransform;
    public Vector3 worldOffset;
    private Camera cam;

    void Start()
    {
        Game.OnGameReady += ()=> cam = Camera.main;
    }

    void Update()
    {
        if (cam == null || worldTransform == null)
            return;

        transform.position = cam.WorldToScreenPoint(worldTransform.position + worldOffset);
    }
}
