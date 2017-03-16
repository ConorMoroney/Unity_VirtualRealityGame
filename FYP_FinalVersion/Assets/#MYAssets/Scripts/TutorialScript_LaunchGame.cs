using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialScript_LaunchGame : MonoBehaviour {

    EnemyHealth Health;
	// Use this for initialization
	void Start () {
        Health = GetComponent<EnemyHealth>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Health.currentHealth < 0)
            StartCoroutine(LaunchGame());
    }
    IEnumerator LaunchGame()
    {
        yield return new WaitForSeconds(2f);
        Application.LoadLevel("Lvl_01.2");
    }
}
