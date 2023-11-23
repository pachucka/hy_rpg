using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text dialogueText, nameText;
    public GameObject dialogueBox, nameBox;

    private string[] dialogueLines;
    private int currentLine;
    private bool justStarted;

    private bool isTyping;
    private Coroutine typingCoroutine;

    public static DialogueManager instance;

    private void Start()
    {
        instance = this;
    }

    private void Update()
    {
        if (dialogueBox.activeInHierarchy)
        {
            if (Input.GetButtonUp("Fire1"))
            {
                if (!justStarted)
                {
                    if (isTyping)
                    {
                        CompleteTyping();
                    }
                    else
                    {
                        currentLine++;

                        if (currentLine >= dialogueLines.Length)
                        {
                            dialogueBox.SetActive(false);
                            //PlayerController.instance.canMove = true;
                            GameManager.instance.dialogueActive = false;
                        }
                        else
                        {
                            checkName();
                            StartTyping(dialogueLines[currentLine]);
                        }
                    }
                }
                else
                {
                    justStarted = false;
                }
            }
        }
    }

    public void showDialogue(string[] newLines)
    {
        dialogueLines = newLines;
        currentLine = 0;

        checkName();

        dialogueText.text = "";
        dialogueBox.SetActive(true);
        justStarted = true;
        //PlayerController.instance.canMove = false;
        GameManager.instance.dialogueActive = true;

        StartTyping(dialogueLines[currentLine]);
    }

    public void checkName()
    {
        if (dialogueLines[currentLine].StartsWith("n-"))
        {
            nameText.text = dialogueLines[currentLine].Replace("n-", "");
            currentLine++;
        }
    }

    private void StartTyping(string text)
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }
        typingCoroutine = StartCoroutine(TypeText(text));
    }

    private void CompleteTyping()
    {
        StopCoroutine(typingCoroutine);
        dialogueText.text = dialogueLines[currentLine];
        isTyping = false;
    }

    private IEnumerator TypeText(string text)
    {
        isTyping = true;
        dialogueText.text = "";

        foreach (char letter in text)
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.03f);
        }

        isTyping = false;
    }
}
