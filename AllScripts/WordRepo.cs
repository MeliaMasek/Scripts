using UnityEngine;
using Random = UnityEngine.Random;

//code borrowed and modified by Warped Imagination on youtube https://www.youtube.com/watch?v=wsWeI7APjAU
//code borrowed and modified by Zigurous on youtube https://www.youtube.com/watch?v=Tbcgqz5lM38
//code borrowed and modified by TaranSaini2005 on UnityAnswers https://answers.unity.com/questions/1658930/how-to-shuffle-the-charaters-within-words-whilst-n.html

public class WordRepo : MonoBehaviour
{
    [SerializeField] [Tooltip("Word text files")]
    
    private string[] solutionsWords;
    private string[] validWords;
    private string word;
    public string jumbled;
    private string singleLetter;

    private void Start()
    {
        LoadData();
        SetRandomWord();
    }

    private void LoadData()
    {
        TextAsset textFile = Resources.Load("Seven_Letter_Words") as TextAsset;
        validWords = textFile.text.Split('\n');
        
        textFile = Resources.Load("Seven_Letter_Words") as TextAsset;
        solutionsWords = textFile.text.Split('\n');
    }

    public void SetRandomWord()
    { 
        word = solutionsWords[Random.Range(0, solutionsWords.Length)];
        word = word.ToUpper().Trim();

        jumbled = word;

        //Jumbles the random word
        char[] myChar = jumbled.ToCharArray();
        
        for (int i = myChar.Length - 1; i > 0; i--)
        {
                int rnd = Random.Range(0, i);
                (myChar[i], myChar[rnd]) = (myChar[rnd], myChar[i]);
                
                jumbled = new string(myChar);
        }
        
        //creates an index for each letter in the jumbled word
        char[] myCharJumbled = jumbled.ToCharArray();
        singleLetter = myCharJumbled[0].ToString();
    }
}
