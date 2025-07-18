using UnityEngine;

public class FXSystem : MonoBehaviour
{
    [SerializeField] private Transform bodyTrans;

    /// <summary>
    /// Ability Tag에 따른 FX 실행
    /// </summary>
    /// <param name="abilityTag"></param>
    public void ExecuteFX(eTagType abilityTag)
    {
        // FX Object는 독립적인 오브젝트이다.

        var _fx = FXFactory.Instance.GetFX(abilityTag, bodyTrans.position, Quaternion.identity);
        // FX 초기화
    }

    public void ExecuteFX(eTagType abilityTag, Transform parent)
    {
        // FX Object는 독립적인 오브젝트이다.

        var _fx = FXFactory.Instance.GetFX(abilityTag, bodyTrans.position, Quaternion.identity);
        _fx.transform.SetParent(parent);
        // FX 초기화
    }
}
