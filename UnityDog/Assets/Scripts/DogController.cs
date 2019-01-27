using UnityEngine;
using DoodleStudio95;

public class DogController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float movingDrag;
    [SerializeField] private float stopDrag;

    private DogAnimator dogAnimator;
    private Rigidbody rb;
    private Vector2 moveInput;

    private bool gameStarted;
    private bool dogBusy;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        dogAnimator = GetComponentInChildren<DogAnimator>();

        EventManager.onGameStart.Register(ToggleGameStarted);
        EventManager.onGameEnd.Register(ToggleGameStarted);
        EventManager.onDogAction.Register(ToggleBusy);
        EventManager.onDogActionEnd.Register(ToggleBusy);
    }

    private void OnDestroy()
    {
        EventManager.onGameStart.Unregister(ToggleGameStarted);
        EventManager.onGameEnd.Unregister(ToggleGameStarted);
        EventManager.onDogAction.Unregister(ToggleBusy);
        EventManager.onDogActionEnd.Unregister(ToggleBusy);
    }

    private void ToggleGameStarted()
    {
        gameStarted = !gameStarted;
    }

    private void ToggleBusy(GoalData.Action action, GoalData.ObjectId objectId, Vector3 position)
    {
        dogBusy = !dogBusy;
    }

    private bool CanAct()
    {
        return gameStarted && !dogBusy;
    }

    private void Update()
    {
        if (CanAct())
        {
            moveInput.x = Input.GetAxis("Horizontal");
            moveInput.y = Input.GetAxis("Vertical");
        }
        else
        {
            moveInput = Vector2.zero;
        }

        dogAnimator.moveInput = moveInput;
        rb.drag = moveInput.sqrMagnitude > 0.0f ? movingDrag : stopDrag;
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

