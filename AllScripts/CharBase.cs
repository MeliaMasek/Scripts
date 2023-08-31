using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CharacterController))]
public class CharBase : MonoBehaviour, IMove, ITrigger
{
    public float moveSpeed = 1f;
    private Vector3 direction;
    private CharacterController charControllerObj;
    public UnityEvent triggerEnterEvent;
    private void Awake()
    {
        charControllerObj = GetComponent<CharacterController>();
    }

    public void Move()
    {
        //print("Character moving");    
        direction.x = moveSpeed;
        charControllerObj.Move(direction);
    }

    public void AttachToVehicle(Transform obj)
    {
        transform.parent = obj;
    }

    public void OnTriggerEnter(Collider other)
    {
        triggerEnterEvent.Invoke();    
    }
}
