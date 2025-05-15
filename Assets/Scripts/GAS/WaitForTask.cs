using System.Threading.Tasks;
using UnityEngine;
/// <summary>
/// CustomYieldInstruction�� ��ӹ޾� Unity �ڷ�ƾ���� ����� �� �ְ� ����ϴ�.
/// keepWaiting�� true�̸� Unity�� ��� ��ٸ��ϴ�.
/// _task�� �Ϸ�Ǹ� _isDone = true�� ����Ǿ� �ڷ�ƾ ����.
/// </summary>
public class WaitForTask : CustomYieldInstruction
{
    private readonly Task task;
    private bool _isDone = false;
    public override bool keepWaiting => !_isDone;

    public WaitForTask(Task task)
    {
        this.task = task;
        CheckTaskCompletion();
    }

    private async void CheckTaskCompletion()
    {
        await task;
        _isDone = true;
    }

}