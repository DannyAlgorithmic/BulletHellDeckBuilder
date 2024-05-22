using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.CompilerServices;

using UnityEngine.Splines;
using Unity.Mathematics;
// using ReadOnlyAttribute = Sirenix.OdinInspector.ReadOnlyAttribute;

public class ActionHand : MonoBehaviour
{
    public HandDisplaySettings displaySettings = null;

    // [SerializeField] CardManagement cardManagement = null;
    [SerializeField] AudioSource focusedAudio = null, unfocusedAudio = null, errorAudio = null;

    public SplineContainer spline = null;
    public Transform handEnterTrans = null; //, handLeftAnchor = null, handRightAnchor = null;
    public HandItemState normalPositionFilter, highlightedFilter, dragPositionFilter;
    public MouseData mouseData = null;
    public List<Card> hand = new List<Card>(30);
    public Combatant player = null;
    public int currentFocus = -1, previousFocus = 0;
    Action<List<Card>, float> handResetResponse = delegate { };
    public Transform camTrans = null;

    private float spawnDelay => displaySettings.SpawnDelay;
    private float dragSpeed => displaySettings.DragSpeed;
    private float repositionSpeed => displaySettings.RepositionSpeed;
    private float visualsRotationSpeed => displaySettings.VisualsRotationSpeed;
    private float rotationSpeed => displaySettings.RotationSpeed;
    private float splineOffset => displaySettings.SplineOffset;
    private float focusedElevation => displaySettings.FocusedElevation;
    private float normalScale => displaySettings.NormalScale;
    private float focusedScale => displaySettings.FocusedScale;
    private float scaleSpeed => displaySettings.ScaleSpeed;

    private float cardDepth => displaySettings.CardDepth;
    private float maxHandSeparation => displaySettings.MaxHandSeparation;
    private float maxSpreadOffset => displaySettings.MaxSpreadOffset;
    private float maximumTilt => displaySettings.MaximumTilt;
    private AnimationCurve spreadCurve => displaySettings.SpreadCurve;

    private void Awake()
    {
        hand.Clear();
        handResetResponse = HandReset;
    }

    void Start()
    {
        Set();
        Show();
    }

    void Update()
    {
        UpdateHand();
    }

    public void Set()
    {
        player.onHandUpdate = handResetResponse;

        int handCount = player.hand.Count;
        if (handCount <= 0) return;

        for (int i = 0, max = handCount; i < max; i++)
        {
            Card currentCard = player.hand[i];
            currentCard.trans.position = handEnterTrans.position;
            hand.Add(currentCard);
        }
    }
    public void HandReset()
    {
        hand.Clear();
        int handCount = player.hand.Count;
        if (handCount <= 0) return;
        for (int i = 0; i < handCount; i++)
        {
            Card info = player.hand[i];
            if (info is null) continue; // maybe remove
            info.index = i;
            info.state = HandItemState.None;
            info.sortingGroup.sortingOrder = 0;
            hand.Add(info);
        }
    }
    public void HandReset(List<Card> _cards, float _displayDelay = 0)
    {
        hand.Clear();
        if (_cards.Count <= 0) return;
        for (int i = 0, max = _cards.Count; i < max; i++)
        {
            Card card = _cards[i];
            if (card is null) continue;
            hand.Add(card);
        }
        Show();
    }
    public void Clear()
    {
        if (hand.Count > 0)
        {
            currentFocus = -1;
            for (int i = 0, max = hand.Count; i < max; i++)
            {
                Card card = hand[i];
                if (card is null) continue; // maybe remove
                card.index = -1;
                card.state = HandItemState.None;
                card.sortingGroup.sortingOrder = 0;
                card.GO.SetActive(false);
            }
            hand.Clear();
        }

        player.onHandUpdate = delegate { };
    }

    public void Show()
    {
        if (hand.Count <= 0) return;

        for (int i = 0; i < hand.Count; i++)
        {
            Card card = hand[i];
            card.index = i;
            card.state = HandItemState.None;
            card.GO.SetActive(true);
        }
    }
    public void Hide()
    {
        currentFocus = -1;
        if (hand.Count <= 0) return;
        for (int i = 0, max = hand.Count; i < max; i++)
        {
            Card card = hand[i];

            if (card is null) continue; // maybe remove
            card.index = -1;
            card.state = HandItemState.None;
            card.sortingGroup.sortingOrder = 0;
            card.GO.SetActive(false);
        }
    }

