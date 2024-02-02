using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueActivator : MonoBehaviour
{
    public string[] lines;
    public bool canActivate = false;
    public static DialogueActivator instance;
    public bool seenDialogue = false;

    private void Start()
    {
        instance = this;
    }

    private void Update()
    {
        if(canActivate && Input.GetButtonDown("Fire1") && !DialogueManager.instance.dialogueBox.activeInHierarchy && !GameManager.instance.menuOpen && !seenDialogue)
        {
            DialogueManager.instance.showDialogue(lines);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player" && !seenDialogue)
        {
            canActivate = true;

            if(this.tag == "AutoDialogue")
            {
                DialogueManager.instance.showDialogue(lines);
                seenDialogue = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            canActivate = false;
        }
    }
}
