using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLoader : MonoBehaviour
{
    public GameObject player;

    private void Awake()
    {
        if (PlayerController.instance == null)
        {
            Instantiate(player);
        }
    }
}
