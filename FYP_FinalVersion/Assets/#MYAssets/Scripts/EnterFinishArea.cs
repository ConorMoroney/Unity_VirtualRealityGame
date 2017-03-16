using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterFinishArea : MonoBehaviour {
    public int duration = 4;
    private ParticleSystem particles;
    private void Awake()
    {
        particles = GetComponent<ParticleSystem>();
    }
    private void OnTriggerEnter(Collider other)
    {
        Animator anim = other.gameObject.GetComponent<Animator>();
        EnemyHealth enemy = other.gameObject.GetComponent<EnemyHealth>();
        if (enemy != null)
            enemy.TakeDamage(100);
        if (anim != null && enemy == null)
            StartCoroutine(Duartion(anim));
    }
    IEnumerator Duartion(Animator anim)
    {
        yield return new WaitForSeconds(duration);
        anim.SetTrigger("Winning");
        particles.Play();
    }
}
