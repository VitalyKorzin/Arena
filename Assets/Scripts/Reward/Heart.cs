using UnityEngine;

public class Heart : Reward
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Hero hero))
            ReactToHero(hero);
    }

    private void ReactToHero(Hero hero)
    {
        hero.Heal();
        Deactivate();
    }
}