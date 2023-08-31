using UnityEngine;
using UnityEngine.UI;
using Button = UnityEngine.UIElements.Button;

//code borrowed and modified by Tabsil on youtube https://www.youtube.com/watch?v=rYcE_Fem4NE
//code borrowed and modified by Tabsil on youtube https://www.youtube.com/watch?v=ZGtpZ24-tWc
//code borrowed and modified by Tabsil on youtube https://www.youtube.com/watch?v=T3MT9Tb1juc

public class Key : MonoBehaviour
{
    [Header("Elements")] 
    [SerializeField] private Text keyText;
    
    private char key;
    
    public void SetKey(char key)
    {
        this.key = key;
        keyText.text = key.ToString();
    }

    public Button GetButton()
    {
        return GetComponent<Button>();
    }
}
