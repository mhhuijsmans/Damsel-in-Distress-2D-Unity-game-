// https://kylewbanks.com/blog/unity-2d-checking-if-a-character-or-object-is-on-the-ground-using-raycasts
// https://assetstore.unity.com/packages/essentials/asset-packs/standard-assets-32351 -> Standard Assets -> 2D -> Scripts -> PlatformerCharacter2D.cs

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	public Text velocityX;
	public Text velocityY;

	public float speed = 5f;
	public float jumpForce = 275f;    
	public bool airControl;
	public LayerMask groundLayer;
	private bool jumpBool;
	private bool FacingRight = false;

	private Rigidbody2D rb2d;
	private Animator anim;

	void Start() {
		rb2d = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
	}

	private void Update() {
		if (!jumpBool) {
			jumpBool = Input.GetButtonDown("Jump");
		}

		if (rb2d.velocity.x != 0) {
			anim.SetBool ("Moving", true);
		} 
		else {
			anim.SetBool ("Moving", false);
		}

		anim.SetFloat ("Airborne", rb2d.velocity.y);
		anim.SetBool ("Grounded", isGrounded());

	}

	void FixedUpdate() {
		float moveHorizontal = Input.GetAxis ("Horizontal");

		if (moveHorizontal > 0 && !FacingRight) {
			Flip();
		}
		else if (moveHorizontal < 0 && FacingRight) {
			Flip();
		}

		if (isGrounded () || airControl) {
			rb2d.velocity = new Vector2 (moveHorizontal * speed, rb2d.velocity.y);
		}

		if (isGrounded () && jumpBool) {		

			if (jumpBool) {
				AudioSource audioSource = gameObject.GetComponent<AudioSource>();
				audioSource.PlayOneShot(audioSource.clip);
				rb2d.AddForce (new Vector2 (0f, jumpForce));
			}
		}

		jumpBool = false;
	}

	bool isGrounded() {
		Vector2 position = transform.position;
		Vector2 direction = Vector2.down;
		float distance = 1.3f;

		//Debug.DrawRay(position, direction, Color.green);
		RaycastHit2D hit = Physics2D.Raycast(position, direction, distance, groundLayer);
		if (hit.collider != null) {
			return true;
		}

		return false;
	}

	private void Flip()	{
		FacingRight = !FacingRight;

		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
			
}