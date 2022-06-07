using UnityEngine;

public class EnemyBullet : Bullet
{
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Hero hero))
            ReactToHero(hero);

        base.OnTriggerEnter2D(collision);
    }

    private void ReactToHero(Hero hero)
    {
        hero.ApplyHit();
        Deactivate();
    }
}