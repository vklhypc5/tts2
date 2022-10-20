using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class manager : MonoBehaviour
{
    public enum GameState {Playing,Start,ReStart}
    public GameState gameState;
    [Header("UI")]
    public static manager Instance;
    public TextMeshProUGUI PointText;
    public float Point;
    public GameObject Cube;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        Point = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void CreatNewCube()
    {
        Instantiate(Cube);
    }
    public void AddPoint()
    {
        Point += 1;
        PointText.text = "Point: " + Point;
    }
}
