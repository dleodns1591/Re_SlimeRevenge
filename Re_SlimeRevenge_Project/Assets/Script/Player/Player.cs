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
    bool isStateCheck = false;
    bool isReuse = false;

    [Header("��ġ�� ������")]
    public EState eState;

    public float maxHp;
    public float currentHp;
    public float hpReductionSpeed;
    public int defense;
    public int moveSpeed;
    public float maxExperience;
    public float currentExperience;
    public float getHP;
    public float getExperience;

    public int specialAbility = 2;
    public int specialAbilityCount;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

    }

    void Update()
    {
        PlayerState();
        StartCoroutine(PlayerSkill());
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

    // �÷��̾� Ư���ɷ�
    IEnumerator PlayerSkill()
    {
        float posY = 0.5f;

        if (Input.GetKeyDown(KeyCode.Z) && specialAbilityCount > 0 && isReuse == false)
        {
            isReuse = true;

            --specialAbilityCount;
            eState = EState.Skill;

            for (int i = 0; i <= EnemySpawn.instance.transform.childCount - 1; i++)
            {
                Transform enemyPos = EnemySpawn.instance.transform.GetChild(i).transform;

                if (enemyPos.position.x <= -3)
                {
                    if (transform.position.y >= enemyPos.position.y && transform.position.y - enemyPos.position.y <= posY)
                    {
                        enemyPos.transform.DOKill();
                        Destroy(enemyPos.gameObject);
                    }
                    else
                    {
                        enemyPos.transform.DOKill();
                        Destroy(enemyPos.gameObject);
                    }

                    break;
                }
                else
                    Debug.Log("�ȵ�");
            }

            yield return new WaitForSeconds(1f);

            isReuse = false;
            eState = EState.Walk;
        }
    }
}
