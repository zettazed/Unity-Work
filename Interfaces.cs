using UnityEngine;

public interface IAnyWindow
{
    public void CloseMenu();
    public void OpenMenu();
}

public interface IWindowsWithCloseOtherWindows : IAnyWindow
{
    public void OpenCloseOtherWindows(bool enabled);
}