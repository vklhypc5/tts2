using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float Speed, ScreenSizePerWorldSize,baseForcex,baseForcey,distanceToHit;
    [SerializeField] private TennisBall tennis;
    [SerializeField] private LineRender tennisLine;
    [SerializeField] Bot bot;
    private Vector3 StartPos, EndPos, Velocity;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        move();
        if (Input.GetMouseButtonDown(0))
        {
            StartPos= Input.mousePosition;
        }
        if (Input.GetMouseButtonUp(0))
        {
            GetVelocity();
            if (tennis.AbleToHit(transform,distanceToHit))
            {
                Hit();
            }
        }
        if (Input.GetMouseButton(0))
        {
            GetVelocity();
            if (tennis.AbleToHit(transform,distanceToHit))
            {
                tennisLine.DrawLine(tennis.gameObject.transform.position, Velocity);
            }
            else
            {
                tennisLine.DrawLine(transform.position, Velocity);
            }
        }
    }
    void SendBotInfo()
    {
        bot.dropPoint = tennisLine.Droppoint.transform.position;
        bot.dropTime = tennisLine.timeDrop / manager.Instance.tennisSpeddModifie;
        bot.GetInfo();
    }
    void Hit()
    {
        tennis.PlayerHit(transform, Velocity, distanceToHit);
        SendBotInfo();
        GameLogic.Instance.PlayerHit();
    }
    void move()
    {
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.forward * Time.deltaTime * Speed);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.back * Time.deltaTime * Speed);
        }
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.right * Time.deltaTime * Speed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.left * Time.deltaTime * Speed);
        }
    }
    void GetVelocity()
    {
        EndPos = Input.mousePosition;
        Velocity = (EndPos - StartPos) / ScreenSizePerWorldSize;
        //changing velocity of y axis
        Velocity.z = -Velocity.x;
        Velocity.x = (Mathf.Abs(Velocity.y)+baseForcex);
        Velocity.y = (Velocity.y+ baseForcey);
    }
}
