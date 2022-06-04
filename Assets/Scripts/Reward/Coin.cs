using System.Collections;
using UnityEngine;

public class Coin : Reward
{
    private Coroutine _movementJob;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Magnet magnet) && magnet.IsActive)
            _movementJob = StartCoroutine(Move(magnet));

        if (collision.TryGetComponent(out RewardsCollector collector))
        {
            if (_movementJob != null)
                StopCoroutine(_movementJob);

            collector.AddCoin();
            Deactivate();
        }
    }

    private IEnumerator Move(Magnet target)
    {
        while (transform.position != target.transform.position && target != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position,
                target.AttractionForce * Time.deltaTime);
            yield return null;
        }
    }
}