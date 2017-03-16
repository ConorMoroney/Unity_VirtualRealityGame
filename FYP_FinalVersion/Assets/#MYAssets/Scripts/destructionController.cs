using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destructionController : MonoBehaviour {
    public GameObject Remains;
    private bool Hit = false;
	void Update () {
        if (Hit)
        {
            Instantiate(Remains, transform.position, transform.rotation);
            Destroy(gameObject);
        }
	}
    public void shoot()
    {
        Hit = true;
    }
}
