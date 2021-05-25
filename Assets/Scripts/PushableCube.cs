using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushableCube : MonoBehaviour
{
    public Rigidbody rb;
    public static bool duvar;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public bool Ilerle(Vector3 direction)
    {
        if (!duvar)
        {
            transform.position += direction;
            return true;
        }
        return false;
    }

    public void YaniDoluMu(Vector3 direction)
    {
        RaycastHit hit;

        if (rb.SweepTest(direction, out hit, 0.3f))
        {
            DuvarVarMi(direction);
            if (hit.collider.tag == "Push")
            {
                Debug.Log(hit.collider.gameObject);
                hit.collider.GetComponent<PushableCube>().YaniDoluMu(direction);

                 if (!duvar)
                 {
                     hit.transform.position += direction;
                 }
            }

        }
    }

    public bool DuvarVarMi(Vector3 direction)
    {
        RaycastHit hit;

        if(rb.SweepTest(direction, out hit, 0.3f))
        {
            if (hit.collider.CompareTag("Wall"))
            {
                Debug.Log("duvar var");
                duvar = true;
                return true;
            }
        }
        duvar = false;
        return false;
    }
}
