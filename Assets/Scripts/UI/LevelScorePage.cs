using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LoLSDK;

public class LevelScorePage : CascadeTextPage
{
    private int[] progress;
    private float[] time;
    private int gameScore;
    private int timeScore;

    public void Generate(int level)
    {
        progress = ProgressTracker.Instance.GetProgressArray(level);
        time = ProgressTracker.Instance.GetTimeArray(level);
        gameScore = (int)ProgressTracker.Instance.GetScore(ProgressTracker.ScoreType.Success, level);
        timeScore = (int)ProgressTracker.Instance.GetScore(ProgressTracker.ScoreType.Time, level);

        string text = "";
        if (progress[0] == progress[1])
        {
            text = "Congratulations! You successfully produced all " + progress[1] + " " + GV.MoleculeFormula((GV.MoleculeType)progress[2]) + " molecules! " +
                "You will receive a full score of " + gameScore + " points for your performance.\n\n" +
                "Because you finished in only " + TimeString(time[0]) + ", you will also get +" + timeScore + " bonus points for your speed!\n\n" +
                "The quiz will also give you an opportunity to boost your score even more.";
        }
        else if (progress[0] == 0)
        {
            text = "Unfortunately, you didn’t manage to produce any " + GV.MoleculeFormula((GV.MoleculeType)progress[2]) + " molecules before the time ran out, " +
                "and so you won’t receive any points for your performance in this level.\n\n" +
                "But don’t forget that you can still get points by doing well on the quiz!";
        }
        else
        {
            text = "Well done! You managed to produce " + progress[0] + " out of " + progress[1] + " " + GV.MoleculeFormula((GV.MoleculeType)progress[2]) + " molecules.\n\n" +
                "Although you didn’t completely achieve the level’s goal before the time ran out, you will still receive a score of " + gameScore + " points!\n\n" +
                "You will also be able to boost your score for this level by doing well on the quiz.";
        }
        SetTextContent(text);
    }

    public string TimeString(float t)
    {
        int min = (int)t / 60;
        int sec = (int)t % 60;
        string minString = min.ToString();
        string secString = sec.ToString();
        string minPlural = "s";
        string secPlural = "s";
        if (min == 1)
            minPlural = "";
        if (sec == 1)
            secPlural = "";
        if (min == 0)
            return secString + " second" + secPlural;
        else
            return minString + " minute" + minPlural + " and " + secString + " second" + secPlural;
    }
}
