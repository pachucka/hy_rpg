using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuButtonsController : MonoBehaviour
{
    public void NewGame()
    {
        GameManager.instance.NextLevel();
    }

    public void LoadGame()
    {
        GameManager.instance.LoadPlayerDelayed();
    }

    public void QuitGame()
    {
        GameManager.instance.Quit();
    }
}
