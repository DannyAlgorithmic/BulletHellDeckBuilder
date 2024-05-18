using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Profiles/Rotation")]
public class RotationProfile : ScriptableObject
{
    // [field: OdinSerialize] public float MinSpeed { get; } = 0;
    public float maxSpeed = 0;
    // [field: OdinSerialize] public float Acceleration { get; } = 0;
    // [field: OdinSerialize] public float Decceleration { get; } = 0;

    public void SetProfile(Rotation _rotation)
    {
        _rotation.maxSpeed = maxSpeed;
    }
}