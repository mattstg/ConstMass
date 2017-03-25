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
        // INTRODUCTION
        Page a = AddPage(simpleText, panelManager, pagesParent, pages);
        a.TitleSettings(true, true, "INTRODUCTION");
        a.SetTextContent("");

        // PRE-0
        Page b = AddPage(simpleText, panelManager, pagesParent, pages);
        b.TitleSettings(true, true, "LESSON 1");
        b.SetTextContent("");
        Page c = AddPage(simpleText, panelManager, pagesParent, pages);
        c.TitleSettings(true, true, "TUTORIAL 1");
        c.SetTextContent("");
        Page d = AddPage(simpleText, panelManager, pagesParent, pages);
        d.TitleSettings(true, true, "OBJECTIVE 1");
        d.SetTextContent("");

        BreakdownPage pre0breakdown = (BreakdownPage)AddPage(breakdown, panelManager, pagesParent, pages);
        pre0breakdown.SetLevel(0, true);
        pre0breakdown.TitleSettings(true, true, "LEVEL 1:  PRE-GAME BREAKDOWN");
        pre0breakdown.NextSettings(true, true, true, "Begin!");
    }

    private void PreLevel1(PanelManager panelManager, RectTransform pagesParent, List<Page> pages)
    {
      // POST-0
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
        score0.UpdateScores(0);

        // PRE-1
        Page a = AddPage(simpleText, panelManager, pagesParent, pages);
        a.TitleSettings(true, true, "LESSON 2");
        a.BackSettings(false, true, false, "");
        a.SetTextContent("");
        Page b = AddPage(simpleText, panelManager, pagesParent, pages);
        b.TitleSettings(true, true, "OBJECTIVE 2");
        b.SetTextContent("");

        BreakdownPage pre1breakdown = (BreakdownPage)AddPage(breakdown, panelManager, pagesParent, pages);
        pre1breakdown.SetLevel(1, true);
        pre1breakdown.TitleSettings(true, true, "LEVEL 2:  PRE-GAME BREAKDOWN");
        pre1breakdown.NextSettings(true, true, true, "Begin!");
    }

    private void PreLevel2(PanelManager panelManager, RectTransform pagesParent, List<Page> pages)
    {
      // POST-1
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
        score1.UpdateScores(1);

        // PRE-2
        Page a = AddPage(simpleText, panelManager, pagesParent, pages);
        a.TitleSettings(true, true, "LESSON 3");
        a.BackSettings(false, true, false, "");
        a.SetTextContent("");
        Page b = AddPage(simpleText, panelManager, pagesParent, pages);
        b.TitleSettings(true, true, "OBJECTIVE 3");
        b.SetTextContent("");

        BreakdownPage pre2breakdown = (BreakdownPage)AddPage(breakdown, panelManager, pagesParent, pages);
        pre2breakdown.SetLevel(2, true);
        pre2breakdown.TitleSettings(true, true, "LEVEL 3:  PRE-GAME BREAKDOWN");
        pre2breakdown.NextSettings(true, true, true, "Begin!");
    }

    private void PreLevel3(PanelManager panelManager, RectTransform pagesParent, List<Page> pages)
    {
      // POST-2
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
        score2.UpdateScores(2);

        // PRE-3
        Page a = AddPage(simpleText, panelManager, pagesParent, pages);
        a.TitleSettings(true, true, "LESSON 4");
        a.BackSettings(false, true, false, "");
        a.SetTextContent("");
        Page b = AddPage(simpleText, panelManager, pagesParent, pages);
        b.TitleSettings(true, true, "OBJECTIVE 4");
        b.SetTextContent("");

        BreakdownPage pre3breakdown = (BreakdownPage)AddPage(breakdown, panelManager, pagesParent, pages);
        pre3breakdown.SetLevel(3, true);
        pre3breakdown.TitleSettings(true, true, "LEVEL 4:  PRE-GAME BREAKDOWN");
        pre3breakdown.NextSettings(true, true, true, "Begin!");
    }

    private void Final(PanelManager panelManager, RectTransform pagesParent, List<Page> pages)
    {
      // POST-3
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
        score3.UpdateScores(3);

        // CONCLUSION
        Page a = AddPage(simpleText, panelManager, pagesParent, pages);
        a.TitleSettings(true, true, "CONCLUSION");
        a.SetTextContent("");
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
}
