using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class popup : MonoBehaviour {

    private GameObject player;
    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        transform.position = player.transform.position +
       player.transform.TransformDirection(Vector3.forward);
    }
}
