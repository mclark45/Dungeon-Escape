using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private Animator _playerAnimations;
    private SpriteRenderer _playerSpriteRender;
    private float _playerSpritesXAxis;
    [SerializeField] private Transform _playerSpritePosition;
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

    public void Jump(bool jumping)
    {
        _playerAnimations.SetBool("Jumping", jumping);
    }

    private void FlipSprite(float horizontalMovement)
    {
        if (horizontalMovement < 0f)
        {
            _playerSpriteRender.flipX = true;
            _playerSpritesXAxis = -0.09f;
        }
        else if (horizontalMovement > 0f)
        {
            _playerSpriteRender.flipX = false;
            _playerSpritesXAxis = 0.09f;
        }

        _playerSpritePosition.localPosition = new Vector2(_playerSpritesXAxis, 0.2f);
    }
}
