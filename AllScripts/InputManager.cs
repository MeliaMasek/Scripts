using UnityEngine;
using UnityEngine.UI;

//code borrowed and modified by Tabsil on youtube https://www.youtube.com/watch?v=rYcE_Fem4NE
//code borrowed and modified by Tabsil on youtube https://www.youtube.com/watch?v=ZGtpZ24-tWc
//code borrowed and modified by Tabsil on youtube https://www.youtube.com/watch?v=T3MT9Tb1juc
public class InputManager : MonoBehaviour
{
    [Header("Elements")] 
    [SerializeField] private Text text;
    [SerializeField] private Keyboard keyboard;
    
    private void Start()
    {
        keyboard.onKeyPressed += KeyPressedCallback;
    }

    private void KeyPressedCallback(char key)
    {
        text.text += key.ToString();
    }
}
