using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    private Rigidbody2D _playerRigidBody;
    private PlayerAnimations _playerAnimations;
    [SerializeField] private float _jumpForce = 5.0f;
    [SerializeField] private float _speed = 2.5f;
    private bool _resetJump = false;
    private bool _grounded;
    private bool _isDead = false;
    private bool _isAttacking = false;
    private float _horizontal;

    public float Health { get; set; }
    [SerializeField] private float health = 3.0f;

    void Start()
    {
        Health = health;
        _playerRigidBody = GetComponent<Rigidbody2D>();
        _playerAnimations = GetComponent<PlayerAnimations>();

        if (_playerRigidBody == null)
            Debug.LogError("Player rigidbody is Null");

        if (_playerAnimations == null)
            Debug.LogError("Player Animation Script is Null");
    }

    void Update()
    {
        if (_isDead || _isAttacking)
            return;

        PlayerMovement();
        PlayerAttack(_grounded);
    }

    private void PlayerMovement()
    {
        _horizontal = Input.GetAxisRaw("Horizontal");
        _grounded = IsGrounded();


        if (Input.GetKeyDown(KeyCode.Space) && _grounded)
        {
            _playerRigidBody.velocity = new Vector2(_playerRigidBody.velocity.x, _jumpForce);
            StartCoroutine(ResetJumpRoutine());
            _playerAnimations.Jump(true);
        }

        _playerRigidBody.velocity = new Vector2(_horizontal * _speed, _playerRigidBody.velocity.y);
        _playerAnimations.Move(_horizontal);
    }

    private void PlayerAttack(bool grounded)
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && grounded == true)
        {
            StartCoroutine(IsAttacking());
            _playerAnimations.Attack();
        }
    }

    private bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.8f, 1 << 3);

        if (hit.collider != null)
        {
            if (_resetJump == false)
            {
                _playerAnimations.Jump(false);
                return true;
            }
        }

        return false;
    }

    public void Damage()
    {
        Health--;

        if (Health < 1f)
        {
            _isDead = true;
            StartCoroutine(Die());
        }
    }

    IEnumerator ResetJumpRoutine()
    {
        _resetJump = true;
        yield return new WaitForSeconds(0.1f);
        _resetJump = false;
    }

    IEnumerator IsAttacking()
    {
        _playerRigidBody.velocity = new Vector2(0f, _playerRigidBody.velocity.y);
        _isAttacking = true;
        yield return new WaitForSeconds(0.9f);
        _isAttacking = false;
    }

    IEnumerator Die()
    {
        _playerAnimations.DeathAnimation();
        Physics2D.IgnoreLayerCollision(7, 9);
        yield return new WaitForSeconds(2.0f);
        Destroy(this.gameObject);
    }
}
