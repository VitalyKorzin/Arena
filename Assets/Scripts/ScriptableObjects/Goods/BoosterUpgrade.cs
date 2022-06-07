using UnityEngine;

[CreateAssetMenu(menuName = "Goods/BoosterUpgrade", order = 51)]
public class BoosterUpgrade : Goods
{
    [SerializeField] private Booster _booster;
    [SerializeField] private uint _maximumLevel;
    [SerializeField] private uint _durationMultiplier;
    [SerializeField] private uint _priceMultiplier;

    private uint _currentLevel;
    private float _duration;

    public uint CurrentLevel => _currentLevel;
    public bool UpgradeCompleted => _currentLevel == _maximumLevel;

    public override void Save(GoodsSaver saver)
    {
        var boosterUpgradeData = new BoosterUpgradeData
        {
            CurrentLevel = CurrentLevel,
            Duration = _duration,
            Price = Price
        };
        saver.SaveBooster(boosterUpgradeData, FileName);
    }

    public override void Initialize(GoodsSaver saver)
    {
        var boosterUpgradeData = saver.LoadBoosterUpgrade(FileName);
        _duration = boosterUpgradeData.Duration;
        Price = boosterUpgradeData.Price;
        _currentLevel = boosterUpgradeData.CurrentLevel;
        _booster.Initialize(_duration);
    }

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

[System.Serializable]
public class BoosterUpgradeData
{
    public float Duration;
    public uint CurrentLevel;
    public uint Price;
}