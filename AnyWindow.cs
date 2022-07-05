using UnityEngine;

public class AnyWindow : MonoBehaviour, IWindowsWithCloseOtherWindows
{
    [SerializeField] private GameObject[] _menu;
    private GameObject _lastWindow;

    public void CloseMenu()
    {
        gameObject.SetActive(false);
        OpenCloseOtherWindows(true);
    }

    public void OpenMenu()
    {
        OpenCloseOtherWindows(false);
        gameObject.SetActive(true);
    }

    public void OpenCloseOtherWindows(bool enabled)
    {
        if (_menu.Length == 0) return;

        foreach (GameObject menu in _menu)
        {
            if (enabled)
            {
                if (_lastWindow != null)
                {
                    if (_lastWindow.name == menu.name)
                        _lastWindow.SetActive(true);
                }
            }
            else
            {
                if (menu.activeInHierarchy)
                {
                    _lastWindow = menu;
                    menu.SetActive(false);
                }
            }
        }
    }
}