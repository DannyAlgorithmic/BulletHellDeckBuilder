using UnityEngine;
// using Cinemachine;

public class HandInteractionManager : MonoBehaviour
{
    [SerializeField] float detectionSize = 0.25f;
    [SerializeField] LayerMask spawnArea, cardLayers;
    [SerializeField] ActionHand actionDisplay = null;

    // [SerializeField] ControlProxy controlProxy = null;
    [SerializeField] AudioSource focusedAudio = null, unfocusedAudio = null, errorAudio = null;

    [SerializeField] MouseData mouseData = null;
    // [SerializeField] MatchData battleData = null;
    public Combatant player = null;

    [Header("Debug")] public Card previousCard = null;
    [Header("Debug")] public Card currentCard = null;


    RaycastHit2D[] rayHits = new RaycastHit2D[1];

    public void ClearDataAll()
    {
        previousCard = null;
        currentCard = null;
    }
    void UnplayableError(Card _card)
    {
        _card.shake.Shake(0.3f);
        errorAudio.Play(0);
    }
    void PlayAndReset(Card _card)
    {
        actionDisplay.ClearFocus();
        actionDisplay.HandReset();
    }

    private void Update()
    {
        ActionCardUsage();
        ActionFocus();
    }

    public void ActionFocus()
    {
        if (currentCard != null && currentCard.state.ContainsFlag(HandItemState.DRAGGING)) return;
        if (actionDisplay.hand == null || actionDisplay.hand.Count <= 0)
        {
            actionDisplay.currentFocus = -1;
            if (currentCard != null)
            {
                currentCard = null;
                // TriggerShit();
            }
            return;
        }

        Card card = DetectCard();
        if (card == previousCard) return;

        if (card == null) actionDisplay.currentFocus = -1;
        if (card != null) actionDisplay.currentFocus = card.index;

        if (previousCard != null) previousCard.state = HandItemState.None;
        if (card != null) card.state = HandItemState.FOCUSED;

        previousCard = currentCard;
        currentCard = card;

        if (currentCard != null)
        {
            // cardManagement.SetCard(currentCard.gameObject, Vector3.zero);
            focusedAudio.Play();
        }
        else
        {
            currentCard = null;
            // cardManagement.ClearCard();
            unfocusedAudio.Play();
        }
        // TriggerShit();
    }
    public void ActionCardUsage()
    {
        if (   Input.GetMouseButtonUp(1) // controlProxy.Current.BattleMap.Selection.WasReleasedThisFrame()
            &&  currentCard != null
            && !Physics2D.OverlapCircle(mouseData.worldPosition, detectionSize, 1 << LayerMask.NameToLayer("Card"))
            && Physics2D.OverlapCircle(mouseData.worldPosition, detectionSize, spawnArea.value))
        { PlayAndReset(currentCard); }
    }

    private Card DetectCard()
    {
        Vector3 origin      = new Vector3(mouseData.worldPosition.x, mouseData.worldPosition.y, Camera.main.transform.position.z);
        int rayHitCount     = Physics2D.RaycastNonAlloc(origin, Vector3.forward, rayHits, Mathf.Abs(Camera.main.transform.position.z), cardLayers.value);
        return rayHitCount  > 0 ? rayHits[0].collider.GetComponent<Card>() : null;
    }
}