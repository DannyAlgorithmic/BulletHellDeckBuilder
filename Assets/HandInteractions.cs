using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class HandInteractions : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] MouseData mouseData = null;

    Queue<GameObject> pointerEnteredBuffer = new Queue<GameObject>(1);

    Card card = null;
    RaycastHit2D[] rayHits = new RaycastHit2D[1];

    HandItemState previousHandState, draggedHandState = HandItemState.FOCUSED | HandItemState.DRAGGING;

    private void OnEnable()
    {
        previousHandState = HandItemState.None;
    }

    private void Awake()
    {
        card = GetComponent<Card>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // Debug.Log("Began Drag");
        previousHandState = card.state;
        card.state = draggedHandState;
    }

    public void OnDrag(PointerEventData eventData)
    {
        // throw new System.NotImplementedException();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Debug.Log("Ended Drag");
        card.state = previousHandState; // HandItemState.None;
    }
}