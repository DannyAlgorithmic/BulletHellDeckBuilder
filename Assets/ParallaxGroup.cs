using Unity.Mathematics;
using UnityEngine;

public class ParallaxGroup : MonoBehaviour
{
    [SerializeField] Collider2D parallaxCollider = null;
    [SerializeField] ParallaxItem[] items = new ParallaxItem[1];
    Transform selfTrans = null;

    private void Awake() => selfTrans = GetComponent<Transform>();

    public void Refresh(bool _isHovered, Vector3 _groupPosition, Vector3 _mousePosition)
    {
        if (_isHovered)
        {
            for (int i = 0, max = items.Length; i < max; i++)
            {
                ParallaxItem item = items[i];

                float scaleTiming = item.scaleExitTime;
                float offsetTiming = item.offsetExitTime;
                Vector3 mouseDirection = (_groupPosition - _mousePosition).normalized;

                Vector2 previousPosition = item.trans.localPosition;
                Vector2 nextPosition = item.originPosition + (mouseDirection * new Vector2(-item.xOffsetStrength, -item.yOffsetStrength));

                Vector3 previousScale = item.trans.localScale;
                Vector3 nextScale = item.originScale * item.scaleStrength;

                item.trans.localScale = Vector3.Lerp(previousScale, nextScale, scaleTiming * Time.deltaTime);
                item.trans.localPosition = Vector2.Lerp(previousPosition, nextPosition, offsetTiming * Time.deltaTime);

                Vector3 rotationDirection = mouseDirection;
                quaternion previousRotation = item.trans.localRotation;
                quaternion nextRotation = quaternion.LookRotation(rotationDirection, Vector3.up);
                Quaternion finalVisualsRotation = Quaternion.SlerpUnclamped(previousRotation, nextRotation, item.rotationTime * Time.deltaTime);

                item.trans.localRotation = finalVisualsRotation;
            }
        }
        else
        {
            for (int i = 0, max = items.Length; i < max; i++)
            {
                ParallaxItem item = items[i];

                float scaleTiming = item.scaleExitTime;
                float offsetTiming = item.offsetExitTime;

                Vector2 previousPosition = item.trans.localPosition;
                Vector3 previousScale = item.trans.localScale;

                item.trans.localScale = Vector3.Lerp(previousScale, item.originScale, scaleTiming * Time.deltaTime);
                item.trans.localPosition = Vector2.Lerp(previousPosition, item.originPosition, offsetTiming * Time.deltaTime);

                item.trans.localRotation = Quaternion.SlerpUnclamped(item.trans.localRotation, item.originRotation, item.rotationExitTime * Time.deltaTime);
            }
        }
    }

}


// mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
// isHover = parallaxCollider.OverlapPoint(mousePosition) | Input.GetMouseButton(1);