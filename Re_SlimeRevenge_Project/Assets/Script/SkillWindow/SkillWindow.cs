using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

public class SkillWindow : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public enum EskillWindow
    {
        Top,
        Among,
        Bottom,
    }
    public EskillWindow eskillWindow;

    public GameObject skillWindow;

    [Header("왼쪽")]
    [SerializeField] Image selectBarTop;
    [SerializeField] Image mouseRangeTop;
    [SerializeField] GameObject windowTop;
    [SerializeField] GameObject barUpTop;
    [SerializeField] GameObject barDownTop;
    [SerializeField] RectTransform rectSkillTop;

    [Header("가운데")]
    [SerializeField] Image selectBarAmong;
    [SerializeField] Image mouseRangeAmong;
    [SerializeField] GameObject windowAmong;
    [SerializeField] GameObject barUpAmong;
    [SerializeField] GameObject barDownAmong;
    [SerializeField] RectTransform rectSkillAmong;

    [Header("오른쪽")]
    [SerializeField] Image selectBarBottom;
    [SerializeField] Image mouseRangeBottom;
    [SerializeField] GameObject windowBottom;
    [SerializeField] GameObject barUpBottom;
    [SerializeField] GameObject barDownBottom;
    [SerializeField] RectTransform rectSkillBottom;

    public const int windowWidth = 545;
    public const int windowHeight = 845;
    public const float barOpenSpeed = 0.45f;
    public const float barCloseSpeed = 0.35f;

    float timer = 0.0f;

    bool isOpenCheck = false;

    void Start()
    {
        StartCoroutine(SkillWindowOpen());
    }

    void Update()
    {
        timer += Time.unscaledDeltaTime * 2.5f;
    }

    IEnumerator SkillWindowOpen()
    {
        int barOpenPosY = 440;

        barUpTop.transform.DOLocalMoveY(barOpenPosY, barOpenSpeed).SetEase(Ease.Linear).SetUpdate(true);
        barDownTop.transform.DOLocalMoveY(-barOpenPosY, barOpenSpeed).SetEase(Ease.Linear).SetUpdate(true);

        barUpAmong.transform.DOLocalMoveY(barOpenPosY, barOpenSpeed).SetEase(Ease.Linear).SetUpdate(true);
        barDownAmong.transform.DOLocalMoveY(-barOpenPosY, barOpenSpeed).SetEase(Ease.Linear).SetUpdate(true);

        barUpBottom.transform.DOLocalMoveY(barOpenPosY, barOpenSpeed).SetEase(Ease.Linear).SetUpdate(true);
        barDownBottom.transform.DOLocalMoveY(-barOpenPosY, barOpenSpeed).SetEase(Ease.Linear).SetUpdate(true);

        while (timer < 1)
        {
            //rectSkillTop.sizeDelta = Vector2.Lerp(windowSize * Vector2.right, windowSize, timer);

            rectSkillTop.sizeDelta = new Vector2(windowWidth, Mathf.Lerp(0, windowHeight, timer));
            rectSkillAmong.sizeDelta = new Vector2(windowWidth, Mathf.Lerp(0, windowHeight, timer));
            rectSkillBottom.sizeDelta = new Vector2(windowWidth, Mathf.Lerp(0, windowHeight, timer));
            yield return null;
        }
        isOpenCheck = true;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isOpenCheck == true)
        {
            switch (eskillWindow)
            {
                case EskillWindow.Top:
                    selectBarTop.DOFade(1, 0.2f).SetEase(Ease.Linear).SetUpdate(true);
                    break;
                case EskillWindow.Among:
                    selectBarAmong.DOFade(1, 0.2f).SetEase(Ease.Linear).SetUpdate(true);
                    break;
                case EskillWindow.Bottom:
                    selectBarBottom.DOFade(1, 0.2f).SetEase(Ease.Linear).SetUpdate(true);
                    break;
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (isOpenCheck == true)
        {
            switch (eskillWindow)
            {
                case EskillWindow.Top:
                    selectBarTop.DOFade(0, 0.2f).SetEase(Ease.Linear).SetUpdate(true);
                    break;
                case EskillWindow.Among:
                    selectBarAmong.DOFade(0, 0.2f).SetEase(Ease.Linear).SetUpdate(true);
                    break;
                case EskillWindow.Bottom:
                    selectBarBottom.DOFade(0, 0.2f).SetEase(Ease.Linear).SetUpdate(true);
                    break;
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Time.timeScale = 1;
        Player.Instance.currentExperience = 0;
        StartCoroutine(SkillWindowClick());
    }

    IEnumerator SkillWindowClick()
    {
        timer = 0;
        int barClosePosY = 35;

        mouseRangeTop.raycastTarget = false;
        mouseRangeAmong.raycastTarget = false;
        mouseRangeBottom.raycastTarget = false;

        switch (eskillWindow)
        {
            case EskillWindow.Top:
                for (int i = 0; i < SkillManager.instance.skillCount; i++)
                {
                    if (SkillManager.instance.skill[i].skillName == SkillManager.instance.skillTop.skillName)
                        ++SkillManager.instance.skill[i].skillLevel;
                }

                switch (SkillManager.instance.skillTop.eskill)
                {
                    case SkillData.Eskill.Vitality:
                        Debug.Log("끈질긴 생명력");
                        Player.Instance.hpReductionSpeed += 0.1f;
                        break;

                    case SkillData.Eskill.Shell:
                        Debug.Log("거북이 등껍질");
                        Player.Instance.defense += 2;
                        break;

                    case SkillData.Eskill.Exercise:
                        Debug.Log("근육운동");
                        Player.Instance.currentHp += ((Player.Instance.maxHp * 20) / 100);
                        break;

                    case SkillData.Eskill.Predator:
                        Debug.Log("포식자");
                        Player.Instance.getExperience += 0.15f;
                        break;

                    case SkillData.Eskill.SumptuousFeast:
                        Debug.Log("진수성찬");
                        Player.Instance.getExperience += 0.2f;
                        break;

                    case SkillData.Eskill.EnergyBomb:
                        Debug.Log("에너지탄");
                        if (SkillManager.instance.isEnergyBombClick == false)
                        {
                            SkillManager.instance.isEnergyBombClick = true;
                            Instantiate(SkillManager.instance.energyBomb, new Vector2(Player.Instance.transform.position.x, Player.Instance.transform.position.y + 0.5f), Quaternion.identity);
                        }
                        else
                            --SkillManager.instance.energyBombCoolTime;
                        break;

                    case SkillData.Eskill.SlimeBomb:
                        Debug.Log("슬라임탄");
                        if (SkillManager.instance.isSlimeBombClick == false)
                        {
                            SkillManager.instance.isSlimeBombClick = true;
                            Instantiate(SkillManager.instance.slimeBomb, new Vector2(Player.Instance.transform.position.x, Player.Instance.transform.position.y + 0.5f), Quaternion.Euler(0, -180, 0));
                        }
                        else
                           --SkillManager.instance.slimeBombCoolTime;
                        break;

                    case SkillData.Eskill.Resurrection:
                        Debug.Log("부활");
                        --SkillManager.instance.skillCount;
                        SkillManager.instance.isResurrectionCheck = true;
                        break;
                }


                #region 창 닫기
                selectBarTop.DOFade(0, 0).SetEase(Ease.Linear);

                barUpTop.transform.DOLocalMoveY(barClosePosY, barCloseSpeed).SetEase(Ease.Linear).SetUpdate(true);
                barDownTop.transform.DOLocalMoveY(-barClosePosY, barCloseSpeed).SetEase(Ease.Linear).SetUpdate(true);

                windowAmong.transform.DOLocalMoveY(1200, 0.5f).SetUpdate(true);
                windowBottom.transform.DOLocalMoveY(1200, 0.5f).SetUpdate(true);

                while (timer < 1)
                {
                    rectSkillTop.sizeDelta = new Vector2(windowWidth, Mathf.Lerp(windowHeight, 0, timer));
                    yield return null;
                }

                barUpTop.transform.DOKill();
                barDownTop.transform.DOKill();
                windowAmong.transform.DOKill();
                windowBottom.transform.DOKill();

                Destroy(skillWindow);
                #endregion
                break;

            case EskillWindow.Among:
                for (int i = 0; i < SkillManager.instance.skillCount; i++)
                {
                    if (SkillManager.instance.skill[i].skillName == SkillManager.instance.skillAmong.skillName)
                        ++SkillManager.instance.skill[i].skillLevel;
                }

                switch (SkillManager.instance.skillAmong.eskill)
                {
                    case SkillData.Eskill.Vitality:
                        Debug.Log("끈질긴 생명력");
                        Player.Instance.hpReductionSpeed += 0.1f;
                        break;

                    case SkillData.Eskill.Shell:
                        Debug.Log("거북이 등껍질");
                        Player.Instance.defense += 2;
                        break;

                    case SkillData.Eskill.Exercise:
                        Debug.Log("근육운동");
                        Player.Instance.currentHp += ((Player.Instance.maxHp * 20) / 100);
                        break;

                    case SkillData.Eskill.Predator:
                        Debug.Log("포식자");
                        Player.Instance.getExperience += 0.15f;
                        break;

                    case SkillData.Eskill.SumptuousFeast:
                        Debug.Log("진수성찬");
                        Player.Instance.getExperience += 0.2f;
                        break;

                    case SkillData.Eskill.EnergyBomb:
                        Debug.Log("에너지탄");
                        if (SkillManager.instance.isEnergyBombClick == false)
                        {
                            SkillManager.instance.isEnergyBombClick = true;
                            Instantiate(SkillManager.instance.energyBomb, new Vector2(Player.Instance.transform.position.x, Player.Instance.transform.position.y + 0.5f), Quaternion.identity);
                        }
                        else
                            --SkillManager.instance.energyBombCoolTime;
                        break;

                    case SkillData.Eskill.SlimeBomb:
                        Debug.Log("슬라임탄");
                        if (SkillManager.instance.isSlimeBombClick == false)
                        {
                            SkillManager.instance.isSlimeBombClick = true;
                            Instantiate(SkillManager.instance.slimeBomb, new Vector2(Player.Instance.transform.position.x, Player.Instance.transform.position.y + 0.5f), Quaternion.Euler(0, -180, 0));
                        }
                        else
                            --SkillManager.instance.slimeBombCoolTime;
                        break;

                    case SkillData.Eskill.Resurrection:
                        Debug.Log("부활");
                        --SkillManager.instance.skillCount;
                        SkillManager.instance.isResurrectionCheck = true;
                        break;
                }

                #region 창 닫기
                selectBarAmong.DOFade(0, 0).SetEase(Ease.Linear);

                barUpAmong.transform.DOLocalMoveY(barClosePosY, barCloseSpeed).SetEase(Ease.Linear).SetUpdate(true);
                barDownAmong.transform.DOLocalMoveY(-barClosePosY, barCloseSpeed).SetEase(Ease.Linear).SetUpdate(true);

                windowTop.transform.DOLocalMoveY(1200, 0.5f).SetUpdate(true);
                windowBottom.transform.DOLocalMoveY(1200, 0.5f).SetUpdate(true);

                while (timer < 1)
                {
                    rectSkillAmong.sizeDelta = new Vector2(windowWidth, Mathf.Lerp(windowHeight, 0, timer));
                    yield return null;
                }

                barUpAmong.transform.DOKill();
                barDownAmong.transform.DOKill();
                windowTop.transform.DOKill();
                windowBottom.transform.DOKill();

                Destroy(skillWindow);
                #endregion
                break;

            case EskillWindow.Bottom:
                for (int i = 0; i < SkillManager.instance.skillCount; i++)
                {
                    if (SkillManager.instance.skill[i].skillName == SkillManager.instance.skillBottom.skillName)
                        ++SkillManager.instance.skill[i].skillLevel;
                }

                switch (SkillManager.instance.skillBottom.eskill)
                {
                    case SkillData.Eskill.Vitality:
                        Debug.Log("끈질긴 생명력");
                        Player.Instance.hpReductionSpeed += 0.1f;
                        break;

                    case SkillData.Eskill.Shell:
                        Debug.Log("거북이 등껍질");
                        Player.Instance.defense += 2;
                        break;

                    case SkillData.Eskill.Exercise:
                        Debug.Log("근육운동");
                        Player.Instance.currentHp += ((Player.Instance.maxHp * 20) / 100);
                        break;

                    case SkillData.Eskill.Predator:
                        Debug.Log("포식자");
                        Player.Instance.getExperience += 0.15f;
                        break;

                    case SkillData.Eskill.SumptuousFeast:
                        Debug.Log("진수성찬");
                        Player.Instance.getExperience += 0.2f;
                        break;

                    case SkillData.Eskill.EnergyBomb:
                        Debug.Log("에너지탄");
                        if (SkillManager.instance.isEnergyBombClick == false)
                        {
                            SkillManager.instance.isEnergyBombClick = true;
                            Instantiate(SkillManager.instance.energyBomb, new Vector2(Player.Instance.transform.position.x, Player.Instance.transform.position.y + 0.5f), Quaternion.identity);
                        }
                        else
                            --SkillManager.instance.energyBombCoolTime;
                        break;

                    case SkillData.Eskill.SlimeBomb:
                        if (SkillManager.instance.isSlimeBombClick == false)
                        {
                            Debug.Log("슬라임탄");
                            SkillManager.instance.isSlimeBombClick = true;
                            Instantiate(SkillManager.instance.slimeBomb, new Vector2(Player.Instance.transform.position.x, Player.Instance.transform.position.y + 0.5f), Quaternion.Euler(0, -180, 0));
                        }
                        else
                            --SkillManager.instance.slimeBombCoolTime;
                        break;

                    case SkillData.Eskill.Resurrection:
                        Debug.Log("부활");
                        --SkillManager.instance.skillCount;
                        SkillManager.instance.isResurrectionCheck = true;
                        break;
                }

                #region 창 닫기
                selectBarBottom.DOFade(0, 0).SetEase(Ease.Linear);

                barUpBottom.transform.DOLocalMoveY(barClosePosY, barCloseSpeed).SetEase(Ease.Linear).SetUpdate(true);
                barDownBottom.transform.DOLocalMoveY(-barClosePosY, barCloseSpeed).SetEase(Ease.Linear).SetUpdate(true);

                windowTop.transform.DOLocalMoveY(1200, 0.5f).SetUpdate(true);
                windowAmong.transform.DOLocalMoveY(1200, 0.5f).SetUpdate(true);

                while (timer < 1)
                {
                    rectSkillBottom.sizeDelta = new Vector2(windowWidth, Mathf.Lerp(windowHeight, 0, timer));
                    yield return null;
                }

                barUpBottom.transform.DOKill();
                barDownBottom.transform.DOKill();
                windowTop.transform.DOKill();
                windowAmong.transform.DOKill();

                Destroy(skillWindow);
                #endregion
                break;
        }
    }
}