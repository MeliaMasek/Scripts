using System.Globalization;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]

//borrowed and modified from Anthony Romrell https://github.com/anthonyromrell
public class TextBehaviour : MonoBehaviour
{
    private Text textObj;
    public GameAction gameActionObj;
    public UnityEvent awakeEvent, raiseEvent;
    
    protected virtual void Start()
    {
        textObj = GetComponent<Text>();
        awakeEvent.Invoke();
    }

    /*
    public void UpdateText(TextDataList textListDataObj)
    {
        textObj.text = textListDataObj.ReturnCurrentLine();
    }
    */
    
    public void UpdateText(IntData intDataObj)
    {
        textObj.text = intDataObj.value.ToString();
    }

    public void UpdateText(string obj)
    {
        textObj.text = obj;
    }

    public void UpdateText(FloatData obj)
    {
        textObj.text = obj.value.ToString(CultureInfo.CurrentCulture);
    }
}