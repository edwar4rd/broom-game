using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using PlayerStats;
using System.Linq;
using Unity.VisualScripting;

public enum WaterZonePhysicsVer {
	Constant,
	ExponentialDecay,
	ExponentialDecayToHalfOriginal,
	ExponentialDecayToThirdOfFullSpeed,
}

public class PlayerManager : MonoBehaviour {
	private Rigidbody2D rb2d;
	public Rigidbody2D broomRigidBody;
	public GameObject pausePanel;


	public Vector2 startingPos;
	[Range(0, 2)] static public int roleID_Now = 0;
	public float moveSpeed = 8f;
	public float torquePower = 6f;
	public bool facingRight = true;

	[Header("跳躍設定")]
	public float jumpForce = 300.0f;
	public Transform footPoint;
	public Transform footPoint2;
	public bool touchGround = true;
	private bool canJump = true;
	private Coroutine cor_canJump_dead;
	public string littleRedName;
	private bool failed = false;
	private float velocity = 0f;
	public bool alternativeJumpTest;

	[Header("水坑設定")]

	public WaterZonePhysicsVer waterZonePhysicsVer;
	public Sprite normalShoeSprite;
	public Sprite waterZoneShoeSprite;

	private bool touchingWater = false;
	private float enterWaterVelocity = 0f;
	// private uint enterWaterFrameCount = 0;

	[Header("效果設定")]
	public GameObject fallingLeftParticleSystem;
	public GameObject fallingRightParticleSystem;
	public GameObject particleSystem2ForChar;
	public GameObject clockwiseRotateIndicator;
	public GameObject counterCWRotateIndicator;

	public bool isFinalLevel = false;


	private bool restartEnabled = false;

	void Start() {
		rb2d = GetComponent<Rigidbody2D>();
		broomRigidBody.simulated = true;
		failed = false;
		restartEnabled = false;
		// Time.timeScale = 1;

		while (SceneManager.GetActiveScene().buildIndex >= GameStats.startTimeStamps.Count) {
			GameStats.startTimeStamps.Add(Time.time);
		}
		while (SceneManager.GetActiveScene().buildIndex >= GameStats.restartTimeStamps.Count) {
			GameStats.restartTimeStamps.Add(0);
		}
		GameStats.restartTimeStamps[SceneManager.GetActiveScene().buildIndex] = Time.time;
	}

	void Update() {
		checkRestart();
		Jump();
		righting();
		particles();
		indicators();
	}
	void FixedUpdate() {
		Movement();
		checkDeath();
	}

	private void Movement() {


		if (failed)
			return;

		velocity = rb2d.velocity.x;
		if (!touchingWater) {
			if (touchGround) {
				if ((Math.Sign(velocity) == Math.Sign(Input.GetAxis("Horizontal"))) || (Input.GetAxis("Horizontal") == 0)) {
					velocity = Math.Sign(velocity) * Math.Max(Math.Abs(velocity) * 0.87f, Math.Abs(moveSpeed * Input.GetAxis("Horizontal") * 0.85f));
				}
				else
					velocity = moveSpeed * Input.GetAxis("Horizontal");
			}
			else {
				if ((Math.Sign(velocity) == Math.Sign(Input.GetAxis("Horizontal"))) || (Input.GetAxis("Horizontal") == 0)) {
					velocity = Math.Sign(velocity) * Math.Max(Math.Abs(velocity), Math.Abs(moveSpeed * Input.GetAxis("Horizontal") * 0.85f));
				}
				else
					velocity = moveSpeed * Input.GetAxis("Horizontal") * 0.85f;
			}
		}
		else {
			switch (waterZonePhysicsVer) {
				case WaterZonePhysicsVer.Constant:
				// WuaterZone v0: velocity stay as a constant in the WatenZone
				// velocity = velocity;
				break;
				case WaterZonePhysicsVer.ExponentialDecay:
				velocity = velocity * 0.97f;
				break;
				case WaterZonePhysicsVer.ExponentialDecayToHalfOriginal:
				velocity = velocity * 0.95f + enterWaterVelocity * 0.5f * 0.05f;
				break;
				case WaterZonePhysicsVer.ExponentialDecayToThirdOfFullSpeed:
				velocity = velocity * 0.95f + Math.Sign(enterWaterVelocity) * moveSpeed * 0.3333f * 0.05f;
				break;
			}
		}

		rb2d.velocity = new Vector2(velocity, rb2d.velocity.y);

		if (Input.GetAxis("Horizontal") != 0f) {
			if (!(facingRight ^ (Input.GetAxis("Horizontal") < 0f))) {
				Flip();
			}
		}
	}

	private void particles() {
		if (broomRigidBody.rotation < -30f) {
			fallingRightParticleSystem.SetActive(true);
			fallingLeftParticleSystem.SetActive(false);
		}
		else if (broomRigidBody.rotation > 30f) {
			fallingRightParticleSystem.SetActive(false);
			fallingLeftParticleSystem.SetActive(true);
		}
		else {
			fallingRightParticleSystem.SetActive(false);
			fallingLeftParticleSystem.SetActive(false);
		}
	}

