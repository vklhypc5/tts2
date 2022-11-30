using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TennisBall : MonoBehaviour
{
    public enum TennisState {Pull,Fly,StandStill}
    [SerializeField] public TennisState MyState;
    [SerializeField] private Vector3 BeginPos,AwakePos;
    [SerializeField] private Vector3 Velocity,DrawVelocity;
    [SerializeField] private float Times, Friction;
    [SerializeField] private GameObject DestroyEffect;
    
    // Start is called before the first frame update
    void Start()
    {
        MyState = TennisState.Pull;
        BeginPos = transform.position;
        AwakePos = BeginPos;
    }

    // Update is called once per frame
    void Update()
    {
        switch (MyState)
        {
            case TennisState.Pull:
                break;
            case TennisState.Fly:
                Times +=manager.Instance.tennisSpeddModifie * Time.deltaTime;
                transform.position = manager.Instance.PosByTimes(Times, BeginPos, Velocity);
                break;
            case TennisState.StandStill:
                transform.position = AwakePos;
                Times = 0;
                MyState = TennisState.Pull;
                BeginPos = AwakePos;
                break;
        }
    }
    //bot and player can also hit tennis but with their tranform and distance to hit 
    public void PlayerHit(Transform player,Vector3 velocity,float hitDistance)
    {
        if(MyState == TennisState.Pull || MyState == TennisState.Fly)
        {
            if (AbleToHit(player,hitDistance))
            {
                Hit(velocity);
                MyState = TennisState.Fly;
            }
        }
    }
    //bot and player can also hit tennis but with their tranform and distance to hit 
    public bool AbleToHit(Transform player,float hitDistance)
    {
        bool hit = false;
        if (Vector3.Distance(transform.position, player.position) < hitDistance)
        {
            hit = true;
        }
        return hit;
    }
    void Hit(Vector3 velocity)
    {
        Velocity = velocity;
        MyState = TennisState.Fly;
        ResetTrajectory();
    }
    void HitWall()
    {
        Velocity.x = -(Velocity).x;
        Velocity.y = Velocity.y + Times * manager.Instance.Gravity.y;
        ResetTrajectory();
    }
    void HitSideWall()
    {
        Velocity.z = -(Velocity + manager.Instance.Gravity * Times).z * Friction;
        Velocity.y = Velocity.y + Times * manager.Instance.Gravity.y;
        ResetTrajectory();
    }
    void HitGround()
    {
        GameLogic.Instance.BallHitGround(transform.position);
        Velocity.y = -(Velocity + manager.Instance.Gravity * Times).y*Friction;
        ResetTrajectory();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            HitGround();
        }
        if (collision.gameObject.tag == "outofgame")
        {
            ResetTurn();
            GameLogic.Instance.BallOutOfGame();
        }
    }
    void ResetTrajectory()
    {
        BeginPos = transform.position;
        Times = 0;
    }
    public void ResetTurn()
    {
        MyState = TennisState.StandStill;
    }
}
