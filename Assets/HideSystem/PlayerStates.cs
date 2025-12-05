using UnityEngine;

public class Running : State
{
    public Running(PlayerContext ctx) : base(ctx) { }

    public override void OnEnter()
    {
        
        ctx.spriteRenderer.color = new Color(1, 1, 1, 1);

        ctx.move.moveSpeed = ctx.move.playerSpeed;
        ctx.light.LightOn();
        

    }

    public override void OnUpdate()
    {
        ctx.light.Light();
    }

    public override void OnExit()
    {
       
    }

}

public class Hiding : State
{
    private Transform hidePos;
    public Hiding(PlayerContext ctx,Transform hidePos) : base(ctx)
    {
        this.hidePos = hidePos;
    }

    public override void OnEnter()
    {

        Debug.Log("호잇짜!(선반 숨는 소리)");
        ctx.spriteRenderer.color = new Color(1, 1, 1, 0);
        ctx.move.moveSpeed = 0f;
        ctx.playerTransform.position = hidePos.position;
        ctx.light.LightOff();
    }

    public override void OnExit()
    {
        ctx.playerTransform.position += new Vector3(0, -1, 0);
        Debug.Log("짜잇호!(선반 나가는 소리)");
    }

    public override void OnUpdate()
    {
        
    }
}