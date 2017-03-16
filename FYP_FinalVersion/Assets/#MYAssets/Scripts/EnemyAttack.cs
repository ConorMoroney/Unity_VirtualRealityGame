using UnityEngine;
using System.Collections;


namespace UnityStandardAssets.Characters.ThirdPerson
{
    public class EnemyAttack : MonoBehaviour
    {
        public float timeBetweenAttacks = 0.5f;     // The time in seconds between each attack.
        public int attackDamage = 10;
        public float hitforce = 10f;
        public float speed = 2f;
        public Transform origin;
        GameObject player;
        Animator anim;
        AnimationClip clip;// Reference to the animator component.
        PlayerHealth playerHealth;                  // Reference to the player's health.
        EnemyHealth enemyHealth;                    // Reference to this enemy's health.
        bool playerInRange;                         // Whether player is within the trigger collider and can be attacked.
        float timer;                                // Timer for counting up to the next attack.
        bool win = false;

        void Awake()
        {
            // Setting up the references.
            player = GameObject.FindGameObjectWithTag("Player");
            playerHealth = player.GetComponent<PlayerHealth>();
            enemyHealth = GetComponent<EnemyHealth>();
            anim = GetComponent<Animator>();
            clip = GetAnimationClip("Attack");



        }
         AnimationClip GetAnimationClip(string name)
        {
            if (!anim) return null; // no animator

            foreach (AnimationClip clip in anim.runtimeAnimatorController.animationClips)
            {
                if (clip.name == name)
                {
                    return clip;
                }
            }
            return null; // no clip by that name
        }

        //Detect if player is in range of attack
        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject == player)
            {
                playerInRange = true;
            }
        }
    
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject == player)
            {
                playerInRange = true;
            }
        }

        void OnTriggerExit(Collider other)
        {
            if (other.gameObject == player)
            {
                playerInRange = false;
            }
        }

       
        void FixedUpdate()
        {
            // Add the time since Update was last called to the timer.
            timer += Time.deltaTime;
         
            // If the timer exceeds the time between attacks, the player is in range and this enemy is alive...
            if (timer >= timeBetweenAttacks && playerInRange && enemyHealth.currentHealth > 0)
            {
                Vector3 face = player.transform.position - transform.position;
                face.y = 0;
                Quaternion dir =  Quaternion.LookRotation(face);
                transform.rotation = Quaternion.Slerp(transform.rotation,dir , Time.time * speed);
                // ... attack.
                anim.SetTrigger("Attack");
                
            }

            // If the player has zero or less health...
            if (playerHealth.currentHealth <= 0 && !win)
            {
                win = true;
                anim.SetTrigger("Winning");
            }
        }
        public void Punched()
        {
            
            Debug.Log("Punched was Called");
            RaycastHit hit;
            if (Physics.Raycast(origin.position,origin.forward, out hit, 1.5f))
            {

                PlayerHealth playerHealth = hit.collider.GetComponent<PlayerHealth>();
               
                if (playerHealth != null)
                {
                    // ... the enemy should take damage.
                   playerHealth.TakeDamage(attackDamage);
                }
                if (hit.rigidbody != null)
                {
                    hit.rigidbody.AddForce(-hit.normal * hitforce);
                    destructionController x = hit.collider.GetComponent<destructionController>();
                    if (x != null)
                        x.shoot();
                }
            }
            playerInRange = false;
        }
    
    }
}