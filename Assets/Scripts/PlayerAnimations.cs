using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private Animator _playerAnimations;
    private SpriteRenderer _playerSpriteRender;
    void Start()
    {
        _playerAnimations = GetComponentInChildren<Animator>();
        _playerSpriteRender = GetComponentInChildren<SpriteRenderer>();

        if (_playerAnimations == null)
        {
            Debug.LogError("Player animator is Null");
        }

        if (_playerSpriteRender == null)
        {
            Debug.LogError("Player Sprite Renderer is Null");
        }
    }

    public void Move(float horizontalMovement)
    {
        _playerAnimations.SetFloat("Speed", Mathf.Abs(horizontalMovement));

        FlipSprite(horizontalMovement);
    }

    private void FlipSprite(float horizontalMovement)
    {
        if (horizontalMovement < 0f)
        {
            _playerSpriteRender.flipX = true;
        }
        else if (horizontalMovement > 0f)
        {
            _playerSpriteRender.flipX = false;
        }
    }
}
