using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SotryboardController : MonoBehaviour
{
    public TextMeshProUGUI textToChange;
    public string[] texts;
    private int currentIndex = 0;
    private Coroutine typingCoroutine;

    void Start()
    {
        if (textToChange == null)
        {
            Debug.LogError("TextToChange is not assigned! Please assign a TextMeshProUGUI component.");
        }
        else
        {
            UpdateText();
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ChangeText();
        }
    }

    void ChangeText()
    {
        currentIndex++;

        if (currentIndex < texts.Length)
        {
            if (typingCoroutine != null)
            {
                StopCoroutine(typingCoroutine);
            }

            UpdateText();
        }
        else
        {
            LoadNextScene();
        }
    }

    void UpdateText()
    {
        if (textToChange != null)
        {
            typingCoroutine = StartCoroutine(TypeText(texts[currentIndex]));
        }
    }

    IEnumerator TypeText(string text)
    {
        // Clear the existing text
        textToChange.text = "";

        // Iterate through each character in the input text
        foreach (char letter in text)
        {
            // Append the current letter to the displayed text
            textToChange.text += letter;

            // Wait for a short duration to simulate typing speed
            yield return new WaitForSeconds(0.03f);
        }
    }


    void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
