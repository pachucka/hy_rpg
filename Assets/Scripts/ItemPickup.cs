using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Item item;
    private bool canPickup;

    void Pickup()
    {
        Debug.Log($"Picked up item: {item.ItemName}, ID: {item.id}");

        // Sprawdü, czy przedmiot ma ustawiony icon i ItemName przed dodaniem do listy
        if (item != null && item.icon != null && !string.IsNullOrEmpty(item.ItemName))
        {
            InventoryManager.instance.addItem(item);
            Destroy(gameObject);
        }
        else
        {
            Debug.LogWarning("Item is missing icon or ItemName. Unable to add to the inventory.");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (InventoryManager.instance.Items.Count < InventoryManager.instance.INVENTORY_CAPACITY)
        {
            Pickup();
        }
        
    }
}
