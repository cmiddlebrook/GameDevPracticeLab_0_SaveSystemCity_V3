using UnityEngine;

public class CharacterAnimator : MonoBehaviour {


    private const string IS_WALKING = "IsWalking";


    [SerializeField] private Animator animator;


    private CharacterWaypoints characterWaypoints;


    private void Awake() {
        characterWaypoints = GetComponent<CharacterWaypoints>();
    }

    private void Start() {
        characterWaypoints.OnStateChanged += CharacterWaypoints_OnStateChanged;

        UpdateAnimatorParameters();
    }

    private void CharacterWaypoints_OnStateChanged(object sender, System.EventArgs e) {
        UpdateAnimatorParameters();
    }

    private void UpdateAnimatorParameters() {
        switch (characterWaypoints.GetState()) {
            case CharacterWaypoints.State.Idle:
                animator.SetBool(IS_WALKING, false);
                break;
            case CharacterWaypoints.State.Walking:
                animator.SetBool(IS_WALKING, true);
                break;
        }
    }


}
