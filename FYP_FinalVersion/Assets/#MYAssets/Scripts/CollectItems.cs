using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectItems : MonoBehaviour
{


    // Script to collect Ammo of the ground 

    GameObject bullet;
    bool Attract = false;
    public float gravity = -10;
    public void Collect(Transform other)
    {
        Vector3 gravityUp = (other.position - transform.position).normalized;
        Vector3 localUp = other.up;
        other.GetComponent<Rigidbody>().AddForce(gravityUp * gravity);
        Quaternion targetRotation = Quaternion.FromToRotation(localUp, gravityUp) * other.rotation;
        other.rotation = Quaternion.Slerp(other.rotation, targetRotation, 50 * Time.deltaTime);
    }
}
