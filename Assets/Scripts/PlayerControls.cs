﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public GameObject thisPlayer; //variable to store GameObject
    public Transform tf; // A variable to hold our Transform component
    public Rigidbody2D rb; //Rigidbody2D var
    public float thrust; //to hold movement speed
    public float turnThrust; //to hold rotation speed
    private float thrustInput; //to set thrust inputs
    private float turnInput; //to set turn input
    

    public float screenTop; //hold screen boundary +y
    public float screenBottom; //hold screen boundary -y
    public float screenRight; //hold screen boundary x
    public float screenLeft; //hold screen boundary -x

    public float laserForce; //laser speed
    public GameObject laser; //var for laser

    public float terminalForce;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
      
    }

    // Update is called once per frame
    void Update()
    {
        

        // shift "dodge" function, it really just makes you teleport but I'll leave it for now
        if (Input.GetKey("left shift") | Input.GetKey("right shift"))
        {
            if (Input.GetKeyDown("w") | Input.GetKeyDown("up"))
            {
                Vector3 myVector = new Vector3(0, 1, 0); // create vector to add
                myVector = myVector.normalized; // You could also call the function myVector.Normalize();
                tf.position += (myVector * thrust); // change position and add magnitude
            }

            if (Input.GetKeyDown("a") | Input.GetKeyDown("left"))
            {
                Vector3 myVector = new Vector3(-1, 0, 0); // create vector to add
                myVector = myVector.normalized; // You could also call the function myVector.Normalize();
                tf.position += (myVector * thrust); // change position and add magnitude
            }

            if (Input.GetKeyDown("s") | Input.GetKeyDown("down"))
            {
                Vector3 myVector = new Vector3(0, -1, 0); // create vector to add
                myVector = myVector.normalized; // You could also call the function myVector.Normalize();
                tf.position += (myVector * thrust); // change position and add magnitude
            }

            if (Input.GetKeyDown("d") | Input.GetKeyDown("right"))
            {
                Vector3 myVector = new Vector3(1, 0, 0); // create vector to add
                myVector = myVector.normalized; // You could also call the function myVector.Normalize();
                tf.position += (myVector * thrust); // change position and add magnitude
            }
        }

        //check for keyboard input
        else
        {
            thrustInput = Input.GetAxis("Vertical");
            turnInput = Input.GetAxis("Horizontal");
            //transform.Rotate(Vector3.forward * turnInput * Time.deltaTime * turnThrust); //better turn movement than adding force
        }


        //check for fire input and make bullets
        if (Input.GetButtonDown("Fire1")) 
        {
            GameObject newLaser = Instantiate(laser, transform.position, transform.rotation);
            newLaser.GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.up * laserForce);
            newLaser.GetComponent<Transform>();
            Destroy(newLaser, 2.0f);
        }

        //screen wrapping
        Vector2 newPos = transform.position;
        if (transform.position.y > screenTop) 
        {
            newPos.y = screenBottom;
        }
        if (transform.position.y < screenBottom) 
        {
            newPos.y = screenTop;
        }
        if (transform.position.x > screenRight)
        {
            newPos.x = screenLeft;
        }
        if (transform.position.x < screenLeft)
        {
            newPos.x = screenRight;
        }
        
        //set player transform to new Pos
        transform.position = newPos;

        //Return player to (0, 0, 0)
        if (Input.GetKeyDown("u"))
        {
            tf.position = new Vector3(0, 0, 0);
        }

        //exit application with escape key
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        //make thisShip inactive
        if (Input.GetKeyDown("q"))
        {
            thisPlayer.SetActive(false);
        }
    }

    //update on a fixed time interval for consistency
    private void FixedUpdate()
    {
        //apply thrust
        rb.AddRelativeForce(Vector2.up * thrustInput * thrust);
        rb.AddTorque(turnInput * turnThrust);
    }

    //Player collision function
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.relativeVelocity.magnitude);
        if (collision.relativeVelocity.magnitude > terminalForce) 
        {
            Debug.Log("You died!");
        }
    }
}
