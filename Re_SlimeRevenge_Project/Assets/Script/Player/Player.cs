using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : Singleton<Player>
{

    public enum EState
    {
        Walk,
        Skill,
        Shock,
        Eat,
        Die,
    }

    public SpriteRenderer spriteRenderer;
    private bool isStateCheck = false;

    [Header("��ġ�� ������")]
    public EState eState;

    public float maxHp;
    public float currentHp;
    public float hpReductionSpeed;
    public int defense;
    public int moveSpeed;
    public int experienceValue;
    public int getPlayerHP;
    public int getExperienceValue;
    public int specialAbility = 2;



    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        PlayerState();
    }

    private void FixedUpdate()
    {
        PlayerMove();
    }



    // �÷��̾� �̵�
    void PlayerMove()
    {
        if (GameManager.instance._isStartGame == true)
        {
            if (Input.GetKey(KeyCode.Space))
                transform.Translate(0, moveSpeed * Time.deltaTime, 0);
            else
                transform.Translate(0, -moveSpeed * Time.deltaTime, 0);
        }
    }

    // �÷��̾� ������
    void PlayerState()
    {
        maxHp = GameManager.instance.skillHP;

        if (GameManager.instance._isStartGame == true && isStateCheck == false)
        {
            isStateCheck = true;

            currentHp = maxHp;
            defense = (int)GameManager.instance.skillDefense;
            hpReductionSpeed = GameManager.instance.skillRespiration;
        }
    }
}
