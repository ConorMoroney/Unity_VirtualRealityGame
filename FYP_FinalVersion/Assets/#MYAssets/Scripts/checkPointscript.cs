using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class checkPointscript : MonoBehaviour
{

    private ParticleSystem particles;
    private AudioSource _audio;
    private TutorialScript_Checkpoint _checkpoint;
    private bool once = false;
    private void Awake()
    {
        particles = GetComponent<ParticleSystem>();
        _audio = GetComponent<AudioSource>();
        _checkpoint = GetComponent<TutorialScript_Checkpoint>();
    }
    private void OnTriggerEnter(Collider other)
    {
        PlayerHealth health = other.GetComponent<PlayerHealth>();
        if (health != null && health.currentHealth > 0 && !once)
        {
            health.setSpawn(other.transform.position);
            once = true;
            particles.Play();
            _audio.Play();
        }
        if(_checkpoint != null)
        {
            _checkpoint.Triggered();
        }
    }
  
}
