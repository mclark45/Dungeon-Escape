using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidShot : MonoBehaviour
{
    private float _speed = 1.5f;

    private void Start()
    {
        Destroy(this.gameObject, 5.0f);
    }

    void Update()
    {
        transform.Translate(Vector2.right * _speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        IDamageable hit = other.GetComponent<IDamageable>();

        if (hit != null)
        {
            hit.Damage();
            Destroy(this.gameObject);
        }
    }
}
