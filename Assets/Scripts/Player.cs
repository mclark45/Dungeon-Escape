using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D _playerRigidBody;
    [SerializeField] private float _jumpForce = 5.0f;
    private bool _resetJump = false;
    void Start()
    {
        _playerRigidBody = GetComponent<Rigidbody2D>();
        if (_playerRigidBody == null)
        {
            Debug.LogError("Player rigidbody is Null");
        }
    }

    void Update()
    {
        PlayerMovement();
    }

    private void PlayerMovement()
    {
        float horizontalMovement = Input.GetAxisRaw("Horizontal");

        _playerRigidBody.velocity = new Vector2(horizontalMovement, _playerRigidBody.velocity.y);

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            _playerRigidBody.velocity = new Vector2(_playerRigidBody.velocity.x, _jumpForce);
            StartCoroutine(ResetJumpRoutine());
        }
    }

    private bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.8f, 1 << 3);

        if (hit.collider != null)
        {
            if (_resetJump == false)
                return true;
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