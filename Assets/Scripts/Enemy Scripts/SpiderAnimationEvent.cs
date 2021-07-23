using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderAnimationEvent : MonoBehaviour
{
    [SerializeField] private GameObject _acid;

    public void Fire()
    {
        Instantiate(_acid, transform.position, Quaternion.identity);
    }
}
