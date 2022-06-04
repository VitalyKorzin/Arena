using UnityEngine;

public class HeroBullet : Bullet
{
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Enemy enemy))
        {
            enemy.Kill();
            Deactivate();
        }

        base.OnTriggerEnter2D(collision);
    }
}