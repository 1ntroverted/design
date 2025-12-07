using UnityEngine;

public class ScoreManager : Singleton<ScoreManager>
{
    public float Score { get; private set; }

    private bool isAlive;

    public void GameStart()
    {
        Score = 0;
        isAlive = true;
    }

    public void GameOver()
    {
        isAlive = false;
    }


    void Update()
    {
        if(isAlive)
        {
            Score += Time.deltaTime;
        }
    }
}
