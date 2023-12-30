using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BreakableObject : MonoBehaviour
{
    [SerializeField] GameObject brokenPrefab; // Reference to the broken object prefab
    [SerializeField] float lowerOffset = 1.3f; // Adjust the offset for the lower position of the broken object
    [SerializeField] private Canvas canvasObj;
    Item hammer;
    private bool isEPressed = false;


    void Start()
    {
        canvasObj.enabled = false; 
    }

    void Update()
    {
        isEPressed = Input.GetKey(KeyCode.E);
    }

    private void OnTriggerEnter(Collider other)
    {
        ShowCanvas(); 
    }

    private void OnTriggerStay(Collider other)
    {
        hammer = InventoryManager.Instance.getItem("Hammer");
        if (hammer != null && isEPressed)
        {
            HideCanvas();
            // Spawn the broken object prefab at the same position and rotation
            Vector3 lowerPosition = transform.position - new Vector3(0f, lowerOffset, 0f);
            Quaternion newR = transform.rotation;
            Instantiate(brokenPrefab, transform.position, Quaternion.Euler(270.0f, 0f, 0f));
            // Assume "gameObject" is your GameObject
            MeshRenderer meshRenderer = brokenPrefab.GetComponent<MeshRenderer>();
            meshRenderer.enabled = true;

            // Destroy the current breakable object
            Destroy(gameObject);
            Destroy(brokenPrefab);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        HideCanvas();
    }

    private void ShowCanvas()
    {
        if (!canvasObj.enabled)
        {
            canvasObj.enabled = true;
        }
    }

    private void HideCanvas()
    {
        if (canvasObj.enabled)
        {
            canvasObj.enabled = false;
        }
    }
}