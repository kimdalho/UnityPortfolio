using UnityEditor.Playables;
using UnityEngine;

public class Fly : MonoBehaviour
{
    private Transform target;       
    private float radius = 2f;      // �߽ɿ����� �Ÿ�
    private float speed = 50f;
    private GA_AlienBody ability;
    private float angle;           // ���� ����
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

        // ȸ�� ���� ���� (�ð��� ����)
        angle += speed * Time.deltaTime;
        if (angle > 360f) angle -= 360f;

        // �������� ��ȯ
        float rad = angle * Mathf.Deg2Rad;

        // �߽�(target)�� �������� ȸ�� ��ġ ��� (X, Z ���)
        Vector3 offset = new Vector3(Mathf.Cos(rad), 0.7f, Mathf.Sin(rad)) * radius;
        transform.position = target.position + offset;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Monster")
        {
            // ���Ϳ��� �ǰ� -1
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
