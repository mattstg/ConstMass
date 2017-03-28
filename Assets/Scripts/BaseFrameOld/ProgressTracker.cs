using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LoLSDK;
using System.Linq;

public class ProgressTracker {
    public enum ScoreType { Success, Time, Quiz }
    public static readonly int maxScorePerRound = 1500;
    public static readonly float lossPerQuizAttempt = .05f; //out of one, so

    #region singleton
    private static ProgressTracker instance;

	public static ProgressTracker Instance
	{
		get 
		{
			if (instance == null)
			{
				instance = new ProgressTracker();
			}
			return instance;
		}
	}
    #endregion

    Dictionary<ScoreType, float[]> scoreDict;
	float[] successScore = new float[4];
    float[] timeScore    = new float[4];
	float[] quizScore    = new float[4];
    
    bool trackProgress = true;

	private ProgressTracker()
	{
		for (int i = 0; i < 4; i++) 
			successScore [i] = timeScore[i] = quizScore[i] = 0;
        scoreDict = new Dictionary<ScoreType, float[]>()
        {
            {ScoreType.Success,successScore},
            {ScoreType.Time,timeScore},
            {ScoreType.Quiz,quizScore}
        };

    }

    public float GetScore(ScoreType scoreType,int lesson, bool raw = false)
    {
        return (raw) ? scoreDict[scoreType][(int)lesson] : (int)(scoreDict[scoreType][(int)lesson] * maxScorePerRound);
    }

    public void SetScore(ScoreType scoreType, int lesson, float scoreValue)
    {
        try {
            scoreDict[scoreType][(int)lesson] = scoreValue;
        }
        catch
        {
            Debug.Log("score set fail for: " + scoreType + " index: " + lesson + "  value of " + scoreValue);
        }
    }

    public void SubmitProgress(int progressNumber)
	{
        if(trackProgress)
            LOLSDK.Instance.SubmitProgress((int)GetTotalScore(), progressNumber, 8);// SCORE, CURRENTPROGRESS, MAXPROGRESS
    }

    public int GetTotalScore()
    {
        int totalScore = 0;
        foreach (ScoreType st in System.Enum.GetValues(typeof(ScoreType)))
            for (int i = 0; i < 4; i++)
                totalScore += (int)GetScore(st, i);
        return totalScore;
    }
}
