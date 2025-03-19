using UnityEngine;

[CreateAssetMenu(menuName = "Parkour Menu/Create New Parkour Action")]
public class NewParkourAction : ScriptableObject
{
    [Header("Checking Obstacle Height")]
    [SerializeField] string animationName;
    [SerializeField] string barrierTag;

    [SerializeField] float minimumHeight; // Fixed spelling mistake
    [SerializeField] float maximumHeight; // Changed string to float (assuming height should be a number)

    [Header("Rotate Player Towards Obstacle")]
    [SerializeField] bool lookAtObstacle;

    [SerializeField] float parkourActionDelay;
    public Quaternion RequiredRotation {get; set;}

    
    [Header("Checking Obstacle Height")]
    [SerializeField] bool allowTargetMatchning = true;
    [SerializeField] AvatarTarget compareBodyPart;
    [SerializeField] float compareStartTime;
    [SerializeField] float compareEndTime;
    [SerializeField] Vector3 comparePositionWeight = new Vector3(0,1,0);
    public Vector3 ComparePosition {get; set;}




    public bool CheckIfAvailable(EnvironmentChecker.ObstacleInfo hitData, Transform player)
{

        if (!string.IsNullOrEmpty(barrierTag) && hitData.hitInfo.transform.tag != barrierTag)
        {
            return false;
        }

        float checkHeight = hitData.heightInfo.point.y  - player.position.y;

        if (checkHeight<minimumHeight || checkHeight>maximumHeight)
        {
            return false;
        }
        if (lookAtObstacle)
        {
            RequiredRotation = Quaternion.LookRotation(-hitData.hitInfo.normal);;
        }
        if (allowTargetMatchning)
        {
            ComparePosition = hitData.heightInfo.point;

        }
        return true;
    }


    public string AnimationName => animationName;
    public bool LookAtObstacle => lookAtObstacle;


    public bool AllowTargetMatchning => allowTargetMatchning;
    public AvatarTarget CompareBodyPart => compareBodyPart;
    public float CompareStartTime => compareStartTime;
    public float CompareEndTime => compareEndTime;
    public Vector3 ComparePositionWeight => comparePositionWeight;
    public float ParkourActionDelay => parkourActionDelay;

}
