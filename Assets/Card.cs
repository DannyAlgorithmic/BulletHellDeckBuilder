using UnityEngine;
using System;
using Unity.Mathematics;
using TMPro;
using UnityEngine.Rendering;

public class Card : MonoBehaviour
{
    public SpriteRenderer spriteRenderer = null;

    public int index = -1;
    public HandItemState state = HandItemState.None;
    public bool interactable = true;
    public Vector3 targetPosition = Vector3.negativeInfinity;
    public Quaternion targetRotation = Quaternion.identity;

    public GameObject GO = null;
    public Transform trans = null;
    public Transform visualsTrans = null;
    public Transform portraitTrans = null;
    public Rigidbody2D body = null;
    public SortingGroup sortingGroup = null;
    public ParallaxGroup parallaxGroup = null;
    public SavageShake shake = null;

    // placeholder
    public GameObject shipPrefab = null;
}