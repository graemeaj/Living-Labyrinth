﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class KeyScript : MonoBehaviour {
	
	GameObject inventory;
	InventoryScript itemScript;
	public int keyNumber;
	bool nearKey = false;
	bool keyObtained;

    Canvas canvas;

    public AudioClip keysound;
	
	// Use this for initialization
	void Start () {
		inventory = GameObject.Find("KeyInventory");
		itemScript = inventory.GetComponent<InventoryScript>();

        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
    }
	
	// Update is called once per frame
	void Update () {
		if (nearKey == true && itemScript.hasKey(keyNumber) == false && Input.GetKeyDown(KeyCode.F)) {
            GetComponent<AudioSource>().PlayOneShot(keysound, 0.8f);
            itemScript.obtainKey(keyNumber);
            GetComponentInChildren<Renderer>().enabled = false;
            this.enabled = false;

			//gameObject.SetActive(false);

        } 
		else if (itemScript.hasKey(keyNumber) == true) {
			Debug.LogError ("Key already obtained");
		}

        if (nearKey)
        {
            canvas.GetComponentInChildren<Text>().text = "Press 'F' to take key";
            //GUI.Label(new Rect(Screen.width / 2 - 75, Screen.height - 100, 170, 30), "Press 'F' to obtain key");
        }
    }

    void lateUpdate()
    {
        
    }
	
	void OnTriggerEnter (Collider other) {
		if (other.CompareTag("Player")){
			//Debug.Log ("Player Near key #" + (keyNumber+1));
			nearKey = true;
        }
	}
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            nearKey = false;

        }
    }

    /*void OnGUI()
    {
        if (nearKey)
        {
            GUI.Label(new Rect(Screen.width / 2 - 75, Screen.height - 100, 170, 30), "Press 'F' to obtain key");
        } 
    }*/
}
