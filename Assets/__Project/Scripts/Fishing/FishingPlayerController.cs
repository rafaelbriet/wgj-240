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
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip reelingStart;
    [SerializeField]
    private AudioClip reelingMiddle;
    [SerializeField]
    private AudioClip reelingEnd;
    [SerializeField]
    private AudioClip waterSplash;

    private Vector2 moveDirection;
    private Coroutine reelingAudioCoroutine;
    private FishingGameManager gameManager;
    private float xMin;
    private float xMax;
    private float yMin;
    private float yMax;

    public bool IsHooking { get; private set; }
    public Fish HookedFish { get; private set; }

    private void Awake()
    {
        gameManager = FindObjectOfType<FishingGameManager>();
        gameManager.GameEnded += OnGameGameEnded;

        transform.position = hookStartPosition.position;
    }

    private void Start()
    {
        float spriteSize = GetComponentInChildren<SpriteRenderer>().bounds.size.x * .5f;

        Camera cam = Camera.main;
        float camHeight = cam.orthographicSize;
        float camWidth = cam.orthographicSize * cam.aspect;

        yMin = -camHeight + spriteSize;
        yMax = camHeight - spriteSize;

        xMin = -camWidth + spriteSize;
        xMax = camWidth - spriteSize;
    }

    private void OnGameGameEnded(object sender, System.EventArgs e)
    {
        audioSource.Stop();
    }

    private void Update()
    {
        if (gameManager.CurrentGameState != GameState.Playing)
        {
            return;
        }

        // Lock player movement within screen bounds from:
        // https://answers.unity.com/questions/1840867/lock-player-movement-within-screen-bounds.html
        Vector2 direction = moveDirection * speed * Time.deltaTime;

        var xValidPosition = Mathf.Clamp(transform.position.x + direction.x, xMin, xMax);
        var yValidPosition = Mathf.Clamp(transform.position.y + direction.y, yMin, yMax);

        transform.position = new Vector3(xValidPosition, yValidPosition, 0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Fish hookedFish) && IsHooking == false)
        {
            IsHooking = true;
            HookedFish = hookedFish;
            HookedFish.IsHooked = true;
            HookedFish.transform.SetParent(transform);
            moveDirection = (hookStartPosition.position - transform.position).normalized;
            audioSource.PlayOneShot(reelingStart);
            reelingAudioCoroutine = StartCoroutine(PlayReelingOneShot());
        }
        else if (collision.CompareTag("HookStartPosition"))
        {
            IsHooking = false;
            moveDirection = Vector2.zero;
            gameManager.Score(HookedFish.PointsAmount);
            gameManager.RemoveFish(HookedFish.gameObject);
            StopCoroutine(reelingAudioCoroutine);
            audioSource.Stop();
            audioSource.PlayOneShot(reelingEnd);
        }
        else if (collision.CompareTag("FishingPoolBorder"))
        {
            audioSource.PlayOneShot(waterSplash);
        }
    }

    private IEnumerator PlayReelingOneShot()
    {
        while (audioSource.isPlaying)
        {
            yield return null;
        }

        audioSource.clip = reelingMiddle;
        audioSource.Play();
    }

    public void ResetPlayer()
    {
        moveDirection = Vector2.zero;
        transform.position = hookStartPosition.position;
        IsHooking = false;
    }

    public void OnMove(InputValue value)
    {
        if (IsHooking)
        {
            return;
        }

        moveDirection = value.Get<Vector2>();
    }

    private void OnFishUnhooked(object sender, System.EventArgs e)
    {
        moveDirection = Vector2.zero;
    }
}
