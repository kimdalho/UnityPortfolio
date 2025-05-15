using System.Threading.Tasks;
using UnityEngine;
/// <summary>
/// CustomYieldInstruction을 상속받아 Unity 코루틴에서 사용할 수 있게 만듭니다.
/// keepWaiting이 true이면 Unity는 계속 기다립니다.
/// _task가 완료되면 _isDone = true로 변경되어 코루틴 진행.
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