using UnityEngine;
using IJunior.TypedScenes;

public class MenuLoader : MonoBehaviour
{
    public void Load(uint coinsCount, uint diamondsCount, uint score)
    {
        PlayerResult playerResult = new PlayerResult()
        {
            CoinsCount = coinsCount,
            DiamondsCount = diamondsCount,
            Score = score
        };
        Menu.Load();
    }
}

public class PlayerResult 
{
    public uint CoinsCount;
    public uint DiamondsCount;
    public uint Score;
}