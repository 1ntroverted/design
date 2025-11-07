using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{

    public float movePower = 1f;

    Rigidbody2D rigid;

    Vector3 movement;


    //---------------------------------------------------[Override Function]
    //Initialization
    void Start()
    {
        rigid = gameObject.GetComponent<Rigidbody2D>();
    }


    //Physics engine Updates
    void FixedUpdate()
    {
        Move();
    }

    //---------------------------------------------------[Movement Function]

    void Move()
    {
        Vector3 moveVelocity = Vector3.zero;

        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            moveVelocity = Vector3.left;
        }

        else if (Input.GetAxisRaw("Horizontal") > 0)
        {
            moveVelocity = Vector3.right;
        }

        transform.position += moveVelocity * movePower * Time.deltaTime;
    }
}