using UnityEngine;
using System.Collections;

/// <summary>
/// Character parent class.
/// </summary>
public class Character : MonoBehaviour
{
	public bool allowInput = false; // allow to have input (move, etc.)
	public string type; // type as name. E.g. Hammer or EnemySuit

	[HideInInspector] public CharacterController characterController = null; // reference to Controller (hidden)
	[HideInInspector] public CharacterMotor characterMotor = null; // reference to Motor (hidden)

	[HideInInspector] public CharacterInputController controller; // refernce to input controller (hidden)

	internal Animator animator; // reference to animator

	// Raycast setup
	internal Ray ray;
	internal RaycastHit rayCastHit;

    void Start()
	{
		//--------------------------------------------------------------------------------
		// Add (controller) components to man		
		//--------------------------------------------------------------------------------
		characterController = gameObject.GetComponent<CharacterController>();
		characterMotor = gameObject.GetComponent<CharacterMotor>();
        controller = gameObject.GetComponent<CharacterUserInput>();

        //--------------------------------------------------------------------------------
        // animator & others
        //--------------------------------------------------------------------------------
        animator = gameObject.GetComponent<Animator>();
		
		//--------------------------------------------------------------------------------
		// Initialize Character specifics
		//--------------------------------------------------------------------------------
		InitializeSpecific();		
	}	

	public virtual void InitializeSpecific(){}

	public virtual void FixedUpdate()
	{
        // Character movement, we do this here as we're updating physics stuff
        // if (allowInput && !characterData.playerDead)
        if (allowInput)
        { // is playerDead necessary
            characterMotor.inputMoveDirection = transform.rotation * controller.moveDirection;

            gameObject.transform.Rotate(0, controller.sightDirection.x * 10f, 0);
        }
	}

	// Update is called once per frame
	public virtual void Update ()
	{}

	public virtual void AllowInput(bool aState)
	{
		// set bool
		allowInput = aState;
		// reset characterController(?) & characterMotor
		characterMotor.inputMoveDirection = Vector3.zero;
		// reset all necessary
		controller.Reset();
		controller.enabled = aState; // set this instantly to avoid 'confusion' (still updated in the main loop)
	}
}

