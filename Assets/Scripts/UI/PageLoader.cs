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

    private PanelManager panelManager;
    private RectTransform pagesParent;
    private List<Page> pages;

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

    private void PreLevel0()
    {
        // INTRODUCTION, PRE-0
        CascadeTextPage a = (CascadeTextPage)AddPage(cascadeText);
        a.TitleSettings(true, true, "");
        a.SetTextContent("<i>Dance of Atoms</i> is a game about what happens to atoms and molecules during <i>chemical reactions</i>.\n\n" +

            "Chemical reactions occur when atoms of colliding molecules rearrange themselves by breaking current bonds and creating new ones, leading to the formation of new substances.");

        CascadeTextPage b = (CascadeTextPage)AddPage(cascadeText);
        b.TitleSettings(true, true, "CONSERVATION OF MATTER\n<size=18>CONSTANT NUMBER OF ATOMS FOR EACH ELEMENT  /  CONSTANT ATOMIC MASS</size>");
        b.SetTextContent("In a chemical reaction, the molecules that collide are called the <i>reactants</i>, and the molecules that are produced are called the <i>products</i>.\n\n" +
            "During a chemical reaction, atoms are never created or destroyed. Therefore, even though the atoms are rearranged into new molecules, the total amount of each type of atom in the reactants " +
            "is always equal to the total amount of each type of atom in the products.\n\n" +
            "Every atom of each type of element has a specific mass, known as <i>atomic mass</i> (measured in <i>atomic mass units</i>, or <i>amu</i>). During a chemical reaction, atoms’ atomic masses never change.");

        CascadeTextPage c = (CascadeTextPage)AddPage(cascadeText);
        c.TitleSettings(true, true, "CONSERVATION OF MATTER\n<size=18>CONSTANT TOTAL MASS</size>");
        c.SetTextContent("Because both the total number of each type of atom, and the individual mass of each atom, never change during a chemical reaction, the total mass of the reactants is " +
            "always the same as the total mass of the products.\n\n" +
            "This principle is known as the <i>conservation of matter in chemical reactions</i>.");

        CascadeTextPage d = (CascadeTextPage)AddPage(cascadeText);
        d.TitleSettings(true, true, "CONSERVATION OF MATTER\n<size=18>EXAMPLE</size>");
        d.SetTextContent("Think of it this way: if you build four chairs out of an assortment of pieces of wood, and then take them apart and use every piece of wood to build a desk and a stool instead, " +
            "the combined weight of the desk and the stool will equal the total weight of the four chairs!");

        Page tutorial0 = AddPage("Tutorial/TutorialPage0");
        tutorial0.TitleSettings(true, true, "TUTORIAL\n<size=18>TIMER  /  PAUSE BUTTON</size>");

        Page tutorial1 = AddPage("Tutorial/TutorialPage1");
        tutorial1.TitleSettings(true, true, "TUTORIAL\n<size=18>PROGRESS BAR</size>");

        Page tutorial2 = AddPage("Tutorial/TutorialPage2");
        tutorial2.TitleSettings(true, true, "TUTORIAL\n<size=18>SLINGSHOT:  LAUNCHING MOLECULES</size>");

        Page tutorial3 = AddPage("Tutorial/TutorialPage3");
        tutorial3.TitleSettings(true, true, "TUTORIAL\n<size=18>REACTIONS</size>");

        Page tutorial4 = AddPage("Tutorial/TutorialPage4");
        tutorial4.TitleSettings(true, true, "TUTORIAL\n<size=18>DISPLAYING LABELS</size>");

        CascadeTextPage f = (CascadeTextPage)AddPage(cascadeText);
        f.TitleSettings(true, true, "BREAKDOWNS\n<size=18>MASS  /  NUMBER OF ATOMS  /  TOTALS</size>");
        f.SetTextContent("Before and after each level, we will show you a detailed breakdown of all the molecules present in the level.\n\n" +

            "For each molecule, we will show its mass, the total number of atoms that it contains, and the number of each type of atom that it contains. At the bottom we will show the total for each of these values, " +
            "representing the total mass and number of atoms contained within the level as a whole.");

        CascadeTextPage g = (CascadeTextPage)AddPage(cascadeText);
        g.TitleSettings(true, true, "BREAKDOWNS\n<size=18>COMPARISON:  PRE-GAME  /  POST-GAME</size>");
        g.SetTextContent("Before each level, we will show you the <i>pre-game breakdown</i>, which represents the state of the level as it is at the very beginning, before you start playing.\n\n" +

            "After each level, we will show you the pre-game breakdown again, to remind you of how things were initially. Then we will show you a similar <i>post-game breakdown</i>, " +
            "which represents the state of the level after you have finished playing, so that you will be able to do a before/after comparison of the matter in the level.");

        CascadeTextPage e = (CascadeTextPage)AddPage(cascadeText);
        e.TitleSettings(true, true, "OBJECTIVE\n<size=18>LEVEL 1</size>");
        e.SetTextContent("In the first level, you will begin with:\n" +
            "    4 H₂ molecules (pure hydrogen)\n" +
            "    4 Cl₂ molecules (pure chlorine)\n\n" +
            "Your objective:\n" +
            "    Make <color=#77FF77>8 HCl</color> molecules (hydrogen chloride)\n\n" +
            "To do so, use the following chemical reaction:\n" +
            "    H₂ + Cl₂   →   <color=#77FF77>2HCl</color>");

        BreakdownPage pre0breakdown = (BreakdownPage)AddPage(breakdown);
        pre0breakdown.SetLevel(0, true);
        pre0breakdown.TitleSettings(true, true, "PRE-GAME BREAKDOWN\n<size=18>LEVEL 1</size>");
        pre0breakdown.NextSettings(true, true, true, "Begin!");
    }

    private void PreLevel1()
    {
        // POST-0
        LevelScorePage levelScore0 = (LevelScorePage)AddPage(levelScore);
        levelScore0.Generate(0);

        CascadeTextPage a = (CascadeTextPage)AddPage(cascadeText);
        a.TitleSettings(true, true, "");
        a.SetTextContent("Here are the breakdowns. Pay attention to the total mass of the molecules in the chamber both before and after the level!");

        BreakdownPage pre0breakdown = (BreakdownPage)AddPage(breakdown);
        pre0breakdown.SetLevel(0, true);

        pre0breakdown.TitleSettings(true, true, "PRE-GAME BREAKDOWN\n<size=18>LEVEL 1</size>");
        BreakdownPage post0breakdown = (BreakdownPage)AddPage(breakdown);
        post0breakdown.SetLevel(0, false);
        post0breakdown.TitleSettings(true, true, "POST-GAME BREAKDOWN\n<size=18>LEVEL 1</size>");

        CascadeTextPage ab = (CascadeTextPage)AddPage(cascadeText);
        ab.TitleSettings(true, true, "");
        ab.SetTextContent("Did the total mass of all the molecules ever change during the level?\nOr did the total mass remain constant?");

        QuizPage quiz0 = (QuizPage)AddPage(quiz);
        quiz0.TitleSettings(true, true, "QUIZ\n<size=18>LEVEL 1</size>");
        quiz0.LoadQuiz(QuizID.Level0);

        ScorePage score0 = (ScorePage)AddPage(score);
        score0.backActive = false;
        score0.SetLevel(0);

        // PRE-1
        CascadeTextPage b = (CascadeTextPage)AddPage(cascadeText);
        b.SetTextContent("In the real world, when a chemical reaction occurs, it affects the temperature of the matter around it.\n\n" +
            "All chemical reactions are either <i>endothermic</i> or <i>exothermic</i>.\n\n" +
            "For an endothermic reaction to occur, it requires more energy than it has stored in its chemical bonds, and so it must absorb energy from its surroundings (usually in the form of heat).\n\n" +
            "Exothermic reactions, on the other hand, produce an excess of energy, which is then released into the surroundings (again, often as heat).");
        b.TitleSettings(true, true, "REACTIONS:  <size=30>ENDOTHERMIC / EXOTHERMIC</size>\n<size=18>ABSORBING ENERGY  /  RELEASING ENERGY</size>");
        b.backActive = false;

        CascadeTextPage c = (CascadeTextPage)AddPage(cascadeText);
        c.SetTextContent("When an endothermic reaction absorbs heat from its surroundings, it slows down the movements of the surrounding molecules. When an exothermic reactions emits heat, it speeds up the movements of the surrounding molecules.\n\n" +
            "The faster that molecules are moving, the more frequently they collide, and potentially react with each other. Therefore, warmer reactants will tend to react at a higher rate than cooler ones.\n\n" +
            "That is why an egg fries faster when you turn up the element: the increased heat energizes the egg’s molecules, leading to a higher rate of chemical reactions!");
        c.TitleSettings(true, true, "REACTIONS:  <size=30>ENDOTHERMIC / EXOTHERMIC</size>\n<size=18>HEAT  /  MOLECULAR SPEED  /  REACTION RATE</size>");

        CascadeTextPage d = (CascadeTextPage)AddPage(cascadeText);
        d.SetTextContent("In the next level, you will begin with:\n" +
            "    2 H₂ molecules\n    2 Cl₂ molecules\n    4 NaHCO₃ molecules (sodium bicarbonate, or baking soda)\n\n" +
            "Your objective:\n    Make <color=#77FF77>4 H₂O</color> molecules (water)\n\n" +
            "To do so, use this new chemical reaction:\n    NaHCO₃ + HCl   →   NaCl + CO₂ + <color=#77FF77>H₂O</color>\n" +
            "together with the previous reaction:\n    H₂ + Cl₂   →   2HCl");
        d.TitleSettings(true, true, "OBJECTIVE\n<size=18>LEVEL 2</size>");

        BreakdownPage pre1breakdown = (BreakdownPage)AddPage(breakdown);
        pre1breakdown.SetLevel(1, true);
        pre1breakdown.TitleSettings(true, true, "PRE-GAME BREAKDOWN\n<size=18>LEVEL 2</size>");
        pre1breakdown.NextSettings(true, true, true, "Begin!");
    }

    private void PreLevel2()
    {
        // POST-1
        LevelScorePage levelScore1 = (LevelScorePage)AddPage(levelScore);
        levelScore1.Generate(1);

        BreakdownPage pre1breakdown = (BreakdownPage)AddPage(breakdown);
        pre1breakdown.SetLevel(1, true);
        pre1breakdown.TitleSettings(true, true, "PRE-GAME BREAKDOWN\n<size=18>LEVEL 2</size>");
        BreakdownPage post1breakdown = (BreakdownPage)AddPage(breakdown);
        post1breakdown.SetLevel(1, false);
        post1breakdown.TitleSettings(true, true, "POST-GAME BREAKDOWN\n<size=18>LEVEL 2</size>");

        CascadeTextPage x = (CascadeTextPage)AddPage(cascadeText);
        x.TitleSettings(true, true, "");
        x.SetTextContent("Did the total number of any kind of atom change at all during the level?\nOr did the amounts of each kind—the amount of hydrogen, of carbon, of oxygen, of sodium, of chlorine, and of potassium—all remain constant?");

        QuizPage quiz1 = (QuizPage)AddPage(quiz);
        quiz1.TitleSettings(true, true, "QUIZ\n<size=18>LEVEL 2</size>");
        quiz1.LoadQuiz(QuizID.Level1);

        ScorePage score1 = (ScorePage)AddPage(score);
        score1.backActive = false;
        score1.SetLevel(1);

        // PRE-2
        CascadeTextPage a = (CascadeTextPage)AddPage(cascadeText);
        a.SetTextContent("A chemical equation is considered a <i>balanced</i> chemical equation if the total number of each type of atom in the reactants " +
            "is equal to the total number of each type of atom in the products.");
        a.TitleSettings(true, true, "BALANCED EQUATIONS\n<size=18>DEFINITION</size>");
        a.backActive = false;

        CascadeTextPage b = (CascadeTextPage)AddPage(cascadeText);
        b.SetTextContent("Remember: when considering a chemical expression, such as 3CO₂, the large number that comes before the molecule (the <i>coefficient</i>) refers to the amount of " +
            "that type of molecule in the situation, while the small numbers that come after certain atoms (the <i>subscripts</i>) refer to the amount of that kind of atom in " +
            "every molecule of that type.\n\n" +

            "(If there is no coefficient before a molecule, it means that there is just 1 of those molecules in the situation. Similarly, if there is no subscript after an atom, it means there is just 1 of those atoms in the molecule).");
        b.TitleSettings(true, true, "BALANCED EQUATIONS\n<size=18>COEFFICIENTS  /  SUBSCRIPTS</size>");

        CascadeTextPage c = (CascadeTextPage)AddPage(cascadeText);
        c.SetTextContent("So, 3CO₂ refers to 3 carbon dioxide molecules, each of which contains a single carbon atom and two oxygen atoms, for a total of 3 carbon atoms and 6 oxygen atoms.\n\n" +

            "By totaling up the atoms on either side of a chemical equation in this manner, we can check whether the equation is balanced!");
        c.TitleSettings(true, true, "BALANCED EQUATIONS\n<size=18>CALCULATING NUMBER OF ATOMS</size>");

        CascadeTextPage d = (CascadeTextPage)AddPage(cascadeText);
        d.SetTextContent("For instance, consider the following equation:  4Fe + 3O₂   →   Fe₂O₃\n\n" +

            "This says that 4 Fe (iron) molecules react with 3 O₂ (oxygen) molecules to create one Fe₂O₃ (ferric oxide) molecule. " +
            "The reactants are made up of 4 iron atoms and 6 oxygen atoms. However, the ferric oxide molecule that is produced " +
            "contains only 2 iron atoms and 3 oxygen atoms; half of the iron and half of the oxygen have gone missing! Therefore, this equation must not be balanced.\n\n" +

            "However, if the reaction produced <i>two</i> ferric oxide molecules, then the reactants and the products would be made up of the same number and types of atoms! " +
            "The balanced equation looks like this:  4Fe + 3O₂   →   2Fe₂O₃.");
        d.TitleSettings(true, true, "BALANCED EQUATIONS\n<size=18>EVALUATING AND BALANCING EQUATIONS</size>");

        CascadeTextPage e = (CascadeTextPage)AddPage(cascadeText);
        e.SetTextContent("In the next level, you will begin with:\n" +
            "    3 H₂ molecules\n    3 Cl₂ molecules\n    4 NaHCO₃ molecules\n    2 Na₂O molecules (sodium oxide)\n\n" +
            "Your objective:\n    Make <color=#77FF77>6 NaCl</color> molecules (sodium chloride, or salt)\n\n" +
            "To do so, use these new chemical reactions:\n    Na₂O + H₂O   →   2NaOH\n    NaOH + HCl   →   <color=#77FF77>NaCl</color> + H₂O\n" +
            "together with the previous reactions:\n    H₂ + Cl₂   →   2HCl\n    NaHCO₃ + HCl   →   <color=#77FF77>NaCl</color> + CO₂ + H₂O");
        e.TitleSettings(true, true, "OBJECTIVE\n<size=18>LEVEL 3</size>");

        BreakdownPage pre2breakdown = (BreakdownPage)AddPage(breakdown);
        pre2breakdown.SetLevel(2, true);
        pre2breakdown.TitleSettings(true, true, "PRE-GAME BREAKDOWN\n<size=18>LEVEL 3</size>");
        pre2breakdown.NextSettings(true, true, true, "Begin!");
    }

    private void PreLevel3()
    {
        // POST-2
        LevelScorePage levelScore2 = (LevelScorePage)AddPage(levelScore);
        levelScore2.Generate(2);

        BreakdownPage pre2breakdown = (BreakdownPage)AddPage(breakdown);
        pre2breakdown.SetLevel(2, true);
        pre2breakdown.TitleSettings(true, true, "PRE-GAME BREAKDOWN\n<size=18>LEVEL 3</size>");
        BreakdownPage post2breakdown = (BreakdownPage)AddPage(breakdown);
        post2breakdown.SetLevel(2, false);
        post2breakdown.TitleSettings(true, true, "POST-GAME BREAKDOWN\n<size=18>LEVEL 3</size>");

        CascadeTextPage e = (CascadeTextPage)AddPage(cascadeText);
        e.TitleSettings(true, true, "");
        e.SetTextContent("Was any matter lost or gained during the level?\nOr was it all conserved?");

        QuizPage quiz2 = (QuizPage)AddPage(quiz);
        quiz2.TitleSettings(true, true, "QUIZ\n<size=18>LEVEL 3</size>");
        quiz2.LoadQuiz(QuizID.Level2);

        ScorePage score2 = (ScorePage)AddPage(score);
        score2.backActive = false;
        score2.SetLevel(2);

        // PRE-3
        CascadeTextPage a = (CascadeTextPage)AddPage(cascadeText);
        a.SetTextContent("Sometimes, part of a reactant can appear to go missing during a chemical reaction.\n\n" +

            "However, we know that this never actually happens. Usually, the “missing” matter either escaped as a gas (sometimes an invisible gas), " +
            "or else combined with something else to form a new substance with different properties.");
        a.TitleSettings(true, true, "ILLUSIONS <size=30>OF</size> NON-CONSERVATION\n<size=18>ESCAPED GAS  /  CHEMICAL TRANSFORMATIONS</size>");
        a.backActive = false;

        CascadeTextPage b = (CascadeTextPage)AddPage(cascadeText);
        b.SetTextContent("For example, think of a burning log. The wood is reacting with nearby oxygen molecules in a chemical reaction called <i>combustion</i>.\n\n" +

            "When the fire has finished, it might seem like the reaction has destroyed some of the reactant matter: the ash and coals that remain weigh a lot less than the log did before it was burned.\n\n" +

            "However, it turns out that if you capture all the gas and smoke that rises from the log during the reaction, and add their masses to the masses of the ash and coal, " +
            "the total mass will always be equal to that of the original log!");
        b.TitleSettings(true, true, "ILLUSIONS <size=30>OF</size> NON-CONSERVATION\n<size=18>COMBUSTION:  GAS / SMOKE</size>");

        CascadeTextPage c = (CascadeTextPage)AddPage(cascadeText);
        c.SetTextContent("When you balance a chemical equation, you are ensuring that the chemical reaction can occur without breaking the law of conservation of matter. In a balanced equation, " +
            "the total mass of the products is always equal to the total mass of the reactants.\n\n" +

            "Applying this principle to physical systems is known as using a <i>mass balance</i>. If you account for every atom going <i>into</i> a chemical reaction, " +
            "you already know what atoms will make up the <i>products</i> of the chemical reaction!");
        c.TitleSettings(true, true, "CONSERVATION <size=30>AND</size> BALANCED EQUATIONS\n<size=18>MASS BALANCES</size>");

        CascadeTextPage d = (CascadeTextPage)AddPage(cascadeText);
        d.SetTextContent("In the final level, you will begin with:\n" +
            "    3 H₂ molecules\n    3 Cl₂ molecules\n    1 NaHCO₃ molecule\n    1 Na₂O molecule\n    1 K₂O molecule (potassium oxide)\n\n" +
            "Your objective:\n    Make <color=#77FF77>3 H₂O</color> molecules\n\n" +
            "To do so, use these new chemical reactions:\n    <color=#FFA5A5>H₂O</color> + K₂O   →   2KOH\n    KOH + HCl   →   KCl + <color=#77FF77>H₂O</color>\n" +
            "together with the previous reactions:\n    H₂ + Cl₂   →   2HCl\n    NaHCO₃ + HCl   →   NaCl + CO₂ + <color=#77FF77>H₂O</color>\n    Na₂O + <color=#FFA5A5>H₂O</color>   →   2NaOH\n    NaOH + HCl   →   NaCl + <color=#77FF77>H₂O</color>");
        d.TitleSettings(true, true, "OBJECTIVE\n<size=18>LEVEL 4</size>");

        BreakdownPage pre3breakdown = (BreakdownPage)AddPage(breakdown);
        pre3breakdown.SetLevel(3, true);
        pre3breakdown.TitleSettings(true, true, "PRE-GAME BREAKDOWN\n<size=18>LEVEL 4</size>");
        pre3breakdown.NextSettings(true, true, true, "Begin!");
    }

    private void Final()
    {
        // POST-3
        LevelScorePage levelScore3 = (LevelScorePage)AddPage(levelScore);
        levelScore3.Generate(3);

        BreakdownPage pre3breakdown = (BreakdownPage)AddPage(breakdown);
        pre3breakdown.SetLevel(3, true);
        pre3breakdown.TitleSettings(true, true, "PRE-GAME BREAKDOWN\n<size=18>LEVEL 4</size>");
        BreakdownPage post3breakdown = (BreakdownPage)AddPage(breakdown);
        post3breakdown.SetLevel(3, false);
        post3breakdown.TitleSettings(true, true, "POST-GAME BREAKDOWN\n<size=18>LEVEL 4</size>");

        QuizPage quiz3 = (QuizPage)AddPage(quiz);
        quiz3.TitleSettings(true, true, "QUIZ\n<size=18>LEVEL 4</size>");
        quiz3.LoadQuiz(QuizID.Level3);

        ScorePage score3 = (ScorePage)AddPage(score);
        score3.backActive = false;
        score3.SetLevel(3);

        // CONCLUSION
        CascadeTextPage a = (CascadeTextPage)AddPage(cascadeText);
        a.SetTextContent("This final lesson is optional material. Feel free to skip it, but we recommend you check it out!\n\n" +
            "It covers two chemical reactions that we think are truly fascinating. These reactions are occurring constantly around you and inside of you, and they are fundamental to life on earth!");
        a.TitleSettings(true, true, "");
        a.backActive = false;

        CascadeTextPage b = (CascadeTextPage)AddPage(cascadeText);
        b.SetTextContent("<i>Photosynthesis</i> is a chemical reaction used by plant life to harness the energy in sunlight. It follows this equation:  " +
            "6H₂O + 6CO₂   →   C₆H₁₂O₆ + 6O₂.\n\n" +

            "This says that photosynthesis is a reaction where 6 water molecules and 6 carbon dioxide molecules combine to form 1 big glucose molecule, and 6 oxygen molecules.\n\n" +

            "However, photosynthesis is endothermic, so it needs energy from an outside source to occur. This energy is supplied by sunlight, and is stored in the chemical bonds " +
            "inside the glucose molecule and the 6 oxygen molecules.");
        b.TitleSettings(true, true, "PHOTOSYNTHESIS\n<size=18>ENDOTHERMIC REACTION</size>");
        b.canSkip = true;

        CascadeTextPage c = (CascadeTextPage)AddPage(cascadeText);
        c.SetTextContent("When an animal eats a plant, it consumes the plant’s glucose. When the animal inhales, it consumes oxygen molecules that were produced by photosynthesis reactions. " +
            "A chemical reaction then occurs inside the body of the animal, known as <i>cellular respiration</i>.\n\n" +

            "Cellular respiration follows this equation:  C₆H₁₂O₆ + 6O₂   →   6H₂O + 6CO₂.\n\n" +

            "Notice anything about this equation? It’s the exact reverse of photosynthesis! So, cellular respiration occurs when 1 glucose molecule and 6 oxygen molecules " +
            "combine to produce 6 water molecules and 6 carbon dioxide molecules.");
        c.TitleSettings(true, true, "CELLULAR RESPIRATION\n<size=18>RELATIONSHIP WITH PHOTOSYNTHESIS</size>");
        c.canSkip = true;

        CascadeTextPage d = (CascadeTextPage)AddPage(cascadeText);
        d.SetTextContent("However, cellular respiration is an exothermic reaction, so it releases energy as it occurs. In fact, the amount of energy it releases is equal to the amount of " +
            "sunlight energy that it took for the photosynthesis reaction that created the glucose in the first place! This energy is then used by the cells of the animal, " +
            "and is what enables animals to move, think, and do pretty much everything that they do!");
        d.TitleSettings(true, true, "CELLULAR RESPIRATION\n<size=18>EXOTHERMIC REACTION</size>");
        d.canSkip = true;

        CascadeTextPage e = (CascadeTextPage)AddPage(cascadeText);
        e.SetTextContent("After the cellular respiration reaction has occurred, the 6 water molecules and 6 carbon dioxide molecules are shed from the body of the animal and released back " +
            "into the atmosphere, where they can once again be the reactants of a photosynthesis reaction.\n\n" +

            "This is how plants and animals continuously exchange matter and energy in a way that benefits them both, and allows life to harness energy from the sun’s rays. " +
            "This is one of the reasons it is so important to preserve plant life on earth!");
        e.TitleSettings(true, true, "PHOTOSYNTHESIS  /  CELLULAR RESPIRATION\n<size=18>CYCLE  /  NECESSARY FOR LIFE</size>");
        e.canSkip = true;

        CascadeTextPage f = (CascadeTextPage)AddPage(cascadeText);
        f.SetTextContent("If you were playing close attention, you might have noticed that throughout the entire cycle of photosynthesis and cellular respiration, no matter went missing.\n\n" +

            "Though the atoms may change partners on a regular basis, it will always be the very same atoms, dancing on and on!");
        f.TitleSettings(true, true, "PHOTOSYNTHESIS  /  CELLULAR RESPIRATION\n<size=18>CONSERVATION OF MATTER</size>");
        f.canSkip = true;

        CascadeTextPage g = (CascadeTextPage)AddPage(cascadeText);
        g.SetTextContent("Thanks for playing! We hope you enjoyed your playthrough of <i>Dance of Atoms</i>.\n\n" +

            "Have a great day!");
        g.TitleSettings(true, true, "");
        g.NextSettings(true, true, true, "Finish");
    }

    public List<Page> LoadPages(PanelID panelID, PanelManager pm)
    {
        panelManager = pm;
        pagesParent = panelManager.pagesParent;
        pages = new List<Page>();
        if (!panelManager || !pagesParent)
            return pages;

        switch (panelID)
        {
            case PanelID.PreLevel0:
                PreLevel0();
                break;
            case PanelID.PreLevel1:
                PreLevel1();
                break;
            case PanelID.PreLevel2:
                PreLevel2();
                break;
            case PanelID.PreLevel3:
                PreLevel3();
                break;
            case PanelID.Final:
                Final();
                break;

            case PanelID.PanelTutorial:
                PanelTutorial();
                break;
            default:
                break;
        }
        int pagesCount = pages.Count;
        for (int i = 0; i < pagesCount; i++)
            pages[i].SetPageNumber(i, pagesCount);
        return pages;
    }

    public Page AddPage(string prefabName)
    {
        prefabName = pagePrefabDirectory + prefabName;
        GameObject go = Object.Instantiate(Resources.Load(prefabName)) as GameObject;
        go.transform.SetParent(pagesParent, false);
        Page page = go.GetComponent<Page>();
        page.AssignPanelManager(panelManager);
        pages.Add(page);
        return page;
    }

    private void PanelTutorial()
    {
        /*  
            For each page:

            1.  Declare an instance of Page (base/derived class)* as the return of this.AddPage():
                    Page AddPage(string prefabName)
                        prefabName: choose from the strings declared above:
                            simpleText / scrollText / cascadeText
                            simpleImage / scrollImage
                            quiz
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

        Page a = AddPage(simpleText);
        a.SetTextContent("SimpleTextPage prefab, generated by PageLoader.\n\n" +
            "The panel is in its default state (except for this text content).\n\n" +
            "(Back button is automatically inactive on first page.)");

        Page b = AddPage(simpleText);
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

        QuizPage c = (QuizPage)AddPage(quiz);
        c.TitleSettings(true, true, "QuizPage: Quiz Tutorial");
        c.LoadQuiz(QuizID.QuizTutorial);
        // quiz content is defined in QuestionLoader
        // no need to set NextButton text; QuizPage manages this automatically

        Page d = AddPage(simpleImage);
        d.TitleSettings(true, true, "SimpleImagePage");
        d.NextSettings(true, true, true, "Onward!");
        d.SetImageContent(Resources.Load<Sprite>("Graphics/UI/button.selected"));
        d.SetImageAlpha(0.6f);

        Page e = AddPage(scrollImage);
        e.TitleSettings(true, true, "ScrollImagePage");
        e.SetImageContent(Resources.Load<Sprite>("Graphics/fish.circuit"));

        CascadeTextPage f = (CascadeTextPage)AddPage(cascadeText);
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
}
