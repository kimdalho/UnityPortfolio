using System.Threading.Tasks;
using UnityEngine;
/// <summary>
/// 애니메이션, 이펙트, 대기 등 추가 로직 관리
/// </summary>
public abstract class GameAbilityTask : MonoBehaviour
{
    private float duration;

    public bool IsCompleted { get; protected set; } = false;
    public abstract Task Execute();
}
