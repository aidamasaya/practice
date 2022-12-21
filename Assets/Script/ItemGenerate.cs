using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGenerate : MonoBehaviour
{
    [SerializeField] Transform[] _spawnPoint = null;
    [SerializeField] GameObject[] _ItemPrefabs = null;
    float _spawnInterval = 2f; //¶¬ŠÔŠu
    float _timer; //ŠÔŒv‘ª
    float _RTime;//ƒ‰ƒ“ƒ_ƒ€ŠÔŒv‘ª

    void Start()
    {
        _RTime = Random.Range(5f, 10f);
    }


    void Update()
    {
        _RTime -= Time.deltaTime;
        if (_RTime <= 0.0f)
        {
            _RTime = Random.Range(5f, 10f);
            
            GameObject go = Instantiate(_ItemPrefabs[1]);
            go.transform.position = _spawnPoint[Random.Range(0, _spawnPoint.Length)].position;
        }

        _timer += Time.deltaTime;
        if(_timer > _spawnInterval)
        {
            _timer = 0;
            GameObject go = Instantiate(_ItemPrefabs[0]);
            go.transform.position = _spawnPoint[Random.Range(0, _spawnPoint.Length)].position;
        }
    }
}
