using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float maxSpeed, acceleration, decceleration, disabledTime, changeThreshhold;
    public bool disabled;

    public void Move(Rigidbody2D _body, Vector2 _direction)
    {
        if (disabled || disabledTime > 0f) return;

        float currentVelocityX = _body.velocity.x;
        float currentVelocityY = _body.velocity.y;

        float targetVelocityX = _direction.x * maxSpeed;
        float targetVelocityY = _direction.y * maxSpeed;

        float changeRateX = Mathf.Abs(targetVelocityX) > changeThreshhold ? acceleration : decceleration;
        float changeRateY = Mathf.Abs(targetVelocityY) > changeThreshhold ? acceleration : decceleration;

        float speedDifferenceX = targetVelocityX - currentVelocityX;
        float speedDifferenceY = targetVelocityY - currentVelocityY;

        Vector2 force = new Vector2(speedDifferenceX * changeRateX, speedDifferenceY * changeRateY);
        _body.AddForce(force * _body.mass, ForceMode2D.Force);
        _body.velocity = Vector2.ClampMagnitude(_body.velocity, maxSpeed);
    }

    public void Cooldown(float _deltaTime)
    {
        if (disabled) return;
        disabledTime = Mathf.Max(disabledTime - _deltaTime, 0f);
    }
}