using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;


public class MenuScript : MonoBehaviour {

    public MenuDialog _menu;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Menu Active");
            _menu.SetActive(true);
        }
	}
}
