using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DataManagement : MonoBehaviour
{
    int[] Number = new int[10];
    string[] Challenge = new string [10];

    [SerializeField] Button[] buttons;

    [Header("Texts in game")]
    [SerializeField] private TextMeshProUGUI First;
    [SerializeField] private TextMeshProUGUI Second;
    [SerializeField] private TextMeshProUGUI Third;
    [SerializeField] private TextMeshProUGUI Fourth;
    [SerializeField] private TextMeshProUGUI Fifth;
    [SerializeField] private TextMeshProUGUI Sixth;
    [SerializeField] private TextMeshProUGUI Seventh;
    [SerializeField] private TextMeshProUGUI Eighth;
    [SerializeField] private TextMeshProUGUI Ninth;

    [SerializeField] private TextMeshProUGUI Description;
    [SerializeField] private Sprite DONE;

    private Button theButton;
    private int tag;
    private int doneChallenge = 0;

    void Start()
    {
        foreach (Button btn in buttons)
        {
            Button choosen = btn;
            btn.onClick.AddListener(() => TaskOnClick(choosen));
        }
    }

    void TaskOnClick(Button choosen)
    {
        if (choosen.gameObject.tag == "1")
        {
            tag = 1;
            UpdateDescText();
            theButton = choosen;
        }
        else if (choosen.gameObject.tag == "2")
        {
            tag = 2;
            UpdateDescText();
            theButton = choosen;
        }
        else if (choosen.gameObject.tag == "3")
        {
            tag = 3;
            UpdateDescText();
            theButton = choosen;
        }
        else if (choosen.gameObject.tag == "4")
        {
            tag = 4;
            UpdateDescText();
            theButton = choosen;
        }
        else if (choosen.gameObject.tag == "5")
        {
            tag = 5;
            UpdateDescText();
            theButton = choosen;
        }
        else if (choosen.gameObject.tag == "6")
        {
            tag = 6;
            UpdateDescText();
            theButton = choosen;
        }
        else if (choosen.gameObject.tag == "7")
        {
            tag = 7;
            UpdateDescText();
            theButton = choosen;
        }
        else if (choosen.gameObject.tag == "8")
        {
            tag = 8;
            UpdateDescText();
            theButton = choosen;
        }
        else if (choosen.gameObject.tag == "9")
        {
            tag = 9;
            UpdateDescText();
            theButton = choosen;
        }
    }

    private void UpdateDescText()
    {
        Description.text = Number[tag] +"x "+Challenge[tag];
    }

    public void GetNumbers()
    {
        Number[1] = Bingo.Instance.CN_1;
        Number[2] = Bingo.Instance.CN_2;
        Number[3] = Bingo.Instance.CN_3;
        Number[4] = Bingo.Instance.CN_4;
        Number[5] = Bingo.Instance.CN_5;
        Number[6] = Bingo.Instance.CN_6;
        Number[7] = Bingo.Instance.CN_7;
        Number[8] = Bingo.Instance.CN_8;
        Number[9] = Bingo.Instance.CN_9;
    }
    public void GetStrings()
    {
        Challenge[1] = Bingo.Instance.CH_1;
        Challenge[2] = Bingo.Instance.CH_2;
        Challenge[3] = Bingo.Instance.CH_3;
        Challenge[4] = Bingo.Instance.CH_4;
        Challenge[5] = Bingo.Instance.CH_5;
        Challenge[6] = Bingo.Instance.CH_6;
        Challenge[7] = Bingo.Instance.CH_7;
        Challenge[8] = Bingo.Instance.CH_8;
        Challenge[9] = Bingo.Instance.CH_9;
    }

    public void SetTexts()
    {
        First.text = Number[1] +"x "+Challenge[1];
        Second.text = Number[2] +"x "+Challenge[2];
        Third.text = Number[3] +"x "+Challenge[3];
        Fourth.text = Number[4] +"x "+Challenge[4];
        Fifth.text = Number[5] +"x "+Challenge[5];
        Sixth.text = Number[6] +"x "+Challenge[6];
        Seventh.text = Number[7] +"x "+Challenge[7];
        Eighth.text = Number[8] +"x "+Challenge[8];
        Ninth.text = Number[9] +"x "+Challenge[9];
    }

    public void Remove()
    {
        Number[tag] -= 1;
        UpdateDescText();
        SetTexts();

    }

    public void Add()
    {
        Number[tag] += 1;
        UpdateDescText();
        SetTexts();
    }

    private bool CheckIfDone()
    {

        if(Number[tag] <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void BackChecks()
    {
        if (CheckIfDone() == true)
            {
                doneChallenge++;
                theButton.gameObject.GetComponent<Image>().sprite = DONE;
                theButton.gameObject.GetComponentInChildren<TMP_Text>().enabled = false;
                theButton.interactable = false;
            }
        SetTexts();

        //IF WE WIN
        if (doneChallenge == 9)
        {
            Winner.Instance.FinishGame();
            Winner.Instance.Win();
        }
    }

}
