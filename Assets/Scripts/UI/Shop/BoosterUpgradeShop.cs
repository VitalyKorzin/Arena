using UnityEngine;

public class BoosterUpgradeShop : Shop<BoosterUpgrade, BoosterUpgradeShopItem>
{
    protected override void AddShopItem(BoosterUpgradeShopItem shopItem)
        => shopItem.Upgraded += OnUpgraded;

    private void OnUpgraded(BoosterUpgrade booster, BoosterUpgradeShopItem shopItem)
    {
        if (booster.UpgradeCompleted)
        {
            shopItem.Upgraded -= OnUpgraded;
            return;
        }

        if (booster.CheckSolvency(Buyer))
        {
            booster.Buy(Buyer);
            booster.Save(Saver);
            shopItem.Render();
        }
    }
}