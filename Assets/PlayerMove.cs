using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player_Move : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] float moveSpeed;

    void Awake()
    {
        rb = GetComponent & lt; Rigidbody2D & gt; ();
    }

    void Update()
    {
        float inputX = Input.GetAxisRaw(&quot; Horizontal & quot;);
        float inputY = Input.GetAxisRaw(&quot; Vertical & quot;);
        rb.linearVelocity = new Vector2(inputX, inputY) * moveSpeed *
        Time.deltaTime;
    }
}