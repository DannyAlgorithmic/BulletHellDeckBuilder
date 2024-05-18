using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    public float maxSpeed, disabledTime;
    public bool disabled = false;

    public void Rotate(Rigidbody2D _body, Vector2 _direction, float _fixedDeltaTime)
    {
        if (disabled || disabledTime > 0f) return;

        float currentRotation = _body.rotation;
        float aimRotation = Quaternion.FromToRotation(Vector2.down, _direction).eulerAngles.z;
        float angle = Mathf.LerpAngle(currentRotation, aimRotation, maxSpeed * _fixedDeltaTime);
        if (Mathf.Abs(aimRotation - currentRotation) <= 0.5f) angle = aimRotation;
        _body.MoveRotation(angle);
    }

    public void Cooldown(float _deltaTime)
    {
        if (disabled) return;
        disabledTime = Mathf.Max(disabledTime - _deltaTime, 0f);
    }
}