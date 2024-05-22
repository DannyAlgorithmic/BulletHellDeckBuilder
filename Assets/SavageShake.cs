// using CW.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavageShake : MonoBehaviour
{
    /// <summary>This allows you to set the speed of the shake animation.</summary>
    public float Speed { set { speed = value; } get { return speed; } }
    [SerializeField] private float speed = 0.5f;

    /// <summary>This allows you to set the current Strength of the shake. This value can automatically decay based on the <b>Dampening</b> and <b>Reduction</b> settings.</summary>
    public float Strength { set { strength = value; } get { return strength; } }
    [SerializeField] private float strength = 20.0f;

    /// <summary>This allows you to set the final shake Strength multiplier. This remains constant.</summary>
    public float Multiplier { set { multiplier = value; } get { return multiplier; } }
    [SerializeField] private float multiplier = 1.0f;

    /// <summary>This allows you to set the dampening of the <b>Strength</b> value. This decay slows down as it approaches 0.</summary>
    public float Damping { set { damping = value; } get { return damping; } }
    [SerializeField] private float damping;

    /// <summary>This allows you to set the reduction of the <b>Strength</b> value. This decay slows down at a constant rate per second.</summary>
    public float Reduction { set { reduction = value; } get { return reduction; } }
    [SerializeField] private float reduction;

    /// <summary>This allows you to set the position axes you want to shake in local units.</summary>
    public Vector3 ShakePosition { set { shakePosition = value; } get { return shakePosition; } }
    [SerializeField, Space(20)] private Vector3 shakePosition = new Vector3(1.0f, 1.0f, 0.0f);

    /// <summary>This allows you to set the rotation axes you want to shake in degrees.</summary>
    public Vector3 ShakeRotation { set { shakeRotation = value; } get { return shakeRotation; } }
    [SerializeField] private Vector3 shakeRotation = new Vector3(0.0f, 0.0f, 1.0f);

    public Vector3 ShakeScale { set { shakeScale = value; } get { return shakeScale; } }
    [SerializeField] private Vector3 shakeScale = new Vector3(0.0f, 0.0f, 0.0f);

    [SerializeField, Space(20)]
    private Transform trans;

    // Offset
    [SerializeField, Header("Shake Details"), Space(40)]
    private Vector3 offsetPosition;
    [SerializeField]
    private Vector3 offsetRotation;
    [SerializeField]
    private Vector3 offsetScale;

    // Local
    [SerializeField]
    private Vector3 localPosition;
    [SerializeField]
    private Quaternion localRotation;
    [SerializeField]
    private Vector3 localScale;

    // Expected
    [SerializeField]
    private Vector3 expectedPosition;
    [SerializeField]
    private Quaternion expectedRotation;
    [SerializeField]
    private Vector3 expectedScale;

    public void Shake(float addedStrength)
    {
        strength += addedStrength;
    }

    protected virtual void Start()
    {
        offsetPosition.x = Random.Range(0.0f, 2.0f);
        offsetPosition.y = Random.Range(0.0f, 2.0f);
        offsetPosition.z = Random.Range(0.0f, 2.0f);

        offsetRotation.x = Random.Range(0.0f, 2.0f);
        offsetRotation.y = Random.Range(0.0f, 2.0f);
        offsetRotation.z = Random.Range(0.0f, 2.0f);

        offsetScale.x = Random.Range(0.0f, 2.0f);
        offsetScale.y = Random.Range(0.0f, 2.0f);
        offsetScale.z = Random.Range(0.0f, 2.0f);

        localRotation = expectedRotation = trans.localRotation;
        localPosition = expectedPosition = trans.position;
        localScale = expectedScale = trans.localScale;
        
    }

    protected virtual void Update()
    {
        var factor = 1f; // CwHelper.DampenFactor(damping, Time.deltaTime);
        var position = default(Vector3);
        var rotation = default(Vector3);
        var scale = default(Vector3);

        strength = Mathf.Lerp(strength, 0.0f, factor);
        strength = Mathf.MoveTowards(strength, 0.0f, reduction * Time.deltaTime);

        position.x = Sample(ref offsetPosition.x, 0.29f) * shakePosition.x;
        position.y = Sample(ref offsetPosition.y, 0.31f) * shakePosition.y;
        position.z = Sample(ref offsetPosition.z, 0.37f) * shakePosition.z;

        rotation.x = Sample(ref offsetRotation.x, 0.41f) * shakeRotation.x;
        rotation.y = Sample(ref offsetRotation.y, 0.43f) * shakeRotation.y;
        rotation.z = Sample(ref offsetRotation.z, 0.47f) * shakeRotation.z;

        scale.x = Sample(ref offsetScale.x, 0.29f) * shakeScale.x;
        scale.y = Sample(ref offsetScale.y, 0.31f) * shakeScale.y;
        scale.z = Sample(ref offsetScale.z, 0.37f) * shakeScale.z;


        // Rotation
        var currentRotation = trans.localRotation;
        if (currentRotation != expectedRotation)
            localRotation = currentRotation;

        trans.localRotation = expectedRotation = localRotation * Quaternion.Euler(multiplier * strength * rotation);


        // Position
        var currentPosition = trans.localPosition;
        if (currentPosition != expectedPosition)
            localPosition = currentPosition;

        trans.localPosition = expectedPosition = localPosition + multiplier * strength * position;

        // Scale
        var currentScale = trans.localScale;
        if (currentScale != expectedScale)
            localScale = currentScale;

        trans.localScale = expectedScale = localScale + multiplier * strength * scale;
    }

    private float Sample(ref float x, float prime)
    {
        x = (x + speed * prime * Time.deltaTime) % 2.0f;

        return Mathf.SmoothStep(-1.0f, 1.0f, x > 1.0f ? 2.0f - x : x);
    }
}
