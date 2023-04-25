using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTheAbily1 : MonoBehaviour
{
    [SerializeField] Climbing isClimbOn;

    //Double Jump Activate
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isClimbOn.isWallClimbOn = true;
            Destroy(gameObject);
        }
    }
}
