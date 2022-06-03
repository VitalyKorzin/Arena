using UnityEngine;

public class EnemyBullet : Bullet
{
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Hero hero))
        {
            hero.ApplyHit();
            Deactivate();
        }

        base.OnTriggerEnter2D(collision);
    }
}