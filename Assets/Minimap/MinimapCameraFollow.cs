using UnityEngine;

public class MinimapCameraFollow : MonoBehaviour
{

    //Player location.
    [SerializeField] Transform playerTransform;

    //Sets location of the minimap camera to the player location.
    public void FollowPlayer()
    {
        this.transform.position = new Vector3(playerTransform.position.x, playerTransform.position.y, this.transform.position.z);
    }
}
