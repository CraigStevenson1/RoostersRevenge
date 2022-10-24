using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class QuestionManager : MonoBehaviour
{
    [SerializeField] TextAsset qna;
    [SerializeField] TMP_Text question;
    [SerializeField] TMP_Text a;
    [SerializeField] TMP_Text b;
    [SerializeField] TMP_Text c;
    [SerializeField] TMP_Text d;
    [SerializeField] int awardPerQuestion;
    [SerializeField] TMP_Text score;
    [SerializeField] int min;
    [SerializeField] int sec;
    [SerializeField] TMP_Text timer;
    [SerializeField] List<int> questionRangeIndexs;
    [SerializeField] GameObject endWindow;
    [SerializeField] GameObject questionWindow;
    [SerializeField] AudioSource correctFX;

    private QuestionSet qSet;
    string answer;
    private int scoreTotal;
    private float totalSeconds;
    private List<int> usedIndexs;
    private int chosenIndex;
    private string usedQKeyOne;
    private string scoreKey;
    private bool scoreRecorded;
    //List<Question> q;

    // Start is called before the first frame update
    void Start()
    {
        answer = "";
        scoreTotal = 0;
        usedQKeyOne = "usedq1";
        qSet = JsonUtility.FromJson<QuestionSet>(qna.text);
        usedIndexs = new List<int>();
        chosenIndex = Random.Range(questionRangeIndexs[0], questionRangeIndexs[1]);
        populateUsedQArray();
        populateQuestionBox();
        totalSeconds = min * 60 + sec;
        scoreKey = "score";
        unpause();


    }
   /* private void OnEnable()
    {
        populateUsedQArray();
    }*/
    // Update is called once per frame
    void Update()
    {
        if (totalSeconds > 0f && usedIndexs.Count < questionRangeIndexs[1] + 1)
        {
            totalSeconds -= Time.deltaTime;
            Debug.Log(totalSeconds);
            timer.text = (((int)totalSeconds / 60).ToString() + ":" + (Mathf.Round(totalSeconds % 60)).ToString());
        }

        else
        {
            if (!scoreRecorded)
            {
                endLevel();
            }
        }
    }

    private void populateQuestionBox()
    {
        if (usedIndexs.Count < questionRangeIndexs[1] + 1)
        {
            chosenIndex = Random.Range(questionRangeIndexs[0], questionRangeIndexs[1] + 1);
            while (usedIndexs.Contains(chosenIndex))
            {
                chosenIndex = Random.Range(questionRangeIndexs[0], questionRangeIndexs[1] + 1);
            }
            Question val = qSet.questions[chosenIndex];
            question.text = val.question;
            a.text = val.A;
            b.text = val.B;
            c.text = val.C;
            d.text = val.D;
            answer = val.Answer;
        }
    }

    public void ansA()
    {
        if(answer == a.text)
        {
            Debug.Log("Correct");
            awardScore();
            populateQuestionBox();
            

        }
        else
        {
            Debug.Log("Incorrect");
            populateQuestionBox();
        }
    }

    public void ansB()
    {
        if (answer == b.text)
        {
            Debug.Log("Correct");
            awardScore();
            populateQuestionBox();
           
        }

        else
        {
            Debug.Log("Incorrect");
            populateQuestionBox();

        }
    }

    public void ansC()
    {
        if (answer == c.text)
        {
            Debug.Log("Correct");
            awardScore();
            populateQuestionBox();
            

        }

        else
        {
            Debug.Log("Incorrect");
            populateQuestionBox();
        }
    }

    public void ansD()
    {
        if (answer == d.text)
        {
            Debug.Log("Correct");
            awardScore();
            populateQuestionBox();
            

        }

        else
        {
            Debug.Log("Incorrect");
            populateQuestionBox();
        }
    }

    private void awardScore()
    {
        correctFX.Play();
        scoreTotal += awardPerQuestion;
        score.text = "Score: " + scoreTotal;
        usedIndexs.Add(chosenIndex);

    }

    private void populateUsedQArray()
    {
        if (PlayerPrefs.HasKey(usedQKeyOne))
        {
            string strArr = PlayerPrefs.GetString(usedQKeyOne);


            string[] temp = strArr.Split(",");

            foreach (string i in temp)
            {
                usedIndexs.Add(int.Parse(i));
            }
        }

        else
        {
            return;
        }
    }
    private void endLevel()
    {
        if (usedIndexs.Count > 0)
        {
            string strArray = usedIndexs[0].ToString();
            for (int i = 1; i < usedIndexs.Count; i++)
            {
                strArray += "," + usedIndexs[i].ToString();
            }

            PlayerPrefs.SetString(usedQKeyOne, strArray);
        }
        Debug.Log("???????");
        updateScorePlayerPrefs();
        endWindow.SetActive(true);
        pause();
        scoreRecorded = true;
    }

    private void updateScorePlayerPrefs()
    {
        float current = PlayerPrefs.GetFloat(scoreKey);
        float newVal = current + scoreTotal;
        PlayerPrefs.SetFloat(scoreKey, newVal);
    }

    private void pause()
    {
        Time.timeScale = 0;
        questionWindow.SetActive(false);
    }

    private void unpause()
    {
        Time.timeScale = 1;
        scoreRecorded = false;
        //questionWindow.SetActive(false);
    }

    public void mainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
[System.Serializable]
public class Question
{
    public string question;
    public string A;
    public string B;
    public string C;
    public string D;
    public string Answer;
}
[System.Serializable]
public class QuestionSet
{
    public Question[] questions;
}
