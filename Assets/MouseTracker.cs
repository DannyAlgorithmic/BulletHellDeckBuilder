using System.Collections;

using UnityEngine;
// using UnityEngine.InputSystem;
// using UnityEngine.InputSystem.EnhancedTouch;

public class MouseTracker : MonoBehaviour
{
    // [SerializeField] ControlProxy controlProxy = null;
    [SerializeField] MouseData mouseData = null;
    Camera cam = null;
    bool isMobilePlatform = false;

    private void OnEnable()
    {
        cam = Camera.main;
        isMobilePlatform = SystemInfo.deviceType == DeviceType.Handheld | Application.isMobilePlatform | Application.platform == RuntimePlatform.Android | Application.platform == RuntimePlatform.IPhonePlayer;
    }
    private void Update()
    {
        // if(controlProxy.ControlCount <= 0) return;
        Vector2 screenPosition = Input.mousePosition; // Pointer.current.position.ReadValue(); //!isMobilePlatform ? Mouse.current.position.ReadValue() : controlProxy.Current.BattleMap.PointerPosition.ReadValue<Vector2>(); // Pointer.current.position.ReadValue(); // isMobilePlatform ? Pointer.current.position.ReadValue() : Mouse.current.position.ReadValue();
        if (!mouseData.AreScreenPositionsSame) mouseData.previousScreenPosition = mouseData.screenPosition;
        if(!mouseData.AreWoldPositionsSame) mouseData.previousWorldPosition = mouseData.worldPosition;
        mouseData.screenPosition = screenPosition;
        mouseData.worldPosition = cam.ScreenToWorldPoint(screenPosition);
    }
}