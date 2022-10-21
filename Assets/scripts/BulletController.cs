using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public enum BulletState {Pull,Fly,StandStill}
    [SerializeField] private BulletState MyState;
    [SerializeField] private Vector3 StartPos, EndPos, BeginPos;
    [SerializeField] private Vector3 Velocity;
    [SerializeField] private float Times,FramesBeforeCollide;
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
    }

    // Update is called once per frame
    void Update()
    {
        
        switch (MyState)
        {
            case BulletState.Pull:
                if (Input.GetMouseButtonDown(0))
                {
                    StartPos = Input.mousePosition;
                }
                if (Input.GetMouseButtonUp(0))
                {
                    GetVelocity();
                    Velocity = (StartPos - EndPos) / 10;
                    MyState = BulletState.Fly;
                }
                if (Input.GetMouseButton(0))
                {

                    GetVelocity();
                    BulletLine.DrawLine(BeginPos, Velocity);
                }
                break;
            case BulletState.Fly:
                Times += 1 * Time.deltaTime;
                transform.position = PosByTimes(Times, BeginPos, Velocity);
                break;
            case BulletState.StandStill:
                transform.position = BeginPos;
                Times = 0;
                MyState = BulletState.Pull;
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
    void _checkNextFrame()
    {
        Vector3 NextFramesPos = PosByTimes(Times + Time.fixedDeltaTime * FramesBeforeCollide, BeginPos, Velocity);
        if (Physics.CheckSphere(NextFramesPos,ColliderRadius))
        {
            Debug.Log("Collider after " + FramesBeforeCollide + " frame");
        }
        
    }

    void GetVelocity()
    {
        EndPos = Input.mousePosition;
        Velocity = (StartPos - EndPos) / 10;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            MyState = BulletState.StandStill;
        }
        if (collision.gameObject.tag == "cube")
        {
            Instantiate(DestroyEffect, transform.position, Quaternion.identity);
            manager.Instance.AddPoint();
        }
    }
    public Vector3 PosByTimes(float time,Vector3 _BeginPos,Vector3 velocity)
    {
        Vector3 pos= Gravity * time * time + velocity * time + _BeginPos;
        return pos;
    }
}
