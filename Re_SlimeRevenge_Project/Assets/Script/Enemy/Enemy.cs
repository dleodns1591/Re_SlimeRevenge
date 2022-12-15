using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Enemy : MonoBehaviour
{
    public static Enemy instance;
    void Awake() => instance = this;

    public enum Eenemy
    {
        Noob1,
        Noob2,
        Shieldbearer,
        Bargate,
        Swordman,
        Archer,
        HeavyCavalry,
        Berserker,
    }

    public enum EMove
    {
        None,
        BackMove,
        ForwardMove,
        Die
    }

    public enum Espeed
    {
        Slow,
        Usual,
        Fast,
    }

    [Header("수치적 데이터")]
    public Eenemy eenemy;
    public EMove emove;
    public Espeed espeed;

    public int hp;
    public int attack;
    public int bigBoneNum;
    public int smallBoneNum;
    public int boneFestivalNum;
    private float moveSpeed;
    private float archerAttackTimer = 0.0f;
    public bool isKnockBack;
    public bool isCollsionAttack;

    [Space(10)]
    bool isArrow;
    public GameObject arrow;

    [Space(10)]
    public GameObject bigBone;
    public GameObject smallBone;

    Animator animator;
    Rigidbody2D rb2D;
    SpriteRenderer spriteRenderer;
    BoxCollider2D boxCollider2D;


    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        EnemyMove();
        StateAnimation();
    }


    void EnemyMove()
    {
        switch (emove)
        {
            case EMove.None:
                break;
            case EMove.BackMove:
                isKnockBack = true;
                if ((int)rb2D.velocity.x <= 0)
                    emove = EMove.ForwardMove;
                break;
            case EMove.ForwardMove:
                isKnockBack = false;
                transform.Translate(moveSpeed * Time.deltaTime, 0, 0);
                break;
            case EMove.Die:
                boxCollider2D.enabled = false;
                transform.DOLocalMove(new Vector2(Player.Instance.transform.position.x, Player.Instance.transform.position.y + 0.5f), 0.5f);
                break;
        }

        switch (espeed)
        {
            case Espeed.Slow:
                moveSpeed = -2;
                break;
            case Espeed.Usual:
                moveSpeed = -3f;
                break;
            case Espeed.Fast:
                moveSpeed = -5f;
                break;

        }
    }

    #region 적 애니메이션
    private void StateAnimation()
    {
        archerAttackTimer += Time.deltaTime;

        switch (eenemy)
        {
            case Eenemy.Shieldbearer:
                animator.SetInteger("Idle", hp);
                break;

            case Eenemy.Swordman:
                animator.SetBool("isKnockBack", isKnockBack);

                if (emove == EMove.ForwardMove)
                {
                    if (transform.position.x <= -4.5f && Player.Instance.transform.position.y + 0.5f >= transform.position.y && Player.Instance.transform.position.y - 0.5f <= transform.position.y)
                        animator.SetBool("Attack", true);
                    else
                        animator.SetInteger("Walk", hp);
                }
                else if (emove == EMove.BackMove)
                    animator.SetBool("Attack", false);
                break;

            case Eenemy.Archer:
                animator.SetBool("isKnockBack", isKnockBack);

                if (emove == EMove.ForwardMove)
                {
                    if (1 < archerAttackTimer && hp == 2)
                    {
                        animator.SetBool("Attack", true);

                        if (isArrow == false && transform.position.x >= -5)
                        {
                            isArrow = true;
                            Instantiate(arrow, new Vector2(transform.localPosition.x - 0.55f, transform.localPosition.y + 0.5f), Quaternion.identity);
                        }

                        if (1.5f < archerAttackTimer)
                        {
                            animator.SetBool("Attack", false);
                            isArrow = false;
                            archerAttackTimer = 0;
                        }
                    }
                    else
                        animator.SetInteger("Walk", hp);

                }
                break;

            case Eenemy.HeavyCavalry:
                EnemyState();
                break;

            case Eenemy.Berserker:
                EnemyState();
                break;
        }
    }

    void EnemyState()
    {
        animator.SetBool("isKnockBack", instance.isKnockBack);

        if (emove == EMove.ForwardMove)
            animator.SetInteger("Walk", hp);
        else if (emove == EMove.BackMove)
            animator.SetInteger("KnockBack", hp);
    }
    #endregion

    private IEnumerator OnTriggerEnter2D(Collider2D collision)
    {
        float waitTime = 0.5f;
        int playerInvincibility = 3;

        float bone = ((20 * bigBoneNum) + (10 * smallBoneNum));

        if (collision.CompareTag("Player"))
        {
            --hp;

            if (hp == 0)
            {
                emove = EMove.Die;
                Player.Instance.eState = Player.EState.Eat;
                spriteRenderer.DOFade(0, waitTime);
                transform.DOScale(new Vector2(0.1f, 0.1f), waitTime);
                transform.DORotate(new Vector3(0, 0, -180), waitTime);


                Player.Instance.currentHp += bone + Player.Instance.getHP;
                Player.Instance.currentExperience += bone + Player.Instance.getExperience;

                yield return new WaitForSeconds(2f);
                Player.Instance.tag = "Player";
                Player.Instance.spriteRenderer.DOKill();
                Player.Instance.spriteRenderer.DOFade(1, 0);

                Player.Instance.eState = Player.EState.Walk;
                transform.DOKill();
                Destroy(gameObject);
            }

            else
            {
                //넉백
                emove = EMove.BackMove;
                rb2D.AddForce(new Vector2(7, 0), ForceMode2D.Impulse);

                if (isCollsionAttack == true)
                {
                    // 공격력 만큼 플레이어 체력 차감
                    Player.Instance.currentHp -= (attack - Player.Instance.defense);

                    // 무적 처리
                    Player.Instance.eState = Player.EState.Shock;
                    Player.Instance.tag = "invincibility";
                    Player.Instance.spriteRenderer.DOFade(0.5f, waitTime).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);

                    yield return new WaitForSeconds(playerInvincibility);

                    Player.Instance.spriteRenderer.DOKill();
                    Player.Instance.spriteRenderer.DOFade(1, 0);

                    Player.Instance.eState = Player.EState.Walk;
                    Player.Instance.tag = "Player";
                }
            }


        }

        if (collision.CompareTag("DestroyBox"))
            Destroy(gameObject);

        if (collision.CompareTag("Bomb"))
        {
            --hp;
            if (hp == 0)
            {
                transform.DOKill();
                Destroy(gameObject);

                if (bigBoneNum > 0)
                {
                    for (int i = 0; i < bigBoneNum; i++)
                    {
                        int randomRot = Random.Range(-180, 0);
                        Instantiate(bigBone, transform.position, Quaternion.Euler(0, 0, randomRot));
                    }
                }

                if (smallBoneNum + SkillManager.instance.skill[7].skillLevel > 0)
                {
                    for (int i = 0; i < smallBoneNum; i++)
                    {
                        int randomRot = Random.Range(-180, 0);
                        Instantiate(smallBone, transform.position, Quaternion.Euler(0, 0, randomRot));
                    }
                }

                //뼈 소환
            }
            else
            {
                emove = EMove.BackMove;
                rb2D.AddForce(new Vector2(7, 0), ForceMode2D.Impulse);
            }
        }
    }
}
