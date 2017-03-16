using UnityEngine;
using System.Collections;

public class fireBullet : MonoBehaviour {

    public float bulletSpeed = 1000.0f;
    public float shotInterval = 0.5f;
    public Rigidbody bulletPrefab;
    private float shootTime = 0.5f;
    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButton("Fire1"))
        {
            if (Time.time >= shootTime)
            {
                Rigidbody bullet = Instantiate(bulletPrefab, transform.position, transform.rotation) as Rigidbody;
                bullet.GetComponent<Rigidbody>().AddForce(transform.forward * bulletSpeed);
                shootTime = Time.time + shotInterval;

          
            }
          
        }
    }
}
