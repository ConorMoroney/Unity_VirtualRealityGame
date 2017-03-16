using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;


public class Collectable : MonoBehaviour {

    //Scirpt for Object TO BE Attracted to the player

    public bool collect = false;
    Light _light;
    AudioSource Audio;
    private CollectItems CA;
    private GameObject player;
    private PlayerHealth health;
    private Rigidbody _rigidbody;
    private Collider _collider;
    
	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        health = player.GetComponent<PlayerHealth>();
        CA = player.GetComponent<CollectItems>();
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
        _light = GetComponent<Light>();
        Audio = GetComponent<AudioSource>();

        //StartCoroutine(Wait());
    }
	
    public IEnumerator Wait()
    {
      yield return new WaitForSeconds(3f);
      collect = true;
    }
  
	// Update is called once per frame
	void Update () {
        if (collect && player != null)
        {
            _rigidbody.useGravity = false;
            CA.Collect(transform);
        }
        else
        {
            _rigidbody.useGravity = true;
        }
       // if (transform.position.y < 1)
       //     Destroy(gameObject);

        //if player dies all object are uncollectable
        if (health.isDead)
            collect = false;
    }
    public void setCollectablilty(bool v)
    {
        collect = v;
    }
    private void OnCollisionEnter(Collision collision)
    {
        //if you collide with a collectable object 
        if(collision.gameObject == player&&collect)
        {
            _collider.enabled = false;
            Audio.Play();
            _light.enabled = true;
            Destroy(gameObject,0.1f);
        }
    }
}
