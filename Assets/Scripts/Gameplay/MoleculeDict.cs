using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MoleculeDict
{
    #region Singleton
    private static MoleculeDict instance;

    private MoleculeDict() { SetupAtomDict(); SetupMoleculeDict(); SetupReactionDictionary(); }

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

    Dictionary<GV.AtomType, AtomStruct> atomDict;
    Dictionary<GV.MoleculeType, MoleculeStruct> moleculeDict;
    Dictionary<Vector2, ReactionStruct> reactionDictionary;


    private void SetupAtomDict()
    {
        atomDict = new Dictionary<GV.AtomType, AtomStruct>()
        {
            { GV.AtomType.C,  new AtomStruct(12.011f) },
            { GV.AtomType.Cl, new AtomStruct(35.354f) },
            { GV.AtomType.H,  new AtomStruct(1.0079f) },
            { GV.AtomType.K,  new AtomStruct(39.098f) },
            { GV.AtomType.Na, new AtomStruct(22.990f) },
            { GV.AtomType.O,  new AtomStruct(15.999f) }
        };
    }

    private void SetupMoleculeDict()
    {
        moleculeDict = new Dictionary<GV.MoleculeType, MoleculeStruct>()
        {
            { GV.MoleculeType.H2,     new MoleculeStruct(new List<GV.AtomType>() { GV.AtomType.H,  GV.AtomType.H                 })},
            { GV.MoleculeType.Cl2,    new MoleculeStruct(new List<GV.AtomType>() { GV.AtomType.Cl, GV.AtomType.Cl                })},
            { GV.MoleculeType.CO2,    new MoleculeStruct(new List<GV.AtomType>() { GV.AtomType.C,  GV.AtomType.O,  GV.AtomType.O })},
            { GV.MoleculeType.H2O,    new MoleculeStruct(new List<GV.AtomType>() { GV.AtomType.H,  GV.AtomType.H,  GV.AtomType.O })},
            { GV.MoleculeType.HCl,    new MoleculeStruct(new List<GV.AtomType>() { GV.AtomType.H,  GV.AtomType.Cl                })},
            { GV.MoleculeType.K2O,    new MoleculeStruct(new List<GV.AtomType>() { GV.AtomType.K,  GV.AtomType.K,  GV.AtomType.O })},
            { GV.MoleculeType.KCl,    new MoleculeStruct(new List<GV.AtomType>() { GV.AtomType.K,  GV.AtomType.Cl                })},
            { GV.MoleculeType.KOH,    new MoleculeStruct(new List<GV.AtomType>() { GV.AtomType.K,  GV.AtomType.O,  GV.AtomType.H })},
            { GV.MoleculeType.Na2O,   new MoleculeStruct(new List<GV.AtomType>() { GV.AtomType.Na, GV.AtomType.Na, GV.AtomType.O })},
            { GV.MoleculeType.NaCl,   new MoleculeStruct(new List<GV.AtomType>() { GV.AtomType.Na, GV.AtomType.Cl                })},
            { GV.MoleculeType.NaHCO3, new MoleculeStruct(new List<GV.AtomType>() { GV.AtomType.Na, GV.AtomType.H,  GV.AtomType.C, GV.AtomType.O, GV.AtomType.O, GV.AtomType.O })},
            { GV.MoleculeType.NaOH,   new MoleculeStruct(new List<GV.AtomType>() { GV.AtomType.Na, GV.AtomType.O,  GV.AtomType.H })}
        };

        foreach(KeyValuePair<GV.MoleculeType,MoleculeStruct> kv in moleculeDict)
            kv.Value.mass = GetAtomMass(kv.Value.baseAtoms);
    }

    private void SetupReactionDictionary()
    {
        reactionDictionary = new Dictionary<Vector2, ReactionStruct>()
        {
            { new Vector2((int)GV.MoleculeType.H2    , (int)GV.MoleculeType.Cl2), new ReactionStruct(1, new List<GV.MoleculeType>() { GV.MoleculeType.HCl , GV.MoleculeType.HCl })},
            { new Vector2((int)GV.MoleculeType.NaHCO3, (int)GV.MoleculeType.HCl), new ReactionStruct(1, new List<GV.MoleculeType>() { GV.MoleculeType.NaCl, GV.MoleculeType.CO2, GV.MoleculeType.H2O })},
            { new Vector2((int)GV.MoleculeType.Na2O  , (int)GV.MoleculeType.H2O), new ReactionStruct(1, new List<GV.MoleculeType>() { GV.MoleculeType.NaOH, GV.MoleculeType.NaOH })},
            { new Vector2((int)GV.MoleculeType.NaOH  , (int)GV.MoleculeType.HCl), new ReactionStruct(1, new List<GV.MoleculeType>() { GV.MoleculeType.NaCl, GV.MoleculeType.H2O })},
            { new Vector2((int)GV.MoleculeType.H2O   , (int)GV.MoleculeType.K2O), new ReactionStruct(1, new List<GV.MoleculeType>() { GV.MoleculeType.KOH , GV.MoleculeType.KOH })},
            { new Vector2((int)GV.MoleculeType.KOH   , (int)GV.MoleculeType.HCl), new ReactionStruct(1, new List<GV.MoleculeType>() { GV.MoleculeType.KCl , GV.MoleculeType.H2O })}
        };

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

    public float GetAtomMass(GV.AtomType atomType)
    {
        return atomDict[atomType].mass;
    }

    public float GetMoleculeMass(GV.MoleculeType molType)
    {
        return moleculeDict[molType].mass;
    }

    public float GetAtomMass(List<GV.AtomType> atomList)
    {
        float mass = 0;
        foreach (GV.AtomType atom in atomList)
            mass += GetAtomMass(atom);
        return mass;
    }

    public List<GV.AtomType> GetAtomList(GV.MoleculeType mtype)
    {
        return moleculeDict[mtype].baseAtoms.ToList<GV.AtomType>();
    }

    private class AtomStruct
    {
        public float mass;

        public AtomStruct(float _mass)
        {
            mass = _mass;
        }
    }


    private class MoleculeStruct
    {
        public float mass = 0;
        public List<GV.AtomType> baseAtoms;

        public MoleculeStruct(List<GV.AtomType> _baseAtoms)
        {
            baseAtoms =_baseAtoms;
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
