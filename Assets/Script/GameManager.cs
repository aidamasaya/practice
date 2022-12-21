using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }
    [Tooltip("Windowsのマウスカーソルをゲーム中に消すかどうかの設定")]
    [SerializeField] bool _hideCusor = true;
    [Tooltip("ライフの初期値")]
    [SerializeField] public int _initialLife = 5;
    [Tooltip("スコアを表示するためのText(UI)")]
    [SerializeField] Text _scoreText;
    [Tooltip("ゲームスタート時に呼び出す処理")]
    [SerializeField] UnityEvent _onGameStart = null;
    [Tooltip("ゲームオーバー時に呼び出す処理")]
    [SerializeField] UnityEvent _onGameOver = null;
    [Tooltip("全ステージクリア時に呼び出す処理")]
    [SerializeField] UnityEvent _onGameClear = null;
    [Tooltip("ステージクリアごとに呼び出す処理")]
    [SerializeField] UnityEvent _onresult = null;　//ステージをクリアする度に表示される

    /// </summary>ライフの現在値</summary>
    [SerializeField] public  int _life;
    /// <summary>ゲームのスコア</summary>summary>
    [SerializeField] public static int _score = 0;
    ///<summary>スコアの総得点</summary>
    [SerializeField] public static int _totalScore = 0;
    /// <summary>全ての敵オブジェクトを入れておくためのList</summary>
    //List<EnemyController> _enemies = null;
    ///<summary>全てのアイテムオブジェクトを入れておくためのList</summary>
    List<ItemController> _Items = null;
    [SerializeField] Text _text = null;

    public const int _scoreconst = 999;
    ItemController _item;
    [SerializeField] int _bulletinscene = 1;

    /// <summary>
    /// 画面内に連射可能な球数
    /// </summary>
    public int BulletInScene
    {
        get { return _bulletinscene; }
        set { _bulletinscene = value; }
    }

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
         GameStart();
        _text.gameObject.SetActive(true);
        _life = _initialLife;
        _Items = GameObject.FindObjectsOfType<ItemController>().ToList();
        _scoreText.text = _totalScore.ToString("d2");
        _item = GetComponent<ItemController>();
        _hideCusor = true;
    }

    public void Restart()
    {
        Debug.Log("Restart");
        _Items.ForEach(Item => Item.gameObject.SetActive(true));
        Start();
    }

   
    void Update()
    {
        if (_scoreconst < _score)
        {
            _score = _scoreconst;
        }
    }
    public void GameStart()
    {
        _onGameStart.Invoke();
        if(_hideCusor)
        {
           Cursor.visible = false;
        }
    }
    public void GameOver()
    {
        Debug.Log("Gameover");
        _Items.ForEach(Item => Item.gameObject.SetActive(false));
        _onGameOver.Invoke();
        _score = 0;
        SceneManager.LoadScene(5);
    }
    public bool GameClear()
    {
        _onGameClear.Invoke();
        SceneManager.LoadScene(6);//リザルト画面
        return true;
    }
    
    public void AddScore(int score)
    { 
        _totalScore += score;
        _scoreText.text = _totalScore.ToString("d2");
    }

    public void Damage(int damage)
    {
         _life -= damage;
    }
}
