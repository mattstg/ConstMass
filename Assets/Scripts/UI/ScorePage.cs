using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LoLSDK;

public class ScorePage : Page
{
    public List<RectTransform> levelEntry;
    public List<Text> gameText;
    public List<Text> timeText;
    public List<Text> quizText;
    public List<Text> totalText;
    public Text totalTotalText;

    private int level = 0;

    private int[] game = new int[4];
    private int[] time = new int[4];
    private int[] quiz = new int[4];
    private int[] total = new int[4];
    private int totalTotal;

    public override void PageOpened()
    {
        base.PageOpened();
        UpdateScores();
    }

    public void SetLevel(int l)
    {
        level = l;
    }

    public void UpdateScores()
    {
        totalTotal = 0;
        for (int i = 0; i < 4; i++)
        {
            game[i] = (int)ProgressTracker.Instance.GetScore(ProgressTracker.ScoreType.Success, i);
            time[i] = (int)ProgressTracker.Instance.GetScore(ProgressTracker.ScoreType.Time, i);
            quiz[i] = (int)ProgressTracker.Instance.GetScore(ProgressTracker.ScoreType.Quiz, i);
            total[i] = game[i] + time[i] + quiz[i];
            totalTotal += total[i];
        }
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
                timeText[i].text = "+" + time[i].ToString();
                quizText[i].text = quiz[i].ToString();
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
