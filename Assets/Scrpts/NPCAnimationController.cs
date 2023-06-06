using UnityEngine;

public class NPCAnimationController : MonoBehaviour
{
    private Animator animator;
    private FriendController friendController;

    private void Start()
    {
        // Get the Animator component attached to the NPC
        animator = GetComponent<Animator>();

        // Get the FriendController component attached to the NPC
        friendController = GetComponent<FriendController>();
    }

    private void Update()
    {
        // Check the CharacterState of the FriendController component
        if (friendController.state == FriendController.CharacterState.Shooting)
        {
            // Play the Shooting animation
            animator.SetTrigger("Shooting");
        }
    }
}
