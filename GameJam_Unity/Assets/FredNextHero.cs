using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FredNextHero : MonoBehaviour
{
    bool isMaster;
    bool follow = false;
    bool canContaminate = false;

    protected static Vector3 offset;

    void Awake()
    {
        isMaster = Game.HeroManager.listOwnedHero.Count == 0;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && isMaster)
            follow = true;

        if (follow)
        {
            Transform camT = Camera.main.transform;
            Vector3 pos = transform.position;
            camT.position = camT.position.Lerpped(new Vector3(pos.x, pos.y, camT.position.z) + offset, FixedLerp.Fix(0.1f));
        }

        offset = offset.MovedTowards(Vector3.zero, Time.deltaTime * 10);

        canContaminate = true;
    }

    public void SetAsTarget()
    {
        canContaminate = false;
        follow = true;
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (!follow)
            return;

        FredNextHero nh = collider.GetComponent<FredNextHero>();
        if (nh != null)
        {
            if (canContaminate)
            {
                nh.SetAsTarget();
                offset = transform.position - nh.transform.position;
                follow = false;
            }
        }
    }
}
