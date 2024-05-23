using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    [SerializeField]
    private float health, healthMax, energy, energyMax;



    [Header("Movement")]
    public MoveIntent moveIntent;
    public Movement movement = null;
    public MovementProfile movementProfile = null;
    private bool movementAvailable = false;

    [Header("Rotation")]
    public RotationIntent rotationIntent;
    public Rotation rotation = null;
    public RotationProfile rotationProfile = null;
    private bool rotationAvailable = false;

    [Header("Body and Colliders")]
    public Rigidbody2D body = null;

    public bool debugDrain = false;

    public float Health
    {
        get => health;
        set
        {
            health = value;
            healthListeners?.Invoke(health);
        }
    }
    public float HealthMax
    {
        get => healthMax;
        set
        {
            healthMax = value;
            healthListeners?.Invoke(healthMax);
        }
    }

    public float Energy
    {
        get => energy;
        set
        {
            energy = value;
            energyListeners?.Invoke(energy);
        }
    }
    public float EnergyMax
    {
        get => energyMax;
        set
        {
            energyMax = value;
            energyListeners?.Invoke(energyMax);
        }
    }

    Action<float> healthListeners = null, healthMaxListeners = null, energyListeners = null, energyMaxListeners = null;

    public void HealthTrigger(Ship _ship) => healthListeners?.Invoke(health);
    public void HealthRegister(Action<float> _listener) => healthListeners += _listener;
    public void HealthUnregister(Action<float> _listener) => healthListeners -= _listener;

    public void EnergyTrigger(Ship _ship) => energyListeners?.Invoke(energy);
    public void EnergyRegister(Action<float> _listener) => energyListeners += _listener;
    public void EnergyUnregister(Action<float> _listener) => energyListeners -= _listener;

    public void HealthMaxTrigger(Ship _ship) => healthMaxListeners?.Invoke(healthMax);
    public void HealthMaxRegister(Action<float> _listener) => healthMaxListeners += _listener;
    public void HealthMaxUnregister(Action<float> _listener) => healthMaxListeners -= _listener;

    public void EnergyMaxTrigger(Ship _ship) => energyMaxListeners?.Invoke(energyMax);
    public void EnergyMaxRegister(Action<float> _listener) => energyMaxListeners += _listener;
    public void EnergyMaxUnregister(Action<float> _listener) => energyMaxListeners -= _listener;


    private void OnEnable()
    {
        if (!rotationAvailable) rotationAvailable = TryGetComponent(out rotation);
        if (!movementAvailable) movementAvailable = TryGetComponent(out movement);
    }

    private void Start()
    {
        if (rotationAvailable) rotationProfile.SetProfile(rotation);
        if (movementAvailable) movementProfile.SetProfile(movement);
    }

    private void FixedUpdate()
    {
        float deltaTimeFixed = Time.fixedDeltaTime;

        if (rotationAvailable && !Input.GetKey(KeyCode.LeftControl))
            rotation.Rotate(body, rotationIntent.rotation, deltaTimeFixed);
        if (movementAvailable)
            movement.Move(body, moveIntent.direction);
    }
    private void Update()
    {
        float deltaTime = Time.deltaTime;

        rotationIntent.rotation = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - body.position;
        moveIntent.direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if (rotationAvailable) rotation.Cooldown(deltaTime);
        if (movementAvailable) movement.Cooldown(deltaTime);

        if (debugDrain)
        {
            Health = Mathf.Max(health - Time.deltaTime, 0f);
            Energy = Mathf.Max(energy - Time.deltaTime, 0f);
        }
    }
}