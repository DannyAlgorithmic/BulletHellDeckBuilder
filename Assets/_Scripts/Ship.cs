using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    public FactionEnum faction = FactionEnum.None;
    public float health, healthMax, energy, energyMax;
    public float flightSpeed, flightCost;
    public float regenCost, repairCost;
    public float shotSpeed, shotSpread, reloadSpeed, reloadCooldown;





    [Header("Movement")]
    public Movement movement = null;
    public MoveIntent moveIntent = null;
    public MovementProfile movementProfile = null;

    [Header("Rotation")]
    public Rotation rotation = null;
    public RotationIntent rotationIntent = null;
    public RotationProfile rotationProfile = null;

    [Header("Body and Colliders")]
    public Rigidbody2D body = null;

    [Header("Event")]
    public SetOverlayEvent overlayEvent = null;
    public Card card = null;

    public bool mainShip = false, overlayTarget = false, debugDrain = false;

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

    public Action<float> healthListeners = null, healthMaxListeners = null, energyListeners = null, energyMaxListeners = null;

    private void OnEnable()
    {
        TryGetComponent(out rotation);
        TryGetComponent(out movement);
    }

    private void Start()
    {
        rotationProfile.SetProfile(rotation);
        movementProfile.SetProfile(movement);
        if (overlayTarget) overlayEvent?.Trigger(this);
    }

    private void FixedUpdate()
    {
        float deltaTimeFixed = Time.fixedDeltaTime;
        if (!Input.GetKey(KeyCode.LeftControl)) rotation.Rotate(body, rotationIntent.rotation, deltaTimeFixed);
        movement.Move(body, moveIntent.direction);
    }
    private void Update()
    {
        float deltaTime = Time.deltaTime;

        // rotationIntent.rotation = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - body.position;
        // moveIntent.direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        rotation.Cooldown(deltaTime);
        movement.Cooldown(deltaTime);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(!mainShip || !collision.gameObject.CompareTag("Ship")) return;
        // card
    }
}