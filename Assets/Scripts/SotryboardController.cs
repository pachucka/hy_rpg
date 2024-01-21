using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SotryboardController : MonoBehaviour
{
    public TextMeshProUGUI textToChange;
    public string[] texts;
    private int currentIndex = 0;

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
            StartCoroutine(TypeText(texts[currentIndex]));
        }
    }

    IEnumerator TypeText(string text)
    {
        textToChange.text = "";
        foreach (char letter in text)
        {
            textToChange.text += letter;
            yield return new WaitForSeconds(0.03f);
        }
    }

    void LoadNextScene()
    {
         SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
