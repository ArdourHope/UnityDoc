//Guide by Ardour Hope :: https://github.com/ArdourHope

using UnityEngine;
using System.Collections;
public class myFPSController;

// Key binding
// Moving key could be change with W,A,S,D by Changing Arrowkeys with capital alphabet
// example : public Keycode LeftKey = KeyCode.A; (to change left arrow to A )

public KeyCode LeftKey = KeyCode.LeftArrow; //bind moving left with keyboard left arrow key
public KeyCode RightKey = KeyCode.RightArrow; //bind moving right with keyboard right arrow key
public KeyCode FowardKey = KeyCode.UpArrow; //bind moving Foward with keyboard up arrow key
public KeyCode BackwardKey = KeyCode.DownArrow; //bind moving backward with keyboard back arrow key
public KeyCode JumpKey = KeyCode.Space; //bind Jump movement with space button 

// Variable

public float speed = 2; //movement speed
public float MaxJump = 10; //Jump Range
public bool MouseLookActive; //Mousemovement

Vector3 MoveDirection; // Vector use as direction of movement
CharacterController myController;
bool isJump;

// Init
// you could put start condition or function called only on start of the game

void Start()
{

}

// WARNING : funtion will be called per frame NOT PER TIME (SECOND OR MILISECOND)

void Update()
{
    MoveDirection = Vector3.zero; 
    myController = GetComponent<CharacterController>();
    
    // Movement input 

    //If the key pressed. movedirection will be change with vector direction multiply with speed
    
    //NOTES
    //if the speed is 2 then the movement will be 2 unit/frame
    //if the game runs on 60 fps then the character moving with speed 120 unit/second

    if (Input.GetKey(LeftKey))
    {
        MoveDirection = Vector3.left * speed;
    }
    if (Input.GetKey(RightKey))
    {
        MoveDirection = Vector3.right * speed;
    }
    if (Input.GetKey(FowardKey))
    {
        MoveDirection = Vector3.foward * speed;
    }
    if (Input.GetKey(BackwardKey))
    {
        MoveDirection = Vector3.back * speed;
    }

    //Jump Action

    if (Input.GetKey(JumpKey))
    {
        if (myController.isGrounded) //Checking if the character already in jump movement or not
        {
            isJump = true; //if it isn't jump. Declaring jump condition for prevent double jump;
            LimitJump = transform.position.y + MaxJump; 


        }
    }

    if (isJump)
    {
        MoveDirection.y = Speed;

        // if the y position exceed the limit.
        // declaring is jump to false to move the character falling to the ground

        if (transform.position.y >= LimitJump){
            isJump = false; 
        }
    }


    //Action

    ChangeHeadingByMouse();
    MoveDirection = transform.TransformDirection(MoveDirection);
    
    //Fall to the ground if jumping is false using gravity variable (10 or 9.8)
    if (!isJump) MoveDirection.y -= 10;

    myController.Move(MoveDirection * Time.deltaTime);
}


// Mouse Control

void WrapAngel (ref float angle)
{
    // reset angle everytime value exceeding 360 
    if (angle < -360F) angle += 360F;
    if (angle > 360F) angle -= 360F;
}


void ChangeHeading (float aVal)
{
    Heading += aVal;
    WrapAngel(ref Heading);
    transform.localEulerAngles = new Vector3(Pitch, ChangeHeading, 0);
}

void ChangePitch(float aVal)
{
    Pitch += aVal;
    WrapAngel(ref Pitch);
    transform.localEulerAngles = new Vector3(Pitch, ChangeHeading, 0);
}



void ChangeHeadingByMouse ()
{
    if (MouseLookActive && Input.GetMouseButton (0))
    {
        float deltaX = input.GetAxis("Mouse X") * 2;
        float deltay = input.GetAxis("Mouse Y") * 2;
        ChangeHeading(deltaX);
        ChangePitch(deltaY);

    }
}