using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EssentialsLoader : MonoBehaviour
{
    public GameObject gameManager;
    public GameObject player;
    public GameObject uiCanvas;

    private void Awake()
    {
        if(GameManager.instance == null)
        {
            Instantiate(gameManager);
        }
        if (PlayerController.instance == null)
        {
            Instantiate(player);
        }
        if (InventoryManager.instance == null)
        {
            Instantiate(uiCanvas);
        }

    }
}
