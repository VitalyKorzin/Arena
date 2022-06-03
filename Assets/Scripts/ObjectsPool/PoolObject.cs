using UnityEngine;

public abstract class PoolObject : MonoBehaviour
{
    [SerializeField] private uint _clonesCount;

    public uint ClonesCount => _clonesCount;
    public bool Deactivated => gameObject.activeSelf == false;

    public void Activate() => gameObject.SetActive(true);

    public void Deactivate() => gameObject.SetActive(false);
}