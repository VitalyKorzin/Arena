using UnityEngine;

public class HeroBullet : Bullet
{
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Enemy enemy))
            ReactToEnemy(enemy);

        base.OnTriggerEnter2D(collision);
    }

    private void ReactToEnemy(Enemy enemy)
    {
        enemy.Kill();
        Deactivate();
    }
}