using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SetOverlayEvent : ScriptableObject
{
    Action<Ship> listeners = null;

    public void Trigger(Ship _ship) => listeners?.Invoke(_ship);
    public void Register(Action<Ship> _listener)    => listeners += _listener;
    public void Unregister(Action<Ship> _listener)  => listeners -= _listener;
}