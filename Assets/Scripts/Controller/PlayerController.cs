using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour 
//SakuraGames
{

	public float jumpHeight = 1000F;
	public float flyStrength = 30;
	public bool isFallin = false;
	public bool isGrounded = false;
	public Transform GroundCheck;

	private Animator playerAnimator;
	
	
	private float currentHeight;
	private float previousHeight;

	void Start(){
		playerAnimator = GetComponent<Animator> ();
	}

	void FixedUpdate(){

		ReadKey();
		if (isGrounded) {
			playerAnimator.SetTrigger("Run");
		}
	}
	
	void Update(){
		currentHeight =  this.transform.position.y;
		IsFallin ();
		IsGrounded ();
	}
	
	void LateUpdate() {
		previousHeight = currentHeight;
	}
	
	// Player status
	
	private void IsFallin(){
		if (currentHeight < previousHeight) {
			isFallin = true;
		} else if (currentHeight == previousHeight)
			isFallin = false;
		
	}

	void IsGrounded(){
		Debug.DrawLine (transform.position, GroundCheck.position, Color.magenta);
		// karakter yere değiyor mu 
		if (Physics2D.Linecast (transform.position, GroundCheck.position, 1 << LayerMask.NameToLayer ("Ground"))) {
			isGrounded = Physics2D.Linecast (transform.position, GroundCheck.position, 1 << LayerMask.NameToLayer ("Ground"));
		} else {
			isGrounded = false;
		}
	}

	// Button press action
	private void ReadKey(){
		if (Input.GetButton ("Fire1") 
		    && GameObject.Find("GamePlayController").GetComponent<GamePlayController>().gameRunning) {
			// karakter havada
			if (isGrounded && !isFallin){
				playerAnimator.SetTrigger("Jump");
				PlayerJump(jumpHeight);

			} 
			// karakter düşüyor
			if (!isGrounded && isFallin){
				GameObject.Find("FlapSound").GetComponent<FlapSound>().PlayFlapSound();
				playerAnimator.SetTrigger("Fly");
				PlayerFly(flyStrength);
			} 
		} 
	}
	
	// zıplama ve süzülme kodu
	private void PlayerFly(float fly_strength){
		this.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
		this.GetComponent<Rigidbody2D>().AddForce(transform.up * fly_strength, ForceMode2D.Force);
	}
	
	private void PlayerJump(float jump_height){
		this.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
		this.GetComponent<Rigidbody2D>().AddForce(transform.up * jump_height, ForceMode2D.Force);
	}

}
