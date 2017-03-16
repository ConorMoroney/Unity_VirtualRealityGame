using UnityEngine;
using System.Collections;

public class DestroyObject : MonoBehaviour {

    public float howLong = 5f;
    public int duration = 1;
    private MeshRenderer body;
    private Color start;
    private Color end;
    // Update is called once per frame
    private void Awake()
    {
        body = GetComponent<MeshRenderer>();
        start = body.material.color;
        end = new Color(start.r, start.g, start.b, 0.0f);
        if (body != null)
        {
            StartCoroutine(AlphaFade());
        }
    }

    void Update () {
        DestroyObject(gameObject,howLong);
	}
    
    IEnumerator fadeOut()
    {
        float change = 1.0f;
        while(change < 0.0f)
        {
            change -= duration * Time.deltaTime;
            body.material.color = Color.Lerp(start, end,change);
            yield return null;
        }
    }
    IEnumerator AlphaFade()
    {
        // Alpha start value.
        float alpha = 1.0f;

        // Loop until aplha is below zero (completely invisalbe)
        while (alpha > 0.0f)
        {
            // Reduce alpha by fadeSpeed amount.
            alpha -= duration * Time.deltaTime;

            // Create a new color using original color RGB values combined
            // with new alpha value. We have to do this because we can't 
            // change the alpha value of the original color directly.
            body.material.color = new Color(start.r, start.g, start.b, alpha);

            yield return null;
        }
    }
}
