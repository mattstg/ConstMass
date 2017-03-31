using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LoLSDK;
using System.Linq;

public class ProgressTracker {
    public enum ScoreType { Success, Time, Quiz }
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
    int[][] progressArray;
    float[][] timeArray;
	float[] successScore = new float[4];
    float[] timeScore    = new float[4];
	float[] quizScore    = new float[4];

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
        progressArray = new int[][]
        {
            new int[] { 0, 8, (int)GV.MoleculeType.HCl },
            new int[] { 0, 4, (int)GV.MoleculeType.H2O },
            new int[] { 0, 6, (int)GV.MoleculeType.NaCl },
            new int[]{ 0, 3, (int)GV.MoleculeType.H2O }
        };
        timeArray = new float[][]
        {
            new float[] { 0f, GV.Game_Length[0] },
            new float[] { 0f, GV.Game_Length[1] },
            new float[] { 0f, GV.Game_Length[2] },
            new float[] { 0f, GV.Game_Length[3] }
        };
    }

    public float GetScore(ScoreType scoreType,int lesson, bool raw = false)
    {
        return (raw) ? scoreDict[scoreType][(int)lesson] : (int)(scoreDict[scoreType][(int)lesson] * GV.MaxScore(scoreType));
    }

    public void SetScore(ScoreType scoreType, int lesson, float scoreValue)
    {
        try
        {
            scoreDict[scoreType][(int)lesson] = scoreValue;
        }
        catch
        {
            Debug.Log("score set fail for: " + scoreType + " index: " + lesson + "  value of " + scoreValue);
        }
    }

    public void SetProgressAndTime(int level, int progress, float time)
    {
        progressArray[level][0] = progress;
        timeArray[level][0] = timeArray[level][1] - time;
    }

    public int[] GetProgressArray(int level)
    {
        return (int[])progressArray[level].Clone();
    }

    public float[] GetTimeArray(int level)
    {
        return (float[])timeArray[level].Clone();
    }

    public void SubmitProgress(int progressNumber)
	{
        if(LOLSDK.Instance.IsInitialized)
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
