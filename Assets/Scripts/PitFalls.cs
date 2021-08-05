using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PitFalls : MonoBehaviour
{
    [SerializeField] protected Transform startPosition;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            collision.transform.position = startPosition.transform.position;
    }
}
