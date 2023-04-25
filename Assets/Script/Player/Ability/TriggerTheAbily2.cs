using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTheAbily2 : MonoBehaviour
{
    [SerializeField] Dashing isDashOn;

    //Double Jump Activate
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isDashOn.isDashingOn = true;
            Destroy(gameObject);
        }
    }
}
