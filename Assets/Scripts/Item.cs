using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    // What kind of item is it
    public bool isItem, isWeapon;
    public string itemName, itemDescription;
    public int price;
    // Sprite of the item
    public Sprite itemSprite;
    // How much health / strength does it give
    public int amountToChange;
    // What it gives (HP, strength)
    public bool affectHP, affectStrength;
    // current weapon strength
    public int weaponStrength;


}
