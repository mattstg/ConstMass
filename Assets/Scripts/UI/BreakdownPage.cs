using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LoLSDK;

public enum BreakdownType {Before, After};

public class BreakdownPage : Page
{
    public RectTransform entriesParent;
    public BreakdownEntry totalBeforeEntry;
    public BreakdownEntry totalAfterEntry;
    public BreakdownType type = BreakdownType.Before;

    protected List<GV.MoleculeType> molecules = new List<GV.MoleculeType>();
    protected List<BreakdownEntry> entries = new List<BreakdownEntry>();

}
