using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class manager : MonoBehaviour
{
    public enum GameState {Playing,Start}
    public GameState gameState;
    public float ScreenSIzePerWorldSize;
    public Transform DrawPoint;
    [Header("UI")]
    public static manager Instance;
    [SerializeField] private TextMeshProUGUI PointText;
    public float Point;
    [SerializeField] private GameObject Cube;
    // Start is called before the first frame update
    private void Awake()
    {
        Instance = this;
        Point = 0;
    }
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
