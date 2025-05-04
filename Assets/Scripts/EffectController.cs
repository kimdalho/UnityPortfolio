using UnityEngine;

public class EffectController : MonoBehaviour
{
    float deltime;

    public float condition = 3;

    private void Update()
    {
        deltime += Time.deltaTime;
        if ( deltime > condition)
        {
            Destroy(gameObject);
        }
    }
}
