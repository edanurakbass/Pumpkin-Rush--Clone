using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pumpkin : MonoBehaviour
{
    public GameObject successPanel;

    private void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.tag == "Target")
        {
            successPanel.SetActive(true);
        }

    }
}
