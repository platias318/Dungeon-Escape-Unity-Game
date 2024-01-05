using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item/Create New Item")]       //enable the option to create "Item" game objects 

public class Item : ScriptableObject
{
    [SerializeField] public  GameObject prefab;         //the game object
    public int id;                                      //id (not used)
    public string itemName;                             //item name
    public Sprite icon;                                 //icon showed in inventory canvas
}
