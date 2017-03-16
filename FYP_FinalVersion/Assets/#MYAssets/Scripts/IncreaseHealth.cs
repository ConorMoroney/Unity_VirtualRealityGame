using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class IncreaseHealth : MonoBehaviour {

    public int Amount;
    private PlayerHealth health;
    private GameObject player;
    private Collectable _object;

    // Use this for initialization
    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
        health = player.GetComponent<PlayerHealth>();
        _object = gameObject.GetComponent<Collectable>();
        _object.setCollectablilty(false);
    }
    void Update()
    {
        if (health.currentHealth < 100)
            _object.setCollectablilty(true);
        else
            _object.setCollectablilty(false);
    }

    // Update is called once per frame
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == player)
        {
            if (health.currentHealth < 100 && _object.collect) 
            health.increaseHealth(Amount);
        }
    }
}
