using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameMenuManager : MonoBehaviour
{
    public GameObject menu, chatBox;
    public GameObject[] windows;
    public Text health;

    public ItemButton[] itemsBtns;

    public GameObject itemWindow;

    // Update is called once per frame
    void Update()
    {
        updateStats();

        if (!menu.activeInHierarchy && !chatBox.activeInHierarchy)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Debug.Log("escape clicked");
                InventoryManager.instance.listItems();
                menu.SetActive(true);
                GameManager.instance.menuOpen = true;
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                InventoryManager.instance.listItems();
                menu.SetActive(true);
                GameManager.instance.menuOpen = true;
                ActivateItemWindow();
            }
        } else if (menu.activeInHierarchy)
        {
            
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                closeMenu();
            }
        }
    }
    private void ActivateItemWindow()
    {
        itemWindow.SetActive(!itemWindow.activeInHierarchy);
    }
    public void changeWindow(int windowNum)
    {
        Debug.Log("change window");
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

        DialogueActivator.instance.canActivate = true;

        // Zamykanie wszystkich okienek menu, które by³y otwarte przed klikniêciem przycisku "CLOSE"
        for (int i = 0; i < windows.Length; i++)
        {
            windows[i].SetActive(false);
        }
    }

    public void updateStats()
    {
        health.text = $"Health: {PlayerController.instance.health.ToString()}";
    }

    
}
