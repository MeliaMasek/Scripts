using UnityEngine;
using UnityEngine.InputSystem;
//code borrowed and modified from xOctoManx from Youtube https://www.youtube.com/watch?v=9n1NrP8bpyA
public class TurnMovement : MonoBehaviour
{
    private Vector3
        up = Vector3.zero,
        right = new Vector3(0, 90, 0),
        down = new Vector3(0, 180, 0),
        left = new Vector3(0, 270, 0),
        currentDirection = Vector3.zero;

    private Vector3 nextPos, destination, direction;

    public float speed;
    public float rayLength = 1f;
    private bool canMove;
    private void Start()
    {
        currentDirection = up;
        nextPos = Vector3.forward;
        destination = transform.position;
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            nextPos = Vector3.forward;
            currentDirection = up;
            canMove = true;
        }

        //if (Input.GetKeyDown(KeyCode.DownArrow))
        //{
        //    nextPos = Vector3.back;
        //    currentDirection = down;
        //    canMove = true;
        //}

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            nextPos = Vector3.right;
            currentDirection = right;
            canMove = true;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            nextPos = Vector3.left;
            currentDirection = left;
            canMove = true;
        }

        if (Vector3.Distance(destination, transform.position) <= .00001f)
        {
            transform.localEulerAngles = currentDirection;
            if (canMove)
            {
                if (ValidMovement())
                {
                    destination = transform.position + nextPos;
                    direction = nextPos;
                    canMove = false;
                }
            }
        }
    }

    
     bool ValidMovement()
    {
        Ray myRay = new Ray(transform.position + new Vector3(0.2f, 0.27f, 0), transform.forward);
        
        RaycastHit hit;

        Debug.DrawRay(myRay.origin, myRay.direction, Color.blue);
        Debug.DrawRay(transform.position, (myRay.direction - transform.position));

        if (Physics.Raycast(myRay,out hit, rayLength))
        {
            if (hit.rigidbody.CompareTag("Obstacle"))
            {
                //Debug.Log("Hit");
                return false;
            }
        }
        return true;
    }
}