using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FishingPlayerController : MonoBehaviour
{
    [SerializeField]
    private float speed = 8f;
    [SerializeField]
    private Transform hookStartPosition;

    private Vector2 moveDirection;

    public bool IsHooking { get; private set; }
    public Fish HookedFish { get; private set; }

    private void Update()
    {
        transform.Translate(speed * Time.deltaTime * moveDirection);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Fish hookedFish))
        {
            IsHooking = true;
            HookedFish = hookedFish;
            HookedFish.transform.SetParent(transform);

        }
        else if (collision.CompareTag("HookStartPosition"))
        {
            IsHooking = false;
            moveDirection = Vector2.zero;
            Destroy(HookedFish.gameObject);
        }
    }

    public void OnMove(InputValue value)
    {
        if (IsHooking)
        {
            moveDirection = hookStartPosition.position - transform.position;
            return;
        }

        moveDirection = value.Get<Vector2>();
    }

    private void OnFishUnhooked(object sender, System.EventArgs e)
    {
        moveDirection = Vector2.zero;
    }
}
