using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaEntrance : MonoBehaviour
{
    public string transitionName;

    private void Start()
    {
        if (PlayerController.instance != null)
        {
            if (transitionName == PlayerController.instance.areaTransitionName)
            {
                PlayerController.instance.transform.position = transform.position;
                UIFade.instance.FadeFromBlack();
            }
        }

        if (UIFade.instance != null)
        {
            UIFade.instance.FadeFromBlack();
        }
    }
}
