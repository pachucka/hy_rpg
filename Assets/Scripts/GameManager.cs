using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public List<Item> Items = new List<Item>();

    public bool menuOpen, dialogueActive;


    public Transform ItemContent;
    public GameObject InventoryItem;

    private void Start()
    {
        instance = this;
    }

    // controlling if Player can move
    private void Update()
    {
        if(menuOpen || dialogueActive)
        {
            PlayerController.instance.canMove = false;
        }
        else
        {
            PlayerController.instance.canMove = true;
        }
    }

    public void addItem(Item item)
    {
        Items.Add(item);
    }

    public void removeItem(Item item)
    {
        Items.Remove(item);
    }

    public void listItems()
    {
        foreach (var item in Items)
        {
            GameObject obj = Instantiate(InventoryItem, ItemContent);
            var itemName = obj.transform.Find("Item/ItemName").GetComponent<Text>();
            var itemIcon = obj.transform.Find("Item/ItemName").GetComponent<Image>();

            itemName.text = item.ItemName;
            itemIcon.sprite = item.icon;
        }
    }
}
