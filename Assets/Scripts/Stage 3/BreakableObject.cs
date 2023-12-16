using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableObject : MonoBehaviour
{
    [SerializeField] GameObject brokenPrefab; // Reference to the broken object prefab
    [SerializeField] float lowerOffset = 1.3f; // Adjust the offset for the lower position of the broken object

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the collision is from a specific object (you can customize this condition)
        if (collision.gameObject.CompareTag("Player"))
        {

            // Spawn the broken object prefab at the same position and rotation
            Vector3 lowerPosition = transform.position - new Vector3(0f, lowerOffset, 0f);
            Quaternion newR = transform.rotation;
            Instantiate(brokenPrefab, transform.position, Quaternion.Euler(270.0f, 0f, 0f));

            // Destroy the current breakable object
            Destroy(gameObject);
            
        }
    }
}

