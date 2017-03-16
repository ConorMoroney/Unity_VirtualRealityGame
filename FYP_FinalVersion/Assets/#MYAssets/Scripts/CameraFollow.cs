using UnityEngine;
using System.Collections;
using UnityEngine.VR;

public class CameraFollow : MonoBehaviour
{

   
    public float damping = 1;
    public float turnSpeed = 4.0f;
    public float LookSpeed = 10f;
    private Vector3 currentOffset;
    private Vector3 offset;
    private GameObject player;
    private Animator anim;
    private bool speedup = false;
    void Start()
    {
        
        player = GameObject.FindGameObjectWithTag("Player");
        anim = player.GetComponent<Animator>();
        offset = transform.position - player.transform.position;
     
    }
  


    private void Update()
    {
        if (!VRSettings.enabled)
        {
            Quaternion targetRotation = Quaternion.LookRotation(player.transform.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * damping * LookSpeed);
        }
    }
    
    void LateUpdate()
    {

        offset = Quaternion.AngleAxis(Input.GetAxis("Mouse ScrollWheel") * turnSpeed, Vector3.up) * offset;
        Vector3 desiredPosition = player.transform.position + offset;
        Vector3 position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * damping);
        transform.position = position;
    }
   
}

   