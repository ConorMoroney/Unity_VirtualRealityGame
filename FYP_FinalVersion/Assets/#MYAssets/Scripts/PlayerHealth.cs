using UnityEngine;
using UnityEngine.UI;
using System.Collections;


namespace UnityStandardAssets.Characters.ThirdPerson
{

    public class PlayerHealth : MonoBehaviour
    {
        public int startingHealth = 100;                            // The amount of health the player starts the game with.
        public int currentHealth;                                   // The current health the player has.
        public Slider healthSlider;                                 // Reference to the UI's health bar.
        public Image damageImage;                                   // Reference to an image to flash on the screen on being hurt.
        public GameObject playerShooting;                                                            //public AudioClip deathClip;                                 // The audio clip to play when the player dies.
        public float flashSpeed = 5f;                               // The speed the damageImage will fade at.
        public Color flashColour = new Color(1f, 0f, 0f, 0.1f);     // The colour the damageImage is set to, to flash.

        Color normalColor;
        Animator anim;                                              // Reference to the Animator component.                                                           //AudioSource playerAudio;                                    // Reference to the AudioSource component.
        ThirdPersonUserControl PlayerControls;                             // Reference to the player's movement.                                                                //PlayerShooting playerShooting;                              // Reference to the PlayerShooting script.
        public bool isDead;                                                // Whether the player is dead.
        bool damaged;                                               // True when the player gets damaged.
        private SkinnedMeshRenderer body;
        private RaycastShoot Gun;
        private Vector3 Location;

        void Awake()
        {
            // Setting up the references.
            Location = transform.position;
            anim = GetComponent<Animator>();
            //playerAudio = GetComponent <AudioSource> ();
            PlayerControls = GetComponent<ThirdPersonUserControl>();
            //playerShooting = GetComponentInChildren <PlayerShooting> ();
            body = GetComponentInChildren<SkinnedMeshRenderer>();
            normalColor = body.material.color;
            Gun = playerShooting.GetComponent<RaycastShoot>();
            // Set the initial health of the player.
            currentHealth = startingHealth;
            if (Gun != null)
                Debug.Log("Gun not set");
        }


        void Update()
        {
            if (damaged)
            {
                damageImage.color = flashColour;
            }
            // Otherwise...
            else
            {
                damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
            }
            healthSlider.value = currentHealth;
            // Reset the damaged flag.
            damaged = false;
        }
        public void increaseHealth(int Amount)
        {
            currentHealth += Amount;
            healthSlider.value = currentHealth;
        }

        public void TakeDamage(int amount)
        {
            // Set the damaged flag so the screen will flash.
            damaged = true;

            // Reduce the current health by the damage amount.
            currentHealth -= amount;
            StartCoroutine(Flasher());
            // Set the health bar's value to the current health.
            healthSlider.value = currentHealth;

            // Play the hurt sound effect.
            //playerAudio.Play ();

            // If the player has lost all it's health and the death flag hasn't been set yet...
            if (currentHealth <= 0 && !isDead)
            {
                // ... it should die.
                Death();
            }
        }

        IEnumerator Flasher()
        {
            for (int i = 0; i < 3; i++)
            {
                body.enabled = false;
                yield return new WaitForSeconds(.1f);
                body.enabled = true;
                yield return new WaitForSeconds(.1f);
            }
        }
        public void Death()
        {
            // Set the death flag so this function won't be called again.
            isDead = true;
            
            anim.SetTrigger("Dying");
            // Turn off the movement and shooting scripts.
            PlayerControls.enabled = false;         
            Gun.enabled = false;
        }
        public void startSinking()
        {
            // After 2 seconds destory the enemy.
            //Destroy(gameObject, 2f);

            Respawn();
            //Restart Player if still has lives
            
        }
        public void setSpawn(Vector3 Location)
        {
            this.Location = Location;
        }

        void Respawn()
        {
            transform.position = Location;
            isDead = false;
            currentHealth = startingHealth;
            PlayerControls.enabled = true;
            Gun.enabled = true;
            StartCoroutine(Flasher());
        }
    }
}