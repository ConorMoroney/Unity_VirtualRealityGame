using System;
using System.Collections;
using UnityEngine;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    [RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
    [RequireComponent(typeof(ThirdPersonCharacter))]
    public class AIScript : MonoBehaviour
    {
        public UnityEngine.AI.NavMeshAgent agent { get; private set; }             // the navmesh agent required for the path finding
        public ThirdPersonCharacter character { get; private set; }                // the character we are controlling
        public float StoppingDistance;
        public EnemyGunfire Gun;
        public Transform[] points;
        private GameObject Player;
        private EnemyHealth health;
        private int destPoint = 0;
        private PlayerHealth pHealth;
        private bool Detected = false;
        private bool canFire = false;
        public float fireRate = 1.5f;
        private float nextFire;
        private void Start()
        {
            // get the components on the object we need ( should not be null due to require component so no need to check )
            agent = GetComponentInChildren<UnityEngine.AI.NavMeshAgent>();
            character = GetComponent<ThirdPersonCharacter>();
            health = character.GetComponent<EnemyHealth>();
            Player = GameObject.FindGameObjectWithTag("Player");
            pHealth = Player.GetComponent<PlayerHealth>();
            agent.updateRotation = false;
            agent.updatePosition = true;
            agent.stoppingDistance = StoppingDistance;

            agent.autoBraking = false;

            GotoNextPoint();
        }
        private void FixedUpdate()
        {
            if (canFire && Time.time > nextFire && Gun != null)
            {
                nextFire = Time.time + fireRate;
                Gun.FireWeapon();
            }
            if (health.isDead)
            {
                canFire = false;
                Gun.enabled = false;
            }
        }

        private void Update()
        {
            if (pHealth.isDead)
            {
                canFire = false;
                agent.Stop();
            }

            if (GetComponent<UnityEngine.AI.NavMeshAgent>().enabled == true)
            {
                agent.Resume();
                if (Detected)//if player is detected , go to a shotting position and take a shot
                {
                    
                    agent.SetDestination(Player.transform.position);
                    agent.stoppingDistance = 7f;
                    canFire = true;
                    if (Vector3.Distance(transform.position, Player.transform.position) > 10f)
                    {// if player is out of range set to undetected 
                        Detected = false;
                        GotoNextPoint();
                    }
                    if(agent.remainingDistance < 5f && canFire)// if player gets closer , go for the melee attack 
                    {
                        agent.stoppingDistance = StoppingDistance;
                        canFire = false;
                    }
                }
                else
                {
                   
                    if (agent.remainingDistance < 1.5f) // if reached point , go to next point 
                        GotoNextPoint();
                }
                if (Vector3.Distance(transform.position, Player.transform.position) < 10f) // if player is within range set to detected
                    Detected = true;

                if (agent.remainingDistance > agent.stoppingDistance)//move towards target if out of stopping distance 
                {
                    canFire = false;
                    if (!Detected)
                        character.Move(agent.desiredVelocity / 2, false, false);
                    else
                        character.Move(agent.desiredVelocity, false, false);
                }
                else
                {
                    character.Move(Vector3.zero, false, false);
                    agent.Stop();
                }

            }
        }
        void GotoNextPoint()
        {
            // Returns if no points have been set up
            if (points.Length == 0)
                return;
            
            agent.destination = points[destPoint].position;
            destPoint = (destPoint + 1) % points.Length;
        }
    }
}
