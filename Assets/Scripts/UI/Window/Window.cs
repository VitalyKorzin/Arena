using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class Window : MonoBehaviour
{
    private CanvasGroup _canvasGroup;

    protected virtual void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        Close();
    }

    public void Open()
    {
        _canvasGroup.alpha = 1;
        _canvasGroup.interactable = true;
        _canvasGroup.blocksRaycasts = true;
    }

    public void Close()
    {
        _canvasGroup.alpha = 0;
        _canvasGroup.interactable = false;
        _canvasGroup.blocksRaycasts = false;
    }
}