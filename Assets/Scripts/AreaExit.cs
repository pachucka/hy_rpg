using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AreaExit : MonoBehaviour
{
    public string areaToLoad;

    public string areaTransitionName;

    public AreaEntrance entrance;

    private void Start()
    {
        entrance.transitionName = PlayerController.instance.areaTransitionName;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            SceneManager.LoadScene(areaToLoad);
            PlayerController.instance.areaTransitionName = areaTransitionName;
        }
    }
}
