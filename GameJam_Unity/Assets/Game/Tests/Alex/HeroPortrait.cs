using CCC.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroPortrait : MonoBehaviour {

	public void OnPortraitClicked()
    {
        Scenes.LoadAsync(DisplayBehavior.SCENE_NAME,UnityEngine.SceneManagement.LoadSceneMode.Additive);
    }
}
