using System.Text;
using UnityEngine;
using System.Collections;

public class PlayerVisualRendering : MonoBehaviour
{
    //What the player sees.
    [SerializeField] GameObject playerScreen;

    //Minimap Camera that follows the player.
    [SerializeField] GameObject minimapCamera;

    //Player location.
    [SerializeField] Transform playerTransform;

    //Locations of where to check for walls to render the player screen.
    [SerializeField] Transform rightCheck, rightUpCheck, leftCheck, leftUpCheck, upCheck, upUpCheck;

    //All player screens.
    [SerializeField] Sprite[] playerScreens;

    //Wall LayerMask to check for walls in the scene.
    [SerializeField] LayerMask wallLayerMask;

    //Enum to represent the different screen states based on wall checks.
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
        twoForwardUpLeftUpRight
        
    }

    //Start is called once before the first execution of Update after the MonoBehaviour is created.
    void Start()
    {
        RenderNextScreen();
    }

    //Update is called once per frame.
    void Update()
    {
        
    }

    //Renders the next screen based on the player's position and the surrounding walls.
    public void RenderNextScreen()
    {
        bool isRightWall = !Physics2D.Linecast(playerTransform.position, rightCheck.position, wallLayerMask);
        bool isRightUpWall = !Physics2D.Linecast(playerTransform.position, rightUpCheck.position, wallLayerMask);
        bool isLeftWall = !Physics2D.Linecast(playerTransform.position, leftCheck.position, wallLayerMask);
        bool isLeftUpWall = !Physics2D.Linecast(playerTransform.position, leftUpCheck.position, wallLayerMask);
        bool isUpWall = !Physics2D.Linecast(playerTransform.position, upCheck.position, wallLayerMask);
        bool isUpUpWall = !Physics2D.Linecast(playerTransform.position, upUpCheck.position, wallLayerMask);

        // Determine the screen state based on the wall checks.
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

        //Set the player screen sprite based on the determined screen state.
        playerScreen.GetComponent<SpriteRenderer>().sprite = playerScreens[playerScreens.Length - 1];
        StartCoroutine(RenderNextScreenHelper(playerScreens[(int)System.Enum.Parse(typeof(ScreenState), sb.ToString())]));
        minimapCamera.GetComponent<MinimapCameraFollow>().FollowPlayer();
    }

    private IEnumerator RenderNextScreenHelper(Sprite newScreen)
    {
        //Delay rendering by 0.1 seconds to have play effect when the player moves.
        yield return new WaitForSeconds(0.1f);
        playerScreen.GetComponent<SpriteRenderer>().sprite = newScreen;
    }
}
