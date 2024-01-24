using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void NextLevel()
    {
        // Upewnij siê, ¿e PlayerController.instance nie jest null
        if (PlayerController.instance != null)
        {
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
            PlayerController.instance.resetStats();
        }
        else
        {
            Debug.LogError("PlayerController.instance is null.");
        }
    }
}
