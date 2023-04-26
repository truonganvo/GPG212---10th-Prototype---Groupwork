using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTheAbily : MonoBehaviour
{
    [SerializeField] DoubleJump isJumpOn;
    [SerializeField] GameObject canvas;

    //Double Jump Activate
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isJumpOn.isDoubleJumpOn = true;
            Destroy(gameObject);
        }
    }
}
