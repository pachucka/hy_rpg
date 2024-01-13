using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool menuOpen, dialogueActive;


    private void Start()
    {
        instance = this;
    }

    // controlling if Player can move
    private void Update()
    {
        if(menuOpen || dialogueActive)
        {
            PlayerController.instance.canMove = false;
        }
        else
        {
            PlayerController.instance.canMove = true;
        }
    }
}
