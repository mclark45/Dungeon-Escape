using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamond : MonoBehaviour
{
    private Player _player;
    [SerializeField] private int _score = 10;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        if (_player == null)
            Debug.LogError("Playe script is Null");
    }

    public void Gems(int gems)
    {
        _score = gems;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == ("Player"))
        {
            _player.Score(_score);
            Destroy(this.gameObject);
        }
    }
}
