using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRender : MonoBehaviour
{
    public Vector3[] LinePoints;
    public LineRenderer BulletLine;
    public GameObject Butllet;
    public BulletController moveByTime;
    public Vector3 MouseDownPos, MouseUpPos;
    public Vector3 Velocity;
    public float PointTime;
    private Vector3 Gravity = new Vector3(0, -10, 0);
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    public Vector3 PosByTimes(float time, Vector3 _BeginPos, Vector3 velocity)
    {
        Vector3 pos = new Vector3(0, -10, 0) * time * time + velocity * time + _BeginPos;
        return pos;
    }
    public void DrawLine(Vector3 _BeginPos, Vector3 velocity)
    {
        float time = 0;
        for(int j = 0; j < LinePoints.Length; j++)
        {
            LinePoints[j]= Gravity * time * time + velocity * time + _BeginPos;
            time += PointTime;
        }
        BulletLine.SetPositions(LinePoints);
    }
}
