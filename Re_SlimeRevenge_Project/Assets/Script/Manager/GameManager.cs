using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("소지 금액")]
    private float money;

    public float _money
    {
        get { return money; }
        set { money = value; }
    }

    [Header("스킬")]
    public float skillHP;
    public float skillRespiration;
    public float skillDefense;
    public float skillCamouflage;
    public float skillIntellect;

    [Header("스킬 레벨")]
    public int hpLevel; // 굳건한 체력
    public int respirationLevel; // 심호흡
    public int defenseLevel; // 탄성력
    public int camouflageLevel; // 튼튼한 위장
    public int intellectLevel; // 지능 학습

    [Header("스킬 가격")]
    public float hpPrice;
    public float respirationPrice;
    public float defensePrice;
    public float camouflagePrice;
    public float intellectPrice;

    private bool isStartGame;

    public bool _isStartGame
    {
        get { return isStartGame; }
        set { isStartGame = value; }
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
        skillHP = (200 +(hpLevel * 5));
        skillRespiration = respirationLevel * 0.2f;
        skillDefense = defenseLevel;
        skillCamouflage = camouflageLevel * 5;
        skillIntellect = intellectLevel * 6;
    }

    void Cheat()
    {
        if (Input.GetKeyDown(KeyCode.G))
            money += 10000f;
    }

}
