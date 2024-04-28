using UnityEngine;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FightingBandit : MonoBehaviour
{

	[SerializeField] float m_speed = 4.0f;
	[SerializeField] float m_jumpForce = 7.5f;

	private bool diedP = false;
	public EnemyFight enemyFight;
	public GameObject enemy;
	public float attackRange = 0.9f;

	public Image playerHeart;

	public Animator m_animator;
	private Rigidbody2D m_body2d;
	private Sensor_Bandit m_groundSensor;

	private bool m_grounded = false;
	private bool m_combatIdle = false;
	private bool m_isDead = false;

	public int water;
	public int food;
	public int health = 100;

	[SerializeField] public TMP_Text Text_water;

	// Use this for initialization
	void Start()
	{
		m_animator = GetComponent<Animator>();
		m_body2d = GetComponent<Rigidbody2D>();
		m_groundSensor = transform.Find("GroundSensor").GetComponent<Sensor_Bandit>();
		water = 100;
		food = 100;
		health = 100;
		//Text_water = new TextMeshPro();
	}

	// Update is called once per frame
	void Update()
	{
		playerHeart.fillAmount = (float)health / 100f;

		if (health <= 0)
		{
			diedP = true;
			m_animator.SetTrigger("Death");
		}
		if(diedP)
		{
			SceneManager.LoadScene(0);
		}
		if (!diedP)
		{


			//Check if character just landed on the ground
			if (!m_grounded && m_groundSensor.State())
			{
				m_grounded = true;
				m_animator.SetBool("Grounded", m_grounded);
			}

			//Check if character just started falling
			if (m_grounded && !m_groundSensor.State())
			{
				m_grounded = false;
				m_animator.SetBool("Grounded", m_grounded);
			}

			// -- Handle input and movement --
			float inputX = Input.GetAxis("Horizontal");

			// Swap direction of sprite depending on walk direction
			if (inputX > 0)
			{
				transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
			}
			else if (inputX < 0)
				transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

			// Move
			m_body2d.velocity = new Vector2(inputX * m_speed, m_body2d.velocity.y);

			//Set AirSpeed in animator
			m_animator.SetFloat("AirSpeed", m_body2d.velocity.y);

			// -- Handle Animations --
			//Death
			/*if (Input.GetKeyDown("e")) {
				if(!m_isDead)
					m_animator.SetTrigger("Death");
				else
					m_animator.SetTrigger("Recover");

				m_isDead = !m_isDead;
			}

			//Hurt
			else if (Input.GetKeyDown("q"))
				m_animator.SetTrigger("Hurt");
			*/
			//Attack
			if (Input.GetMouseButtonDown(0))
			{
				m_animator.SetTrigger("Attack");

			}

			//Change between idle and combat idle
			//else if (Input.GetKeyDown("f"))
			//   m_combatIdle = !m_combatIdle;

			//Jump
			else if (Input.GetKeyDown("space") && m_grounded)
			{
				m_animator.SetTrigger("Jump");
				m_grounded = false;
				m_animator.SetBool("Grounded", m_grounded);
				m_body2d.velocity = new Vector2(m_body2d.velocity.x, m_jumpForce);
				m_groundSensor.Disable(0.2f);
			}

			//Run
			else if (Mathf.Abs(inputX) > Mathf.Epsilon)
				m_animator.SetInteger("AnimState", 2);

			//Combat Idle
			else if (m_combatIdle)
				m_animator.SetInteger("AnimState", 1);

			//Idle
			else
				m_animator.SetInteger("AnimState", 0);

		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Checkpoint"))
		{
			food = food - 10;
			water = water - 10;
			Destroy(collision.gameObject);
			Debug.Log("water :" + water + "\nfood :" + food);
			updateText();
		}
	}


	void updateText()
	{
		//Text_water.SetText(water.ToString());
		Text_water.text = water.ToString();

	}



	public void DamageReciveEnemy()
	{
		if (Vector2.Distance(transform.position, enemy.transform.position) <= attackRange)
		{
			enemyFight.m_animator.SetTrigger("Hurt");
			enemyFight.healthEnemy -= 10; // Her seferinde 10 hasar alacak, deðiþtirebilirsinizs
		}
			
	}
}

