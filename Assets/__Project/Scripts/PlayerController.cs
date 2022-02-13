using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Transform crosshair;

    private Vector2 mousePosition;
    private GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        if (gameManager.CurrentGameState != GameState.Playing)
        {
            return;
        }

        Vector3 newCrosshairPosition = mousePosition + Random.insideUnitCircle;
        crosshair.position += (newCrosshairPosition - crosshair.position) * 0.05f;
    }

    public void OnAim(InputValue value)
    {
        mousePosition = Camera.main.ScreenToWorldPoint(value.Get<Vector2>());
    }

    public void OnShoot()
    {
        if (gameManager.CurrentGameState != GameState.Playing)
        {
            return;
        }

        var hit = Physics2D.Raycast(crosshair.position, Vector2.zero);

        if (hit.collider != null && hit.collider.CompareTag("Target"))
        {
            hit.collider.GetComponent<Target>().Hit();
        }
    }
}
