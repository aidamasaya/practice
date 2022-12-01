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
    [SerializeField]  int _jumpcount = 0;
    GameManager _gamemaneger;

    /// <summary>
    /// 継承クラスのオブジェクト
    /// </summary>
    GameObject _teacher;
    GameObject _opera;
    GameObject _dealer;
    [SerializeField] string _name = "Normal";
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _sprite = GetComponent<SpriteRenderer>();
    }

    Status s = new Status();
    void Update()
    {
        Vector2 velocity = _rb.velocity;
        h = Input.GetAxisRaw("Horizontal");
        if (Input.GetButtonDown("Jump") && _jumpcount < 1)
        {
            //_jumpcount = 1;
            velocity.y = _jumppower;
        }
        //else if(!Input.GetButton("Jump") && velocity.y > 0)
        //{
        //    _jumpcount++;
        //    velocity.y *= _gravityDrag;
        //}
        _rb.velocity = velocity;

        jumpRay();

        
    }
    private void FixedUpdate()
    {
        _rb.AddForce(h * _movespeed * Vector2.right * 100);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "enemy")
        {
            GameManager.instance.Damage(1);
        }
        if(collision.gameObject.tag == "Teacher")
        {
            Destroy(this.gameObject);
            Instantiate(_teacher);
            _name = "Teacher";
        }
        else if(collision.gameObject.tag == "Opera")
        {
            Destroy(this.gameObject);
            Instantiate(_opera);
            _name = "Opera";
        }
        else if(collision.gameObject.tag == "Dealer")
        {
            Destroy(this.gameObject);
            Instantiate(_dealer);
            _name = "Dealer";
        }

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

    [SerializeField] Vector2 _lineForGround = new Vector2(2f, -2f);
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
        else if(!hit.collider)
        {
            _jumpcount = 1;
        }
        velo.y = _rb.velocity.y;
        _rb.velocity = velo;
    }

    enum Status
    {
        Normal,
       
    }

}
