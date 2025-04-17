using UnityEngine;

public class NinjaStars : Projectile
{
    [SerializeField] private float rotSpeed = 720f;

    private void RotateObject()
    {
        transform.Rotate(0f, rotSpeed * Time.deltaTime, 0f);
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        RotateObject();
    }
}
