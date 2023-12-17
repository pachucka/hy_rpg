using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Item", menuName = "Item/Create New Item")]
public class Item : ScriptableObject
{
    public int id;
    public string ItemName;
    public int value;
    public Sprite icon;
}
