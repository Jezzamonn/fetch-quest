using DoodleStudio95;
using UnityEngine;

public class DogAnimator : MonoBehaviour
{
    [SerializeField] private DoodleAnimationFile runAnim;
    [SerializeField] private DoodleAnimationFile idleAnim;

    [SerializeField] private DoodleAnimationFile contextAnim;

    [SerializeField] private RandomSound randomSound;

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
        randomSound.Play();
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
            if (moveInput.x > 0.1f)
            {
                Vector3 scale = transform.localScale;
                scale.x = Mathf.Abs(scale.x);
                transform.localScale = scale;
            }
            else if (moveInput.x < -0.1f)
            {
                Vector3 scale = transform.localScale;
                scale.x = -Mathf.Abs(scale.x);
                transform.localScale = scale;
            }
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
