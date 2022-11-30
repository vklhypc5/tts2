using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    public enum Turn {Player,Bot,None};
    public Turn turn = Turn.Player;
    public static GameLogic Instance;
    public bool HitGround1stTime;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void BallHitGround(Vector3 pos)//called every time tennis hit ground
    {
        // 0 is midle of x axis
        if (turn == Turn.Player)
        {
            if (pos.x < 0)//player hit owner 
            {
                manager.Instance.BotWin();
            }
            if (pos.x > 0 && HitGround1stTime)
            {
                manager.Instance.PlayerWin();
            }
            if (pos.x > 0)
            {
                HitGround1stTime = true;
            }
        }
        if (turn == Turn.Bot)
        {
            if (pos.x > 0)//bot his owner
            {
                manager.Instance.PlayerWin();
            }
            if (pos.x < 0 && HitGround1stTime)
            {
                manager.Instance.BotWin();
            }
            if (pos.x < 0)
            {
                HitGround1stTime = true;
            }
        }
    }
    public void BallHitWeb()
    {

    }
    public void BallOutOfGame()
    {
        if (turn == Turn.Player)
        {
            if (HitGround1stTime)
            {
                manager.Instance.PlayerWin();
            }
            else
            {
                manager.Instance.BotWin();
            }           
        }
        if (turn == Turn.Bot)
        {
            if (HitGround1stTime)
            {
                manager.Instance.BotWin();
            }
            else
            {
                manager.Instance.PlayerWin();
            }
        }
        NewTurn();
    }
    public void PlayerHit()
    {
        turn = Turn.Player;
        NewTurn();
    }
    public void BotHit()
    {
        turn = Turn.Bot;
        NewTurn();
    }
    void NewTurn()
    {
        HitGround1stTime = false;
    }
}
