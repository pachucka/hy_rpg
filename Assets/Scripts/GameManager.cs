using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool menuOpen, dialogueActive;
    [SerializeField] Animator transitionAnim;

    public Button newGame;


    private void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        //AssignFunctionToButton();
    }

    private void AssignFunctionToButton()
    {
        // ZnajdŸ przycisk w hierarchii sceny
        Button yourButton = GameObject.Find("newGame").GetComponent<Button>(); // Zmieñ "YourButtonName" na nazwê Twojego przycisku

        // SprawdŸ, czy znaleziono przycisk
        if (yourButton != null)
        {
            // Przypisz funkcjê do zdarzenia klikniêcia przycisku
            yourButton.onClick.AddListener(NextLevel);
        }
        else
        {
            Debug.LogWarning("Button not found in the scene with the name 'YourButtonName'");
        }
    }

    // controlling if Player can move
    private void Update()
    {
        if(PlayerController.instance != null)
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
        Debug.Log("Button clicked!");
        StartCoroutine(LoadLevel());
    }

    IEnumerator LoadLevel()
    {
        transitionAnim.SetTrigger("End");
        menuOpen = true;
        yield return new WaitForSeconds(1);
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        Debug.Log("Loading next scene index: " + nextSceneIndex);

        SceneManager.LoadSceneAsync(nextSceneIndex);
        transitionAnim.SetTrigger("Start");
        menuOpen = false;
    }

    public void BackToMenu()
    {
        SceneManager.LoadSceneAsync(0);
    }
}