	private void indicators() {
		// Debug.Log(broomRigidBody.angularVelocity);
		if (broomRigidBody.angularVelocity > 30f) {
			clockwiseRotateIndicator.SetActive(true);
			counterCWRotateIndicator.SetActive(false);
		}
		else if (broomRigidBody.angularVelocity < -30f) {
			clockwiseRotateIndicator.SetActive(false);
			counterCWRotateIndicator.SetActive(true);
		}
		else {
			clockwiseRotateIndicator.SetActive(false);
			counterCWRotateIndicator.SetActive(false);
		}
	}

	private void checkRestart() {
		if (restartEnabled) {
			if (Input.GetKeyDown("r") || Input.GetKeyDown("`")) {
				restartEnabled = false;
				restartScene();
			}
		}
		else {
			if (Time.timeSinceLevelLoad > 0.2) {
				restartEnabled = true;
			}
		}
	}

	//讓傾斜的掃帚回正
	private void righting() {
		if (Input.GetKey("d")) {
			broomRigidBody.AddTorque(torquePower);
		}
		if (Input.GetKey("f")) {
			broomRigidBody.AddTorque(-1 * torquePower);
		}

		if (Input.GetKey("g")) {
			if (broomRigidBody.rotation < 0f) {
				broomRigidBody.AddTorque(torquePower);
			}
			else if (broomRigidBody.rotation > 0f) {
				broomRigidBody.AddTorque(-1 * torquePower);
			}
		}

	}

	private void Flip() {
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	private void footPointCheck() {
		if (Physics2D.OverlapCircle(footPoint.position, 0.15f, LayerMask.GetMask("Ground & Wall") | LayerMask.GetMask("Platform")) == true ||
			Physics2D.OverlapCircle(footPoint2.position, 0.15f, LayerMask.GetMask("Ground & Wall") | LayerMask.GetMask("Platform")) == true) {
			touchGround = true;
		}
		else {
			touchGround = false;
		}

		//touchGround = Physics2D.OverlapCircle(footPoint.position, 0.15f, LayerMask.GetMask("Ground & Wall") | LayerMask.GetMask("Platform"));
		if (touchGround == true) {
			canJump = true;
			if (cor_canJump_dead != null)
				StopCoroutine(cor_canJump_dead);
		}
		else {
			if (cor_canJump_dead == null)
				cor_canJump_dead = StartCoroutine(canJump_dead());
		}
	}

	private void checkDeath() {
		footPointCheck();
		if (touchGround && Mathf.Abs(broomRigidBody.rotation) > 90f)
			failAndStop();
	}

	private IEnumerator canJump_dead() {
		yield return new WaitForSeconds(0.1f);
		canJump = false;
	}

	private void Jump() {
		footPointCheck();
		if (failed)
			return;

		if (Input.GetButtonDown("Jump") && canJump && touchGround) {
			canJump = false;
			if (alternativeJumpTest) {
				float jumpAngle = broomRigidBody.rotation / 180f * Mathf.PI + Mathf.PI / 2;
				rb2d.AddForce(new Vector2(Mathf.Cos(jumpAngle), Mathf.Sin(jumpAngle)) * jumpForce);
			}
			else {
				rb2d.AddForce(Vector2.up * jumpForce);
			}
		}
	}

	public void failAndStop() {
		Debug.Log("Player fail detected!");
		// TEST KEY: s = super, grants invincibility
		if (Input.GetKey("s"))
			return;
		GameStats.totalFail++;
		while (SceneManager.GetActiveScene().buildIndex >= GameStats.levelFail.Count) {
			GameStats.levelFail.Add(0);
		}
		GameStats.levelFail[SceneManager.GetActiveScene().buildIndex] += 1;
		pausePanel.SetActive(true);
		Time.timeScale = 0;
		Debug.Log("Player fail finished!");
	}

	public void restartScene() {
		if (isFinalLevel && GameObject.Find("Door").GetComponent<DoorManager>().isWin) {
			Debug.Log("Going back to menu...");
			SceneManager.LoadScene(0);
			return;
		}
		GameStats.totalRestart++;
		while (SceneManager.GetActiveScene().buildIndex >= GameStats.levelRestart.Count) {
			GameStats.levelRestart.Add(0);
		}
		GameStats.levelRestart[SceneManager.GetActiveScene().buildIndex] += 1;

		// string restartData = "Failed " + GameStats.totalFail + " time(s), restarted " + GameStats.totalRestart + " time(s).\n";
		// // Level0 => menu
		// for (int i = 1; i < GameStats.levelFail.Count; i++) {
		// 	restartData += "Failed Level " + i + " " + GameStats.levelFail[i] + " time(s) \n";
		// }
		// for (int i = 1; i < GameStats.levelRestart.Count; i++) {
		// 	restartData += "Restarted Level " + i + " " + GameStats.levelRestart[i] + " time(s) \n";
		// }
		// Debug.Log(restartData);

		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	public void TouchedByWater() {
		// Debug.Log("oh no water");
		if (!touchingWater) {
			touchingWater = true;
			enterWaterVelocity = velocity;
			// enterWaterFrameCount = 0;
		}
		this.GetComponent<SpriteRenderer>().sprite = waterZoneShoeSprite;
	}
	public void NolongerTouchedByWater() {
		// Debug.Log("yeah super dry");
		touchingWater = false;
		this.GetComponent<SpriteRenderer>().sprite = normalShoeSprite;
	}
}
