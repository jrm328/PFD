using UnityEngine;

public class PopupWindowController : MonoBehaviour
{
    public PopupWindowConfig popupWindowConfig;
    public PopupWindowBase activePopup;

    public void ShowWindow(string _ID)
    {
        foreach (GameObject _gameObject in popupWindowConfig.windowPrefabs)
        {
            PopupWindowBase _popup = _gameObject.GetComponent<PopupWindowBase>();
            if (_popup.GetWindowID() == _ID)
            {
                GameObject _out = Instantiate(_gameObject, transform);
                activePopup = _out.GetComponent<PopupWindowBase>();
                return;
            }
        }
    }

    public void ShowBuildingLockedPopup(BuildingModel model)
    {
        GameObject _out = Instantiate(popupWindowConfig.buildingLockedPopupWindowPrefab, transform);
        _out.GetComponent<BuildingLockedPopupWindow>().SetBuildingModel(model);
    }

    public void Notification()
    {
        //GameObject _out = Instantiate(popupWindowConfig.notificationPrefab, transform);
        //_out.GetComponent<UINotificationController>().SetNotification();
    }
}

