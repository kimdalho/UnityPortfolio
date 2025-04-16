using UnityEditor.Playables;
using UnityEngine;

public class Fly : MonoBehaviour
{
    private Transform target;       
    private float radius = 2f;      // 중심에서의 거리
    private float speed = 50f;
    private GA_AlienBody ability;
    private float angle;           // 현재 각도
    public LayerMask targetLayerMask;
    public void SetData(GA_AlienBody _ability, Transform _target)
    {
        ability = _ability;
        target = _target;
        radius = _ability.radius;
        speed = _ability.speed;
        ability.flys.Add(this);
    }


    void Update()
    {
        if (target == null) return;

        // 회전 각도 증가 (시간에 따라)
        angle += speed * Time.deltaTime;
        if (angle > 360f) angle -= 360f;

        // 라디안으로 변환
        float rad = angle * Mathf.Deg2Rad;

        // 중심(target)을 기준으로 회전 위치 계산 (X, Z 평면)
        Vector3 offset = new Vector3(Mathf.Cos(rad), 0.7f, Mathf.Sin(rad)) * radius;
        transform.position = target.position + offset;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Monster")
        {
            // 몬스터에게 피격 -1
            var target = other.GetComponent<Character>();
            GameEffectSelf ge = new GameEffectSelf();
            ge.effect.CurHart = -1;
            target.OnHit(ge);

            Destroy(gameObject);
        }
        else if(other.tag == "Projectile")
        {
            Destroy(gameObject);
        }
    }


    private void OnDestroy()
    {
        DestroyFly();
    }

    private void DestroyFly()
    {
        ability.flys.Remove(this);        
    }
}
