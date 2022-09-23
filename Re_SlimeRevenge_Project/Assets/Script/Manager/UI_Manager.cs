using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_Manager : MonoBehaviour
{
    public static UI_Manager Inst;

    void Awake() => Inst = this;

    [Header("���� �ݾ�")]
    public TextMeshProUGUI Money_Txt;

    [Header("��ų���� �ؽ�Ʈ")]
    public TextMeshProUGUI HP_Level_Txt; // ü��
    public TextMeshProUGUI Respiration_Level_Txt; // ��ȣ��
    public TextMeshProUGUI Defense_Level_Txt; // ����
    public TextMeshProUGUI Camouflage_Level_Txt; // ưư�� ����
    public TextMeshProUGUI Intellect_Level_Txt; // ���� �н�

    [Header("��ų ���� ��� �ؽ�Ʈ")]
    public TextMeshProUGUI HP_Price_Txt; // ü��
    public TextMeshProUGUI Respiration_Price_Txt; // ��ȣ��
    public TextMeshProUGUI Defense_Price_Txt; // ����
    public TextMeshProUGUI Camouflage_Price_Txt; // ưư�� ����
    public TextMeshProUGUI Intellect_Price_Txt; // ���� �н�

    [Header("��ų ���� �ؽ�Ʈ")]
    public TextMeshProUGUI HP_Content_Text; // ü��
    public TextMeshProUGUI Respiration_Content_Txt; // ��ȣ��
    public TextMeshProUGUI Defense_Content_Txt; // ����
    public TextMeshProUGUI Camouflage_Content_Txt; // ưư�� ����
    public TextMeshProUGUI Intellect_Content_Txt; // ���� �н�

    void Start()
    {

    }

    void Update()
    {
        Amount_Text();

        SkillLevel_Text();
        SkillPrice_Text();
        SkillContent_Text();
    }
    void Amount_Text() => Money_Txt.text = GameManager.Inst.Money.ToString();

    void SkillLevel_Text()
    {
        if (GameManager.Inst.HP_Level < 5) // ü��
            HP_Level_Txt.text = GameManager.Inst.HP_Level.ToString();
        else
            HP_Level_Txt.text = "Max";

        if (GameManager.Inst.Defense_Level < 10) // ź����
            Defense_Level_Txt.text = GameManager.Inst.Defense_Level.ToString();
        else
            Defense_Level_Txt.text = "Max";

        if (GameManager.Inst.Intellect_Level < 5) // �����н�
            Intellect_Level_Txt.text = GameManager.Inst.Intellect_Level.ToString();
        else
            Intellect_Level_Txt.text = "Max";

        if (GameManager.Inst.Camouflage_Level < 5) // ưư�� ����
            Camouflage_Level_Txt.text = GameManager.Inst.Camouflage_Level.ToString();
        else
            Camouflage_Level_Txt.text = "Max";

        if (GameManager.Inst.Respiration_Level < 10) // ��ȣ��
            Respiration_Level_Txt.text = GameManager.Inst.Respiration_Level.ToString();
        else
            Respiration_Level_Txt.text = "Max";
    }

    void SkillPrice_Text()
    {
        if (GameManager.Inst.HP_Level < 5) // ü��
            HP_Price_Txt.text = GameManager.Inst.HP_Price.ToString();
        else
            HP_Price_Txt.text = "Max";

        if (GameManager.Inst.Defense_Level < 10) // ź����
            Defense_Price_Txt.text = GameManager.Inst.Defense_Price.ToString();
        else
            Defense_Price_Txt.text = "Max";

        if (GameManager.Inst.Intellect_Level < 5) // �����н�
            Intellect_Price_Txt.text = GameManager.Inst.Intellect_Price.ToString();
        else
            Intellect_Price_Txt.text = "Max";

        if (GameManager.Inst.Camouflage_Level < 5) // ưư�� ����
            Camouflage_Price_Txt.text = GameManager.Inst.Camouflage_Price.ToString();
        else
            Camouflage_Price_Txt.text = "Max";

        if (GameManager.Inst.Respiration_Level < 10) // ��ȣ��
            Respiration_Price_Txt.text = GameManager.Inst.Respiration_Price.ToString();
        else
            Respiration_Price_Txt.text = "Max";
    }

    void SkillContent_Text()
    {

    }
}
