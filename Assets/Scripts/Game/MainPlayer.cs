using UnityEngine;
using System.Collections;

public class MainPlayer : MonoBehaviour {
    CharacterController m_player;       // 主角
    public float m_maxSpeed;            // 移动速度
    public float m_maxRotateSpeed;      // 旋转速度
    private Animator m_animator;        // 主角动作
    public bool m_faceRight;     // 默认朝向
    //private Rigidbody m_Rigidbody;

    private Vector3 m_moveDirection = Vector3.zero;

    // Use this for initialization
    void Start()
    {
        m_player = GetComponent<CharacterController>();
        m_animator = GetComponent<Animator>();      // 获得当前角色的动作组
        //m_Rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizon = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        PlayerMove(horizon, vertical);
        // 把当前速度传给动作控制器
        m_animator.SetFloat("curSpeed", m_player.velocity.magnitude);
    }

    private void FixedUpdate()
    {
        
    }

    //Move(速度, 方向[0水平x轴，1垂直y轴])
    //public void Move(float move, int direction)
    //{
    //    //only control the player if grounded or airControl is turned on
    //    if (true)       //这里以后加限制条件，如被钩、被控的时候无法移动
    //    {
    //        // Move the character	按键时赋予一个新的速度向量
    //        //m_Rigidbody.velocity = new Vector3
    //        switch (direction)
    //        {
    //            case 0:     //水平方向加速度
    //                m_Rigidbody.velocity = new Vector3(move * m_MaxSpeed, m_Rigidbody.velocity.y, m_Rigidbody.velocity.z);
    //                break;
    //            case 1:     //垂直方向加速度
    //                m_Rigidbody.velocity = new Vector3(m_Rigidbody.velocity.x, m_Rigidbody.velocity.y, move * m_MaxSpeed);
    //                break;
    //        }
    //    }
    //}

    private void PlayerMove(float horizon, float vertical)
    {
        if (horizon != 0 || vertical != 0)
        {
            m_moveDirection = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
            m_moveDirection = transform.TransformDirection(m_moveDirection);
            m_moveDirection *= m_maxSpeed;
            m_player.Move(m_moveDirection * Time.deltaTime);
        }

        if (horizon > 0 && !m_faceRight)
        {
            Flip();
        }
        else if (horizon < 0 && m_faceRight)
        {
            Flip();
        }
    }

    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_faceRight = !m_faceRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
