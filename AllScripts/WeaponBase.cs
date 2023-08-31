using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]
public class WeaponBase : MonoBehaviour, IMove, ITrigger
{
    public UnityEvent triggerEnterEvent;
    private Rigidbody rigidbodyObj;
    public float moveSpeed = 1f;
    public Vector3 direction;
    private void Awake()
    {
        GetComponent<BoxCollider>().isTrigger = true;
        rigidbodyObj = GetComponent<Rigidbody>();
    }

    public void Move()
    {
        //print("vehicle moving");
        direction.x = moveSpeed;
        rigidbodyObj.AddForce(direction);
    }

    public void OnTriggerEnter(Collider other)
    {
        print("switching controller");
        triggerEnterEvent.Invoke();
    }
}
