using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    //Player Input from the Move Input System
    private Vector2 moveInput;

    //Player Location, the position to check for walls in front of the player.
    [SerializeField] Transform playerTransform, forwardCheck;

    //What the player sees
    [SerializeField] GameObject playerScreen;

    //Wall LayerMask to check for walls in the scene.
    [SerializeField] LayerMask wallLayerMask;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();

        //Turn Right
        if (moveInput.x > 0)
        {
            playerTransform.localRotation *= Quaternion.Euler(0, 0, -90);

        }
        //Turn Left
        else if (moveInput.x < 0)
        {
            Debug.Log("Turning Left");
            playerTransform.localRotation *= Quaternion.Euler(0, 0, 90);
        }
        //Move Forward if wall is not in front of the player.
        else if (moveInput.y > 0 && !Physics2D.Linecast(playerTransform.position, forwardCheck.position, wallLayerMask))
        {
            Debug.Log("Trying to move");
            float directionFaced = playerTransform.localEulerAngles.z;
            Debug.Log("Direction Faced: " + directionFaced);
            if (directionFaced == 0)
            {
                playerTransform.localPosition += new Vector3(0, 1, 0);
                Debug.Log("Moving Up");
            }
            else if (directionFaced == 270)
            {
                playerTransform.localPosition += new Vector3(1, 0, 0);
                Debug.Log("Moving Right");
            }
            else if (directionFaced == 90)
            {
                playerTransform.localPosition += new Vector3(-1, 0, 0);
                Debug.Log("Moving Left");
            }
            else if (directionFaced == 180)
            {
                playerTransform.localPosition += new Vector3(0, -1, 0);
                Debug.Log("Moving Down");
            }
        }
        //Update the player screen
        playerScreen.GetComponent<PlayerVisualRendering>().renderNextScreen();
    }

}