    public void MoveToSpawnPoint()
    {
        if (hand.Count <= 0) return;
        for (int i = 0, max = hand.Count; i < max; i++)
            hand[i].trans.position = handEnterTrans.position;
    }
    public void ClearFocus()
    {
        previousFocus = currentFocus;
        currentFocus = -1;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    float Offset(int _index, int _currentFocus, int _max)
    {
        if (_currentFocus < 0 || _currentFocus > _max) return 0;
        float xOffset;
        float maximumDistance = (float)_max - 1f;
        float distanceFromFocus = math.distance((float)_currentFocus, (float)_index);

        xOffset = maxSpreadOffset * spreadCurve.Evaluate(distanceFromFocus / maximumDistance);
        xOffset = _index < currentFocus ? -xOffset : xOffset;

        return xOffset;
    }

    public void UpdateHand()
    {
        if (hand.Count <= 0) return; // || controlProxy.Current == null
        float time = Time.smoothDeltaTime;
        float camDepth = camTrans.position.z;
        float percentage;
        float depthValue;
        Card info;

        for (int i = 0, max = hand.Count; i < max; i++)
        {
            // Base
            info = hand[i];
            if (!info.gameObject.activeSelf || info.index < 0) continue;
            HandItemState state = info.state;
            percentage = ((float)i + 1.0f) / ((float)max + 1.0f);
            spline.Evaluate(percentage, out float3 _position, out float3 _tangent, out float3 upVector);
            depthValue = i;

            // Stats directionality
            /*if ( (i >= currentFocus | currentFocus == -1) && info.display.direction != StatDirection.RIGHT)
                info.display.SetDirection(StatDirection.RIGHT);
            if (i < currentFocus && info.display.direction != StatDirection.LEFT)
                info.display.SetDirection(StatDirection.LEFT);
*/
            //  Depth
            if (!state.ContainsFlag(HandItemState.FOCUSED) && currentFocus > -1 && i != currentFocus)
            {
                float distance = math.distance(currentFocus, i);
                depthValue = math.abs(distance);
            }

            else if (state.ContainsFlag(HandItemState.FOCUSED)) // && info.targetPosition.z != camDepth + 1)
                depthValue = camDepth + 1;


            // Position
            if (state.EqualsFlags(dragPositionFilter))
            {
                Vector2 finalPosition = Vector2.Lerp(info.trans.position, mouseData.worldPosition, dragSpeed * 10 * time);
                info.trans.position = new Vector3(finalPosition.x, finalPosition.y, depthValue);
            }
            else
            {
                float xOffset = !state.ContainsFlag(HandItemState.FOCUSED) && hand.Count > 1 ? Offset(i, currentFocus, max) : 0;
                float elevation = i == currentFocus ? (camTrans.position.y + focusedElevation) : _position.y + splineOffset;
                Vector2 preLerpPosition = new Vector2(_position.x + xOffset, elevation);
                Vector2 finalPosition = Vector2.Lerp(info.trans.position, preLerpPosition, repositionSpeed * 10 * time);
                info.trans.position = new Vector3(finalPosition.x, finalPosition.y, depthValue);
            }


            // Parallax
            info.parallaxGroup.Refresh(i == currentFocus | state.EqualsFlags(dragPositionFilter), info.trans.position, new Vector3(mouseData.worldPosition.x, mouseData.worldPosition.y, info.trans.position.z - 15));
            /* if (selectionTransform != null && state.ContainsFlag(dragPositionFilter))
            {
                info.visualsTrans.localRotation = Quaternion.identity;
                info.portraitTrans.localRotation = Quaternion.identity;
                info.portraitTrans.localPosition = new Vector2(0f, 1.4f); // 0.5f
            }
            else info.parallaxGroup.Refresh(i == currentFocus | state.EqualsFlags(dragPositionFilter), info.trans.position, new Vector3(mouseData.worldPosition.x, mouseData.worldPosition.y, info.trans.position.z - 15));
            */

            // Rotation
            if (state.ContainsFlag(dragPositionFilter) | state.ContainsFlag(HandItemState.FOCUSED) | max <= 2)
                info.trans.localRotation = Quaternion.identity;
            else
                info.trans.localRotation = Quaternion.Lerp(info.trans.localRotation, Quaternion.LookRotation(Vector3.forward, upVector), rotationSpeed * 10 * time);


            // Scale
            // if (state.EqualsFlags(dragPositionFilter)) // new for targetting rework
            //     info.trans.localScale = Vector3.one * 0.35f;
            if (state.ContainsFlag(highlightedFilter) && info.trans.localScale != new Vector3(focusedScale, focusedScale, 1f))
                info.trans.localScale = Vector2.Lerp(info.trans.localScale, new Vector2(focusedScale, focusedScale), scaleSpeed * 10 * time);
            else if (info.trans.localScale != new Vector3(normalScale, normalScale, 1f))
                info.trans.localScale = Vector2.Lerp(info.trans.localScale, new Vector2(normalScale, normalScale), scaleSpeed * 10 * time);
        }
    }
}