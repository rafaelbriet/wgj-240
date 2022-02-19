using UnityEngine;
using UnityEngine.InputSystem;

public class TargetShootingPlayerController : PlayerController
{
    [SerializeField]
    private Transform crosshair;
    [SerializeField]
    [Range(0.01f, 0.5f)]
    private float crosshairFollowDelay = 0.05f;
    [SerializeField]
    private float crosshairSpeed = 1f;
    [SerializeField]
    private float crossRandomMoveRadius = 0.5f;
    [SerializeField]
    private AudioClip shootAudioClip;
    [SerializeField]
    private AudioClip hitAudioClip;
    [SerializeField]
    private AudioSource audioSource;

    private Vector3 mousePosition;
    private GameManager gameManager;
    private bool hasCrosshairReachedDestination;
    private float crosshairMoveProgress;
    private Vector3 crosshairStartPosition;
    private Vector3 crosshairDestination;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();

        crosshairStartPosition = crosshair.localPosition;
        PickCrossHairDestination();
    }

    private void Update()
    {
        if (gameManager.CurrentGameState != GameState.Playing)
        {
            return;
        }

        UpdateCrosshairPosition();
    }

    public void OnAim(InputValue value)
    {
        Vector2 mouseScreenToWorldPoint = Camera.main.ScreenToWorldPoint(value.Get<Vector2>());
        mousePosition = new Vector3(mouseScreenToWorldPoint.x, mouseScreenToWorldPoint.y, 0);
    }

    public void OnShoot()
    {
        if (gameManager.CurrentGameState != GameState.Playing)
        {
            return;
        }

        audioSource.PlayOneShot(shootAudioClip);

        var hit = Physics2D.Raycast(crosshair.position, Vector2.zero);

        if (hit.collider != null && hit.collider.CompareTag("Target"))
        {
            hit.collider.GetComponent<Target>().Hit();
            audioSource.PlayOneShot(hitAudioClip);
        }
    }

    private void UpdateCrosshairPosition()
    {
        // Crosshair random movement from: https://stackoverflow.com/questions/38296456/random-movement-within-a-circle
        hasCrosshairReachedDestination = false;
        crosshairMoveProgress += crosshairSpeed * Time.deltaTime;

        if (crosshairMoveProgress >= 1.0f)
        {
            crosshairMoveProgress = 1.0f;
            hasCrosshairReachedDestination = true;
        }

        crosshair.localPosition = (crosshairDestination * crosshairMoveProgress) + crosshairStartPosition * (1 - crosshairMoveProgress);

        if (hasCrosshairReachedDestination)
        {
            crosshairStartPosition = crosshairDestination;
            crosshairMoveProgress = 0f;
            PickCrossHairDestination();
        }

        // Crosshair smoth movement from: https://youtu.be/tu-Qe66AvtY?t=1020
        transform.position += (mousePosition - transform.position) * crosshairFollowDelay;
    }

    private void PickCrossHairDestination()
    {
        crosshairDestination = Random.insideUnitCircle * crossRandomMoveRadius;
    }
}
