using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxDestroyer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out AnimatedBox box))
        {
            Destroy(collision.gameObject);
        }
    }
}
