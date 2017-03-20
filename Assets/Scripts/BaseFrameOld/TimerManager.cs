using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerManager
{
    #region Singleton
    private static TimerManager instance;

    private TimerManager() { SetupTimerManager(); }

    public static TimerManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new TimerManager();
            }
            return instance;
        }
    }
    #endregion

    public List<Timer> timers;

    private void SetupTimerManager()
    {
        timers = new List<Timer>();
    }
    
    public void AddTimer(float sec, Timer.TimerFunc callback)
    {
        Timer t = new Timer(sec, callback);
        timers.Add(t);
    }

    public void UpdateTimers(float dt)
    {
        for(int i = timers.Count - 1; i >= 0; i--)
        {
            bool toRemove = timers[i].UpdateTimer(dt);
            if (toRemove)
                timers.RemoveAt(i);
        }
    }


    public class Timer
    {
        float curTime;
        public delegate void TimerFunc();
        TimerFunc callback;

        public Timer(float sec, TimerFunc _callback)
        {
            curTime = sec;
            callback = _callback;
        }

        public bool UpdateTimer(float dt)
        {
            curTime -= dt;
            if(curTime <= 0)
            {
                callback();
                return true;
            }
            return false;
        }
    }
}
