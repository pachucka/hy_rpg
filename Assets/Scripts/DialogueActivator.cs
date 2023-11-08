using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueActivator : MonoBehaviour
{
    public string[] lines;
    private bool canActivate;

    private void Update()
    {
        if(canActivate && Input.GetButtonDown("Fire1") && !DialogueManager.instance.dialogueBox.activeInHierarchy)
        {
            DialogueManager.instance.showDialogue(lines);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            canActivate = true;
        }
    }

    private void OnTriggerExitr2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            canActivate = false;
        }
    }
}
