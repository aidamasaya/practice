using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class HPController : MonoBehaviour
{
    [Header("ハートのスプライト　: プレハブ"), SerializeField]
    GameObject _HPPrefab;
    GameManager _game;
    [Header("GameManagerのタグ"), SerializeField] string _GameManagerTag;
   
    void Start()
    {
        _game = GameObject.FindGameObjectWithTag(_GameManagerTag).GetComponent<GameManager>();
        for(int i = 0; i < _game._life; i++)
        {
            Instantiate(_HPPrefab, transform);
        }
    }

    void Update()
    {
        if (transform.childCount != _game._life)
        {
            if (transform.childCount < _game._life)
            {
                for (int i = 0; i < Mathf.Abs(transform.childCount - _game._life); i++)
                {
                    Instantiate(_HPPrefab, transform);
                }
            }

            else if (transform.childCount > _game._life)
            {
                for (int i = 0; i < Mathf.Abs(transform.childCount - _game._life); i++)
                {
                    if (transform.GetChild(i) != null)
                    {
                        Destroy(transform.GetChild(i).gameObject);
                    }
                    if (transform.GetChild(i) == null)
                    {
                        SceneManager.LoadScene(7);
                    }
                }
            }
        }
    }
}
