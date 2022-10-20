using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public enum BulletState {Pull,Fly,StandStill}
    public BulletState MyState;
    public Vector3 StartPos, EndPos, BeginPos;
    public Vector3 Velocity;
    public float Times;
    public GameObject DestroyEffect;
    public LineRender BulletLine;
    private Vector3 Gravity= new Vector3(0,-10,0);
    // Start is called before the first frame update
    void Start()
    {
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
