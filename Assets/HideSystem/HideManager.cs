using UnityEngine;

[RequireComponent(typeof(Player_Move))]
public class HideManager : MonoBehaviour
{
    PlayerContext ctx; //플레이어의 정보를 담은 어쩌구

    public StateMachine stateMachine { get; private set;}
    [SerializeField] LayerMask sunbanLayer;
    [SerializeField] LightManager lightManager;
    private GameObject sunban;

    void Awake()
    {
        ctx = new PlayerContext
        {
            spriteRenderer = GetComponent<SpriteRenderer>(),
            move = GetComponent<Player_Move>(),
            light = lightManager,
            playerTransform = transform
        };

    }

    void Start()
    {
        stateMachine = new StateMachine(new Running(ctx));
    }

    void Update()
    {
        if (stateMachine.CurrentState is Running)
        {
            Collider2D sunbanCol = Physics2D.OverlapCircle(transform.position, 3f, sunbanLayer);
            sunban = sunbanCol?.gameObject;

            if (Input.GetKeyDown(KeyCode.F) && sunban != null)
            {
                stateMachine.StateChange(new Hiding(ctx, sunban.transform));
            }

        }

        if (stateMachine.CurrentState is Hiding)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                stateMachine.StateChange(new Running(ctx));
            }
        }



    }
}