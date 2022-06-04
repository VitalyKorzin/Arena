using UnityEngine;

public class Diamond : Reward
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out RewardsCollector collector))
        {
            collector.AddDiamond();
            Deactivate();
        }
    }
}