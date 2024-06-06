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
        overlayEvent.Register(OverlapResponse);
    }

    public void OverlapResponse(Ship _ship)
    {
        cachedShip = _ship;
        
        healthBar.maxValue = _ship.HealthMax;
        healthBar.value     = _ship.Health;

        energyBar.maxValue = _ship.EnergyMax;
        energyBar.value     = _ship.Energy;

        _ship.healthListeners += HealthRefresh;
        _ship.healthMaxListeners += HealthMaxRefresh;

        _ship.energyListeners += EnergyRefresh;
        _ship.energyMaxListeners += EnergyMaxRefresh;
    }

    void OnDestroy()
    {
        cachedShip.healthListeners -= HealthRefresh;
        cachedShip.healthMaxListeners -= HealthMaxRefresh;

        cachedShip.energyListeners -= EnergyRefresh;
        cachedShip.energyMaxListeners -= EnergyMaxRefresh;

        overlayEvent.Unregister(OverlapResponse);
    }

    public void HealthRefresh(float _value) => healthBar.value = _value;
    public void HealthMaxRefresh(float _value) => healthBar.maxValue = _value;

    public void EnergyRefresh(float _value) => energyBar.value = _value;
    public void EnergyMaxRefresh(float _value) => energyBar.maxValue = _value;
}