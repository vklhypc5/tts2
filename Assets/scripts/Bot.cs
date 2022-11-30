using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot : MonoBehaviour
{
    public bool isHard;
    [SerializeField] float Speed,percentToGotoPos;// speed and how fast bot react to the player hit tennis
    [SerializeField] private TennisBall tennis;
    [SerializeField] Vector3 tennisSpeed;//tennis speed for easy mode
    [SerializeField] Vector3 BeginPos;// beginpos of bot
    [SerializeField] Vector3 targetPos;//alway go to this pos (for moving the bot)
    public bool gotoZaxis, gotoPos;//bot has 2 state gotozaxis is go to the positon with z=dropoint.z,gotopos is go to the drp point
    public Vector3 dropPoint;//get drop point when player hit tennis
    [SerializeField] public float dropTime;//get drop time for caculate
    [SerializeField] float timer;//just a cowndown
    [SerializeField] float distanceToHit;
    [SerializeField] protected Transform player;
    // Start is called before the first frame update
    void Start()
    {
        timer = 999;
        BeginPos = transform.position;
        targetPos = BeginPos;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate((targetPos - transform.position).normalized * Speed * Time.deltaTime);
        if (gotoPos)//2nd state
        {
            MoveToPos(dropPoint);
        }
        if (gotoZaxis)//1st state
        {
            MoveToZAxis(dropPoint);
        }
        if (!gotoPos && !gotoZaxis)
        {
            SetTargetPos(BeginPos);// if not in 1st or 2nd state go back to begin pos
        }
        if (tennis.AbleToHit(this.transform,distanceToHit))
        {
            hit();//hit when tennis in range
        }
        timer -= 1 * Time.deltaTime;    
        if(timer<0 && gotoPos == false)
        {
            //chane from 1st state to 2nd state
            gotoPos = true;
            gotoZaxis = false;
        }
        if (timer < -dropTime * manager.Instance.tennisSpeddModifie)
        {
            //go back to begin pos when dont hit tennis
            targetPos = BeginPos;
            gotoPos = false;
            gotoZaxis = false;
        }
    }
    void MoveToZAxis(Vector3 target)
    {
        SetTargetPos(transform.position);
        targetPos.z = target.z;
    }
    void MoveToPos(Vector3 target)
    {
        SetTargetPos(target);
    }
    public void GetInfo()
    {
        gotoZaxis = true;
        dropPoint += Vector3.right * 15;//bot go back 15 distance
        //bot.gotoXaxis = true;
        timer = dropTime*(100-percentToGotoPos)/100;
    }
    void hit()
    {
        if (isHard)//hard mode on
        {
            CaculateTennisSpeed(player.transform.position);
            tennis.PlayerHit(this.transform, tennisSpeed, distanceToHit);
        }
        if (!isHard)//easy mode (alway hit tenins with same velocity)
        {
            tennis.PlayerHit(this.transform, tennisSpeed, distanceToHit);
        }
        gotoPos = false;
        gotoZaxis = false;
        timer = 999;
        GameLogic.Instance.BotHit();
    }
    void SetTargetPos(Vector3 pos)
    {
        pos.y = BeginPos.y;
        targetPos = pos;
    }
    Vector3 CaculateTennisSpeed(Vector3 playerPos)//caculate for hard mode
    {
        Vector3 speed = Vector3.zero;
        Debug.Log(playerPos);
        return speed;
    }
}
