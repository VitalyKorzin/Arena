using UnityEngine;

public class Diamond : Reward
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out RewardsCollector collector))
            ReactToCollector(collector);
    }

    private void ReactToCollector(RewardsCollector collector)
    {
        collector.AddDiamond();
        Deactivate();
    }
}