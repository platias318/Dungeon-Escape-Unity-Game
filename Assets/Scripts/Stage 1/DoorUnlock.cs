using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DoorUnlock : MonoBehaviour
{

    [SerializeField] private GameObject door; //current door we want to open
    private bool hasPlayed = false;
    // Start is called before the first frame update
    void Start()
    {
        if (door.GetComponent<Door>().enabled == true) //disable the <Door> script of the door so that he cant open it when he shouldn't
        {
            door.GetComponent<Door>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (InventoryManager.Instance.getItem("Key")!=null) // If the user has the key, the door automatically eneblas the <Door> script, so now the door can be opened
        {
            Debug.Log("key exists in player");
            gameObject.GetComponent<Door>().enabled = true;
            enabled = false;
        }

    }
}
