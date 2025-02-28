using System;
using System.Numerics;
using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{
	[SerializeField] private float m_JumpForce;				
	[SerializeField] private float m_CrouchForce = 100f;			// Amount of force added when the player jumps.
	[Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;			// Amount of maxSpeed applied to crouching movement. 1 = 100%
	[Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;	// How much to smooth out the movement
	[SerializeField] private bool m_AirControl = true;							// Whether or not a player can steer while jumping;
	[SerializeField] private LayerMask m_WhatIsGround;							// A mask determining what is ground to the character
	[SerializeField] private Transform m_GroundCheck;							// A position marking where to check if the player is grounded.
	[SerializeField] private Transform m_CeilingCheck;							// A position marking where to check for ceilings
	[SerializeField] private Collider2D m_CrouchDisableCollider;				// A collider that will be disabled when crouching

	const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
	private bool m_Grounded;            // Whether or not the player is grounded.
	const float k_CeilingRadius = .2f; // Radius of the overlap circle to determine if the player can stand up
	private bool m_OnAir;
	private Rigidbody2D m_Rigidbody2D;
	private bool m_FacingRight = true;  // For determining which way the player is currently facing.
	private UnityEngine.Vector3 m_Velocity = UnityEngine.Vector3.zero;

	private uint m_JumpStock = 2;


	private UnityEngine.Vector2 m_lastVel;

	[Header("Events")]
	[Space]

	public UnityEvent OnLandEvent;

	[System.Serializable]
	public class BoolEvent : UnityEvent<bool> { }

	public BoolEvent OnCrouchEvent;
	private bool m_wasCrouching = false;

	private void Awake()
	{
		m_Rigidbody2D = GetComponent<Rigidbody2D>();
		m_lastVel = m_Rigidbody2D.linearVelocity;
		m_JumpForce = Math.Abs(m_Rigidbody2D.mass * Physics2D.gravity.y); // this force will allow 2 second aerial time
																// with impulse force. can be regulated
		if (OnLandEvent == null)
			OnLandEvent = new UnityEvent();

		if (OnCrouchEvent == null)
			OnCrouchEvent = new BoolEvent();
	}

	private void FixedUpdate()
	{
		bool wasGrounded = m_Grounded;

		// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
		// This can be done using layers instead but Sample Assets will not overwrite your project settings.
		Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
				m_JumpStock = 2;
				if (!wasGrounded)
					OnLandEvent.Invoke();
			}
		}
	}



	public void Move(float move, bool crouch, bool jump) // this method is called with update fixedUpdate, so regularly called
	// acceleration can be calculated with this assumption (being regularly called assumption)
	{
		// If crouching, check to see if the character can stand up
		if (!crouch)
		{
			// If the character has a ceiling preventing them from standing up, keep them crouching
			if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround))
			{
				crouch = true;
			}
		}

		//only control the player if grounded or airControl is turned on
		if (m_Grounded || m_AirControl)
		{

			// If crouching
			if (crouch)
			{ 
				if (!m_OnAir)
				{
					if (!m_wasCrouching)
					{
						m_wasCrouching = true;
						OnCrouchEvent.Invoke(true);
					}

					// Reduce the speed by the crouchSpeed multiplier
					move *= m_CrouchSpeed;

					// Disable one of the colliders when crouching
					if (m_CrouchDisableCollider != null)
						m_CrouchDisableCollider.enabled = false;
				}
				else // if on air
				{
					m_Rigidbody2D.AddForce(UnityEngine.Vector2.down * m_CrouchForce);
				}
			} else
			{
				// Enable the collider when not crouching
				if (m_CrouchDisableCollider != null)
					m_CrouchDisableCollider.enabled = true;

				if (m_wasCrouching)
				{
					m_wasCrouching = false;
					OnCrouchEvent.Invoke(false);
				}
			}

			// Move the character by finding the target velocity
			UnityEngine.Vector3 targetVelocity = new UnityEngine.Vector2(move * 10f, m_Rigidbody2D.linearVelocity.y);
			// And then smoothing it out and applying it to the character
			m_Rigidbody2D.linearVelocity = UnityEngine.Vector3.SmoothDamp(m_Rigidbody2D.linearVelocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

			// If the input is moving the player right and the player is facing left...
			if (move > 0 && !m_FacingRight)
			{
				// ... flip the player.
				Flip();
			}
			// Otherwise if the input is moving the player left and the player is facing right...
			else if (move < 0 && m_FacingRight)
			{
				// ... flip the player.
				Flip();
			}
		}
		// If the player should jump...
		if (m_JumpStock > 0 && jump)
		{
			// Add a vertical force to the player.
			m_Rigidbody2D.linearVelocityY = 0;
			m_Rigidbody2D.AddForce(UnityEngine.Vector2.up * m_JumpForce, ForceMode2D.Impulse);
			m_JumpStock --;
		}
	}
	private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            m_Grounded = true;
			m_OnAir = false;
			m_JumpStock = 2;
        }
		if (collision.gameObject.CompareTag("wall"))
		{
            m_Grounded = true;
			m_OnAir = false;
			m_JumpStock = 0;
		}
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ground") || collision.gameObject.CompareTag("wall"))
        {
            m_Grounded = false;
			m_OnAir = true;
			m_JumpStock = 1;
        }
    }

	private void Flip()
	{
		// Switch the way the player is labelled as facing.
		m_FacingRight = !m_FacingRight;

		// Multiply the player's x local scale by -1.
		transform.Rotate(0f, 180f, 0f);
	}
}
