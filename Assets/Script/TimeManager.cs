using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimeManager : MonoBehaviour
{
    [SerializeField] Text _text = null;
    [SerializeField] float _timer;
    float _timeInterval;

    void Start()
    {
        
    }

    
    void Update()
    {
        _timer -= Time.deltaTime;
        _text.text = _timer.ToString("f2");
        if(_timer <= 0)
        {
            SceneManager.LoadScene(6);
        }
    }
}
