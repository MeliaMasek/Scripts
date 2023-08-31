using UnityEngine;
public class UserInput : MonoBehaviour
{
    public CharBase characterObj;
    private IMove moveObj;
    private ITrigger triggerObj;
    private void Awake()
    {
        moveObj = characterObj as IMove;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            moveObj.Move();
        }
    }

    public void ChangeWeapon(WeaponBase vehicleObj)
    {
        moveObj = vehicleObj;
    }
}
