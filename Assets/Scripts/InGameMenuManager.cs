using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameMenuManager : MonoBehaviour
{
    private bool canActivateDialogue;

    public GameObject menu, chatBox;
    public GameObject[] windows;

    // Update is called once per frame
    void Update()
    {
        if (!menu.activeInHierarchy && !chatBox.activeInHierarchy)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                canActivateDialogue = DialogueActivator.instance.canActivate;
                menu.SetActive(true);
                //PlayerController.instance.canMove = false;
                GameManager.instance.menuOpen = true;
                DialogueActivator.instance.canActivate = false;
            }
        } else if (menu.activeInHierarchy)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                closeMenu();
            }
        }
    }

    public void changeWindow(int windowNum)
    {
        for(int i = 0; i < windows.Length; i++)
        {
            if(i == windowNum)
            {
                windows[i].SetActive(!windows[i].activeInHierarchy);
            } else
            {
                windows[i].SetActive(false);
            }
        }
    }

    public void closeMenu()
    {
        menu.SetActive(false);
        //PlayerController.instance.canMove = true;
        GameManager.instance.menuOpen = false;
        if (canActivateDialogue)
        {
            DialogueActivator.instance.canActivate = true;
        }

        // closing all menu windows that were opened before the button CLOSE was clicked
        for(int i = 0; i < windows.Length; i++)
        {
            windows[i].SetActive(false);
        }
    }

    public void updateStats()
    {

    }
}
