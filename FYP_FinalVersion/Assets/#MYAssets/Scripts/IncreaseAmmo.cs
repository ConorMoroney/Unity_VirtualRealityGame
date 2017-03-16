using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseAmmo : MonoBehaviour {

    public int Amount = 10;
    private playerAmmo Ammo;
    private GameObject player;
    private Collectable _object;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Ammo = player.GetComponent<playerAmmo>();
        _object = gameObject.GetComponent<Collectable>();
        _object.setCollectablilty(false);
    }
    void Update()
    {
        if (Ammo.currentAmmo < 100)
        {
            _object.setCollectablilty(true);
        }
        else
            _object.setCollectablilty(false);
    }

    // Update is called once per frame
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == player && _object.collect)
        {
            if (Ammo.currentAmmo < 100 )
               Ammo.AddAmmo(Amount);
        }
    }

}