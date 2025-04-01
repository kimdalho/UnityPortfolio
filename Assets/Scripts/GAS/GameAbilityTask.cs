using System.Threading.Tasks;
using UnityEngine;
/// <summary>
/// �ִϸ��̼�, ����Ʈ, ��� �� �߰� ���� ����
/// </summary>
public abstract class GameAbilityTask : MonoBehaviour
{
    private float duration;

    public bool IsCompleted { get; protected set; } = false;
    public abstract Task Execute();
}
