using UnityEngine;
using DoodleStudio95;

public class DogController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float movingDrag;
    [SerializeField] private float stopDrag;

    [SerializeField] private DoodleAnimationFile walkLeftAnim;
    [SerializeField] private DoodleAnimationFile walkRightAnim;
    [SerializeField] private DoodleAnimationFile idleLeftAnim;
    [SerializeField] private DoodleAnimationFile idleRightAnim;

    private Rigidbody rb;
    private DoodleAnimator animator;
    private bool lastMoveRight;

    private Vector2 moveInput;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<DoodleAnimator>();
    }

    private void Update()
    {
        moveInput.x = Input.GetAxis("Horizontal");
        moveInput.y = Input.GetAxis("Vertical");
        bool hasInput = moveInput.sqrMagnitude > 0.0f;

        rb.drag = hasInput ? movingDrag : stopDrag;

        DoodleAnimationFile desiredAnim;
        if (hasInput)
        {
            lastMoveRight = moveInput.x > 0.0f;
            desiredAnim = lastMoveRight ? walkRightAnim : walkLeftAnim;
        }
        else
        {
            desiredAnim = lastMoveRight ? idleRightAnim : idleLeftAnim;
        }

        if (desiredAnim != animator.File)
        {
            animator.ChangeAnimation(desiredAnim);
        }
    }

    private void FixedUpdate()
    {
        Vector3 moveForce = new Vector3(moveInput.x, 0.0f, moveInput.y);
        moveForce.Normalize();
        moveForce *= speed * Time.fixedDeltaTime;
        rb.AddForce(moveForce, ForceMode.VelocityChange);
    }
}

/*using UnityEngine;

public class DogController : MonoBehaviour
{
#pragma warning disable 0649
    [Header("LateralMovement")]
    [SerializeField]
    private float maxSpeed;
    [SerializeField] private AnimationCurve movementStartStopCurve;
    private float movementTimer;

    [Header("Drag")]
    [SerializeField]
    private float groundDrag;
#pragma warning restore 0649

    [ReadOnly] public float currentVelocity;
    private Rigidbody rb;
    private Vector2 moveInput;

    private SphereCollider movementCollider;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        movementCollider = GetComponent<SphereCollider>();
    }

    private void Update()
    {
        // Movement
        moveInput = new Vector2(
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical"));
    }

    private void FixedUpdate()
    {
        // Check Movement Input
        bool hasMovementInput = moveInput.sqrMagnitude > 0f;
        movementTimer += Time.deltaTime * (hasMovementInput ? 1f : -1f);
        movementTimer = Mathf.Clamp(movementTimer, 0f, MaxTimeFromCurve(movementStartStopCurve));

        Vector3 forward = Vector3.ProjectOnPlane(Camera.main.transform.forward, Vector3.up).normalized;
        Vector3 right = Vector3.ProjectOnPlane(Camera.main.transform.right, Vector3.up).normalized;
        Vector3 inputDir = (forward * moveInput.y + right * moveInput.x).normalized;

        rb.drag = groundDrag;
        GroundLateralMovement(hasMovementInput, inputDir);
    }

    private void GroundLateralMovement(bool hasMovementInput, Vector3 inputDir)
    {
        if (hasMovementInput)
        {
            Vector3 desiredVelocity = inputDir * movementStartStopCurve.Evaluate(movementTimer) * maxSpeed;
            float dragFactor = Mathf.Clamp(1f - Time.deltaTime * rb.drag, 0.001f, 1f);
            desiredVelocity /= dragFactor;
            Vector3 currentGroundVelocity = Vector3.ProjectOnPlane(rb.velocity, Vector3.up);

            Vector3 speedDelta = desiredVelocity - currentGroundVelocity;

            rb.AddForce(speedDelta, ForceMode.VelocityChange);
        }
    }

    private float MaxTimeFromCurve(AnimationCurve curve)
    {
        return curve.keys[curve.length - 1].time;
    }
}*/

