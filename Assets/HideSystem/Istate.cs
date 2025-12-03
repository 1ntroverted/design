using UnityEngine;

public class PlayerContext
{
    public SpriteRenderer spriteRenderer;
    public Player_Move move;
    public LightManager light;
    public Transform playerTransform;
}


public abstract class State
{
    protected PlayerContext ctx;
    protected State(PlayerContext ctx)
    {
        this.ctx = ctx;
    }
    public abstract void OnEnter();
    public abstract void OnUpdate();
    public abstract void OnExit();
}

