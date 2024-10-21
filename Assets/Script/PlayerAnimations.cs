using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private Animator animator;
    private PlayerMovementRB playerMovement_RB;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        playerMovement_RB = GetComponent<PlayerMovementRB>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        animator.SetFloat("Speed",
            playerMovement_RB.GetCurrentSpeed() / playerMovement_RB.runingSpeed);
    }
}
