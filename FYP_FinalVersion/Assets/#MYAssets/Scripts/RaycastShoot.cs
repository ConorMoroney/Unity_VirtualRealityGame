using UnityEngine;
using System.Collections;

public class RaycastShoot : MonoBehaviour {

    public int gunDamage = 20;
    public float fireRate = 0.5f;
    public float range = 50f;
    public float hitforce = 100f;
    public Camera Cam;
    public Transform Arm;
    public float LookDirection = 10f;

    private WaitForSeconds shotsDuration = new WaitForSeconds(.07f);
    private AudioSource gunaudio;
    private LineRenderer laserLine; 
    private float nextFire;
    private GameObject Player;
    private playerAmmo Ammo;
    private Animator anim;
    private bool x = false;

	void Start () {
        Player = GameObject.FindGameObjectWithTag("Player");
        Ammo = Player.GetComponent<playerAmmo>();
        laserLine = GetComponent<LineRenderer>();
        gunaudio = GetComponent<AudioSource>();
        anim = Player.GetComponent<Animator>();
	}
	

	void Update () {


        if (Input.GetKey(KeyCode.Tab)|| Input.GetKey(KeyCode.Mouse1))
        {
            anim.SetLayerWeight(1, 0);
            anim.SetLayerWeight(2, 1);

            RaycastHit hit;
            Vector3 rayOrigin = Cam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));

            if (Physics.Raycast(rayOrigin, Cam.transform.forward, out hit, range))
            {

                Arm.transform.rotation = Quaternion.LookRotation(Arm.transform.position - Cam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, LookDirection)));


                if (Input.GetButtonDown("Fire1") && Time.time > nextFire)
                {
                    Ammo.fireAmmo(1);
                    nextFire = Time.time + fireRate;
                    StartCoroutine(ShotEffect());
                    laserLine.SetPosition(0, transform.position);
                    laserLine.SetPosition(1, hit.point);
                    EnemyHealth enemyHealth = hit.collider.GetComponent<EnemyHealth>();
                    if (enemyHealth != null)
                    {
                        // ... the enemy should take damage.
                        enemyHealth.TakeDamage(gunDamage);
                    }
                    if (hit.rigidbody != null)
                    {
                        hit.rigidbody.AddForce(-hit.normal * hitforce);
                        destructionController x = hit.collider.GetComponent<destructionController>();
                        if (x != null)
                            x.shoot();
                    }
                    TutrialScript_BlockPunched Blk = hit.collider.GetComponent<TutrialScript_BlockPunched>();
                    if (Blk != null)
                    {
                        Blk.Triggered();
                    }
                    TutorialScript_DestoryedallCrates Blk2 = hit.collider.GetComponent<TutorialScript_DestoryedallCrates>();
                    if (Blk2 != null)
                    {
                        Blk2.Triggered();
                    }

                }
            }
            else
            {
                laserLine.SetPosition(1, rayOrigin + (Cam.transform.forward * range));
            }

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
