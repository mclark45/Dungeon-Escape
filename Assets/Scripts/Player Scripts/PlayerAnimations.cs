using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private Animator _playerAnimations;
    private Animator _swordEffectsAnimator;
    private SpriteRenderer _playerSpriteRender;
    private SpriteRenderer _swordEffectsSpriteRenderer;
    private Transform _playerSpritePosition;
    private Transform _swordEffectsSpritePosition;
    private float _playerSpritesXAxis;
    private float _swordSpritesXAxis;
    private float _swordSpritesYAxis;
    void Start()
    {
        _playerAnimations = GetComponentInChildren<Animator>();
        _swordEffectsAnimator = transform.GetChild(1).GetComponent<Animator>();
        _playerSpriteRender = GetComponentInChildren<SpriteRenderer>();
        _swordEffectsSpriteRenderer = transform.GetChild(1).GetComponent<SpriteRenderer>();
        _playerSpritePosition = transform.GetChild(0).GetComponent<Transform>();
        _swordEffectsSpritePosition = transform.GetChild(1).GetComponent<Transform>();

        if (_playerAnimations == null)
        {
            Debug.LogError("Player animator is Null");
        }

        if (_swordEffectsAnimator == null)
        {
            Debug.LogError("Sword Effect Animator is Null");
        }

        if (_playerSpriteRender == null)
        {
            Debug.LogError("Player Sprite Renderer is Null");
        }

        if (_swordEffectsSpriteRenderer == null)
        {
            Debug.LogError("Sword Effects Sprite Renderer is Null");
        }

        if (_playerSpritePosition == null)
        {
            Debug.LogError("Player Sprite Position is Null");
        }

        if (_swordEffectsSpritePosition == null)
        {
            Debug.LogError("Sword Sprite Position is Null");
        }
    }

    public void Move(float horizontalMovement)
    {
        _playerAnimations.SetFloat("Speed", Mathf.Abs(horizontalMovement));

        if (horizontalMovement > 0)
            transform.localScale = new Vector3(1f, 1f, 1f);

        if (horizontalMovement < 0)
            transform.localScale = new Vector3(-1f, 1f, 1f);

        //FlipSprite(horizontalMovement);
    }

    public void Jump(bool jumping)
    {
        _playerAnimations.SetBool("Jumping", jumping);
    }

    public void Attack()
    {
        _playerAnimations.SetTrigger("Attack");
        _swordEffectsAnimator.SetTrigger("Sword Arc");
        StartCoroutine(SwordArc());
    }

    private void FlipSprite(float horizontalMovement)
    {
        if (horizontalMovement < 0f)
        {
            _playerSpriteRender.flipX = true;
            _playerSpritesXAxis = -0.09f;
            _swordEffectsSpriteRenderer.flipY = true;
            _swordEffectsSpriteRenderer.sortingOrder = 50;
            _swordEffectsSpritePosition.localRotation = Quaternion.Euler(-70, 48, -80);
            _swordSpritesXAxis = -0.2f;
            _swordSpritesYAxis = -0.2f;
        }
        else if (horizontalMovement > 0f)
        {
            _playerSpriteRender.flipX = false;
            _playerSpritesXAxis = 0.09f;
            _swordEffectsSpriteRenderer.flipY = false;
            _swordEffectsSpriteRenderer.sortingOrder = 51;
            _swordEffectsSpritePosition.localRotation = Quaternion.Euler(70, 48, -80);
            _swordSpritesXAxis = 0f;
            _swordSpritesYAxis = 0.2f;
        }

        _playerSpritePosition.localPosition = new Vector2(_playerSpritesXAxis, 0.2f);
        _swordEffectsSpritePosition.localPosition = new Vector2(_swordSpritesXAxis, _swordSpritesYAxis);
    }

    public void DeathAnimation()
    {
        _playerAnimations.SetTrigger("Death");
    }

    IEnumerator SwordArc()
    {
        yield return new WaitForSeconds(0.3f);
        _swordEffectsSpriteRenderer.enabled = true;
        yield return new WaitForSeconds(0.3f);
        _swordEffectsSpriteRenderer.enabled = false;
    }
}
