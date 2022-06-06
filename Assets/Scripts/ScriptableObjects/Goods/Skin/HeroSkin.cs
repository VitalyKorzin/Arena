using UnityEngine;

[CreateAssetMenu(menuName = "Goods/Skin/Hero", order = 51)]
public class HeroSkin : Skin
{
    [SerializeField] private Hero _hero;

    public Hero Hero => _hero;
}