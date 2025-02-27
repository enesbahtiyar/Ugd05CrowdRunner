using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public static PlayerController instance;

    private void Awake()
    {

        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    [Header("Elements")]
    [SerializeField] CrowdSystem crowdSystem;
    [SerializeField] PlayerAnimator playerAnimator;

    [Header("Settings")]
    [SerializeField] float speed;
    [SerializeField] float slideSpeed;
    [SerializeField] float roadWidth;
    private bool canMove;

    private Vector3 clickedScreenPosition;
    private Vector3 clickedPlayerPosition;

    // Update is called once per frame
    void Update()
    {
        if(canMove == true)
        {
            MoveForward();
            ManageControl();
        }
    }

    private void OnEnable()
    {
        GameManager.onGameStateChanged += GameStateChangedCallback;
    }

    private void OnDisable()
    {
        GameManager.onGameStateChanged -= GameStateChangedCallback;
    }

    private void GameStateChangedCallback(GameState state)
    {
        if(state == GameState.Game)
        {
            StartMoving();
        }
        else if(state == GameState.GameOver) 
        {
            StopMoving();
        }
        else if(state == GameState.LevelComplete)
        {
            StopMoving();
            playerAnimator.Idle();
        }
    }



    private void StartMoving()
    {
        canMove = true;
        playerAnimator.Run();
    }

    private void StopMoving()
    {
        canMove = false;
        playerAnimator.Idle();
    }

    /// <summary>
    /// Karakteri hareket ettiriyoruz
    /// </summary>
    void MoveForward()
    {
        transform.position += Vector3.forward * speed * Time.deltaTime;
    }

    void ManageControl()
    {
        if(Input.GetMouseButtonDown(0))
        {
            clickedScreenPosition = Input.mousePosition;
            clickedPlayerPosition = transform.position;
        }
        else if(Input.GetMouseButton(0))
        {
            float xScreenDifference = Input.mousePosition.x - clickedScreenPosition.x;
            xScreenDifference /= Screen.width;
            xScreenDifference *= slideSpeed;

            Vector3 position = transform.position;

            position.x = clickedPlayerPosition.x + xScreenDifference;

            position.x = Mathf.Clamp(position.x, -roadWidth / 2 + crowdSystem.GetCrowdRadius(), roadWidth / 2 - crowdSystem.GetCrowdRadius());

            transform.position = position;

            //Burada hata yaşamamızın sebebi transform.position dediğimiz zaman x y ve z'yi aynı anda kontrol etmeye çalışıyoruz yukarıdaki yöntemde sadece x'i kontrol ediyoruz.
            //transform.position = clickedPlayerPosition + Vector3.right * xScreenDifference;
        }
    }
}
