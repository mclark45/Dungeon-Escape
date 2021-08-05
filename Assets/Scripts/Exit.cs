using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{

    [SerializeField] private GameObject _winScreen;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Player player = collision.GetComponent<Player>();

            if (player == null)
                Debug.LogError("Player script is null");

            if (player.hasKey == true)
            {
                _winScreen.SetActive(true);
                Time.timeScale = 0f;
            }
        }
    }
}
