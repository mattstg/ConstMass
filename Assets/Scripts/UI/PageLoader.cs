using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LoLSDK;

public enum PanelID
{
    PreLevel0, PreLevel1, PreLevel2, PreLevel3, Final, PanelTutorial, None
};

public class PageLoader
{
    private string pagePrefabDirectory = "Prefabs/UI/Elements/Pages/";

    private string simpleText = "SimpleTextPage";
    private string scrollText = "ScrollTextPage";
    private string cascadeText = "CascadeTextPage";
    private string simpleImage = "SimpleImagePage";
    private string scrollImage = "ScrollImagePage";
    private string quiz = "QuizPage";
    private string breakdown = "BreakdownPage";
    private string score = "ScorePage";
    private string levelScore = "LevelScorePage";

    #region Singleton
    private static PageLoader instance;

    private PageLoader() { }

    public static PageLoader Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new PageLoader();
            }
            return instance;
        }
    }
    #endregion

    /*
    *  ( panel, level ) x 4, panel
    *
    *  ... pre-1 panel, level 1, post-1 panel ...
    *  ... level 1, panel {post-1, pre-2}, level 2 ...
    * 
    *  panel sequence for interlevel panel between levels x and y:
    *    post-x:
    *      - post-x breakdown chart
    *      - x quiz
    *      - x score
    *    pre-y:
    *      - y lesson
    *      - y tutorial
    *      - pre-y breakdown chart
    */

    private void PreLevel0(PanelManager panelManager, RectTransform pagesParent, List<Page> pages)
    {
    // INTRODUCTION, PRE-0
        CascadeTextPage a = (CascadeTextPage)AddPage(cascadeText, panelManager, pagesParent, pages);
        a.TitleSettings(true, true, "");
        a.SetTextContent("<i>Dance of Atoms</i> is a game about what happens to atoms and molecules during <i>chemical reactions</i>.\n\n" +
            
            "Chemical reactions occur when atoms of colliding molecules rearrange themselves by breaking current bonds and creating new ones, leading to the formation of new substances.");

        CascadeTextPage b = (CascadeTextPage)AddPage(cascadeText, panelManager, pagesParent, pages);
        b.TitleSettings(true, true, "");
        b.SetTextContent("In a chemical reaction, the molecules that collide are called the <i>reactants</i>, and the molecules that are produced are called the <i>products</i>.\n\n" +
            "During a chemical reaction, atoms are never created or destroyed. Therefore, even though the atoms are rearranged into new molecules, the total amount of each type of atom in the reactants " +
            "is always equal to the total amount of each type of atom in the products.\n\n" +
            "Every atom of each type of element has a specific mass, known as <i>atomic mass</i> (measured in <i>atomic mass units</i>, or <i>amu</i>). During a chemical reaction, atoms’ atomic masses never change.");

        CascadeTextPage c = (CascadeTextPage)AddPage(cascadeText, panelManager, pagesParent, pages);
        c.TitleSettings(true, true, "");
        c.SetTextContent("Because both the total number of each type of atom, and the individual mass of each atom, never change during a chemical reaction, the total mass of the reactants is " +
            "always the same as the total mass of the products.\n\n" +
            "This principle is known as the <i>conservation of matter in chemical reactions</i>.");

        CascadeTextPage d = (CascadeTextPage)AddPage(cascadeText, panelManager, pagesParent, pages);
        d.TitleSettings(true, true, "");
        d.SetTextContent("Think of it this way: if you build four chairs out of an assortment of pieces of wood, and then take them apart and use every piece of wood to build a desk and a stool instead, " +
            "the combined weight of the desk and the stool will equal the total weight of the four chairs!");

        Page tutorial = AddPage(simpleText, panelManager, pagesParent, pages);
        tutorial.TitleSettings(true, true, "TUTORIAL 1");
        tutorial.SetTextContent("");

        CascadeTextPage e = (CascadeTextPage)AddPage(cascadeText, panelManager, pagesParent, pages);
        e.TitleSettings(true, true, "");
        e.SetTextContent("In the first level, you will be required to make 8 HCl (hydrogen chloride) molecules from 4 H" + S(2) + " (pure hydrogen) molecules and 4 Cl" + S(2) + " (pure chlorine) molecules.\n\n" +
            "You will be using the following chemical reaction equation:\n\n" +
            "H" + S(2) + " + Cl" + S(2) + "  →  2HCl");

        CascadeTextPage f = (CascadeTextPage)AddPage(cascadeText, panelManager, pagesParent, pages);
        f.TitleSettings(true, true, "");
        f.SetTextContent("Before starting each level, we will show you a detailed breakdown of all the molecules that will be present in the level.\n\n" +

            "For each molecule, we will show its mass, the total number of atoms that it contains, and the number of each type of atom that it contains. At the bottom we will show the total for each of these values, " +
            "representing the total mass and number of atoms contained within the level as a whole.\n\n" +

            "This chart, the <i>Pre-Game Breakdown</i>, will represent the state of the level at the very beginning of the level.\n\n" +

            "After each level, we will show you the Pre-Game Breakdown again, to remind you how things were before you started playing, and then we will show you a similar Post-Game Breakdown so you can do a " +
            "before/after comparison of the matter in the level.");

        BreakdownPage pre0breakdown = (BreakdownPage)AddPage(breakdown, panelManager, pagesParent, pages);
        pre0breakdown.SetLevel(0, true);
        pre0breakdown.TitleSettings(true, true, "LEVEL 1:  PRE-GAME BREAKDOWN");
        pre0breakdown.NextSettings(true, true, true, "Begin!");
    }

    private void PreLevel1(PanelManager panelManager, RectTransform pagesParent, List<Page> pages)
    {
    // POST-0
        LevelScorePage levelScore0 = (LevelScorePage)AddPage(levelScore, panelManager, pagesParent, pages);
        levelScore0.Generate(0);

        CascadeTextPage a = (CascadeTextPage)AddPage(cascadeText, panelManager, pagesParent, pages);
        a.TitleSettings(true, true, "");
        a.SetTextContent("Here are the breakdowns. Pay attention to the total mass of the molecules in the chamber before and after the level!");

        BreakdownPage pre0breakdown = (BreakdownPage)AddPage(breakdown, panelManager, pagesParent, pages);
        pre0breakdown.SetLevel(0, true);

        pre0breakdown.TitleSettings(true, true, "LEVEL 1:  PRE-GAME BREAKDOWN");
        BreakdownPage post0breakdown = (BreakdownPage)AddPage(breakdown, panelManager, pagesParent, pages);
        post0breakdown.SetLevel(0, false);
        post0breakdown.TitleSettings(true, true, "LEVEL 1:  POST-GAME BREAKDOWN");

        QuizPage quiz0 = (QuizPage)AddPage(quiz, panelManager, pagesParent, pages);
        quiz0.TitleSettings(true, true, "LEVEL 1:  QUIZ");
        quiz0.LoadQuiz(QuizID.Level0);

        ScorePage score0 = (ScorePage)AddPage(score, panelManager, pagesParent, pages);
        score0.backActive = false;
        score0.SetLevel(0);

    // PRE-1
        CascadeTextPage b = (CascadeTextPage)AddPage(cascadeText, panelManager, pagesParent, pages);
        b.SetTextContent("In the real world, when a chemical reaction occurs, it affects the temperature of the matter around it.\n\n" +
            "All chemical reactions are either <i>endothermic</i> or <i>exothermic</i>.\n\n" +
            "For an endothermic reaction to occur, it requires more energy than it has stored in its chemical bonds, and so it must absorb energy from its surroundings (usually in the form of heat).\n\n" +
            "Exothermic reactions, on the other hand, produce an excess of energy, which is then released into the surroundings (again, often as heat).");
        b.TitleSettings(true, true, "");
        b.backActive = false;

        CascadeTextPage c = (CascadeTextPage)AddPage(cascadeText, panelManager, pagesParent, pages);
        c.SetTextContent("When an endothermic reaction absorbs heat from its surroundings, it slows down the movements of the surrounding molecules. When an exothermic reactions emits heat, it speeds up the movements of the surrounding molecules.\n\n" +
            "The faster that molecules are moving, the more frequently they collide, and potentially react with each other. Therefore, warmer reactants will tend to react at a higher rate than cooler ones.\n\n" +
            "That is why an egg fries faster when you turn up the element: the increased heat energizes the egg's molecules, leading to a higher rate of chemical reactions!");
        c.TitleSettings(true, true, "");

        CascadeTextPage d = (CascadeTextPage)AddPage(cascadeText, panelManager, pagesParent, pages);
        d.SetTextContent("In the next level, you will be required to make 4 H" + S(2) + "O (water) molecules from 2 H" + S(2) + " molecules, 2 Cl molecules, and 4 NaHCO" + S(3) + " (sodium bicarbonate, or baking soda) molecules.\n\n" +
            "This will require that you use the reaction from the previous level together with a new one:\n\n" +
            "NaHCO" + S(3) + " + HCl  →  NaCl + CO" + S(2) + " + H" + S(2) + "O");
        d.TitleSettings(true, true, "");

        CascadeTextPage e = (CascadeTextPage)AddPage(cascadeText, panelManager, pagesParent, pages);
        e.TitleSettings(true, true, "");
        e.SetTextContent("Now for the details:");

        BreakdownPage pre1breakdown = (BreakdownPage)AddPage(breakdown, panelManager, pagesParent, pages);
        pre1breakdown.SetLevel(1, true);
        pre1breakdown.TitleSettings(true, true, "LEVEL 2:  PRE-GAME BREAKDOWN");
        pre1breakdown.NextSettings(true, true, true, "Begin!");
    }

    private void PreLevel2(PanelManager panelManager, RectTransform pagesParent, List<Page> pages)
    {
    // POST-1
        LevelScorePage levelScore1 = (LevelScorePage)AddPage(levelScore, panelManager, pagesParent, pages);
        levelScore1.Generate(1);
        CascadeTextPage f = (CascadeTextPage)AddPage(cascadeText, panelManager, pagesParent, pages);
        f.TitleSettings(true, true, "");
        f.SetTextContent("In the upcoming breakdowns, verify whether the amount of any kind of atom changed during the level.");

        BreakdownPage pre1breakdown = (BreakdownPage)AddPage(breakdown, panelManager, pagesParent, pages);
        pre1breakdown.SetLevel(1, true);
        pre1breakdown.TitleSettings(true, true, "LEVEL 2:  PRE-GAME BREAKDOWN");
        BreakdownPage post1breakdown = (BreakdownPage)AddPage(breakdown, panelManager, pagesParent, pages);
        post1breakdown.SetLevel(1, false);
        post1breakdown.TitleSettings(true, true, "LEVEL 2:  POST-GAME BREAKDOWN");

        QuizPage quiz1 = (QuizPage)AddPage(quiz, panelManager, pagesParent, pages);
        quiz1.TitleSettings(true, true, "LEVEL 2:  QUIZ");
        quiz1.LoadQuiz(QuizID.Level1);

        ScorePage score1 = (ScorePage)AddPage(score, panelManager, pagesParent, pages);
        score1.backActive = false;
        score1.SetLevel(1);

    // PRE-2
        CascadeTextPage a = (CascadeTextPage)AddPage(cascadeText, panelManager, pagesParent, pages);
        a.SetTextContent("A chemical equation is considered a <i>balanced</i> chemical equation if the total number of each type of atom in the reactants " +
            "is equal to the total number of each type of atom in the products.");
        a.TitleSettings(true, true, "");
        a.backActive = false;

        CascadeTextPage b = (CascadeTextPage)AddPage(cascadeText, panelManager, pagesParent, pages);
        b.SetTextContent("Remember: when considering a chemical expression, such as 3CO" + S(2) + ", the large number that comes before the molecule refers to the amount of " +
            "that type of molecule in the situation, while the small subscript numbers that come after certain atoms refer to the amount of that kind of atom in " +
            "every molecule of that type.\n\n" +

            "(If there is no subscript following an atom in such an expression, it means there is just 1 of those atoms in the molecule).");
        b.TitleSettings(true, true, "");

        CascadeTextPage c = (CascadeTextPage)AddPage(cascadeText, panelManager, pagesParent, pages);
        c.SetTextContent("So, 3CO" + S(2) + " refers to 3 carbon dioxide molecules, each of which contains a single carbon atom and two oxygen atoms, for a total of 3 carbon atoms and 6 oxygen atoms.\n\n" +

            "By totaling up the atoms on either side of a chemical equation in this manner, we can check whether the equation is balanced!");
        c.TitleSettings(true, true, "");

        CascadeTextPage d = (CascadeTextPage)AddPage(cascadeText, panelManager, pagesParent, pages);
        d.SetTextContent("For instance, consider the following equation:  4Fe + 3O" + S(2) + "  →  Fe" + S(2) + "O" + S(3) + "\n\n" +

            "This says that 4 Fe (iron) molecules react with 3 O" + S(2) + " (oxygen) molecules to create one Fe" + S(2) + "O" + S(3) + " (ferric oxide) molecule. " +
            "The reactants are made up of 4 iron atoms and 6 oxygen atoms. However, the ferric oxide molecule that is produced " +
            "contains only 2 iron atoms and 3 oxygen atoms; half of the iron and half of the oxygen have gone missing! Therefore, this equation must not be balanced.\n\n" +

            "However, if the reaction produced <i>two</i> ferric oxide molecules, then the reactants and the products would be made up of the same number and types of atoms! " +
            "The balanced equation looks like this:  4Fe + 3O" + S(2) + "  →  2Fe" + S(2) + "O" + S(3) + ".");
        d.TitleSettings(true, true, "");

        CascadeTextPage e = (CascadeTextPage)AddPage(cascadeText, panelManager, pagesParent, pages);
        e.SetTextContent("In the next level, you will be required to produce 6 NaCl (sodium chloride, or salt) molecules out of 3 H" + S(2) + " molecules, 3 Cl molecules, 4 NaHCO" + S(3) + " molecules, " +
            "and 2 Na" + S(2) + "O (sodium oxide) molecules. You will have to use the reactions from the previous levels as well as two new reactions:\n\n" +

            "Na" + S(2) + "O + H" + S(2) + "O  →  2NaOH\n" +

            "NaOH + HCl  →  NaCl + H" + S(2) + "O");
        e.TitleSettings(true, true, "");

        CascadeTextPage g = (CascadeTextPage)AddPage(cascadeText, panelManager, pagesParent, pages);
        g.TitleSettings(true, true, "");
        g.SetTextContent("Let's check the Pre-Game Breakdown:");

        BreakdownPage pre2breakdown = (BreakdownPage)AddPage(breakdown, panelManager, pagesParent, pages);
        pre2breakdown.SetLevel(2, true);
        pre2breakdown.TitleSettings(true, true, "LEVEL 3:  PRE-GAME BREAKDOWN");
        pre2breakdown.NextSettings(true, true, true, "Begin!");
    }

    private void PreLevel3(PanelManager panelManager, RectTransform pagesParent, List<Page> pages)
    {
    // POST-2
        LevelScorePage levelScore2 = (LevelScorePage)AddPage(levelScore, panelManager, pagesParent, pages);
        levelScore2.Generate(2);

        CascadeTextPage e = (CascadeTextPage)AddPage(cascadeText, panelManager, pagesParent, pages);
        e.TitleSettings(true, true, "");
        e.SetTextContent("As you've probably come to expect, the following breakdowns will confirm that no matter was lost or gained during the level!");

        BreakdownPage pre2breakdown = (BreakdownPage)AddPage(breakdown, panelManager, pagesParent, pages);
        pre2breakdown.SetLevel(2, true);
        pre2breakdown.TitleSettings(true, true, "LEVEL 3:  PRE-GAME BREAKDOWN");
        BreakdownPage post2breakdown = (BreakdownPage)AddPage(breakdown, panelManager, pagesParent, pages);
        post2breakdown.SetLevel(2, false);
        post2breakdown.TitleSettings(true, true, "LEVEL 3:  POST-GAME BREAKDOWN");

        QuizPage quiz2 = (QuizPage)AddPage(quiz, panelManager, pagesParent, pages);
        quiz2.TitleSettings(true, true, "LEVEL 3:  QUIZ");
        quiz2.LoadQuiz(QuizID.Level2);

        ScorePage score2 = (ScorePage)AddPage(score, panelManager, pagesParent, pages);
        score2.backActive = false;
        score2.SetLevel(2);

    // PRE-3
        CascadeTextPage a = (CascadeTextPage)AddPage(cascadeText, panelManager, pagesParent, pages);
        a.SetTextContent("Sometimes, part of a reactant can appear to go missing during a chemical reaction.\n\n" +

            "However, we know that this never actually happens. Usually, the “missing” matter either escaped as a gas (sometimes an invisible gas), " +
            "or else combined with something else to form a new substance with different properties.");
        a.TitleSettings(true, true, "");
        a.backActive = false;

        CascadeTextPage b = (CascadeTextPage)AddPage(cascadeText, panelManager, pagesParent, pages);
        b.SetTextContent("For example, think of a burning log. The wood is reacting with nearby oxygen molecules in a chemical reaction called <i>combustion</i>.\n\n" +

            "When the fire has finished, it might seem like the reaction has destroyed some of the reactant matter: the ash and coals that remain weigh a lot less than the log did before it was burned.\n\n" +

            "However, it turns out that if you capture all the gas and smoke that rises from the log during the reaction, and add their masses to the masses of the ash and coal, " +
            "the total mass will always be equal to that of the original log!");
        b.TitleSettings(true, true, "");

        CascadeTextPage c = (CascadeTextPage)AddPage(cascadeText, panelManager, pagesParent, pages);
        c.SetTextContent("When you balance a chemical equation, you are ensuring that the chemical reaction can occur without breaking the law of conservation of matter. In a balanced equation, " +
            "the total mass of the products is always equal to the total mass of the reactants.\n\n" +

            "Applying this principle to physical systems is known as using a <i>mass balance</i>. If you account for every atom going <i>into</i> a chemical reaction, " +
            "you already know what atoms will make up the <i>products</i> of the chemical reaction!");
        c.TitleSettings(true, true, "");

        CascadeTextPage d = (CascadeTextPage)AddPage(cascadeText, panelManager, pagesParent, pages);
        d.SetTextContent("In the final level, you need to produce 3 H" + S(2) + "O molecules out of 1 Na" + S(2) + "O molecule, 1 NaHCO" + S(3) + " molecule, 3 Cl molecules, 3 H" + S(2) + " molecules, " +
            "and 1 K" + S(2) + "O (potassium oxide) molecule. You will need to use every chemical reaction from the previous levels, as well as two new reactions:\n\n" +

            "H" + S(2) + "O + K" + S(2) + "O  →  2KOH\n" +
            "KOH + HCl  →  KCl + H" + S(2) + "O\n\n" +

            "Good luck!");
        d.TitleSettings(true, true, "");

        CascadeTextPage f = (CascadeTextPage)AddPage(cascadeText, panelManager, pagesParent, pages);
        f.TitleSettings(true, true, "");
        f.SetTextContent("Here's the Pre-Game Breakdown for the final level:");

        BreakdownPage pre3breakdown = (BreakdownPage)AddPage(breakdown, panelManager, pagesParent, pages);
        pre3breakdown.SetLevel(3, true);
        pre3breakdown.TitleSettings(true, true, "LEVEL 4:  PRE-GAME BREAKDOWN");
        pre3breakdown.NextSettings(true, true, true, "Begin!");
    }

    private void Final(PanelManager panelManager, RectTransform pagesParent, List<Page> pages)
    {
    // POST-3
        LevelScorePage levelScore3 = (LevelScorePage)AddPage(levelScore, panelManager, pagesParent, pages);
        levelScore3.Generate(3);

        CascadeTextPage h = (CascadeTextPage)AddPage(cascadeText, panelManager, pagesParent, pages);
        h.TitleSettings(true, true, "");
        h.SetTextContent("Let's do one last comparison:");

        BreakdownPage pre3breakdown = (BreakdownPage)AddPage(breakdown, panelManager, pagesParent, pages);
        pre3breakdown.SetLevel(3, true);
        pre3breakdown.TitleSettings(true, true, "LEVEL 4:  PRE-GAME BREAKDOWN");
        BreakdownPage post3breakdown = (BreakdownPage)AddPage(breakdown, panelManager, pagesParent, pages);
        post3breakdown.SetLevel(3, false);
        post3breakdown.TitleSettings(true, true, "LEVEL 4:  POST-GAME BREAKDOWN");

        QuizPage quiz3 = (QuizPage)AddPage(quiz, panelManager, pagesParent, pages);
        quiz3.TitleSettings(true, true, "LEVEL 4:  QUIZ");
        quiz3.LoadQuiz(QuizID.Level3);

        ScorePage score3 = (ScorePage)AddPage(score, panelManager, pagesParent, pages);
        score3.backActive = false;
        score3.SetLevel(3);

    // CONCLUSION
        CascadeTextPage a = (CascadeTextPage)AddPage(cascadeText, panelManager, pagesParent, pages);
        a.SetTextContent("This final lesson is optional material. Feel free to skip it, but we recommend you check it out!\n\n" +
            "It covers two chemical reactions that we think are truly fascinating. These reactions are occurring constantly around you and inside of you, and they are fundamental to life on earth!");
        a.TitleSettings(true, true, "");
        a.backActive = false;
        a.canSkip = true;
        
        CascadeTextPage b = (CascadeTextPage)AddPage(cascadeText, panelManager, pagesParent, pages);
        b.SetTextContent("<i>Photosynthesis</i> is a chemical reaction used by plant life to harness the energy in sunlight. It follows this equation:  " +
            "6H" + S(2) + "O + 6CO" + S(2) + "  →  C" + S(6) + "H" + S(1) + S(2) + "O" + S(6) + " + 6O" + S(2) + ".\n\n" +

            "So, this says that photosynthesis is a reaction where 6 water molecules and 6 carbon dioxide molecules combine to form 1 big glucose molecule, and 6 oxygen molecules.\n\n" +

            "However, photosynthesis is endothermic, so it needs energy from an outside source to occur. This energy is supplied by sunlight, and is stored in the chemical bonds " +
            "inside the glucose molecule and the 6 oxygen molecules.");
        b.TitleSettings(true, true, "");
        b.canSkip = true;

        CascadeTextPage c = (CascadeTextPage)AddPage(cascadeText, panelManager, pagesParent, pages);
        c.SetTextContent("When an animal eats a plant, it consumes the plant’s glucose. When the animal inhales, it consumes oxygen molecules that were produced by photosynthesis reactions. " +
            "A chemical reaction then occurs inside the body of the animal, known as <i>cellular respiration</i>.\n\n" +

            "Cellular respiration follows this equation:  C" + S(6) + "H" + S(1) + S(2) + "O" + S(6) + " + 6O" + S(2) + "  →  6H" + S(2) + "O + 6CO" + S(2) + ".\n\n" +

            "Notice anything about this equation? It’s the exact reverse of photosynthesis! So, cellular respiration occurs when 1 glucose molecule and 6 oxygen molecules " +
            "combine to produce 6 water molecules and 6 carbon dioxide molecules.");
        c.TitleSettings(true, true, "");
        c.canSkip = true;

        CascadeTextPage d = (CascadeTextPage)AddPage(cascadeText, panelManager, pagesParent, pages);
        d.SetTextContent("However, cellular respiration is an exothermic reaction, so it releases energy as it occurs. In fact, the amount of energy it releases is equal to the amount of " +
            "sunlight energy that it took for the photosynthesis reaction that created the glucose in the first place! This energy is then used by the cells of the animal, " +
            "and is what enables animals to move, think, and do pretty much everything that they do!");
        d.TitleSettings(true, true, "");
        d.canSkip = true;

        CascadeTextPage e = (CascadeTextPage)AddPage(cascadeText, panelManager, pagesParent, pages);
        e.SetTextContent("After the cellular respiration reaction has occurred, the 6 water molecules and 6 carbon dioxide molecules are shed from the body of the animal and released back " +
            "into the atmosphere, where they can once again be the reactants of a photosynthesis reaction.\n\n" +

            "This is how plants and animals continuously exchange matter and energy in a way that benefits them both, and allows life to harness energy from the sun’s rays. " +
            "This is one of the reasons it is so important to preserve plant life on earth!");
        e.TitleSettings(true, true, "");
        e.canSkip = true;

        CascadeTextPage f = (CascadeTextPage)AddPage(cascadeText, panelManager, pagesParent, pages);
        f.SetTextContent("If you were playing close attention, you might have noticed that throughout the entire cycle of photosynthesis and cellular respiration, no matter went missing.\n\n" +

            "Though the atoms may change partners on a regular basis, it is always the very same atoms, dancing on and on!");
        f.TitleSettings(true, true, "");
        f.canSkip = true;

        CascadeTextPage g = (CascadeTextPage)AddPage(cascadeText, panelManager, pagesParent, pages);
        g.SetTextContent("Thanks for playing! We hope you enjoyed your playthrough of <i>Dance of Atoms</i>.\n\n" +

            "Have a great day!");
        g.TitleSettings(true, true, "");
        g.NextSettings(true, true, true, "Finish");
        g.canSkip = true;
    }

    public List<Page> LoadPages(PanelID panelID, PanelManager panelManager)
    {
        RectTransform pagesParent = panelManager.pagesParent;
        List<Page> pages = new List<Page>();
        if (!panelManager || !pagesParent)
            return pages;

        switch (panelID)
        {
            case PanelID.PreLevel0:
                PreLevel0(panelManager, pagesParent, pages);
                break;
            case PanelID.PreLevel1:
                PreLevel1(panelManager, pagesParent, pages);
                break;
            case PanelID.PreLevel2:
                PreLevel2(panelManager, pagesParent, pages);
                break;
            case PanelID.PreLevel3:
                PreLevel3(panelManager, pagesParent, pages);
                break;
            case PanelID.Final:
                Final(panelManager, pagesParent, pages);
                break;

            case PanelID.PanelTutorial:
                PanelTutorial(panelManager, pagesParent, pages);
                break;
            default:
                break;
        }
        int pagesCount = pages.Count;
        for (int i = 0; i < pagesCount; i++)
            pages[i].SetPageNumber(i, pagesCount);
        return pages;
    }

    public Page AddPage(string prefabName, PanelManager panelManager, RectTransform pagesParent, List<Page> pages)
    {
        prefabName = pagePrefabDirectory + prefabName;
        GameObject go = Object.Instantiate(Resources.Load(prefabName)) as GameObject;
        go.transform.SetParent(pagesParent, false);
        Page page = go.GetComponent<Page>();
        page.AssignPanelManager(panelManager);
        pages.Add(page);
        return page;
    }

    private void PanelTutorial(PanelManager panelManager, RectTransform pagesParent, List<Page> pages)
    {
        /*  
            For each page:

            1.  Declare an instance of Page (base/derived class)* as the return of this.AddPage():
                    Page AddPage(string prefabName, PanelManager panelManager, RectTransform pagesParent, List<Page> pages)
                        prefabName: choose from the strings declared above:
                            simpleText / scrollText / cascadeText
                            simpleImage / scrollImage
                            quiz
                        For the other three variables, always use:
                            "panelManager, pagesParent, pages"
              * Note on Page base/derived classes:
                    Most of the prefabs use the simple base Page class,
                    but some of them use specialized derived classes.
                    Be sure to declare the right type of class based on
                    what kind of page you are creating, and cast AddPage() to that type
                        Page:               simpleText / scrollText
                                            simpleImage / scrollImage
                        CascadeTextPage:    cascadeText
                        QuizPage:           quiz

            2.  Set the relevant Panel settings for the Page.
                These five Page methods apply to the appearance/function of the Panel container,
                rather than the Page content, and are applied by PanelManager.ApplyPageSettings()
                every time a page is (re)opened.
                Refer to the Page class/prefabs to see the default values.
                    void ScrollSettings(bool scrRect, bool scrollbar, bool pageMask)
                    void TitleSettings(bool active, bool textOverride, string text)
                    void NextSettings(bool active, bool enabled, bool textOverride, string text)
                    void BackSettings(bool active, bool enabled, bool textOverride, string text)
                    void InfoSettings(bool active, bool enabled, bool textOverride, string text)
                        If you need to change these properties at some point after the Panel has
                        started displaying the Page, you can either modify them directly
                        through PanelManager, or else update the settings from within the Page,
                        and then call panelManager.ApplyPageSettings(panelManager.GetCurrentPage())

            3.  Set the relevant Page settings.
                These directly affect Page content.
                    Page:
                       *virtual void SetTextContent(string str)
                        void SetTextAlignment(TextAnchor alignment)
                        void SetTextSize(int fontSize)
                        void SetImageContent(Sprite sprite)
                        void SetImageAlpha(float alpha)
                    CascadeTextPage:
                       *override void SetTextContent(string str)
                        void CascadeTextSettings(float charactersPerSecond, bool autoScroll, float smoothTime, IfExceedsPage ifExceedsPage)
                            For autoscroll, these values are good:
                            charactersPerSecond = 60f  /  smoothTime = 3f
                    QuizPage:
                        void LoadQuiz(QuizID id)

            4.  Make any other custom adjustments as needed by directly modifying values as needed.

            5.  For efficiency, set pages.Capacity to the total number of Pages to be added,
                before the initial AddPage() call.
        */

        pages.Capacity = 6;

        Page a = AddPage(simpleText, panelManager, pagesParent, pages);
        a.SetTextContent("SimpleTextPage prefab, generated by PageLoader.\n\n" +
            "The panel is in its default state (except for this text content).\n\n" +
            "(Back button is automatically inactive on first page.)");

        Page b = AddPage(simpleText, panelManager, pagesParent, pages);
        b.TitleSettings(false, true, "This title is invisible");
        // when title is set inactive, page content is automatically stretched upward
        b.NextSettings(true, true, true, ">");
        b.BackSettings(true, false, true, "<");
        b.InfoSettings(true, true, true, "Information text.");
        b.SetTextAlignment(TextAnchor.UpperRight);
        b.SetTextSize(22);
        b.SetTextContent("Title has been set inactive. Page content has been automatically stretched upward.\n\n" +
            "Back button has been disabled.\n\n" +
            "Info has been activated, with toggle button in lower left corner.\n\n" +
            "Text alignment and font size have been modified.");

        QuizPage c = (QuizPage)AddPage(quiz, panelManager, pagesParent, pages);
        c.TitleSettings(true, true, "QuizPage: Quiz Tutorial");
        c.LoadQuiz(QuizID.QuizTutorial);
        // quiz content is defined in QuestionLoader
        // no need to set NextButton text; QuizPage manages this automatically

        Page d = AddPage(simpleImage, panelManager, pagesParent, pages);
        d.TitleSettings(true, true, "SimpleImagePage");
        d.NextSettings(true, true, true, "Onward!");
        d.SetImageContent(Resources.Load<Sprite>("Graphics/UI/button.selected"));
        d.SetImageAlpha(0.6f);

        Page e = AddPage(scrollImage, panelManager, pagesParent, pages);
        e.TitleSettings(true, true, "ScrollImagePage");
        e.SetImageContent(Resources.Load<Sprite>("Graphics/fish.circuit"));

        CascadeTextPage f = (CascadeTextPage)AddPage(cascadeText, panelManager, pagesParent, pages);
        f.SetTextContent("Abcd efgh i jkl mnopq rst uvw xy z, a bc defg, hij kl mno pqrs t u vwx, yzab, cd efgh ij klmn opq rstuv wx y " +
            "zabc de fg hij klmno pqr st uvwx y z. Abcd efgh i jkl mnopq rst uvw xy z, a bc defg, hij kl mno pqrs t u vwx, yzab, cd efgh " +
            "ij klmn opq rstuv wx y zabc de fg hij klmno pqr st uvwx y z. Abcd efgh i jkl mnopq rst uvw xy z, a bc defg, hij kl mno pqrs " +
            "t u vwx, yzab, cd efgh ij klmn opq rstuv wx y zabc de fg hij klmno pqr st uvwx y z. Abcd efgh i jkl mnopq rst uvw xy z, a bc " +
            "defg, hij kl mno pqrs t u vwx, yzab, cd efgh ij klmn opq rstuv wx y zabc de fg hij klmno pqr st uvwx y z. Abcd efgh i jkl mnopq " +
            "rst uvw xy z, a bc defg, hij kl mno pqrs t u vwx, yzab, cd efgh ij klmn opq rstuv wx y zabc de fg hij klmno pqr st uvwx y z. " +
            "Abcd efgh i jkl mnopq rst uvw xy z, a bc defg, hij kl mno pqrs t u vwx, yzab, cd efgh ij klmn opq rstuv wx y zabc de fg hij " +
            "klmno pqr st uvwx y z. Abcd efgh i jkl mnopq rst uvw xy z, a bc defg, hij kl mno pqrs t u vwx, yzab, cd efgh ij klmn opq rstuv " +
            "wx y zabc de fg hij klmno pqr st uvwx y z. Abcd efgh i jkl mnopq rst uvw xy z, a bc defg, hij kl mno pqrs t u vwx, yzab, cd efgh " +
            "ij klmn opq rstuv wx y zabc de fg hij klmno pqr st uvwx y z. Abcd efgh i jkl mnopq rst uvw xy z, a bc defg, hij kl mno pqrs t u " +
            "vwx, yzab, cd efgh ij klmn opq rstuv wx y zabc de fg hij klmno pqr st uvwx y z. Abcd efgh i jkl mnopq rst uvw xy z, a bc defg, " +
            "hij kl mno pqrs t u vwx, yzab, cd efgh ij klmn opq rstuv wx y zabc de fg hij klmno pqr st uvwx y z. Abcd efgh i jkl mnopq rst uvw " +
            "xy z, a bc defg, hij kl mno pqrs t u vwx, yzab, cd efgh ij klmn opq rstuv wx y zabc de fg hij klmno pqr st uvwx y z. Abcd efgh i " +
            "jkl mnopq rst uvw xy z, a bc defg, hij kl mno pqrs t u vwx, yzab, cd efgh ij klmn opq rstuv wx y zabc de fg hij klmno pqr st uvwx " +
            "y z. Abcd efgh i jkl mnopq rst uvw xy z, a bc defg, hij kl mno pqrs t u vwx, yzab, cd efgh ij klmn opq rstuv wx y zabc de fg hij ");
        f.TitleSettings(true, true, "CascadeTextPage");
        f.CascadeTextSettings(150f, true, 3f, IfExceedsPage.ExtendDownward);
        f.NextSettings(true, true, true, "Goodbye");
    }

    public char S(int s)
    {
        return GV.SubscriptNumeral(s);
    }
}
