using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeManager : MonoBehaviour
{
    private Vector3 initialPos;
    public Vector3 moveDirection;
    Rigidbody rb;
    public bool aboutToCollide;
    public float distanceToCollision;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            initialPos = Input.mousePosition;
        }
        if (Input.GetMouseButtonUp(0))
        {
            Calculate(Input.mousePosition);
            moveDirection = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            aboutToCollide = false;
        }

    }
    void Calculate(Vector3 finalPos)
    {
        float disX = Mathf.Abs(initialPos.x - finalPos.x);
        float disZ = Mathf.Abs(initialPos.y - finalPos.y);
        RaycastHit hit;
        
        if (disX > 0 || disZ > 0)
        {
            if (disX > disZ)
            {
                if (initialPos.x > finalPos.x)
                {
                    Debug.Log("Left");

                    if (rb.SweepTest(-transform.right, out hit, 0.3f))
                    {
                        distanceToCollision = hit.distance;
                        aboutToCollide = true;
                        
                    }
                    if (aboutToCollide && hit.collider.tag == "Push")
                    {
                       
                        hit.transform.gameObject.GetComponent<PushableCube>().YaniDoluMu(-hit.transform.right);
                        if (!hit.transform.gameObject.GetComponent<PushableCube>().Ilerle(-hit.transform.right))
                            return;
                    }
                    else if (aboutToCollide && hit.collider.CompareTag("Wall"))
                    {
                        return;
                    }

                    transform.position += new Vector3(-1f, 0, 0);
                }
                else
                {

                    if (rb.SweepTest(transform.right, out hit, 0.3f))
                    {
                        distanceToCollision = hit.distance;
                        aboutToCollide = true;
                    }
                    if (aboutToCollide && hit.collider.tag == "Push")
                    {

                        hit.transform.gameObject.GetComponent<PushableCube>().YaniDoluMu(hit.transform.right);
                        if (!hit.transform.gameObject.GetComponent<PushableCube>().Ilerle(hit.transform.right))
                            return;

                    }
                    else if (aboutToCollide && hit.collider.CompareTag("Wall"))
                    {
                        return;
                    }

                    transform.position += new Vector3(1f, 0, 0);

                }
            }
            else
            {
                if (initialPos.y > finalPos.y)
                {
                    Debug.Log("Down");

                    if (rb.SweepTest(-transform.forward, out hit, 0.3f))
                    {
                        distanceToCollision = hit.distance;
                        aboutToCollide = true;
                    }
                    if (aboutToCollide && hit.collider.tag == "Push")
                    {
          
                        hit.transform.gameObject.GetComponent<PushableCube>().YaniDoluMu(-hit.transform.forward);
                        if (!hit.transform.gameObject.GetComponent<PushableCube>().Ilerle(-hit.transform.forward))
                            return;
                    }
                    else if (aboutToCollide && hit.collider.CompareTag("Wall"))
                    {
                        return;
                    }

                    transform.position += new Vector3(0, 0, -1f);

                }
                else
                {
                    Debug.Log("Up");

                    if(rb.SweepTest(transform.forward, out hit, 0.3f))
                    {
                        distanceToCollision = hit.distance;
                        aboutToCollide = true;
                    }
                    
                    if (aboutToCollide && hit.collider.tag == "Push")
                    {
                     
                        if (hit.transform.gameObject.GetComponent<PushableCube>().DuvarVarMi(hit.transform.forward))
                        {
                            return;
                        }
                        hit.transform.gameObject.GetComponent<PushableCube>().YaniDoluMu(hit.transform.forward);
                        if (!hit.transform.gameObject.GetComponent<PushableCube>().Ilerle(hit.transform.forward))
                            return;
                    }
                    else if(aboutToCollide && hit.collider.CompareTag("Wall"))
                    {
                        return;
                    }
                    transform.position += new Vector3(0, 0, 1f);
                }
            }
        }
    }
}