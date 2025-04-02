using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ��� �����ۿ� ���� ������ ����
/// �÷��̾ �ʹ� ���ſ����� ������ �� ���� ����
/// </summary>
public partial class Player : Character
{
    public float magnetRange = 3f;  // ��� ����
    public float magnetPower = 1.5f;  // ��� �ӵ� ����ġ
    //�ؽ������� ���� ����
    //����Ʈ�� �翬�� �ߺ��Ǵ�
    //��Ұ��� �� �� ������ �����ϰ�
    //��� �̷� ������ ���鶧 �׻� ��ųʸ��� ����߾�����
    //���� Ű������� ���·� �޸𸮸� ������ �ʿ䰡 ���ٰ� ����
    private HashSet<DroppedItem> nearbyItems = new HashSet<DroppedItem>();

    void DropItemUpdate()
    {
        Vector3 playerPos = transform.position;
        float magnetRangeSqr = magnetRange * magnetRange; // �Ÿ� �� ����ȭ

        foreach (var item in nearbyItems)
        {
            float distanceSqr = (item.transform.position - playerPos).sqrMagnitude;
            if (distanceSqr <= magnetRangeSqr) // ���� ���� ���� ����
            {
                item.AttractToPlayer(playerPos, magnetPower);
            }
        }
    }
}
