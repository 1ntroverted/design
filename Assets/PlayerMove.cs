using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player_Move : MonoBehaviour
{
    Rigidbody2D rb;
    [HideInInspector] public float moveSpeed{ get; set;}     //실제 적용되는 값(상태에 따라 0이 들어갈수 있음)

    [SerializeField] private float _playerSpeed;
    public float playerSpeed => _playerSpeed;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");
        rb.linearVelocity = new Vector2(inputX, inputY) * moveSpeed;
       
    }
}