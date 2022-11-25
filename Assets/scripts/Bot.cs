using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot : MonoBehaviour
{
    [SerializeField] float Speed,percentToGotoPos;
    [SerializeField] private TennisBall tennis;
    [SerializeField] Vector3 tennisSpeed,BeginPos;//toc do danh tenis va vi tri khoi dau
    [SerializeField] Vector3 targetPos;//luon di den vi tri nay
    public bool gotoZaxis, gotoPos;
    public Vector3 dropPoint;
    [SerializeField]public float dropTime,timer,distanceToHit;
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
        if (gotoPos)
        {
            MoveToPos(dropPoint);
        }
        if (gotoZaxis)
        {
            MoveToZAxis(dropPoint);
        }
        if (!gotoPos && !gotoZaxis)
        {
            targetPos = BeginPos;
        }
        if (tennis.AbleToHit(this.transform,distanceToHit))
        {
            hit();
        }
        timer -= 1 * Time.deltaTime;
        if(timer<0 && gotoPos == false)
        {
            gotoPos = true;
            gotoZaxis = false;
        }
    }
    void MoveToZAxis(Vector3 target)
    {
        targetPos = transform.position;
        targetPos.z = target.z;
    }
    void MoveToPos(Vector3 target)
    {
        targetPos = target;
    }
    public void GetInfo()
    {
        gotoZaxis = true;
        //bot.gotoXaxis = true;
        timer = dropTime*(100-percentToGotoPos)/100;
    }
    void hit()
    {
        tennis.PlayerHit(this.transform, tennisSpeed,distanceToHit);
        gotoPos = false;
        gotoZaxis = false;
        timer = 999;
        GameLogic.Instance.BotHit();
    }
}
