using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combatant : MonoBehaviour
{
    public List<Card> hand = new List<Card>();
    public Action<List<Card>, float> onHandUpdate = delegate { };
    void Awake()
    {
        
    }
}