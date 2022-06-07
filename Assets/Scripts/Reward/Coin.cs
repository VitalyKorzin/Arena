using System.Collections;
using UnityEngine;

public class Coin : Reward
{
    private Coroutine _movementJob;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Magnet magnet) && magnet.IsActive)
            ReactToMagnet(magnet);

        if (collision.TryGetComponent(out RewardsCollector collector))
            ReactToCollector(collector);
    }

    private IEnumerator Move(Transform target, float attractionForce)
    {
        while (transform.position != target.transform.position && target != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position,
                attractionForce * Time.deltaTime);
            yield return null;
        }
    }

    private void ReactToMagnet(Magnet magnet)
    {
        StopMovementJob();
        _movementJob = StartCoroutine(Move(magnet.transform, magnet.AttractionForce));
    }

    private void ReactToCollector(RewardsCollector collector)
    {
        StopMovementJob();
        collector.AddCoin();
        Deactivate();
    }

    private void StopMovementJob()
    {
        if (_movementJob != null)
            StopCoroutine(_movementJob);
    }
}