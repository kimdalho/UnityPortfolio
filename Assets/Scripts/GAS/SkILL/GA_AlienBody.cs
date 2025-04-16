using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Member;
/*
Stunned, //����
ninjahead, ///// �ǰ� �� 10% Ȯ���� 1�ʰ� ����
alienhead, /// 	�ǰ� �� 10% Ȯ���� 1�ʰ� ����
bearhead, /// �� 10�� óġ �� ü�� 1 ȸ��
grasshead, // ���� �� 10% Ȯ���� ���� 1 �߰�
Roostershead, // ����ü�� ���� �� �� ���� 1 �߰�
clownhair, // ����ü 3�� �߻�
boxhead, // 3�ʸ��� �ڵ����� ����ü 1�� �߻�
ninjabody, // �� óġ �� 30�ʰ� ���� 30% ����
alienbody, // 1�и��� 30%Ȯ���� ���� ���� o
beartorso, // ���� �� 10% Ȯ���� ����ü 2�� �¿�� �߰� �߻�
grasstrunk, // ���ݷ� 20% ���� ����
Roostersbody, //�� óġ �� 30%Ȯ���� �ִ� ü�� �߰� ȹ��
clowntorso, // 	��ų ��� �� ���� ����ü�� ���� ȿ�� �߰�
boxbody, // �̵� �� ù ���ݿ� ����ü 2�� �߰� �߻�
*/

public class GA_AlienBody : GameAbility 
{
    //���� Ȯ�� 0~1; 1�̸� 100�ۼ�Ʈ ����
    public float condtionper;
    /// �ߵ� ������ 30�̸� 30�ʸ��� Ʈ����
    public float conditionSec;
    [Header("�ö��� ����")]
    public float radius = 2f;      // �߽ɿ����� �Ÿ�
    public float speed = 50f;

    public int maxCount = 2; //���� ������ ���� ��

    public List<Fly> flys;

    float deltime;

    


    private void Start()
    {
        flys = new List<Fly>();
    }


    protected override IEnumerator ExecuteAbility()
    {
        owner.gameplayTagSystem.AddTag(AbilityTag);
        while (true)
        {
            if (flys.Count >= maxCount)
                yield return new WaitForSeconds(3f);

            deltime += Time.deltaTime;
            Debug.Log($"deltime {deltime}");
            if (deltime > conditionSec)
            {
                Debug.Log("Ÿ�� ���Ǽ���");
                float chance = Random.value; // 0.0 ~ 1.0
                Debug.Log($"���� {chance}");
                if (chance < condtionper) //0.3 �̸� 30�ۼ�Ʈ ����
                {
                    deltime = 0;
                    Fly fly =  ResourceManager.Instance.CreateEntity();
                    fly.gameObject.transform.SetParent(owner.transform);
                    fly.SetData(this,owner.transform);

                }
            }            
            yield return null;
        }        
    }

    public void RemoveFly(Fly fly)
    {
        flys.Remove(fly);
    }

    
}