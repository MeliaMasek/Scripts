using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyboardInput : MonoBehaviour
{
    [SerializeField] [Tooltip("Prefab for the letter")]
    private Letter letterPrefab = null;

    [SerializeField] [Tooltip("Amount of rows")]
    private int rows = 1;

    [SerializeField] [Tooltip("Grid Parent")]
    GridLayoutGroup gridLayout = null;

    [SerializeField] [Tooltip("Letter Keys")]
    private Keys[] keys = null;
    
    [Header("Elements")] 
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private Key keyPrefab;

    [Header("KeyboardLine")] 
    [SerializeField] private KeyboardLine[] lines;

    [Header("KeySettings")] 
    [Range(0f, 1f)] [SerializeField] private float keyToLineRatio;
    [Range(0f, .25f)] [SerializeField] private float keyXSpacing;
    
    [Header("Event")] 
    public Action <char> onKeyPressed;
    
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
        UpdateRectTransform();
        PlaceKeys();
    }

    private void Awake()
    {
        GridSetup();

        foreach (Keys key in keys)
        {
            key.pressed += OnKeyPressed;
        }
    }
    
    IEnumerator Start()
    {
        CreateKeys();
        yield return null;
        UpdateRectTransform();
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

            letters[(currentRow * wordLength) + index].EnterLetter(c);
            onKeyPressed?.Invoke(c);
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

    private void UpdateRectTransform()
    {
        rectTransform.sizeDelta = new Vector2(Screen.width, Screen.height / 3);
    }

    private void CreateKeys()
    {
        for (int i = 0; i < lines.Length; i++)
        {
            for (int j = 0; j < lines[i].keys.Length; j++)
            {
                char key = lines[i].keys[j];

                Key keyInstance = Instantiate(keyPrefab, rectTransform);
                keyInstance.SetKey(key);
                
                keyInstance.GetComponent<Button>().onClick.AddListener((() => EnterLetter(key)));
            }
        }
    }

    private void PlaceKeys()
    {
        int lineCount = lines.Length;
        float lineHeight = rectTransform.rect.height / lineCount;
        float keyWidth = lineHeight * keyToLineRatio;
        float spacingX = keyXSpacing * lineHeight;

        int currentKeyIndex = 0;

        for (int i = 0; i < lineCount; i++)
        {
            float halfKeyCount = (float)lines[i].keys.Length / 2;

            float StartX = rectTransform.position.x - (keyWidth + spacingX) * halfKeyCount + (keyWidth + spacingX) / 2;
            float lineY = rectTransform.position.y + rectTransform.rect.height / 2 - lineHeight / 2 - i * lineHeight;            
            
            for (int j = 0; j < lines[i].keys.Length; j++)
            {
                float KeyX = StartX + j * (keyWidth + spacingX);
                Vector2 keyPostion = new Vector2(KeyX, lineY);

                RectTransform keyRectTransform = rectTransform.GetChild(currentKeyIndex).GetComponent<RectTransform>();
                keyRectTransform.position = keyPostion;
                keyRectTransform.sizeDelta = new Vector2(keyWidth, keyWidth);
                currentKeyIndex++;
            }
        }
    }
}

[System.Serializable]
public struct KeyboardLines
{
    public string keys;
}