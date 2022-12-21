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
    [Tooltip("Windows�̃}�E�X�J�[�\�����Q�[�����ɏ������ǂ����̐ݒ�")]
    [SerializeField] bool _hideCusor = true;
    [Tooltip("���C�t�̏����l")]
    [SerializeField] public int _initialLife = 5;
    [Tooltip("�X�R�A��\�����邽�߂�Text(UI)")]
    [SerializeField] Text _scoreText;
    [Tooltip("�Q�[���X�^�[�g���ɌĂяo������")]
    [SerializeField] UnityEvent _onGameStart = null;
    [Tooltip("�Q�[���I�[�o�[���ɌĂяo������")]
    [SerializeField] UnityEvent _onGameOver = null;
    [Tooltip("�S�X�e�[�W�N���A���ɌĂяo������")]
    [SerializeField] UnityEvent _onGameClear = null;
    [Tooltip("�X�e�[�W�N���A���ƂɌĂяo������")]
    [SerializeField] UnityEvent _onresult = null;�@//�X�e�[�W���N���A����x�ɕ\�������

    /// </summary>���C�t�̌��ݒl</summary>
    [SerializeField] public  int _life;
    /// <summary>�Q�[���̃X�R�A</summary>summary>
    [SerializeField] public static int _score = 0;
    ///<summary>�X�R�A�̑����_</summary>
    [SerializeField] public static int _totalScore = 0;
    /// <summary>�S�Ă̓G�I�u�W�F�N�g�����Ă������߂�List</summary>
    //List<EnemyController> _enemies = null;
    ///<summary>�S�ẴA�C�e���I�u�W�F�N�g�����Ă������߂�List</summary>
    List<ItemController> _Items = null;
    [SerializeField] Text _text = null;

    public const int _scoreconst = 999;
    ItemController _item;
    [SerializeField] int _bulletinscene = 1;

    /// <summary>
    /// ��ʓ��ɘA�ˉ\�ȋ���
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
        SceneManager.LoadScene(6);//���U���g���
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
