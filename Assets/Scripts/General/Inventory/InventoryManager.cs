using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public List<Item> Items = new List<Item>();
    [SerializeField] private AudioSource audioEffect;

    public Transform ItemContent;
    public GameObject InventoryItem;

    private void Awake()
    {
        Instance = this; 
    }

    public void Add(Item item)
    {
        if (audioEffect != null)
        {
            audioEffect.Play();
        }
            Items.Add(item);
    }

    public void Remove(Item item) 
    { 
        Items.Remove(item);
        Debug.Log("calling list items from remove");
        ListItems();
    }

    public Item getItem(string ItemName)
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

    public void ListItems()
    {
        foreach (Transform item in ItemContent)
        {
            Destroy(item.gameObject);
        }    

        foreach (var item in Items)
        {
            GameObject obj = Instantiate(InventoryItem, ItemContent);
            var itemName = obj.transform.Find("ItemName").GetComponent<TMPro.TextMeshProUGUI>();
            var itemIcon = obj.transform.Find("ItemIcon").GetComponent<UnityEngine.UI.Image>();
            Debug.Log(itemName);
            itemName.text = item.itemName;
            itemIcon.sprite = item.icon;
        }
    }
}
