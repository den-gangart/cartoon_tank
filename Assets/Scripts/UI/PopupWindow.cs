using UnityEngine;
using UnityEngine.Events;

public class PopupWindow : MonoBehaviour
{
    public UnityEvent OnPopupOpened;
    public UnityEvent OnPopupClosed;

    public void Open()
    {
        OnPopupOpened?.Invoke();
    }

    public void Close()
    {
        OnPopupClosed?.Invoke();
    }

    public void DestroyPopup()
    {
        Destroy(this.gameObject);
    }
}
