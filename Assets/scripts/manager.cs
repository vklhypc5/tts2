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
    [SerializeField] private TextMeshProUGUI PointText,AlarmText;
    public float Point;
    [SerializeField] private GameObject Cube;
    public float tennisSpeddModifie;
    public Vector3 Gravity = new Vector3(0, -2.5f, 0);
    [SerializeField] protected TennisBall tennisBall;
    // Start is called before the first frame update
    private void Awake()
    {
        Instance = this;
        Point = 0;
    }

    // Update is called once per frame
    public void AddPoint()
    {
        Point += 1;
        PointText.text = "Point: " + Point;
    }
    public void ResetPoint()
    {
        Point = 0;
        PointText.text = "Point: " + Point;
    }
    public void SetAlarmText(string text)
    {
        AlarmText.text = text;
        Invoke(nameof(ResetAlarmText),5f);
    }
    private void ResetAlarmText()
    {
        AlarmText.text = "";
    }
    public Vector3 PosByTimes(float time, Vector3 _BeginPos, Vector3 velocity)
    {

        Vector3 pos = Gravity * time * time / 2 + velocity * time + _BeginPos;
        return pos;
    }
    public void PlayerWin()
    {
        SetAlarmText("Player Win");
        tennisBall.ResetTurn();
    }
    public void BotWin()
    {
        SetAlarmText("Bot Win");
        tennisBall.ResetTurn();
    }
}
