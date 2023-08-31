using System;
using UnityEngine;
using UnityEngine.UI;
using Button = UnityEngine.UIElements.Button;

[RequireComponent(typeof(Button))]

//code borrowed and modified by Warped Imagination on youtube https://www.youtube.com/watch?v=wsWeI7APjAU
public class Keys : MonoBehaviour
{
    [SerializeField] [Tooltip("Keycode representing a key")]
    private KeyCode keyCode = UnityEngine.KeyCode.None;

    public Action<KeyCode> pressed;
    
    public KeyCode KeyCode
    {
        get { return keyCode; }
    }

    private void Awake()
    {
        GetComponent<UnityEngine.UI.Button>().onClick.AddListener(OnButtonClick);
        
        Text text = GetComponentInChildren<Text>();
        
        if (text && string.IsNullOrEmpty(text.text))
        {
            text.text = keyCode.ToString();
        }
    }
    
    private void OnButtonClick()
    {
        pressed?.Invoke(keyCode);
        //Debug.Log("Clicked");
    }
}
