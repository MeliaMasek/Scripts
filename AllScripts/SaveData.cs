using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DataStorage", menuName = "Utilities/Data Storage Object")]
public class SaveData : ScriptableObject
{
    public ScriptableObject data;
    public List<ScriptableObject> listData;

    public void SetListData()
    {
        foreach (var obj in listData)
        {
            SetData(obj);
        }
    }

    public void GetListData()
    {
        foreach (var obj in listData)
        {
            GetData(obj);
        }
    }
    
    public void SetData(ScriptableObject obj)
    {
        if (obj == null) return;
        PlayerPrefs.SetString(obj.name, JsonUtility.ToJson(obj));
    }

    public void SetData(MonoBehaviour obj)
    {
        if (data == null) return;
        PlayerPrefs.SetString(obj.name, JsonUtility.ToJson(obj));
    }
    
    public void GetData(ScriptableObject obj)
    {
        if (obj == null) return;
        if (!string.IsNullOrEmpty(PlayerPrefs.GetString(obj.name)))
            JsonUtility.FromJsonOverwrite(PlayerPrefs.GetString(obj.name), obj);
    }

    public void GetData(MonoBehaviour obj)
    {
        if (obj == null) return;
        if (!string.IsNullOrEmpty(PlayerPrefs.GetString(obj.name)))
            JsonUtility.FromJsonOverwrite(PlayerPrefs.GetString(obj.name), obj);
    }
}