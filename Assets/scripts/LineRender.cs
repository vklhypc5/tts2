using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRender : MonoBehaviour
{
    [SerializeField] private Vector3[] LinePoints;
    [SerializeField] private LineRenderer BulletLine;
    [SerializeField] private float PointTime;
    [SerializeField] private GameObject Droppoint,StartDrawPoint;
    private Vector3 Gravity = new Vector3(0, -10, 0);
    private float DropPointRadius;
    // Start is called before the first frame update
    void Start()
    {
        DropPointRadius = Droppoint.GetComponent<Collider>().bounds.size.z / 2;
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    public Vector3 PosByTimes(float time, Vector3 _BeginPos, Vector3 velocity)
    {
        Vector3 pos = new Vector3(0, -10, 0) * time * time/2 + velocity * time + _BeginPos;
        return pos;
    }
    public void DrawLine(Vector3 _BeginPos, Vector3 velocity)
    {
        float time = 0;
        bool StopDropPoint = false;
        for(int j = 0; j < LinePoints.Length; j++)
        {
            LinePoints[j]= PosByTimes(time,StartDrawPoint.transform.position,velocity);
            time += PointTime;
            if (j > 3 && CheckDropPoint(LinePoints[j],DropPointRadius)  && !StopDropPoint)
            {
                Droppoint.transform.position = LinePoints[j-1];
                StopDropPoint = true;
            }
        }
        BulletLine.SetPositions(LinePoints);
    }
    bool CheckDropPoint(Vector3 CastPoint,float radius)
    {
        Collider[] colliders = Physics.OverlapSphere(CastPoint, radius);
        bool hit = false;
        foreach(Collider collider in colliders)
        {
            if (collider.gameObject.tag != "Player")
            {
                hit=true;
            }
        }
        return hit;
    }
}
