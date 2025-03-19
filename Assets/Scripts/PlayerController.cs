using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    [Header("Player Movement")]
    public float movementSpeed = 5f;
    public MainCameraController Mcc;
    // public float rotSpeed = 600f;
    Quaternion requiredRotation;

    public float rotSpeed = 600f;

    bool playerControl = true;

    [Header("Player Animation")]

    public Animator animator;
    
    [Header("Player Collision and gravity")]
    public CharacterController CC;
    public float surfaceCheckRadius = 0.3f;
    public Vector3 surfaceCheckOffeset;
    public LayerMask surfaceLayer;
    bool onSurface;
    [SerializeField] float fallingSpeed;
    [SerializeField] Vector3 moveDir;



    // ----------------------------------------------------------------------------------------------------------------------------
    void Update()
    {
        playerMovement();

        if (!playerControl)
        {
            return;
        }

        if (onSurface)
        {
            fallingSpeed = 0;
        }
        else
        {
            fallingSpeed += Physics.gravity.y * Time.deltaTime; 
        }

        var velocity = moveDir * movementSpeed;
        velocity.y = fallingSpeed;

        playerMovement();     
        surfaceCheck();
        Debug.Log("on surface"+ onSurface);   
    }
void playerMovement()
{
    if (!CC.enabled) return; // Prevent movement if CharacterController is disabled

    float horizontal = Input.GetAxis("Horizontal");
    float Vertical = Input.GetAxis("Vertical");

    float movementAmount = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(Vertical));

    var movementInput = (new Vector3(horizontal, 0, Vertical)).normalized;
    var movementDirection = Mcc.flatRotation * movementInput;

    CC.Move(movementDirection * movementSpeed * Time.deltaTime); // ✅ Only moves when CC is enabled

    if (movementAmount > 0)
    {
        transform.rotation = Quaternion.LookRotation(movementDirection);
    }

    moveDir = movementDirection;
    animator.SetFloat("MovementValue", movementAmount, 0.1f, Time.deltaTime);
}



    void surfaceCheck(){
        onSurface = Physics.CheckSphere(transform.TransformPoint(surfaceCheckOffeset),surfaceCheckRadius, surfaceLayer);
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.TransformPoint(surfaceCheckOffeset),surfaceCheckRadius);
    }

    public void SetControl(bool hasControl){
        this.playerControl = hasControl;
        CC.enabled =  hasControl;

        if (!hasControl)
        {
            animator.SetFloat("MovementValue",0f);
            requiredRotation  = transform.rotation;
        }
    }
}