using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LevelMoleculeTracker
{

    #region Singleton
    private static LevelMoleculeTracker instance;

    private LevelMoleculeTracker() { InitializeTrackerDict(); }

    public static LevelMoleculeTracker Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new LevelMoleculeTracker();
            }
            return instance;
        }
    }
    #endregion

    Dictionary<Vector2, List<GV.MoleculeType>> moleculeTracker;

    private void InitializeTrackerDict()
    {
        moleculeTracker = new Dictionary<Vector2, List<GV.MoleculeType>>()
        {
            { new Vector2(0,0), new List<GV.MoleculeType>() { GV.MoleculeType.H2, GV.MoleculeType.H2, GV.MoleculeType.H2, GV.MoleculeType.H2, GV.MoleculeType.Cl2, GV.MoleculeType.Cl2, GV.MoleculeType.Cl2, GV.MoleculeType.Cl2 } },
            { new Vector2(1,0), new List<GV.MoleculeType>() { GV.MoleculeType.H2, GV.MoleculeType.H2, GV.MoleculeType.Cl2, GV.MoleculeType.Cl2, GV.MoleculeType.NaHCO3, GV.MoleculeType.NaHCO3, GV.MoleculeType.NaHCO3, GV.MoleculeType.NaHCO3 } },
            { new Vector2(2,0), new List<GV.MoleculeType>() {  GV.MoleculeType.H2, GV.MoleculeType.H2, GV.MoleculeType.H2, GV.MoleculeType.Cl2, GV.MoleculeType.Cl2, GV.MoleculeType.Cl2, GV.MoleculeType.NaHCO3, GV.MoleculeType.NaHCO3, GV.MoleculeType.NaHCO3, GV.MoleculeType.NaHCO3, GV.MoleculeType.Na2O, GV.MoleculeType.Na2O } },
            { new Vector2(3,0), new List<GV.MoleculeType>() { GV.MoleculeType.Na2O, GV.MoleculeType.K2O, GV.MoleculeType.NaHCO3, GV.MoleculeType.H2, GV.MoleculeType.H2, GV.MoleculeType.H2, GV.MoleculeType.Cl2, GV.MoleculeType.Cl2, GV.MoleculeType.Cl2} },

            //Post game
            { new Vector2(0,1), new List<GV.MoleculeType>() { } },
            { new Vector2(1,1), new List<GV.MoleculeType>() { } },
            { new Vector2(2,1), new List<GV.MoleculeType>() { } },
            { new Vector2(3,1), new List<GV.MoleculeType>() { } }
        };
    }

    public void RecordLevel(int lvl, List<GV.MoleculeType> endingMolecules)
    {
        moleculeTracker[new Vector2(lvl, 1)] = endingMolecules.ToList<GV.MoleculeType>();
    }

    public List<GV.MoleculeType> GetMoleculeList(int lvl, bool preGame)
    {
        int secondIndex = (preGame) ?0:1;
        return moleculeTracker[new Vector2(lvl, secondIndex)].ToList<GV.MoleculeType>();
    }

}
