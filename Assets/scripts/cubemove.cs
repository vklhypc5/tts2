using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cubemove : MonoBehaviour
{
    [SerializeField] private float DropVelocity;
    [SerializeField] private Rigidbody2D Mybd;
    [SerializeField] private float StopDropAt;
    [SerializeField] private Vector3 BeginPos;

    // Start is called before the first frame update
    void Start()
    {
        BeginPos = transform.position;
        Mybd = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!(transform.position.y < StopDropAt))
        {
            transform.Translate(Vector3.down * Time.deltaTime * DropVelocity);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //manager.Instance.CreatNewCube();
            Destroy(this.gameObject);
            
        }
    }
    void ResetCube()
    {
        transform.position = BeginPos;
    }
}
