using UnityEngine;

[CreateAssetMenu(menuName = "Goods/BoosterUpgrade", order = 51)]
public class BoosterUpgrade : Goods
{
    [SerializeField] private Booster _booster;
    [SerializeField] private uint _maximumLevel;
    [SerializeField] private uint _durationMultiplier;
    [SerializeField] private uint _priceMultiplier;

    private uint _currentLevel = 1;
    private float _duration = 10;

    public uint CurrentLevel => _currentLevel;
    public bool UpgradeCompleted => _currentLevel == _maximumLevel;

    public override void Buy(Buyer buyer)
    {
        base.Buy(buyer);

        if (_currentLevel < _maximumLevel)
        {
            _currentLevel++;
            _duration *= _durationMultiplier;
            Price *= _priceMultiplier;
            _booster.Initialize(_duration);
        }
    }
}