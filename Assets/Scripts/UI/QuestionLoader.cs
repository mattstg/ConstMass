using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LoLSDK;

public enum QuizID
{
    Level0, Level1, Level2, Level3, QuizTutorial, None
};

public class QuestionLoader
{
    private string questionsPrefabDirectory = "Prefabs/UI/Elements/Questions/";
    private string questionSingle = "QuestionSingle";
    private string questionDouble = "QuestionDouble";

    private QuizPage quizPage;
    private RectTransform questionsParent;
    private List<Question> questions;

    #region Singleton
    private static QuestionLoader instance;

    private QuestionLoader() { }

    public static QuestionLoader Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new QuestionLoader();
            }
            return instance;
        }
    }
    #endregion

    public void Level0()
    {
        QuestionData a = new QuestionData();
        a.question = "Which term describes the matter that goes into a chemical reaction?";
        a.info = "Hint: these are the things that do the <i>reacting</i> in a chemical reaction.";
        a.AddAnswer("Products");
        a.AddAnswer("Electrons");
        a.AddAnswer("Solvents");
        a.AddAnswer("Reactants", true);
        a.SetCorrectText("For any given reaction, the chemicals that go into it are the reactants.\nThe chemicals that come out of it are the products.");

        QuestionData b = new QuestionData();
        b.question = "If 3 H₂ molecules and 3 Cl₂ molecules react to form hydrogen chloride, how many HCl molecules will be produced?";
        b.info = "Hint: the reactants in this question are composed of 6 hydrogen atoms and 6 chlorine atoms. Each HCl molecule is composed of 1 hydrogen atom and 1 chlorine atom.";
        b.AddAnswer("1");
        b.AddAnswer("3");
        b.AddAnswer("6", true);
        b.AddAnswer("12");
        b.SetCorrectText("6 HCl molecules will be produced.");

        QuestionData c = new QuestionData();
        c.question = "If 20amu of one substance chemically reacts with 25amu of another substance, what will the total mass of the products be?";
        c.info = "Remember, in a chemical reaction, the total mass of the reactants is always equal to the total mass of the products.";
        c.AddAnswer("5amu");
        c.AddAnswer("45amu", true);
        c.AddAnswer("22.5amu");
        c.AddAnswer("It depends on the types of substances involved");
        c.SetCorrectText("The total mass of the products will be 45amu.");


        AddQuestion(questionDouble, a);
        AddQuestion(questionDouble, b);
        AddQuestion(questionDouble, c);
    }

    public void Level1()
    {
        QuestionData a = new QuestionData();
        a.question = "Fill in the blanks:\n___________________ require energy to occur, and ___________________ release excess energy.";
        a.info = "If you can't remember, you'll just have to guess!";
        a.AddAnswer("Endothermic reactions  /\nexothermic reactions", true);
        a.AddAnswer("Exothermic reactions  /\nendothermic reactions");
        a.SetCorrectText("Endothermic reactions require energy to occur, and exothermic reactions release excess energy.");

        QuestionData b = new QuestionData();
        b.question = "How does heat affect the movement of molecules?";
        b.info = "Remember, heat is a form of energy!";
        b.AddAnswer("Molecules of warmer substances move faster", true);
        b.AddAnswer("Molecules of warmer substances move slower");
        b.AddAnswer("It depends on the type of substance");
        b.SetCorrectText();

        Question aQuestion = AddQuestion(questionDouble, a);
        AddQuestion(questionDouble, b);

        aQuestion.answers[0].GetComponent<RectTransform>().anchorMin = new Vector2(0, 0);
        aQuestion.answers[0].GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 1);
        aQuestion.answers[1].GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 0);
        aQuestion.answers[1].GetComponent<RectTransform>().anchorMax = new Vector2(1, 1);
    }

    public void Level2()
    {
        QuestionData a = new QuestionData();
        a.question = "How many hydrogen atoms and oxygen atoms are present in the expression 4H₂O?";
        a.info = "Remember, the 4 means that there are 4 H₂O molecules present, while the ₂ means there are 2 hydrogen atoms in each molecule.";
        a.AddAnswer("4 hydrogen atoms and 2 oxygen atoms");
        a.AddAnswer("8 hydrogen atoms and 1 oxygen atom");
        a.AddAnswer("4 hydrogen atoms and 8 oxygen atoms");
        a.AddAnswer("8 hydrogen atoms and 4 oxygen atoms", true);
        a.SetCorrectText("4 molecules of H₂O together contain 8 hydrogen atoms and 4 oxygen atoms.");

        QuestionData b = new QuestionData();
        b.question = "Select the balanced form of the following chemical equation:\nZn + HCl   →   H₂ + ZnCl₂";
        b.info = "Hint: the correct answer will have the same number of each type of atom on the right side of the equation as there are on the left.";
        b.AddAnswer("Zn + 2HCl   →\nH₂ + ZnCl₂", true);
        b.AddAnswer("2Zn + HCl   →\nH₂ + ZnCl₂");
        b.AddAnswer("Zn + 2HCl   →\n2H₂ + ZnCl₂");
        b.AddAnswer("2Zn + 2HCl   →\nH₂ + 2ZnCl₂");
        b.SetCorrectText("Zn + 2HCl   →   H₂ + ZnCl₂.");

        QuestionData c = new QuestionData();
        c.question = "Select the balanced form of the following chemical equation:\nC₁₂H₂₂O₁₁ + 12O₂   →   CO₂ + H₂O";
        c.info = "Hint: the correct answer will have the same number of each type of atom on the right side of the equation as there are on the left.";
        c.AddAnswer("C₁₂H₂₂O₁₁ + 12O₂   →   11CO₂ + 12H₂O");
        c.AddAnswer("C₁₂H₂₂O₁₁ + 12O₂   →   12CO₂ + 11H₂O", true);
        c.SetCorrectText();


        AddQuestion(questionDouble, a);
        AddQuestion(questionDouble, b);
        Question cQuestion = AddQuestion(questionDouble, c);

        cQuestion.answers[0].GetComponent<RectTransform>().anchorMin = new Vector2(0, 0);
        cQuestion.answers[0].GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 1);
        cQuestion.answers[1].GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 0);
        cQuestion.answers[1].GetComponent<RectTransform>().anchorMax = new Vector2(1, 1);
    }

    public void Level3()
    {
        QuestionData a = new QuestionData();
        a.question = "Is the following chemical equation balanced?\n6H₂O + 6CO₂   →   C₆H₁₂O₆ + 6O₂";
        a.info = "Hint: count how many hydrogen, oxygen, and carbon atoms are on either side of the equation. If they are equal, then the equation is balanced!";
        a.AddAnswer("Yes", true);
        a.AddAnswer("No");
        a.SetCorrectText("Yes, the equation is balanced.");

        QuestionData b = new QuestionData();
        b.question = "How many oxygen atoms are present in the following expression?\n2C₆H₁₂O₆ + 12O₂";
        b.info = "Just add them up!";
        b.AddAnswer("8");
        b.AddAnswer("13");
        b.AddAnswer("24");
        b.AddAnswer("36", true);
        b.SetCorrectText("2 glucose molecules and 12 O₂ molecules together contain 36 oxygen atoms.");

        QuestionData c = new QuestionData();
        c.question = "Why do the coals and ashes that remain after a log has burned weigh less than the original log?";
        c.info = "Remember, atoms in chemical reactions are never created or destroyed, and their masses never change!";
        c.AddAnswer("They don’t");
        c.AddAnswer("Some of the original matter escaped as gas and smoke", true);
        c.AddAnswer("Some of the original atoms were destroyed");
        c.AddAnswer("The original atoms lost some mass during the reaction");
        c.SetCorrectText();


        AddQuestion(questionDouble, a);
        AddQuestion(questionDouble, b);
        AddQuestion(questionDouble, c);
    }

    public List<Question> LoadQuestions(QuizID quizID, QuizPage qp)
    {
        quizPage = qp;
        questionsParent = quizPage.questionsParent;
        questions = new List<Question>();

        switch (quizID)
        {
            case QuizID.Level0:
                Level0();
                break;
            case QuizID.Level1:
                Level1();
                break;
            case QuizID.Level2:
                Level2();
                break;
            case QuizID.Level3:
                Level3();
                break;
            case QuizID.QuizTutorial:

                /*  
                    For each question:

                    1.  Create a new QuestionData instance

                    2.  Set strings qd.question and .info

                    3.  Add answers sequentially by using qd.AddAnswer():
                            void AddAnswer(string answer, bool isCorrect), or
                            void AddAnswer(string answer)
                                For simplicity, you can use the second one for incorrect answers,
                                but be sure to use the first one for the correct answer
                    
                    4.  After this, you can use qd.SetCorrectText() to append a string to
                        string qd.correctText, which starts out as "Correct!":
                            void SetCorrectText(string appendix), or
                            void SetCorrectText()
                                Use the second one to automatically append the text of the
                                correct answer to the end of "Correct!"
                    
                    5.  After setting the QuestionData as needed, use this.AddQuestion():
                            void AddQuestion(string prefabName, QuestionData data)
                                prefabName: either this.questionSingle or .questionDouble
                                    questionSingle prefab has lower profile, more balanced layout,
                                        with questions and answers aligned on a single line,
                                        but might have too little space to properly display
                                        lengthier questions or answers
                                    questionDouble prefab displays the question on one line,
                                        and the infoButton, infoText, and answers on a second line
                                    avoid mixing these two prefabs within the same quiz
                                data: the QuestionData just generated
                    
                    6.  For efficiency, set questions.Capacity to the total number of Questions to be added,
                        before the initial AddQuestion() call.
                */

                questions.Capacity = 3;     // set List<Question> capacity in advance for efficiency

                QuestionData a = new QuestionData();
                a.question = "Question text?";
                a.info = "Answer 2 is correct.";
                a.AddAnswer("Answer 1");
                a.AddAnswer("Answer 2", true);  // correct answer
                a.AddAnswer("Answer 3");
                a.SetCorrectText();         // correctText = "Correct! Answer 2."
                AddQuestion(questionSingle, a);

                QuestionData b = new QuestionData();
                b.question = "How many uranium atoms are there in a molecule of water?";
                b.info = "Hint: not one million.";
                b.AddAnswer("One");
                b.AddAnswer("Two");
                b.AddAnswer("Seventeen", true);
                b.AddAnswer("One million");
                b.SetCorrectText("Exactly seventeen.");
                    // correctText = "Correct! Exactly seventeen." 
                AddQuestion(questionSingle, b);

                QuestionData c = new QuestionData();
                c.question = "If you need extra space for questions or answers, " +
                    "you can use the questionDouble prefab instead.\n" +
                    "Avoid mixing questionSingles and questionDoubles in the same quiz. Like this. Don't do this.";
                c.info = "Hint: choose the correct answer.";
                c.AddAnswer("Correct", true);
                c.AddAnswer("Incorrect");           // these two lines
                c.AddAnswer("Incorrect", false);    // are equivalent
                AddQuestion(questionDouble, c);

                break;
            default:
                break;
        }
        return questions;
    }

    public Question AddQuestion(string prefabName, QuestionData data)
    {
        prefabName = questionsPrefabDirectory + prefabName;
        GameObject go = Object.Instantiate(Resources.Load(prefabName)) as GameObject;
        go.transform.SetParent(questionsParent, false);
        Question q = go.GetComponent<Question>();
        q.data = data;
        q.ApplyData();
        q.AssignQuiz(quizPage);
        questions.Add(q);
        return q;
    }
}
