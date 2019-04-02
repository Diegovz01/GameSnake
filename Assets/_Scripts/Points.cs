using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Points : MonoBehaviour
{
    int points = 0;
    int maxPoints;
    public Text pointsText;
    public Text maxPointsText;

    // Start is called before the first frame update
    void Start()
    {
        points = 0;
        maxPoints = PlayerPrefs.GetInt("MaxScore");
        maxPointsText.text = ("Max Points: ") + maxPoints.ToString();
    }

    public void AddPoints()
    {
        points++;
        pointsText.text = points.ToString();
        if (points > maxPoints)
        {
            maxPoints = points;
            PlayerPrefs.SetInt("MaxScore", points);
            maxPointsText.text = ("Max Points: ") + maxPoints.ToString(); 
        }
    }
}
