using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class EssentialsLoader : MonoBehaviour
{
    public GameObject gameManager;
    public GameObject player;
    public GameObject uiCanvas;
    public GameObject audioManager;

    private void Awake()
    {
        if(GameManager.instance == null && SceneManager.GetActiveScene().name != "Menu")
        {
            Instantiate(gameManager);
        }
        if (PlayerController.instance == null && SceneManager.GetActiveScene().name != "Menu")
        {
            Instantiate(player);
        }
        if (InGameMenuManager.instance == null && SceneManager.GetActiveScene().name != "Menu")
        {
            Instantiate(uiCanvas);
        }
        if(AudioManager.instance == null)
        {
            Instantiate(audioManager);
        }
        if (FindObjectOfType<EventSystem>() == null)
        {
            // Jeœli nie ma, stwórz EventSystem
            GameObject eventSystem = new GameObject("EventSystem");
            eventSystem.AddComponent<EventSystem>();
            eventSystem.AddComponent<StandaloneInputModule>();
        }


    }
}
