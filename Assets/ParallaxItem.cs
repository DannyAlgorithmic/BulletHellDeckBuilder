using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

public class ParallaxItem : MonoBehaviour
{
    [Header("Scale")]
    [Range(0.1f, 2f)] public float scaleStrength = 1f;
    [Range(0f, 100f)] public float scaleTime;
    [Range(0f, 100f)] public float scaleExitTime;

    [Header("Offset")]
    [Range(-20, 20)] public float xOffsetStrength;
    [Range(-20, 20)] public float yOffsetStrength;
    [Range(0f, 100f)] public float offsetTime;
    [Range(0f, 100f)] public float offsetExitTime;

    [Header("Rotation")]
    [Range(-20, 20)] public float xRotationStrength;
    [Range(-20, 20)] public float yRotationStrength;
    [Range(-20, 20)] public float zRotationStrength;
    [Range(0f, 100f)] public float rotationTime;
    [Range(0f, 100f)] public float rotationExitTime;

    [NonSerialized] public int originDepth = 0;
    [NonSerialized] public Vector2 originPosition = Vector2.zero, originScale = Vector2.one;
    [NonSerialized] public quaternion originRotation;
    [NonSerialized] public Transform trans;

    private void Awake()
    {
        trans           = GetComponent<Transform>();
        originPosition  = trans.localPosition;
        originScale     = trans.localScale;
        originRotation  = trans.localRotation;
    }

    public void SetOrigin(Vector3 _position, Vector3 _scale, quaternion _rotation)
    {
        originPosition  = _position;
        originScale     = _scale;
        originRotation  = _rotation;
    }
}