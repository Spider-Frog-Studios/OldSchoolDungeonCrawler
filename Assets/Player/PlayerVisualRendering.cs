using System.Text;
using UnityEngine;

public class PlayerVisualRendering : MonoBehaviour
{
    //What the player sees
    [SerializeField] GameObject playerScreen;

    //Player Location
    [SerializeField] Transform playerTransform;

    //Locations of where to check for walls to render the player screen.
    [SerializeField] Transform rightCheck, rightUpCheck, leftCheck, leftUpCheck, upCheck, upUpCheck;

    //All player screens.
    [SerializeField] Sprite[] playerScreens;

    //Wall LayerMask to check for walls in the scene.
    [SerializeField] LayerMask wallLayerMask;

    private enum ScreenState
    {
        deadEnd,
        deadEndLeft,
        deadEndRight,
        deadEndLeftRight,
        oneForward,
        oneForwardLeft,
        oneForwardRight,
        oneForwardUpLeft,
        oneForwardUpRight,
        oneForwardUpLeftUpRight,
        twoForward,
        twoForwardLeft,
        twoForwardRight,
        twoForwardLeftRight,
        twoForwardUpLeft,
        twoForwardUpRight,
        twoForwardUpLeftUpRight,
        
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        renderNextScreen();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void renderNextScreen()
    {
        bool isRightWall = !Physics2D.Linecast(playerTransform.position, rightCheck.position, wallLayerMask);
        bool isRightUpWall = !Physics2D.Linecast(playerTransform.position, rightUpCheck.position, wallLayerMask);
        bool isLeftWall = !Physics2D.Linecast(playerTransform.position, leftCheck.position, wallLayerMask);
        bool isLeftUpWall = !Physics2D.Linecast(playerTransform.position, leftUpCheck.position, wallLayerMask);
        bool isUpWall = !Physics2D.Linecast(playerTransform.position, upCheck.position, wallLayerMask);
        bool isUpUpWall = !Physics2D.Linecast(playerTransform.position, upUpCheck.position, wallLayerMask);

        // Determine the screen state based on the wall checks
        StringBuilder sb = new StringBuilder();
        if (isUpWall)
        {
            if (isUpUpWall)
            {
                sb.Append("twoForward");
            }
            else
            {
                sb.Append("oneForward");
            }
        }
        else
        {
            sb.Append("deadEnd");
        }

        if (isLeftWall)
        {
            sb.Append("Left");
        }

        if (isRightWall)
        {
            sb.Append("Right");
        }

        if (isLeftUpWall)
        {
            sb.Append("UpLeft");
        }

        if (isRightUpWall)
        {
            sb.Append("UpRight");
        }

        //Set the player screen sprite based on the determined screen state
        playerScreen.GetComponent<SpriteRenderer>().sprite = playerScreens[(int)System.Enum.Parse(typeof(ScreenState), sb.ToString())];
    }
}
