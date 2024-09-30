using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Bingo : MonoBehaviour
{
    
    [Header("Challenges and Numbers")]
    [SerializeField] private TMP_Dropdown firstNumber;
    [SerializeField] private TMP_Dropdown firstChallange;
    [SerializeField] private TMP_Dropdown N_2;
    [SerializeField] private TMP_Dropdown C_2;
    [SerializeField] private TMP_Dropdown N_3;
    [SerializeField] private TMP_Dropdown C_3;

    [SerializeField] private TMP_Dropdown N_4;
    [SerializeField] private TMP_Dropdown C_4;
    [SerializeField] private TMP_Dropdown N_5;
    [SerializeField] private TMP_Dropdown C_5;
    [SerializeField] private TMP_Dropdown N_6;
    [SerializeField] private TMP_Dropdown C_6;

    [SerializeField] private TMP_Dropdown N_7;
    [SerializeField] private TMP_Dropdown C_7;
    [SerializeField] private TMP_Dropdown N_8;
    [SerializeField] private TMP_Dropdown C_8;
    [SerializeField] private TMP_Dropdown N_9;
    [SerializeField] private TMP_Dropdown C_9;
    
    [Header("Stuff")]
    public int CN_1,CN_2,CN_3,CN_4,CN_5,CN_6,CN_7,CN_8,CN_9;
    public string CH_1,CH_2,CH_3,CH_4,CH_5,CH_6,CH_7,CH_8,CH_9;

    [SerializeField] private TMP_InputField customChallenge;

    List<string> challenges = new List<string>{"Teszt,"};
    List<string> customChallenges = new List<string>();

    //CHALLENGES CITY
    List<string> easyChallenges = new List<string>{
        "Fekete autó",
        "Sapka",
        "Zöld kocsi",
        "Piros kocsi",
        "Piros picikli",
        "Robogó",
        "Kopasz ember",
        "Kék autó",
        "Hátizsák",
        "Sárga autó",
        "Pár",
        "Szatyor",
        "Oldaltáska",
        "Kuka",
    };
    List<string> normalChallenges = new List<string>{
        "Gyerek bicikli",
        "Napszemüveg",
        "Parkolni tilos tábla",
        "Foodorás futár",
        "Woltos futár",
        "Kalapos ember",
        "Tetovált ember",
        "Graffiti",
        "Ázsiai",
        "Afrikai",
        "Banyatank",
        "Babakocsi",
        "Dagi ember",
        "Busz",
        "Macska",
        "Kutya",
        "Tűzcsap",
        "Sárga villamos",
        "Sárga fedeles kuka",
        "Tanuló autó",
    };
    List<string> hardChallenges = new List<string>{
        "Rokkant járgány",
        "Alfa rómeó",
        "Kacsa",
        "Emo",
        "Lexus",
        "Behajtani tilos tábla",
        "Szobor",
        "Rendőr autó",
        "Tűzoltó autó",
        "Mentős",
        "Postás autó",
        "Luxus autó",
        "Zöld rendszám",
        "Póráz",
    };

    //CHALLENGES VILLAGE
    List<string> villageChallenges = new List<string>{
        "Traktor",
        "Sörös doboz",
        "Kocsma",
        "Ló",
        "Kutya",
        "Macska",
        "Gólya",
        "C-típus",
        "Terepjáró",
        "Kis pulya",
        "Kis bolt",
        "Bicikli",
        "Idős nénnye",
        "Cigiző ember",
        "Ültetvény",
        "Szobor",
    };

    //CHALLENGES CARS
    List<string> carsChallenges = new List<string>{
        "Suzuki",
        "Mazda",
        "Skoda",
        "Chevrolet",
        "Zöld autó",
        "Fehér autó",
        "Fekete autó",
        "Piros autó",
        "Kék autó",
        "Sárga autó",
        "Ezüst autó",
        "Alfa rómeó",
        "Terepjáró",
        "Audi",
        "BMW",
        "Fiat",
        "Hyundai",
        "Toyota",
        "Volkswagen",
        "Mercedes",
        "Nissan",
        "Mitsubishi",
        "Opel",
        "Citroen",
        "Kia",
    };

    int maxValue;
    public static Bingo Instance;

    private void Awake() 
    {
        Instance = this;
    }

    private void Start()
    {
        CheckDifficulty();
        CalculateChallengesLeft();
        RestoreOriginalOptions();
        RandomTask();
    }

    public void CheckDifficulty()
    {
        int diff = Difficulty.Instance.GetDiff();
        challenges.Clear();

        if (ModeChange.Instance.isCity == true)
        {
            Debug.Log("Városi");
            if (diff == 3)
            {
               challenges.AddRange(easyChallenges);
            }
            if (diff == 5)
            {
                challenges.AddRange(easyChallenges);
               challenges.AddRange(normalChallenges);
            }
            if (diff == 9)
            {
               challenges.AddRange(easyChallenges);
               challenges.AddRange(normalChallenges);
               challenges.AddRange(hardChallenges);
            }
        }

        if (ModeChange.Instance.isCity == false && ModeChange.Instance.isCars == false)
        {
            challenges.AddRange(villageChallenges);
            Debug.Log("Falusi");
        }

        else if (ModeChange.Instance.isCity == false)
        {
            challenges.AddRange(carsChallenges);
            Debug.Log("Autós");
        }


        challenges.AddRange(customChallenges);
    }

    private void RestoreOriginalOptions()
    {
        CalculateChallengesLeft();

        firstChallange.ClearOptions();
        firstChallange.AddOptions(challenges);
        C_2.ClearOptions();
        C_2.AddOptions(challenges);
        C_3.ClearOptions();
        C_3.AddOptions(challenges);
        C_4.ClearOptions();
        C_4.AddOptions(challenges);
        C_5.ClearOptions();
        C_5.AddOptions(challenges);
        C_6.ClearOptions();
        C_6.AddOptions(challenges);
        C_7.ClearOptions();
        C_7.AddOptions(challenges);
        C_8.ClearOptions();
        C_8.AddOptions(challenges);
        C_9.ClearOptions();
        C_9.AddOptions(challenges);
    }

    private void CalculateChallengesLeft()
    {
        if(challenges.Count > 0)
        {
            maxValue = challenges.Count;
            Debug.Log(maxValue);
        }
    }

    public void StartGame()
    {
        Winner.Instance.GameStart();
        
        int.TryParse(firstNumber.options[firstNumber.value].text,out CN_1);
        int.TryParse(N_2.options[N_2.value].text,out CN_2);
        int.TryParse(N_3.options[N_3.value].text,out CN_3);
        int.TryParse(N_4.options[N_4.value].text,out CN_4);
        int.TryParse(N_5.options[N_5.value].text,out CN_5);
        int.TryParse(N_6.options[N_6.value].text,out CN_6);
        int.TryParse(N_7.options[N_7.value].text,out CN_7);
        int.TryParse(N_8.options[N_8.value].text,out CN_8);
        int.TryParse(N_9.options[N_9.value].text,out CN_9);

    }

    public void RandomTask()
    {
        int diff = Difficulty.Instance.GetDiff();
        CheckDifficulty();
        RestoreOriginalOptions();

        firstChallange.value = Random.Range(0, maxValue);
        firstNumber.value = Random.Range(0,diff);
        CH_1 = firstChallange.options[firstChallange.value].text;
        DeleteTheUsedOne(firstChallange.value);

        C_2.value = Random.Range(0,maxValue);
        N_2.value = Random.Range(0,diff);
        CH_2 = C_2.options[C_2.value].text;
        DeleteTheUsedOne(C_2.value);

        C_3.value = Random.Range(0,maxValue);
        N_3.value = Random.Range(0,diff);
        CH_3 = C_3.options[C_3.value].text;
        DeleteTheUsedOne(C_3.value);
        
        C_4.value = Random.Range(0,maxValue);
        N_4.value = Random.Range(0,diff);
        CH_4 = C_4.options[C_4.value].text;
        DeleteTheUsedOne(C_4.value);

        C_5.value = Random.Range(0,maxValue);
        N_5.value = Random.Range(0,diff);
        CH_5 = C_5.options[C_5.value].text;
        DeleteTheUsedOne(C_5.value);

        C_6.value = Random.Range(0,maxValue);
        N_6.value = Random.Range(0,diff);
        CH_6 = C_6.options[C_6.value].text;
        DeleteTheUsedOne(C_6.value);

        C_7.value = Random.Range(0,maxValue);
        N_7.value = Random.Range(0,diff);
        CH_7 = C_7.options[C_7.value].text;
        DeleteTheUsedOne(C_7.value);

        C_8.value = Random.Range(0,maxValue);
        N_8.value = Random.Range(0,diff);
        CH_8 = C_8.options[C_8.value].text;
        DeleteTheUsedOne(C_8.value);

        C_9.value = Random.Range(0,maxValue);
        N_9.value = Random.Range(0,diff);
        CH_9 = C_9.options[C_9.value].text;
        DeleteTheUsedOne(C_9.value);

    }

    private void DeleteTheUsedOne(int idx)
    {
        firstChallange.options.RemoveAt(idx);
        C_2.options.RemoveAt(idx);
        C_3.options.RemoveAt(idx);
        C_4.options.RemoveAt(idx);
        C_5.options.RemoveAt(idx);
        C_6.options.RemoveAt(idx);
        C_7.options.RemoveAt(idx);
        C_8.options.RemoveAt(idx);
        C_9.options.RemoveAt(idx);
        maxValue--;
    }

    public void AddCustomChallenge()
    {
         string inputText = customChallenge.text;

        if (!string.IsNullOrEmpty(inputText))
        {
            // Add the input text to the lists
            customChallenges.Add(inputText);
            challenges.Add(inputText);

            //Clear Inputfield
            customChallenge.text = string.Empty;
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
