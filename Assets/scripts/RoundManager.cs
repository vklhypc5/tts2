using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RoundManager : MonoBehaviour
{
    [SerializeField] protected int currentRound;
    [SerializeField] protected int[] playerPoints;
    [SerializeField] protected int[] botPoints;
    [SerializeField] private TextMeshProUGUI[] playerPointText;
    [SerializeField] private TextMeshProUGUI[] botPointText;

    private void Start()
    {
        currentRound = 0;
    }
    /*when some one get point check if their point >5 then new round begin 
     */
    public void AddPointPlayer()
    {
        playerPoints[currentRound] += 1;
        playerPointText[currentRound].text = playerPoints[currentRound].ToString();
        if (playerPoints[currentRound] > 5)
        {
            currentRound += 1;
            if (currentRound > 4)
            {
                Debug.Log("end");
            }
        }
    }
    public void AddPointBot()
    {
        botPoints[currentRound] += 1;
        botPointText[currentRound].text = botPoints[currentRound].ToString();
        if (botPoints[currentRound] > 5)
        {
            currentRound += 1;
            Debug.Log("end");
        }
    }
}

