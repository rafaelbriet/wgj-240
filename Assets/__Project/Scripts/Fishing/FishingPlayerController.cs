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

    public bool IsHooking { get; private set; }
    public Fish HookedFish { get; private set; }

    private void Awake()
    {
        gameManager = FindObjectOfType<FishingGameManager>();
        gameManager.GameEnded += OnGameGameEnded;

        transform.position = hookStartPosition.position;
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

        transform.Translate(speed * Time.deltaTime * moveDirection);
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
