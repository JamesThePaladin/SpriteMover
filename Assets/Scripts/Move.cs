using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Move : MonoBehaviour
{
    
    private Transform tf; // A variable to hold our Transform component
    void Start()
    {
        // Get the Transform Component
        tf = GetComponent<Transform>();
    }
    void Update()
    {
        // Move up every frame draw by adding 1 to the y of our position
        tf.position = tf.position + (Vector3.up * 0.5f); // Vector3.up is a preset Vector of (0,1,0) multiply by half to get a viewable speed
        // There is also a Vector3.right (1,0,0) and Vector3.forward (0,0,1)
    }
}
