using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//code borrowed and modified by Warped Imagination on youtube https://www.youtube.com/watch?v=wsWeI7APjAU
public class LetterManager : MonoBehaviour
{
    [SerializeField] [Tooltip("Prefab for the letter")]
    private Letter letterPrefab = null;

    [SerializeField] [Tooltip("Amount of rows")]
    private int rows = 2;

    [SerializeField] [Tooltip("Grid Parent")]
    GridLayoutGroup gridLayout = null;

    [SerializeField] [Tooltip("Letter Keys")]
    private Keys[] keys = null;
    
    private List<Letter> letters = null;
    private const int wordLength = 7;
    private int index = 0;
    private int currentRow = 0;
    private char?[] guess = new char?[wordLength];
    private char[] wordchar = new char[wordLength];

    private void Update()
    {
        if (Input.anyKeyDown)
            ParseInput(Input.inputString);
    }

    private void Awake()
    {
        GridSetup();

        foreach (Keys key in keys)
        {
            key.pressed += OnKeyPressed;
        }
    }

    private void Restart()
    {
        foreach (Letter letter in letters)
            letter.Clear();

        index = 0;
        currentRow = 0;

        for (int i = 0; i < wordLength; i++)
            guess[i] = null;
    }
    
    public void GridSetup()
    {
        if (letters == null)
            letters = new List<Letter>();
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < wordLength; j++)
            {
                Letter letter = Instantiate(this.letterPrefab, gridLayout.transform, true);
                letters.Add(letter);
            }
        }
    }

    public void ParseInput(string value)
    {
        foreach (char c in value)
        {
            if (c == '\b')
            {
                DeleteLetter();
            }
            else if ((c == '\n') || (c == '\r'))
            {
            }
            else
            {
                EnterLetter(c);
            }
        }
    }
    
    public void EnterLetter(char c)
    {
        if (index < wordLength)
        {
            c = char.ToUpper(c);

            //GetComponent<Keyboard>().KeyPressedCallback();
                
            letters[(currentRow * wordLength) + index].EnterLetter(c);
            guess[index] = c;
            index++;
        }
    }
    
    public void DeleteLetter()
    {
        if (index > 0)
        {
            index--;
            letters[(currentRow * wordLength) + index].DeleteLetter();
            guess[index] = null;
        }
    }

    private void OnKeyPressed(KeyCode keycode)
    {
        {
            if (keycode == KeyCode.Return)
                Restart();
        }

        if (keycode == KeyCode.Return)
        {
            //GuessWord();
        }
        else if (keycode == KeyCode.Backspace || keycode == KeyCode.Delete)
        {
            DeleteLetter();
        }
        else if (keycode >= KeyCode.A && keycode <= KeyCode.Z )
        { 
            int index = keycode - KeyCode.A;
            EnterLetter( ((char)((int)'A' + index)));
        }
    }
}
