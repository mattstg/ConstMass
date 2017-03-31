using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LoLSDK;

public class LOLAudio
{
#region Singleton
    private static LOLAudio instance;

    public static LOLAudio Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new LOLAudio();
            }
            return instance;
        }
    }
    #endregion
    
    public bool soundIsActive = true;
    List<string> bannedList = new List<string>();

    private LOLAudio()
    {
    }


    public void PlayAudio(string _name, bool loop = false)
    {
        if (!soundIsActive || !LOLSDK.Instance.IsInitialized)
            return;

        if (bannedList.Contains(_name))
            return;

        LOLSDK.Instance.PlaySound(_name, false, loop);
    }

    public void StopAudio(string _name)
    {
        LOLSDK.Instance.StopSound(_name);
    }

    public void AddToBanList(string toBan)
    {
        bannedList.Add(toBan);
    }

    public void RemoveFromBanList(string toRemove)
    {
        bannedList.Remove(toRemove);
    }

    public void ClearBanList()
    {
        bannedList = new List<string>();
    }
}