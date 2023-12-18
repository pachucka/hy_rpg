using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameMenuManager : MonoBehaviour
{
    private bool canActivateDialogue;

    public GameObject menu, chatBox;
    public GameObject[] windows;
    public Text health;

    public ItemButton[] itemsBtns;

    // Update is called once per frame
    void Update()
    {
        updateStats();

        if (!menu.activeInHierarchy && !chatBox.activeInHierarchy)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                InventoryManager.instance.listItems();
                canActivateDialogue = DialogueActivator.instance.canActivate;
                menu.SetActive(true);
                GameManager.instance.menuOpen = true;
                DialogueActivator.instance.canActivate = false;
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                InventoryManager.instance.listItems();
                canActivateDialogue = DialogueActivator.instance.canActivate;
                menu.SetActive(true);
                windows[0].SetActive(!windows[0].activeInHierarchy);
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
        health.text = $"Health: {PlayerController.instance.health.ToString()}";
    }

    
}
