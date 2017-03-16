     using UnityEngine;
     using System.Collections;
     
    public class Orbit : MonoBehaviour {
    public float damping = 1;
    public float turnSpeed = 4.0f;
    public Transform player;
    private Vector3 offset;
     
    void Start ()
    { 
     offset = transform.position - player.transform.position;
    }
    
    void LateUpdate()
    {

        offset = Quaternion.AngleAxis(Input.GetAxis("Mouse ScrollWheel") * turnSpeed, Vector3.up) * offset;
        Vector3 desiredPosition = player.transform.position + offset;
        transform.position = Vector3.MoveTowards(transform.position, desiredPosition, Time.deltaTime * damping);
        

    }

 
}