using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Bot : MonoBehaviour
{
    /*easy mode:bot always hit tennis with same speed;
     * hard mode: bot hit tennis with caculate speed
     */
    public bool isHard;
    [SerializeField] float Speed;// speed 
    [SerializeField] private TennisBall tennis;
    [SerializeField] Vector3 tennisSpeed;//tennis speed for easy mode
    [SerializeField] Vector3 BeginPos;// beginpos of bot
    [SerializeField] Vector3 targetPos;//alway go to this pos (for moving the bot)
    public bool gotoPos;//gotopos is go to the drop point
    public Vector3 dropPoint;//get drop point when player hit tennis
    [SerializeField] public float dropTime;//get drop time for caculate
    [SerializeField] float timer;//just a cowndown
    [SerializeField] float distanceToHit;
    [SerializeField] protected Transform player;
    [SerializeField] protected TextMeshProUGUI hardText;
    [SerializeField] Vector3 mapsize;
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
        if (Vector3.Distance(transform.position, targetPos) > 0.05f)
        {
            transform.Translate((targetPos - transform.position).normalized * Speed * Time.deltaTime);
        }
        if (gotoPos)//2nd state
        {
            MoveToPos(dropPoint);
        }
        if (!gotoPos)
        {
            SetTargetPos(BeginPos);// if not in 1st or 2nd state go back to begin pos
        }
        if (tennis.AbleToHit(this.transform,distanceToHit))
        {
            hit();//hit when tennis in range
        }
        timer -= 1 * Time.deltaTime;    
        if (timer < -dropTime * manager.Instance.tennisSpeddModifie)
        {
            //go back to begin pos when dont hit tennis
            targetPos = BeginPos;
            gotoPos = false;
        }
    }
    public void Hard()
    {
        if (isHard)
        {
            isHard = false;
            hardText.text = "easy";
        }
        else
        {
            isHard = true;
            hardText.text = "hard";
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
        gotoPos = true;
        dropPoint += Vector3.right * 15;//bot go back 15 distance
    }
    void hit()
    {
        if (isHard)//hard mode on
        {
            CaculateTennisSpeed(player.transform.position);
            tennis.PlayerHit(this.transform, CaculateTennisSpeed(player.transform.position), distanceToHit);
        }
        if (!isHard)//easy mode (alway hit tenins with same velocity)
        {
            tennis.PlayerHit(this.transform, tennisSpeed, distanceToHit);
        }
        gotoPos = false;
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
        //x axis
        speed.x = -((transform.position.x) / 2f + (playerPos.x+mapsize.x)/2);
        //y axis
        speed.y = 12 + Random.Range(-1, 1);
        //z axis
        float delta = mapsize.z - Mathf.Abs(transform.position.z);
        if (transform.position.z * playerPos.z > 0)
        {
            speed.z = Random.Range(delta, -delta) / 2 - playerPos.z;
        }
        else
        {
            speed.z = Random.Range(delta, -delta) / 2;
        }

        return speed;
    }
}
