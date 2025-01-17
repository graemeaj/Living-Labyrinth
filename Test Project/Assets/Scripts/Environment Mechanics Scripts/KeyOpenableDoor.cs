﻿using UnityEngine;
using System.Collections;

public class KeyOpenableDoor : MonoBehaviour {

    public float smooth = 2;
    public float DoorOpenAngle = 90;
    public AudioClip openSound;
    public AudioClip closeSound;
    public float doorVolume = 0.2f;
	public int doorNumber;

    private bool open;
    private bool enter;

    private Vector3 defaultRot;
    private Vector3 openRot;

	// Use this for initialization
	void Start () {
        defaultRot = transform.eulerAngles;
        openRot = new Vector3(defaultRot.x, defaultRot.y + DoorOpenAngle, defaultRot.z);
    }
	
	// Update is called once per frame
	void Update () {
        if (open)
        {
            //Open door
            transform.eulerAngles = Vector3.Slerp(transform.eulerAngles, openRot, Time.deltaTime * smooth);
        }
        else
        {
            //Close door
            transform.eulerAngles = Vector3.Slerp(transform.eulerAngles, defaultRot, Time.deltaTime * smooth);
        }

        if (Input.GetKeyDown("f") && enter)
        {
            if(open == false)
            {
                GetComponent<AudioSource>().PlayOneShot(openSound, doorVolume);
            }
            else
            {
                GetComponent<AudioSource>().PlayOneShot(closeSound, doorVolume);
            }
            open = !open;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            enter = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            enter = false;
        }
    }

    void OnGUI()
    {
        if (enter == true && open == true)
        {
            GUI.Label(new Rect(Screen.width / 2 - 75, Screen.height - 100, 170, 30), "Press 'F' to close the door");
        }
        else if (enter == true && open != true)
        {
            GUI.Label(new Rect(Screen.width / 2 - 75, Screen.height - 100, 170, 30), "Press 'F' to open the door");
        }
    }
}