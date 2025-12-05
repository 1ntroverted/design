using UnityEngine;

[RequireComponent(typeof(Player_Move))]
[RequireComponent(typeof(SpriteRenderer))]
public class HideManager : MonoBehaviour
{
    PlayerContext ctx; //플레이어의 정보를 담은 어쩌구

    public StateMachine stateMachine { get; private set;}
    [SerializeField] LayerMask sunbanLayer;
    [SerializeField] LightManager lightManager;
    [SerializeField] PlayerHpController playerHpController;
    private GameObject sunban;

    private float hideCoolTime = 0;

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

            if (Input.GetKeyDown(KeyCode.F) && sunban != null && hideCoolTime == 0&&playerHpController.PlayerHp>0)
            {
                stateMachine.StateChange(new Hiding(ctx, sunban.transform));
                hideCoolTime = 1f;
            }

        }

        if (stateMachine.CurrentState is Hiding)
        {
            if (Input.GetKeyDown(KeyCode.F) && hideCoolTime == 0)
            {
                stateMachine.StateChange(new Running(ctx));
                hideCoolTime = 10f;
            }
        }
        
        Debug.Log(stateMachine.CurrentState);

        stateMachine.UpdateState();
        hideCoolTime -= Time.deltaTime;
        hideCoolTime = Mathf.Clamp(hideCoolTime, 0, 11);



    }
}