using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour, IListener
{
    public static GameManager instance;
    float distanceTimer = 0;

    #region ��ų ����
    [Header("��ų")]
    public float skillHP;
    public float skillRespiration;
    public float skillDefense;
    public float skillCamouflage;
    public float skillIntellect;

    [Header("��ų ����")]
    public int hpLevel; // ������ ü��
    public int respirationLevel; // ��ȣ��
    public int defenseLevel; // ź����
    public int camouflageLevel; // ưư�� ����
    public int intellectLevel; // ���� �н�

    [Header("��ų ����")]
    public float hpPrice;
    public float respirationPrice;
    public float defensePrice;
    public float camouflagePrice;
    public float intellectPrice;
    #endregion

    [Header("���� �ݾ�")]
    private float money;

    public float _money
    {
        get { return money; }
        set { money = value; }
    }

    private bool isStartGame;

    public bool _isStartGame
    {
        get { return isStartGame; }
        set { isStartGame = value; }
    }

    private int distance;

    public int _distance
    {
        get { return distance; }
        set { distance = value; }
    }

    private int maximumdistance;

    public int _maximumdistance
    {
        get { return maximumdistance; }
        set { maximumdistance = value; }
    }

    void Start()
    {
        hpPrice = 30;
        respirationPrice = 100;
        defensePrice = 300;
        camouflagePrice = 150;
        intellectPrice = 150;
    }

    void Update()
    {
        Cheat();
        Skill();
        Distance();
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    void Skill()
    {
        skillHP = (200 + (hpLevel * 5));
        skillRespiration = respirationLevel * 0.2f;
        skillDefense = defenseLevel;
        skillCamouflage = camouflageLevel * 5;
        skillIntellect = intellectLevel * 6;
    }

    void Distance()
    {
        distanceTimer += Time.deltaTime;

        if (isStartGame == true)
        {
            if (distanceTimer >= 0.8f)
            {
                distanceTimer = 0;
                distance += 1;
            }
        }
    }

    void Cheat()
    {
        if (Input.GetKeyDown(KeyCode.G))
            money += 10000f;
    }

    public void Event(EventType type)
    {

    }
}
