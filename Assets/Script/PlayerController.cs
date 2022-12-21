using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D _rb;
    [SerializeField] float _moveSpeed = default;
    [SerializeField] float _jumpPower = default;
    Vector2 _dir;
    bool _isGrounded;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
       
    }

    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        _dir = new Vector2(h * _moveSpeed, _rb.velocity.y);
        _rb.velocity = _dir;
        if (Input.GetButton("Jump") && _isGrounded)
        {
            _dir = new Vector2(_rb.velocity.x, _jumpPower);
            _rb.velocity = _dir;
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        _isGrounded = true;  
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        _isGrounded = false; 
    }
    void OnTriggerEnter(Collider collision)
    {
       if(collision.gameObject.tag == "Enemy")
        {
            GameManager.instance._life -= 1;
        }
    }
}
