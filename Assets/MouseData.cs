using UnityEngine;

[CreateAssetMenu(menuName = "Mouse Data")]
public class MouseData : ScriptableObject
{
    public Vector2 screenPosition = Vector2.zero, worldPosition = Vector2.zero;
    public Vector2 previousScreenPosition = Vector2.one, previousWorldPosition = Vector2.one;
    public bool AreScreenPositionsSame => Vector2.Distance(screenPosition, previousScreenPosition) <= 0.001f;
    public bool AreWoldPositionsSame => Vector2.Distance(worldPosition, previousWorldPosition) <= 0.001f;
}