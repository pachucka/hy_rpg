using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool menuOpen, dialogueActive;
    [SerializeField] Animator transitionAnim;
    public PlayerData data;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
            Debug.Log("GameManager initialized.");
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Scene loaded: " + scene.name);

        StartCoroutine(OnSceneLoadedCoroutine(scene, mode));
    }

    private IEnumerator OnSceneLoadedCoroutine(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Coroutine started");

        while (PlayerController.instance == null || InventoryManager.instance == null)
        {
            Debug.Log("Waiting for PlayerController and InventoryManager...");
            yield return null;
        }

        data = new PlayerData(PlayerController.instance, InventoryManager.instance);
        data.scene = SceneManager.GetActiveScene().name;
        Debug.Log("Game saved");
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

    public void LoadLastLevel()
    {
        StartCoroutine(LoadLastLevelCoroutine());
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
        if (SceneManager.GetActiveScene().name == "Start")
        {
            InventoryManager.instance.cleanInventory();
        }
    }

    IEnumerator LoadLastLevelCoroutine()
    {
        transitionAnim.SetTrigger("End");
        menuOpen = true;
        yield return new WaitForSeconds(1);

        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 2;
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
        Debug.Log("Click");
        SaveSystem.SavePlayer(PlayerController.instance, InventoryManager.instance);
    }

    public void LoadPlayerDelayed()
    {
        PlayerData data = SaveSystem.LoadPlayer();
        if (data != null)
        {
            StartCoroutine(LoadPlayerCoroutine(data.scene));
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
       // isPlayerLoaded = true;
    }


    public void LoadPlayer()
    {
        Debug.Log("Loading player data...");
        if (PlayerController.instance != null)
        {
            PlayerData loadedData = SaveSystem.LoadPlayer();

            if (loadedData != null)
            {
                PlayerController.instance.lvl = loadedData.level;
                PlayerController.instance.health = loadedData.health;
                PlayerController.instance.xp = loadedData.xp;

                Vector3 position = new Vector3(loadedData.position[0], loadedData.position[1], loadedData.position[2]);
                PlayerController.instance.transform.position = position;

                foreach (var itemID in loadedData.items)
                {
                    Debug.Log("Adding item with ID: " + itemID);
                    InventoryManager.instance.AddItemByID(itemID);
                }
            }
            else
            {
                Debug.LogError("Failed to load player data.");
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
