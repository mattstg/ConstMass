using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoleculeDict
{
    #region Singleton
    private static MoleculeDict instance;

    private MoleculeDict() { SetupMoleculeDict(); SetupReactionDictionary(); }

    public static MoleculeDict Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new MoleculeDict();
            }
            return instance;
        }
    }
    #endregion

    Dictionary<GV.MoleculeType, MoleculeStruct> moleculeDict;
    Dictionary<Vector2, ReactionStruct> reactionDictionary;

    private void SetupMoleculeDict()
    {
        moleculeDict = new Dictionary<GV.MoleculeType, MoleculeStruct>()
        {
            { GV.MoleculeType.H2, new MoleculeStruct(2*1.0079f,new List<GV.MoleculeType>() { GV.MoleculeType.H2 }) }
        };
    }

    private void SetupReactionDictionary()
    {
        reactionDictionary = new Dictionary<Vector2, ReactionStruct>();

        //ReactionStruct hh = new ReactionStruct(1, new List<GV.MoleculeType>() { GV.MoleculeType.Hydrogen2 });
        //reactionDictionary.Add(new Vector2((int)GV.MoleculeType.Hydrogen, (int)GV.MoleculeType.Hydrogen), hh);
       
    }


    public bool CanReact(GV.MoleculeType m1, GV.MoleculeType m2)
    {
        Vector2 key = new Vector2((int)m1, (int)m2);
        if (reactionDictionary.ContainsKey(key) && reactionDictionary[key].tempRequired <= GV.Temperature)
            return true;
        return false;
    }

    public List<GV.MoleculeType> GetProducts(GV.MoleculeType m1, GV.MoleculeType m2) //Get the result of merge
    {
       return new List<GV.MoleculeType>(reactionDictionary[new Vector2((int)m1, (int)m2)].products);        //It should never make it this far if these cannot react, returns a copy of the list
    }


    private class MoleculeStruct
    {
        float mass;
        int[] baseAtoms;

        public MoleculeStruct(float _mass, List<GV.MoleculeType> baseAtoms)
        {
            mass = _mass;
            //Convert list to arr
        }

    }


    private class ReactionStruct
    {
        public float tempRequired;
        public List<GV.MoleculeType> products;

        public ReactionStruct(float _tempRequired, List<GV.MoleculeType> _products)
        {
            tempRequired = _tempRequired;
            products = _products;
        }
    }
}
