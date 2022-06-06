using UnityEngine;

[CreateAssetMenu(menuName = "Payment/DiamondsPayment", order = 51)]
public class DiamondsPayment : Payment
{
    public override void Buy(uint price, Buyer buyer)
        => buyer.BuyPerDiamonds(price);

    public override bool CheckSolvency(uint price, Buyer buyer)
        => buyer.CheckSolvencyInDiamonds(price);
}