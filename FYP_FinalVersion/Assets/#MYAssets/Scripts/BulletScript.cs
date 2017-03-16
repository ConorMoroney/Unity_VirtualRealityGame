using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour {



    private Collectable _object;
    // Use this for initialization
    void Start () {
        _object = gameObject.GetComponent<Collectable>();
        StartCoroutine(_object.Wait());
    }

}
