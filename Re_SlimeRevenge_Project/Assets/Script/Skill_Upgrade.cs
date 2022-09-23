using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Upgrade : MonoBehaviour
{
    public enum Skill_LIst
    {
        HP, // ü��
        Respiration, // ��ȣ��
        Defense, // ����
        Camouflage, // ưư�� ����
        Intellect // ���� �н�
    }
    public Skill_LIst skill_List;


    void Start()
    {

    }

    void Update()
    {

    }

    public void Upgrade_Click()
    {
        switch (skill_List)
        {
            case Skill_LIst.HP: // ������ ü��
                if (GameManager.Inst.HP_Level < 5 && GameManager.Inst.Money >= GameManager.Inst.HP_Price)
                {
                    GameManager.Inst.Money -= GameManager.Inst.HP_Price;
                    GameManager.Inst.HP_Price += 30;
                    GameManager.Inst.HP_Level += 1;
                }
                else
                {

                }
                break;

            case Skill_LIst.Respiration: // ��ȣ��
                if (GameManager.Inst.Respiration_Level < 10 && GameManager.Inst.Money >= GameManager.Inst.Respiration_Price)
                {
                    GameManager.Inst.Money -= GameManager.Inst.Respiration_Price;
                    GameManager.Inst.Respiration_Price += 100;
                    GameManager.Inst.Respiration_Level += 1;
                }
                else
                {

                }
                break;

            case Skill_LIst.Defense: // ź����
                if (GameManager.Inst.Defense_Level < 10 && GameManager.Inst.Money >= GameManager.Inst.Defense_Price)
                {
                    GameManager.Inst.Money -= GameManager.Inst.Defense_Price;
                    GameManager.Inst.Defense_Price += 300;
                    GameManager.Inst.Defense_Level += 1;
                }
                else
                {

                }
                break;

            case Skill_LIst.Camouflage: // ưư�� ����
                if (GameManager.Inst.Camouflage_Level < 5 && GameManager.Inst.Money >= GameManager.Inst.Camouflage_Price)
                {
                    GameManager.Inst.Money -= GameManager.Inst.Camouflage_Price;
                    GameManager.Inst.Camouflage_Price += 150;
                    GameManager.Inst.Camouflage_Level += 1;
                }
                else
                {
                    
                }
                break;

            case Skill_LIst.Intellect: // ���� �н�
                if (GameManager.Inst.Intellect_Level < 5 && GameManager.Inst.Money >= GameManager.Inst.Intellect_Price)
                {

                    GameManager.Inst.Money -= GameManager.Inst.Intellect_Price;
                    GameManager.Inst.Intellect_Price += 150;
                    GameManager.Inst.Intellect_Level += 1;
                }
                else
                {

                }
                break;
        }
    }
}
