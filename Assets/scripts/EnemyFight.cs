using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyFight : MonoBehaviour
{
	public float detectionRadius = 5f;
	public float attackDistance = 1.6f;
	public float runToEnemySpeed = 4.5f;
	public GameObject player;


	private bool attackState = false;
	[SerializeField] float m_speed = 4.0f;
	[SerializeField] float m_jumpForce = 7.5f;

	private Animator m_animator;
	private Rigidbody2D m_body2d;
	private Sensor_Bandit m_groundSensor;
	private bool m_grounded = false;
	private bool m_combatIdle = false;
	private bool m_isDead = false;

	public int water;
	public int food;
	public int health;

	[SerializeField] public TMP_Text Text_water;

	// Use this for initialization
	void Start()
	{
		m_animator = GetComponent<Animator>();
		m_body2d = GetComponent<Rigidbody2D>();
		m_groundSensor = transform.Find("GroundSensor").GetComponent<Sensor_Bandit>();
	
	}


    // Update is called once per frame
    void Update()
    {

		if (Vector2.Distance(transform.position, player.transform.position) <= detectionRadius)
		{
			attackState = true;
		}
		else
		{
			attackState = false;
			m_animator.SetInteger("AnimState", 0);
			m_animator.SetBool("attackEnded", true);
			m_animator.SetBool("attacking", false);

		}
		if (attackState)
		{
			// Move towards the enemy if in attack state and not within stopping distance
			if (Vector2.Distance(transform.position, player.transform.position) > attackDistance)
			{
				Vector2 direction = (player.transform.position - transform.position).normalized;
				transform.Translate(direction * runToEnemySpeed * Time.deltaTime);
				m_animator.SetInteger("AnimState", 2);
				m_animator.SetBool("attackEnded", true);
				m_animator.SetBool("attacking", false);

			}
			if (Vector2.Distance(transform.position, player.transform.position) < attackDistance)
			{
				m_animator.SetBool("attacking",true);
				m_animator.SetInteger("AnimState", 1);
				m_animator.SetBool("attackEnded", false);
			}
		}
	}
}
