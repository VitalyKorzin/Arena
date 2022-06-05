using UnityEngine;
using UnityEngine.Events;

public class RewardsCollector : MonoBehaviour
{
    public uint Coins { get; private set; }
    public uint Diamonds { get; private set; }

    public event UnityAction<uint> CoinsCountChanged;
    public event UnityAction<float, uint> PickedUpDoubleScore;
    public event UnityAction<float> PickedUpMagnet;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Magnet magnet))
        {
            PickedUpMagnet?.Invoke(magnet.Duration);
            magnet.PickUp(transform);
        }

        if (collision.TryGetComponent(out ScoreMultiplier scoreMultiplier))
        {
            PickedUpDoubleScore?.Invoke(scoreMultiplier.Duration, scoreMultiplier.Value);
            scoreMultiplier.Deactivate();
        }
    }

    public void AddDiamond() => Diamonds++;

    public void AddCoin()
    {
        Coins++;
        CoinsCountChanged?.Invoke(Coins);
    }
}