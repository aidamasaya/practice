using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
public class ItemController : MonoBehaviour
{
    Rigidbody2D _rb;
    [SerializeField] float _falling = 0.8f;

    public int _addScore = 1;
    GameManager _gamemaneger;
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

  
    void Update()
    {
        Vector2 velocity = _rb.velocity;
        velocity.y -= _falling;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Destroy(this.gameObject);
            GameManager.instance.AddScore(_addScore);
        }
        if(collision.gameObject.tag == "Ground")
        {
            Destroy(this.gameObject);
        }
    }
}
