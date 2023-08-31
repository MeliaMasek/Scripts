using System;
using System.Collections;
using UnityEngine;

//code borrowed and modified by Tabsil on youtube https://www.youtube.com/watch?v=rYcE_Fem4NE
//code borrowed and modified by Tabsil on youtube https://www.youtube.com/watch?v=ZGtpZ24-tWc
//code borrowed and modified by Tabsil on youtube https://www.youtube.com/watch?v=T3MT9Tb1juc
public class Keyboard : MonoBehaviour
{
    [Header("Elements")] 
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private Key keyPrefab;

    //[Header("Settings")] 
    //[Range(0f, 1f)] [SerializeField] private float widthPercent;
    //[Range(0f, 1f)] [SerializeField] private float heightPercent;
    //[Range(0f, .5f)] [SerializeField] private float bottomOffset;

    [Header("KeyboardLine")] 
    [SerializeField] private KeyboardLine[] lines;

    [Header("KeySettings")] 
    [Range(0f, 1f)] [SerializeField] private float keyToLineRatio;
    [Range(0f, .25f)] [SerializeField] private float keyXSpacing;
    
    [Header("Event")] 
    public Action <char> onKeyPressed;

    IEnumerator Start()
    {
        CreateKeys();
        yield return null;
        UpdateRectTransform();
    }

    private void Update()
    {
        UpdateRectTransform();
        PlaceKeys();
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
                
                keyInstance.GetComponent<UnityEngine.UI.Button>().onClick.AddListener((() => KeyPressedCallback(key)));
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

    public void KeyPressedCallback(char key)
    {
        Debug.Log("Key pressed : " + key);
        onKeyPressed?.Invoke(key);
    }
}

[System.Serializable]
public struct KeyboardLine
{
    public string keys;
    
}