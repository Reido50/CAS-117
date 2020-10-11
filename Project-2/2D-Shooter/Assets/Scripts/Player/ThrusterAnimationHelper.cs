using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class ThrusterAnimationHelper : MonoBehaviour
{
    private InputManager inputManager = null;
    public Animator animator = null;

    private void Start()
    {
        inputManager = InputManager.instance;
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Vector2 input = new Vector2(inputManager.horizontalMoveAxis, inputManager.verticalMoveAxis);
        float thrusterforce = Vector2.Dot(input, transform.up);
        animator.SetFloat("MoveY", thrusterforce);
    }
}
