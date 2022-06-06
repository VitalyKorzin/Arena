using UnityEngine;

[CreateAssetMenu(menuName = "Payment/CoinsPayment", order = 51)]
public class CoinsPayment : Payment
{
    public override void Buy(uint price, Buyer buyer)
        => buyer.BuyPerCoins(price);

    public override bool CheckSolvency(uint price, Buyer buyer)
        => buyer.CheckSolvencyInCoins(price);
}