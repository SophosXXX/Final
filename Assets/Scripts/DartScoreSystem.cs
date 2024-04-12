using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DartScoreSystem : MonoBehaviour
{
    public int dartScore = 0;

    public TextMeshProUGUI DartScore;
    public TextMeshProUGUI DartsLeft;

    public int currentDarts = 5;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetScore()
    {

        dartScore = 0;
        currentDarts = 5;
        GameObject[] outlineObjects = GameObject.FindGameObjectsWithTag("Outline Objects");

        foreach(GameObject outlineObject in outlineObjects)
        {
            if(outlineObject.name.Contains("dart") && outlineObject.name.Contains("(Clone)"))
            {
                Destroy(outlineObject);
            }
        }
        DartScore.text = "Score : 0";
        DartsLeft.text = "Darts Left : 5";
    }

    public void UpdateScore(float distance)
    {
        distance = 100 * distance;
        dartScore += 200 - (int)distance;

        currentDarts = currentDarts - 1;

        DartScore.text = "Score : " + dartScore.ToString();
        DartsLeft.text = "Darts Left : " + currentDarts.ToString();
    }
}
