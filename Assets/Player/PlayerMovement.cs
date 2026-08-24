using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    //Player Input from the Move Input System.
    private Vector2 moveInput;

    //Player Location, the position to check for walls in front of the player.
    [SerializeField] Transform playerTransform, forwardCheck;

    //What the player sees
    [SerializeField] GameObject playerScreen;

    //Wall LayerMask to check for walls in the scene.
    [SerializeField] LayerMask wallLayerMask;

    private bool playerMoved;

    // Start is called once before the first execution of Update after the MonoBehaviour is created.
    void Start()
    {
        
    }

    // Update is called once per frame.
    void Update()
    {
        
    }

    //Called when the player moves using the Move Input System.
    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();

        //Turn Right
        if (moveInput.x > 0)
        {
            playerTransform.localRotation *= Quaternion.Euler(0, 0, -90);
            playerMoved = true;

        }
        //Turn Left
        else if (moveInput.x < 0)
        {
            playerTransform.localRotation *= Quaternion.Euler(0, 0, 90);
            playerMoved = true;
        }
        //Move Forward if wall is not in front of the player.
        else if (moveInput.y > 0 && !Physics2D.Linecast(playerTransform.position, forwardCheck.position, wallLayerMask))
        {
            float directionFaced = playerTransform.localEulerAngles.z;
            playerMoved = true;
            if (directionFaced == 0)
            {
                playerTransform.localPosition += new Vector3(0, 1, 0);
            }
            else if (directionFaced == 270)
            {
                playerTransform.localPosition += new Vector3(1, 0, 0);
            }
            else if (directionFaced == 90)
            {
                playerTransform.localPosition += new Vector3(-1, 0, 0);
            }
            else if (directionFaced == 180)
            {
                playerTransform.localPosition += new Vector3(0, -1, 0);
            }
        }
        //Update the player screen.
        if (playerMoved)
        {
            playerScreen.GetComponent<PlayerVisualRendering>().RenderNextScreen();
            playerMoved = false;
        }
        
    }

}
