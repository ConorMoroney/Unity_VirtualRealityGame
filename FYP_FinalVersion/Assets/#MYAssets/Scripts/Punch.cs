using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    [RequireComponent(typeof(ThirdPersonCharacter))]
    [RequireComponent(typeof(AudioSource))]
    public class Punch : MonoBehaviour {
        public float range;
        public Transform myTransform;
        public float timeBetweenAttacks = 1f;
        public float hitforce = 10f;
        public AudioClip SoundHit;
        public AudioClip SoundMiss;
        private AudioSource _Source;
        private ThirdPersonCharacter character;
        private Animator anim;
        private PlayerHealth pHealth;
        float timer;
        void Start() {
            character = GetComponent<ThirdPersonCharacter>();
            pHealth = character.GetComponent<PlayerHealth>();
            anim = GetComponent<Animator>();
            _Source = GetComponent<AudioSource>();
        }

        // Update is called once per frame
        void Update() {
            timer += Time.deltaTime;

            if (Input.GetKey(KeyCode.LeftAlt) && timer >= timeBetweenAttacks && !pHealth.isDead)
            {
                anim.SetTrigger("Attack");
                timer = 0f;
            }

        }
        public void Punched()
        {
            Debug.Log("Punched was Called");
           RaycastHit hit;
            if (Physics.Raycast(myTransform.position, myTransform.forward, out hit, range))
            {
                if (hit.transform != null)
                    _Source.clip = SoundHit;
                EnemyHealth enemyHealth = hit.collider.GetComponent<EnemyHealth>();
                if (enemyHealth != null)
                {
                    // ... the enemy should take damage.
                    enemyHealth.TakeDamage(35);
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
            else
                _Source.clip = SoundMiss;

            _Source.Play();
        }
    }
}