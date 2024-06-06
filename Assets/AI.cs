using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    [Range(0f, 1f)] public float thrustAimThreshold = 0.5f;
    public MoveIntent moveIntent = null;
    public RotationIntent rotationIntent = null;

    public string targetTag = string.Empty;
    public List<GameObject> targets = null;
    public Dictionary<GameObject, Ship> targetDict = null;
    public Transform trans = null;

    void Awake()
    {
        targets = new List<GameObject>();
        targetDict = new Dictionary<GameObject, Ship>();
        trans = transform;
    }

    void FixedUpdate()
    {
        if (targets.Count > 0) rotationIntent.rotation = (targets[0].transform.position - trans.position).normalized;
        if (targets.Count > 0)
        {
            float aimAccuracy = -Vector2.Dot(trans.up, rotationIntent.rotation);
            if (aimAccuracy >= thrustAimThreshold)
                moveIntent.direction = -((trans.up) * Mathf.Clamp01(aimAccuracy)).normalized;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject go = collision.gameObject;
        bool selfIsShip = gameObject.TryGetComponent(out Ship _shipSelf);
        if (!go.CompareTag(targetTag) || targets.Contains(go) || !go.TryGetComponent(out Ship _ship) || _ship.faction == _shipSelf.faction) return;
        targets.Add(go);
        targetDict.Add(go, _ship);
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        GameObject go = collision.gameObject;
        if (!go.CompareTag(targetTag) || !targets.Contains(go)) return;
        targets.Remove(go);
        targetDict.Remove(go);
    }
}