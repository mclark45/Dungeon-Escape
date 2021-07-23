using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    private bool _canSwing = true;

    private void OnTriggerEnter2D(Collider2D other)
    {
        IDamageable hit = other.GetComponent<IDamageable>();

        if (hit != null && _canSwing)
        {
            hit.Damage();
        }

        _canSwing = false;
        StartCoroutine(SwingDelay());
    }

    IEnumerator SwingDelay()
    {
        yield return new WaitForSeconds(0.5f);
        _canSwing = true;
    }
}
