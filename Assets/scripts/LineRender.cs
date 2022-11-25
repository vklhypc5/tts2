using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRender : MonoBehaviour
{
    [SerializeField] private Vector3[] LinePoints;
    [SerializeField] private LineRenderer BulletLine;
    public float PointTime,timer,timeDrop;
    public GameObject Droppoint,StartDrawPoint;
    private float DropPointRadius;
    // Start is called before the first frame update
    void Start()
    {
        DropPointRadius = Droppoint.GetComponent<Collider>().bounds.size.z / 2;
    }

    public void DrawLine(Vector3 _BeginPos, Vector3 velocity)
    {
        timer = 0;
        bool StopDropPoint = false;
        for(int j = 0; j < LinePoints.Length; j++)
        {
            LinePoints[j]= manager.Instance.PosByTimes(timer,_BeginPos,velocity);
            timer += PointTime;
            if (j > 3 && CheckDropPoint(LinePoints[j],DropPointRadius)  && !StopDropPoint)
            {
                Droppoint.transform.position = LinePoints[j-1];
                StopDropPoint = true;
                timeDrop = timer;
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
