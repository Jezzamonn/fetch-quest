using DoodleStudio95;
using UnityEngine;

public class DogAnimator : MonoBehaviour
{
    [SerializeField] private DoodleAnimationFile runAnim;
    [SerializeField] private DoodleAnimationFile idleAnim;

    [SerializeField] private DoodleAnimationFile contextAnim;

    [HideInInspector] public Vector2 moveInput;

    private DoodleAnimator animator;
    private bool lastMoveRight;
    private GoalData.Action? currentAction;

    private void Awake()
    {
        animator = GetComponentInChildren<DoodleAnimator>();

        EventManager.onDogAction.Register(OnAction);
        EventManager.onDogActionEnd.Register(OnActionEnd);
    }

    private void OnDestroy()
    {
        EventManager.onDogAction.Unregister(OnAction);
        EventManager.onDogActionEnd.Unregister(OnActionEnd);
    }

    private void OnAction(GoalData.Action action, GoalData.ObjectId objectId, Vector3 position)
    {
        currentAction = action;
    }

    private void OnActionEnd(GoalData.Action action, GoalData.ObjectId objectId, Vector3 position)
    {
        currentAction = null;
    }

    private void Update()
    {
        bool hasInput = moveInput.sqrMagnitude > 0.0f;
        if (hasInput)
        {
            lastMoveRight = moveInput.x > 0.0f;
            Vector3 scale = transform.localScale;
            scale.x = lastMoveRight ? Mathf.Abs(scale.x) : -Mathf.Abs(scale.x);
            transform.localScale = scale;
        }

        UpdateAnim(hasInput);
    }

    private void UpdateAnim(bool hasInput)
    {
        DoodleAnimationFile desiredAnim;

        if (currentAction != null)
        {
            switch (currentAction)
            {
                default:
                    desiredAnim = contextAnim;
                    break;
            }
        }
        else
        {
            desiredAnim = hasInput ? runAnim : idleAnim;
        }

        if (desiredAnim != animator.File)
        {
            animator.ChangeAnimation(desiredAnim);
        }
    }
}
