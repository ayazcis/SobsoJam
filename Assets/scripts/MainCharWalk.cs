using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;

public class MainCharWalk : MonoBehaviour
{

    [SerializeField] float m_speed = 4.0f;
    [SerializeField] float m_jumpForce = 7.5f;

    private Animator m_animator;
    private Rigidbody2D m_body2d;
    private Sensor_Bandit m_groundSensor;
    private bool m_grounded = false;
    private bool m_combatIdle = false;
    private bool m_isDead = false;
    [SerializeField] Inventory inventory;
    public int activeSceneIndex;

    [SerializeField] public TMP_Text Text_water;
    [SerializeField] public TMP_Text Text_bread;
    [SerializeField] public TMP_Text Text_coin;

    // Use this for initialization
    void Start()
    {
        m_animator = GetComponent<Animator>();
        m_body2d = GetComponent<Rigidbody2D>();
        m_groundSensor = transform.Find("GroundSensor").GetComponent<Sensor_Bandit>();

        activeSceneIndex = SceneManager.GetActiveScene().buildIndex;
        //Text_water = new TextMeshPro();
    }

    // Update is called once per frame
    void Update()
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
            transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        else if (inputX < 0)
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

        // Move
        m_body2d.velocity = new Vector2(inputX * m_speed, m_body2d.velocity.y);

        //Set AirSpeed in animator
        m_animator.SetFloat("AirSpeed", m_body2d.velocity.y);

        if (Mathf.Abs(inputX) > Mathf.Epsilon)
            m_animator.SetInteger("AnimState", 2);

        //Combat Idle
        else if (m_combatIdle)
            m_animator.SetInteger("AnimState", 1);

        //Idle
        else
            m_animator.SetInteger("AnimState", 0);


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


    void updateText()
    {
        //Text_water.SetText(water.ToString());
        Text_water.text = inventory.water.ToString();
        Text_bread.text = inventory.food.ToString();
        Text_coin.text = inventory.gold.ToString();

    }

    public void updateGold()
    {
        if(activeSceneIndex == 1 && inventory.gold >= 20)
        {
            inventory.gold = inventory.gold - 20;
            inventory.food = inventory.food + 25;
            inventory.water = inventory.water + 25;
            Debug.Log("water :" + inventory.water + "\nfood :" + inventory.food + "gold :" + inventory.gold);
            updateText();
        }

        else if(activeSceneIndex == 2 && inventory.gold >= 25)
        {
            inventory.gold -= 25;
        }
    }
}
