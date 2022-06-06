using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BoosterUpgradeShopItem : ShopItem<BoosterUpgrade>
{
    [SerializeField] private TMP_Text _levelDisplay;
    [SerializeField] private Button _upgradeButton;

    public event UnityAction<BoosterUpgrade, BoosterUpgradeShopItem> Upgraded;

    protected override void OnEnable()
    {
        base.OnEnable();
        _upgradeButton.onClick.AddListener(OnUpgradeButtonClick);
    }

    private void OnDisable() 
        => _upgradeButton.onClick.RemoveListener(OnUpgradeButtonClick);

    public override void Render()
    {
        base.Render();
        _levelDisplay.text = Goods.CurrentLevel.ToString();
        LockUpgradeButton();
    }

    protected override void Validate()
    {
        base.Validate();

        if (_levelDisplay == null)
            throw new InvalidOperationException();

        if (_upgradeButton == null)
            throw new InvalidOperationException();
    }

    private void OnUpgradeButtonClick()
        => Upgraded?.Invoke(Goods, this);

    private void LockUpgradeButton()
    {
        if (Goods.UpgradeCompleted)
            _upgradeButton.interactable = false;
    }
}