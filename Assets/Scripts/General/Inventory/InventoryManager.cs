using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;      //singleton
    public List<Item> Items = new List<Item>();     //list of items
    [SerializeField] private AudioSource audioEffect;

    public Transform ItemContent;        //variables that are needed in order not to duplicate already existing items
    public GameObject InventoryItem;

    private void Awake()
    {
        Instance = this; 
    }

    public void Add(Item item)           //method to add items in the item list
    {
        if (audioEffect != null)
        {
            audioEffect.Play();
        }
            Items.Add(item);
    }

    public void Remove(Item item)         //method to remove items from the item list
    { 
        Items.Remove(item);
        Debug.Log("calling list items from remove");
        ListItems();
    }

    public Item getItem(string ItemName)         //get the item with ItemName
    {
        foreach(Item item in Items)
        {
            if(item.name == ItemName)
            {
                return item;
            }
        }
        return null;
    }
    public void UseItem(Item item)              //use the item
    {
        //Remove the item from the inventory
        Remove(item);

        //update the UI
        ListItems();
    }

    public void DropItem(Item item , GameObject dropPosition)      //drop an item in a specific position 
    {
        //instantiate a new item at the drop position
        Instantiate(item.prefab, dropPosition.transform.position, Quaternion.identity);

        //remove the item from the inventory
        Remove(item);

        //update the UI
        ListItems();
    }

    public void ListItems()     //list the items of the inventory (update the UI)
    {
        foreach (Transform item in ItemContent)      //destroy each item in the UI, so we do not duplicate them when we reput them
        {
            Destroy(item.gameObject);
        }    

        foreach (var item in Items)               //show every item in the item list to the user (UI)
        {
            GameObject obj = Instantiate(InventoryItem, ItemContent);
            var itemName = obj.transform.Find("ItemName").GetComponent<TMPro.TextMeshProUGUI>();        //get its text
            var itemIcon = obj.transform.Find("ItemIcon").GetComponent<UnityEngine.UI.Image>();         //get its icon
            Debug.Log(itemName);
            itemName.text = item.itemName;
            itemIcon.sprite = item.icon;
        }
    }
}
