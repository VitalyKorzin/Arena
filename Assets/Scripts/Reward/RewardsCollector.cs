using UnityEngine;
using UnityEngine.Events;

public class RewardsCollector : MonoBehaviour
{
    public uint Coins { get; private set; }
    public uint Diamonds { get; private set; }

    public event UnityAction<uint> CoinsCountChanged;
    public event UnityAction<float, uint> PickedUpScoreMultiplier;
    public event UnityAction<float> PickedUpMagnet;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Magnet magnet))
            PickUpMagnet(magnet);

        if (collision.TryGetComponent(out ScoreMultiplier scoreMultiplier))
            PickUpScoreMultiplier(scoreMultiplier);
    }

    public void AddDiamond() => Diamonds++;

    public void AddCoin()
    {
        Coins++;
        CoinsCountChanged?.Invoke(Coins);
    }

    private void PickUpMagnet(Magnet magnet)
    {
        PickedUpMagnet?.Invoke(magnet.Duration);
        magnet.PickUp(transform);
    }

    private void PickUpScoreMultiplier(ScoreMultiplier scoreMultiplier)
    {
        PickedUpScoreMultiplier?.Invoke(scoreMultiplier.Duration, scoreMultiplier.Value);
        scoreMultiplier.Deactivate();
    }
}