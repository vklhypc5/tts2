using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public enum BulletState {Pull,Fly,StandStill}
    [SerializeField] private BulletState MyState;
    [SerializeField] private Vector3 StartPos, EndPos, BeginPos,AwakePos;
    [SerializeField] private Vector3 Velocity,DrawVelocity;
    [SerializeField] private float Times,FramesBeforeCollide,ScreenSizePerWorldSize;
    [SerializeField] private GameObject DestroyEffect;
    [SerializeField] private LineRender BulletLine;
    private Vector3 Gravity= new Vector3(0,-10,0);
    private float ColliderRadius;
    // Start is called before the first frame update
    void Start()
    {
        ColliderRadius = GetComponent<SphereCollider>().bounds.size.x / 2;
        MyState = BulletState.Pull;
        BeginPos = transform.position;
        AwakePos = BeginPos;
        ScreenSizePerWorldSize = manager.Instance.ScreenSIzePerWorldSize;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartPos = Input.mousePosition;
        }
        switch (MyState)
        {
            case BulletState.Pull:
                if (Input.GetMouseButtonUp(0))
                {
                    GetVelocity();
                    MyState = BulletState.Fly;
                }
                if (Input.GetMouseButton(0))
                {

                    GetVelocity();
                    BulletLine.DrawLine(BeginPos, Velocity);
                }
                break;
            case BulletState.Fly:
                if (Input.GetMouseButton(0))
                {
                    GetDrawVelocity();
                    BulletLine.DrawLine(AwakePos, DrawVelocity);
                }
                if (Input.GetMouseButtonUp(0))
                {
                    if (AbleToHit())
                    {
                        GetVelocity();
                        PlayerHit(Velocity);
                    }
                }
                Times += 1.5f * Time.deltaTime;
                transform.position = PosByTimes(Times, BeginPos, Velocity);
                break;
            case BulletState.StandStill:
                transform.position = AwakePos;
                Times = 0;
                MyState = BulletState.Pull;
                BeginPos = AwakePos;
                break;
        }
    }
    private void FixedUpdate()
    {
        if (MyState == BulletState.Fly)
        {
            _checkNextFrame();
        }
    }
    bool AbleToHit()
    {
        bool hit = false;
        if (Vector3.Distance(transform.position, PlayerMove.Player1.transform.position) < PlayerMove.Player1.DistanceToHit)
        {
            Debug.Log(Vector3.Distance(transform.position, PlayerMove.Player1.transform.position));
            hit = true;
        }
        return hit;
    }
    void _checkNextFrame()
    {
        Vector3 NextFramesPos = PosByTimes(Times + Time.fixedDeltaTime * FramesBeforeCollide, BeginPos, Velocity);
        if (Physics.CheckSphere(NextFramesPos,ColliderRadius))
        {
            //Debug.Log("Collider after " + FramesBeforeCollide + " frame");
        }
        
    }
    void GetDrawVelocity()
    {
        DrawVelocity = (Input.mousePosition - StartPos) / ScreenSizePerWorldSize;
        DrawVelocity.z = -DrawVelocity.x;
        DrawVelocity.x = DrawVelocity.y;
    }
    void GetVelocity()
    {
        EndPos = Input.mousePosition;
        Velocity = (EndPos-StartPos) / ScreenSizePerWorldSize;
        Velocity.z = -Velocity.x;
        Velocity.x = Velocity.y;
    }
    void HitWall()
    {
        Velocity.x = -(Velocity).x;
        Velocity.y = Velocity.y + Times * Gravity.y;
        ResetTrajectory();
    }
    void PlayerHit(Vector3 velocity)
    {
        GetVelocity();
        MyState = BulletState.Fly;
        ResetTrajectory();
    }
    void HitGround()
    {
        Velocity.y = -(Velocity + Gravity * Times).y;
        ResetTrajectory();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            //MyState = BulletState.StandStill;
            HitGround();
        }
        if (collision.gameObject.tag == "cube")
        {
            Instantiate(DestroyEffect, transform.position, Quaternion.identity);
            manager.Instance.AddPoint();
        }
        if (collision.gameObject.tag == "wall")
        {
            HitWall();
        }
        if (collision.gameObject.tag == "outofgame")
        {
            MyState = BulletState.StandStill;
        }
    }
    public Vector3 PosByTimes(float time,Vector3 _BeginPos,Vector3 velocity)
    {
        Vector3 pos= Gravity * time * time/2 + velocity * time + _BeginPos;
        return pos;
    }
    void ResetTrajectory()
    {
        BeginPos = transform.position;
        Times = 0;
    }
}
