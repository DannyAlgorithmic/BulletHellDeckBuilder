using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Profiles/Movement")]
public class MovementProfile : ScriptableObject
{
    // [field: OdinSerialize] public float MinSpeed { get; } = 0;
    public float maxSpeed = 0;
    public float acceleration = 0;
    public float decceleration = 0;

    public void SetProfile(Movement _movement)
    {
        // _movement.minSpeed = MinSpeed;
        _movement.maxSpeed = maxSpeed;
        _movement.acceleration = acceleration;
        _movement.decceleration = decceleration;
    }
}