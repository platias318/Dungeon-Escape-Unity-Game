using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DoorUnlock : MonoBehaviour
{

    [SerializeField] private GameObject door;
    private bool hasPlayed = false;
    // Start is called before the first frame update
    void Start()
    {
        if (door.GetComponent<Door>().enabled == true)
        {
            door.GetComponent<Door>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (InventoryManager.Instance.getItem("Key")!=null)
        {
            Debug.Log("key exists in player");
            gameObject.GetComponent<Door>().enabled = true;
            enabled = false;
        }

    }
}
