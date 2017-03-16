using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class EnemyGunfire : MonoBehaviour {

    public int gunDamage = 10;
    public float range = 50f;
    public Transform Arm;
    private GameObject Player;
    private WaitForSeconds shotsDuration = new WaitForSeconds(.07f);
    private AudioSource gunaudio;
    private LineRenderer laserLine;
    
    private GameObject _object;
    private bool point = false;

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        _object = transform.root.gameObject;
        laserLine = GetComponent<LineRenderer>();
        gunaudio = GetComponent<AudioSource>();
    }
    private void Update()
    {
        if(point)
            Arm.transform.rotation = Quaternion.LookRotation(Arm.transform.position - Player.transform.position);
    }



    public void FireWeapon()
    {
   
        Vector3 face = Player.transform.position - transform.position;
        face.y = 0;
        Quaternion dir = Quaternion.LookRotation(face);
        transform.rotation = Quaternion.Slerp(transform.rotation, dir, Time.time * 2f);
        
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, range))
        {
            StartCoroutine(ShotEffect());
            laserLine.SetPosition(0, transform.position);
            laserLine.SetPosition(1, hit.point);
            PlayerHealth pHealth = hit.collider.GetComponent<PlayerHealth>();
            if (pHealth != null)
            {
                pHealth.TakeDamage(gunDamage);
            }
        }
        else
        {
            laserLine.SetPosition(1, transform.position + (transform.forward * range));
        }
      
    }

   

    private IEnumerator ShotEffect()
    {
        gunaudio.Play();
        laserLine.enabled = true;
        yield return shotsDuration;
        laserLine.enabled = false;
    }
}
