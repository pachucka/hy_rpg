using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool menuOpen, dialogueActive;
    [SerializeField] Animator transitionAnim;

    private bool isPlayerLoaded = false;


    private void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Scene loaded: " + scene.name);
        if (scene.buildIndex > 0 && scene.name != "Start" && !isPlayerLoaded) // Ignoruj za�adowanie menu
        {
            Debug.Log("Loading player...");
            LoadPlayerDelayed();
        }
    }




    private void Update()
    {
        // Kontrola ruchu gracza
        if (PlayerController.instance != null)
        {
            if (menuOpen || dialogueActive)
            {
                PlayerController.instance.canMove = false;
            }
            else
            {
                PlayerController.instance.canMove = true;
            }
        }
    }

    public void NextLevel()
    {
        Debug.Log("Next Level button pressed");

        StartCoroutine(LoadLevel());
    }

    IEnumerator LoadLevel()
    {
        transitionAnim.SetTrigger("End");
        menuOpen = true;
        yield return new WaitForSeconds(1);

        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        Debug.Log("Loading next scene index: " + nextSceneIndex);

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(nextSceneIndex);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        transitionAnim.SetTrigger("Start");
        menuOpen = false;
    }


    public void BackToMenu()
    {
        SceneManager.LoadSceneAsync(0);
    }

    public void SavePlayer()
    {
        SaveSystem.SavePlayer(PlayerController.instance, InventoryManager.instance);
    }

    public void LoadPlayerDelayed()
    {
        PlayerData data = SaveSystem.LoadPlayer();
        if (data != null)
        {
            StartCoroutine(LoadPlayerCoroutine(data.scene));
        }
        else
        {
            // Je�li brak danych gracza, zacznij now� gr�
            StartCoroutine(StartNewGameCoroutine());
        }
    }

    IEnumerator LoadPlayerCoroutine(string sceneName)
    {
        Debug.Log("Load Player Coroutine for scene: " + sceneName);

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        LoadPlayer();
        isPlayerLoaded = true;
    }

    IEnumerator StartNewGameCoroutine()
    {
        // Tu mo�esz doda� dowolne dodatkowe operacje zwi�zane z rozpocz�ciem nowej gry
        // Na przyk�ad wyczyszczenie ekwipunku, ustawienie domy�lnych warto�ci itp.

        // W tym przypadku, zak�adam, �e InventoryManager posiada metod� do wyczyszczenia ekwipunku
        //InventoryManager.instance.ClearInventory();

        // Poczekaj chwil� przed �adowaniem nowej sceny
        yield return new WaitForSeconds(1);

        // �aduj pierwsz� scen� gry (lub inn�, je�li wymaga to twoja logika gry)
        SceneManager.LoadScene("Story1");
    }


    public void LoadPlayer()
    {
        if (PlayerController.instance != null)
        {
            PlayerData data = SaveSystem.LoadPlayer();

            PlayerController.instance.lvl = data.level;
            PlayerController.instance.health = data.health;
            PlayerController.instance.xp = data.xp;

            Vector3 position = new Vector3(data.position[0], data.position[1], data.position[2]);
            PlayerController.instance.transform.position = position;

            for (int i = 0; i < data.items.Length; i++)
            {
                InventoryManager.instance.AddItemByID(data.items[i]);
            }
        }
        else
        {
            Debug.LogError("PlayerController.instance is null.");
        }
    }


    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
