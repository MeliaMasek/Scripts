using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class ObjectTransformBehavoir : MonoBehaviour
{
    public UnityEvent onEnableEvent;
    public Vector3Data location;
    public float holdTime = 3f;
    private WaitForSeconds wfsObj;

    private void Awake()
    {
        wfsObj = new WaitForSeconds(holdTime);
    }

    private IEnumerator Start()
    {
        yield return wfsObj;
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        onEnableEvent.Invoke();
        StartCoroutine(Start());
    }

    public void UpdatePosition()
    {
        transform.position = location.value;
    }
}
