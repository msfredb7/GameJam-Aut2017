using System.Collections;
using System.Collections.Generic;
using CCC.Manager;
using UnityEngine;

public class DeliveryNotification : MonoBehaviour
{
    public AudioClip sfx_appel;

    private bool isOnGoing;

	// Use this for initialization
	void Start ()
	{
	    isOnGoing = false;

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Notify()
    {
        if (!isOnGoing)
        {
            SoundManager.PlayStaticSFX(sfx_appel, 0.01f);

            isOnGoing = true;
            gameObject.SetActive(true);
            GetComponent<FadeFlash>().Play();
            DelayManager.LocalCallTo(delegate
            {
                GetComponent<FadeFlash>().Stop();
                gameObject.SetActive(false);
                isOnGoing = false;

            }, 3, this);
        }
        
    }
}
