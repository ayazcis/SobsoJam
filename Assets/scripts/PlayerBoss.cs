using UnityEngine;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PlayerBoss : MonoBehaviour
{

	[SerializeField] float m_speed = 4.0f;
	[SerializeField] float m_jumpForce = 5.5f;

	private bool diedP = false;
	public BossEnemy enemyFight;
	public GameObject enemy;
	public float attackRange = 0.9f;

	public Image playerHeart;

	public Animator m_animator;
	private Rigidbody2D m_body2d;
	private Sensor_Bandit m_groundSensor;

	private bool m_grounded = false;
	private bool m_combatIdle = false;
	private bool m_isDead = false;
	public int health;
    private GameObject inventoryGO;
    private Inventory inventory;

    [SerializeField] public TMP_Text Text_water;
    [SerializeField] public TMP_Text Text_bread;
    [SerializeField] public TMP_Text Text_coin;

    // Use this for initialization
    void Start()
	{
		health = 100;
        inventoryGO = GameObject.Find("InventoryManager");
        inventory = inventoryGO.GetComponent<Inventory>();
        m_animator = GetComponent<Animator>();
		m_body2d = GetComponent<Rigidbody2D>();
		m_groundSensor = transform.Find("GroundSensor").GetComponent<Sensor_Bandit>();
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
		if (diedP)
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
            inventory.food = inventory.food - 10;
            inventory.water = inventory.water - 10;
			Destroy(collision.gameObject);
			Debug.Log("water :" + inventory.water + "\nfood :" + inventory.food);
			updateText();
		}
	}

	public void addGood()
	{
		inventory.food += 20;
		inventory.water += 20;
		inventory.gold += 20;
	}


    void updateText()
    {
        //Text_water.SetText(water.ToString());
        Text_water.text = inventory.water.ToString();
        Text_bread.text = inventory.food.ToString();
        Text_coin.text = inventory.gold.ToString();

    }



    public void DamageReciveEnemy()
	{
		if (Vector2.Distance(transform.position, enemy.transform.position) <= attackRange && enemyFight.talked)
		{
			enemyFight.m_animator.SetTrigger("Hurt");
			enemyFight.healthEnemy -= 10; // Her seferinde 10 hasar alacak, deðiþtirebilirsinizs
		}

	}
}

