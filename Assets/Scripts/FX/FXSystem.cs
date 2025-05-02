using UnityEngine;

public class FXSystem : MonoBehaviour
{
    [SerializeField] private Transform bodyTrans;

    /// <summary>
    /// Ability Tag�� ���� FX ����
    /// </summary>
    /// <param name="abilityTag"></param>
    public void ExecuteFX(eTagType abilityTag)
    {
        // FX Object�� �������� ������Ʈ�̴�.

        var _fx = FXFactory.Instance.GetFX(abilityTag, bodyTrans.position, Quaternion.identity);
        // FX �ʱ�ȭ
    }

    public void ExecuteFX(eTagType abilityTag, Transform parent)
    {
        // FX Object�� �������� ������Ʈ�̴�.

        var _fx = FXFactory.Instance.GetFX(abilityTag, bodyTrans.position, Quaternion.identity);
        _fx.transform.SetParent(parent);
        // FX �ʱ�ȭ
    }
}
