using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class WaterScript : MonoBehaviour {

    private GameObject player;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject == player)
        {
            
            PlayerHealth pHealth = player.GetComponent<PlayerHealth>();
            if (pHealth.currentHealth >= 0)
                pHealth.Death();    
        }
    }
}
