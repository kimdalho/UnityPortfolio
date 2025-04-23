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
        try
        {
            var _fx = FXFactory.Instance.GetFX(abilityTag, bodyTrans.position, Quaternion.identity);
            // FX �ʱ�ȭ
        }
        catch (System.Exception e)
        {
            Debug.LogError($"FXSystem Error: {e}");
        }
    }
}
