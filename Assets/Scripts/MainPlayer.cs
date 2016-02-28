using UnityEngine;
using System.Collections;

public class MainPlayer : MonoBehaviour {
    CharacterController m_player;       // 主角
	public float m_MaxSpeed;           // 移动速度
	private Animator m_animator;		// 主角动作
	private Rigidbody m_Rigidbody;

    // Use this for initialization
    void Start () {
        m_player = GetComponent<CharacterController>();
		m_animator = GetComponent<Animator> ();		// 获得当前角色的动作组
		m_Rigidbody = GetComponent<Rigidbody>();
    }

	// Update is called once per frame
	void Update () {
        //向左/向右  
        if (Input.GetAxis("Horizontal") != 0) 
		{
			Move(Input.GetAxis("Horizontal"), 0);
        }
		//向上/向下  
		if (Input.GetAxis("Vertical") != 0) 
		{
			Move(Input.GetAxis("Vertical"), 1);
		}
		// 把当前速度传给动作控制器
		m_animator.SetFloat("curSpeed", m_Rigidbody.velocity.magnitude);
    }

	//Move(速度, 方向[0水平x轴，1垂直y轴])
	public void Move(float move, int direction)
	{
		//only control the player if grounded or airControl is turned on
		if (true)		//这里以后加限制条件，如被钩、被控的时候无法移动
		{
			// Move the character	按键时赋予一个新的速度向量
			//m_Rigidbody.velocity = new Vector3
			switch (direction) {
			case 0:		//水平方向加速度
				m_Rigidbody.velocity = new Vector3 (move * m_MaxSpeed, m_Rigidbody.velocity.y, m_Rigidbody.velocity.z);
				break;
			case 1:		//垂直方向加速度
				m_Rigidbody.velocity = new Vector3 (m_Rigidbody.velocity.x, m_Rigidbody.velocity.y, move * m_MaxSpeed);
				break;
			}
		}
	}
}
