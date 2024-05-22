using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class HandDisplaySettings : ScriptableObject
{
    [Range(0.01f, 1f)] public float SpawnDelay = 0.2f;
    [Range(0.01f, 10f)] public float DragSpeed = 0.75f;
    [Range(0.01f, 10f)] public float RepositionSpeed = 2f;
    [Range(0.01f, 10f)] public float VisualsRotationSpeed = 10f;
    [Range(0.01f, 10f)] public float RotationSpeed = 1.75f;
    public float SplineOffset, FocusedElevation = -10f;

    [Range(1f, 10f)] public float NormalScale= 1f;
    [Range(1f, 10f)] public float FocusedScale = 1.5f;
    [Range(0.01f, 10f)] public float ScaleSpeed = 1.75f;

    [Range(0f, 10f)] public float CardDepth = 0.1f;
    [Range(0.25f, 2.0f)] public float MaxHandSeparation = 1.55f;
    [Range(0.1f, 10.0f)] public float MaxSpreadOffset = 1.35f;
    [Range(-360f, 360f)] public float MaximumTilt = 0;
    public AnimationCurve SpreadCurve = new AnimationCurve();
}
