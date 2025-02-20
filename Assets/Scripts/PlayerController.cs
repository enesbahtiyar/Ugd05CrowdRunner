using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] CrowdSystem crowdSystem;

    [Header("Settings")]
    [SerializeField] float speed;
    [SerializeField] float slideSpeed;
    [SerializeField] float roadWidth;

    private Vector3 clickedScreenPosition;
    private Vector3 clickedPlayerPosition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveForward();
        ManageControl();
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
