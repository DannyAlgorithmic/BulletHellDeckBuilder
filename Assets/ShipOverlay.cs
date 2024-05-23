using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipOverlay : MonoBehaviour
{
    public SetOverlayEvent overlayEvent = null;

    public Slider healthBar, energyBar;

    public Ship cachedShip = null;

    void Awake()
    {
        if (cachedShip != null) OverlapResponse(cachedShip);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) OverlapResponse(cachedShip);
    }

    public void OverlapResponse(Ship _ship)
    {
        cachedShip = _ship;
        
        healthBar.maxValue = _ship.HealthMax;
        healthBar.value     = _ship.Health;

        energyBar.maxValue = _ship.EnergyMax;
        energyBar.value     = _ship.Energy;

        _ship.HealthRegister(HealthRefresh);
        _ship.HealthMaxRegister(HealthMaxRefresh);

        _ship.EnergyRegister(EnergyRefresh);
        _ship.EnergyMaxRegister(EnergyMaxRefresh);
    }

    void OnDestroy()
    {
        cachedShip.HealthUnregister(HealthRefresh);
        cachedShip.HealthMaxUnregister(HealthMaxRefresh);

        cachedShip.EnergyUnregister(EnergyRefresh);
        cachedShip.EnergyMaxUnregister(EnergyMaxRefresh);
    }

    public void HealthRefresh(float _value) => healthBar.value = _value;
    public void HealthMaxRefresh(float _value) => healthBar.maxValue = _value;

    public void EnergyRefresh(float _value) => energyBar.value = _value;
    public void EnergyMaxRefresh(float _value) => energyBar.maxValue = _value;
}