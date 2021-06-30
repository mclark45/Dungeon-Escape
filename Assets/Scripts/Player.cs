using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D _playerRigidBody;
    private PlayerAnimations _playerAnim;
    [SerializeField] private float _jumpForce = 5.0f;
    [SerializeField] private float _speed = 2.5f;
    private bool _resetJump = false;
    private bool _grounded;
    void Start()
    {
        _playerRigidBody = GetComponent<Rigidbody2D>();
        _playerAnim = GetComponent<PlayerAnimations>();

        if (_playerRigidBody == null)
        {
            Debug.LogError("Player rigidbody is Null");
        }

        if (_playerAnim == null)
        {
            Debug.LogError("Player Animation Script is Null");
        }
    }

    void Update()
    {
        PlayerMovement();
    }

    private void PlayerMovement()
    {
        float horizontalMovement = Input.GetAxisRaw("Horizontal");
        _grounded = IsGrounded();

        if (Input.GetKeyDown(KeyCode.Space) && _grounded)
        {
            _playerRigidBody.velocity = new Vector2(_playerRigidBody.velocity.x, _jumpForce);
            StartCoroutine(ResetJumpRoutine());
            _playerAnim.Jump(true);
        }

        _playerRigidBody.velocity = new Vector2(horizontalMovement * _speed, _playerRigidBody.velocity.y);
        _playerAnim.Move(horizontalMovement);
    }

    private bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.8f, 1 << 3);

        if (hit.collider != null)
        {
            if (_resetJump == false)
            {
                _playerAnim.Jump(false);
                return true;
            }
        }

        return false;
    }

    IEnumerator ResetJumpRoutine()
    {
        _resetJump = true;
        yield return new WaitForSeconds(0.1f);
        _resetJump = false;
    }
}
