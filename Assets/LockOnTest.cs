using UnityEngine;

public class LockOnTest : MonoBehaviour ,ILockOnTarget
{
    public bool GetDead()
    {
        return false;
    }

    public Transform GetLockOnTransform()
    {
        return transform;
    }

}
