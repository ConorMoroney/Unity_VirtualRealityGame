using UnityEngine;
using System.Collections;

public class RayViewer : MonoBehaviour {

    public float weaponRange = 50f;
    public Transform myTransform;
	void Update () {
        Vector3 lineOrigin = myTransform.position;
        Debug.DrawRay(lineOrigin, myTransform.forward * weaponRange, Color.green);
	}
}
