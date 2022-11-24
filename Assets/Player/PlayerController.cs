using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float _movespeed = 0.5f;
    [SerializeField] float _jumppower = 10f;
    Rigidbody2D _rb;
    [SerializeField] float _gravityDrag = 0.8f;
    float _h;
    SpriteRenderer _sprite = default;

    float h = 0;
    int _jumpcount = 0;
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _sprite = GetComponent<SpriteRenderer>();
    }

    
    void Update()
    {
        Vector2 velocity = _rb.velocity;
        h = Input.GetAxisRaw("Horizontal");
        if (Input.GetButtonDown("Jump") && _jumpcount < 1)
        {
            _jumpcount++;
            velocity.y = _jumppower;
        }
        else if(!Input.GetButtonDown("Jump") && velocity.y > 0)
        {
            velocity.y *= _gravityDrag;
        }
        _rb.velocity = velocity;

        jumpRay();
    }
    private void FixedUpdate()
    {
        _rb.AddForce(h * _movespeed * Vector2.right * 100);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
    private void LateUpdate()
    {
        if(_h != 0)
        {
            _sprite.flipX = (_h < 0);
        }

        // アニメーションを制御する
        //if (m_anim)
        //{
        //    m_anim.SetFloat("SpeedX", Mathf.Abs(m_rb.velocity.x));
        //    m_anim.SetFloat("SpeedY", m_rb.velocity.y);
        //    m_anim.SetBool("IsGrounded", m_isGrounded);
        //}
    }

    [SerializeField] Vector2 _lineForGround = new Vector2(0f, -2f);
    [SerializeField] LayerMask _groundLayer = 0;
    void jumpRay()
    {
        Vector2 start = this.transform.position;
        Debug.DrawLine(start, start + _lineForGround);
        RaycastHit2D hit = Physics2D.Linecast(start, start + _lineForGround, _groundLayer);
        Vector2 velo = Vector2.zero;

        if(hit.collider)
        {
            _jumpcount = 0;
        }
        velo.y = _rb.velocity.y;
        _rb.velocity = velo;
    }
}
