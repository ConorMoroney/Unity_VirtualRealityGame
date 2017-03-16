using UnityEngine;
using System.Collections;
using System.Linq;

public class PointArm : MonoBehaviour {


    public Transform Arm;
    Transform closest;
    private bool x = false;
    private void Awake()
    {
        closest = GetNearestTarget().transform;
    }

    void Update ()
    {
        if (GameObject.FindGameObjectsWithTag("EnemyTarget") != null)
        {
            /*if (Input.GetKey(KeyCode.Q)) 
            {
               StartCoroutine( point());
            }*/
            if (x || Input.GetKey(KeyCode.Q))
            {
                Arm.transform.rotation = Quaternion.LookRotation(Arm.transform.position - closest.position + Vector3.left);
            }
        }
    }
    public GameObject GetNearestTarget()
    {
        if(GameObject.FindGameObjectsWithTag("EnemyTarget") != null)
        return GameObject.FindGameObjectsWithTag("EnemyTarget").Aggregate((o1, o2) => Vector3.Distance(o1.transform.position, this.transform.position)> Vector3.Distance(o2.transform.position, this.transform.position) ? o2 : o1);

        return null;
    }
    public void Aim(Transform hit)
    {
        closest = hit.transform;
        StartCoroutine(point());
    }
    private IEnumerator point()
    {
        x = true;
        yield return new WaitForSeconds(2f);
        x = false;
    }

}
