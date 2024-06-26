using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;


public class EnemyFight : MonoBehaviour
{
	public float damageTime = 2f;
	public float detectionRadius = 5f;
	public float attackDistance = 1.6f;
	public float runToEnemySpeed = 4.5f;
	public GameObject player;
	public Image enemyHeart;

	public GameObject canvas;

	public FightingBandit fightingBandit;
	public int healthEnemy = 100;

	private bool attacking = false;
	private bool diedE=false;
	private bool attackState = false;
	[SerializeField] float m_speed = 4.0f;
	[SerializeField] float m_jumpForce = 5.5f;

	public Animator m_animator;
	private Rigidbody2D m_body2d;
	private Sensor_Bandit m_groundSensor;
	private bool m_grounded = false;
	private bool m_combatIdle = false;
	private bool m_isDead = false;
    private GameObject inventoryGO;
    private Inventory inventory;
    public int water;
	public int food;

	[SerializeField] public TMP_Text Text_water;

	// Use this for initialization
	void Start()
	{
		m_animator = GetComponent<Animator>();
		m_body2d = GetComponent<Rigidbody2D>();
		m_groundSensor = transform.Find("GroundSensor").GetComponent<Sensor_Bandit>();
        inventoryGO = GameObject.Find("InventoryManager");
        inventory = inventoryGO.GetComponent<Inventory>();
    }


    // Update is called once per frame
    void Update()
    {
		enemyHeart.fillAmount = (float)healthEnemy / 100f;
		if(enemyHeart.fillAmount == 0f)
		{
			canvas.SetActive(false);
		}
		if (healthEnemy <= 0)
		{
			diedE = true;
			m_animator.SetTrigger("Death");
			
			StartCoroutine( died());
			
		}

		if (Vector2.Distance(transform.position, player.transform.position) <= detectionRadius  && !diedE)
		{
			attackState = true;
		}
		else if(!diedE) 
		{
			attackState = false;
			m_animator.SetInteger("AnimState", 0);
			m_animator.SetBool("attackEnded", true);
			m_animator.SetBool("attacking", false);
			attacking = false;

		}
		if (attackState && !diedE)
		{
			// Move towards the enemy if in attack state and not within stopping distance
			if (Vector2.Distance(transform.position, player.transform.position) > attackDistance)
			{
				Vector2 direction = (player.transform.position - transform.position).normalized;
				
				if(direction.x < 0f)
				{
					transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
				}
				else
				{
					transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
				}
				
				transform.Translate(direction * runToEnemySpeed * Time.deltaTime);
				m_animator.SetInteger("AnimState", 2);
				m_animator.SetBool("attackEnded", true);
				m_animator.SetBool("attacking", false);
				attacking = false;

			}
			if (Vector2.Distance(transform.position, player.transform.position) < attackDistance )
			{
				m_animator.SetBool("attacking", true);
				m_animator.SetInteger("AnimState", 1);
				m_animator.SetBool("attackEnded", false);
				attacking = true;
			}

				
		}
	}
	public void DamageRecivePlayer()
	{
		if (Vector2.Distance(transform.position, player.transform.position) <= attackDistance)
		{
			fightingBandit.m_animator.SetTrigger("Hurt");
			fightingBandit.health -= 10; // Her seferinde 10 hasar alacak, değiştirebilirsiniz
			Debug.Log(fightingBandit.health + "  HEALTH ");
		}
			
		
	}
	IEnumerator died()
	{   
		yield return new WaitForSeconds(1f);
		Debug.Log("good: " + inventory.food + inventory.water + inventory.gold);
		addGood();
        Debug.Log("good: " + inventory.food + inventory.water + inventory.gold);

        Debug.Log("aaaaaa");
		SceneManager.LoadScene(3);
	}

    public void addGood()
    {
        inventory.food += 20;
        inventory.water += 20;
        inventory.gold += 20;
    }
}
