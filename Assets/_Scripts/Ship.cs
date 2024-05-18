using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    [Header("Movement")]
    public MoveIntent moveIntent;
    public Movement movement = null;
    public MovementProfile movementProfile = null;
    private bool movementAvailable = false;

    [Header("Rotation")]
    public RotationIntent rotationIntent;
    public Rotation rotation = null;
    public RotationProfile rotationProfile = null;
    private bool rotationAvailable = false;

    [Header("Body and Colliders")]
    public Rigidbody2D body = null;

    private void OnEnable()
    {
        if (!rotationAvailable) rotationAvailable = TryGetComponent(out rotation);
        if (!movementAvailable) movementAvailable = TryGetComponent(out movement);
    }

    private void Start()
    {
        if (rotationAvailable) rotationProfile.SetProfile(rotation);
        if (movementAvailable) movementProfile.SetProfile(movement);
    }

    private void FixedUpdate()
    {
        float deltaTimeFixed = Time.fixedDeltaTime;

        if (rotationAvailable && !Input.GetKey(KeyCode.LeftControl))
            rotation.Rotate(body, rotationIntent.rotation, deltaTimeFixed);
        if (movementAvailable)
            movement.Move(body, moveIntent.direction);
    }
    private void Update()
    {
        float deltaTime = Time.deltaTime;

        rotationIntent.rotation = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - body.position;
        moveIntent.direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if (rotationAvailable)
            rotation.Cooldown(deltaTime);
        if (movementAvailable)
            movement.Cooldown(deltaTime);
    }
}