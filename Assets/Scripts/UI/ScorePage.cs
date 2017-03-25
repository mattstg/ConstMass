using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LoLSDK;

public class ScorePage : Page
{
    public List<RectTransform> levelEntry;
    public List<Text> gameText;
    public List<Text> quizText;
    public List<Text> totalText;
    public Text totalTotalText;

    private int level;

    private int[] game = new int[4];
    private int[] quiz = new int[4];
    private int[] total = new int[4];
    private int totalTotal;
    
    public void UpdateScores(int level)
    {
        // set level
        this.level = level;
        for (int i = 0; i < 4; i++)
        {
            // set game[i], quiz[i], total[i]
            game[i] = 0;
            quiz[i] = 0;  // should be int between 0 and 100, if a bonus to be displayed as a percentage
            total[i] = 0;
        }
        // set totalTotal
        totalTotal = 0;
        UpdateText();
    }

    public void UpdateText()
    {
        for (int i = 0; i < 4; i++)
        {
            if (i <= level)
            {
                levelEntry[i].gameObject.SetActive(true);
                gameText[i].text = game[i].ToString();
                quizText[i].text = quiz[i].ToString() + '%';
                totalText[i].text = total[i].ToString();
            }
            else
            {
                levelEntry[i].gameObject.SetActive(false);
            }
        }
        totalTotalText.text = totalTotal.ToString();
    }
}
