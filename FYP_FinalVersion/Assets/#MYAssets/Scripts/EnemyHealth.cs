using System.Collections;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public bool SpawnItem;
    public int startingHealth = 100;            // The amount of health the enemy starts the game with.
    public int currentHealth;                   // The current health the enemy has.
    public float sinkSpeed = 2.5f;              // The speed at which the enemy sinks through the floor when dead.
    public int scoreValue = 10;                 // The amount added to the player's score when the enemy dies.
    public GameObject Remains;

    Animator anim;                              // Reference to the animator.
    CapsuleCollider capsuleCollider;            // Reference to the capsule collider.
    public bool isDead;                         // Whether the enemy is dead.
    bool isSinking;                             // Whether the enemy has started sinking through the floor.
    private SkinnedMeshRenderer body;

    void Awake()
    {
        // Setting up the references.
        anim = GetComponent<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        body = GetComponentInChildren<SkinnedMeshRenderer>();
        // Setting the current health when the enemy first spawns.
        currentHealth = startingHealth;
    }

    void Update()
    {
        // If the enemy should be sinking...
        if (isSinking)
        {
            // ... move the enemy down by the sinkSpeed per second.
            transform.Translate(-Vector3.up * sinkSpeed * Time.deltaTime);
        }
    }


    public void TakeDamage(int amount)
    {
        if (isDead)
            return;

        currentHealth -= amount;
        StartCoroutine(Flasher());
        
        if (currentHealth <= 0)
        {
            Death();
        }
        else
        {
            anim.SetTrigger("Hit");
        }
    }

    IEnumerator Flasher()
    {

        for (int i = 0; i < 3; i++)
        {
            //body.enabled = false;
            yield return new WaitForSeconds(.1f);

            //body.enabled = true;
            yield return new WaitForSeconds(.1f);
        }
    }
    void Death()
    {
        // The enemy is dead.
        isDead = true;
        // Turn the collider into a trigger so shots can pass through it.
        
        // Tell the animator that the enemy is dead.
        anim.SetTrigger("Dying");
    }

    public void startSinking()
    {
        // Find and disable the Nav Mesh Agent.
        //GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
        // Find the rigidbody component and make it kinematic (since we use Translate to sink the enemy).
        GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
        if (SpawnItem)
            Instantiate(Remains, transform.position, transform.rotation);
        capsuleCollider.isTrigger = true;
        // The enemy should no sink.
        isSinking = true;
        // After 2 seconds destory the enemy.
        Destroy(gameObject, 2f);
    }

}