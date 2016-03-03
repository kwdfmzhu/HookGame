using UnityEngine;
using System.Collections;

public class MainPlayer : MonoBehaviour {
	CharacterController m_player;       // 主角
	public float m_maxSpeed;            // 移动速度
	public float m_maxRotateSpeed;      // 旋转速度
	private Animator m_animator;        // 主角动作
	public bool m_faceRight;     // 默认朝向
	public Vector3 log;
	public Vector3 hookAngle;
	private GameObject m_hook;        //屠夫的钩子
	public float m_hookRotateSpeed = 100.0f;
	public float m_hookTranslateSpeed;
	private int hookFaceFlag;        //记录钩子发出前的面向，正负标记，前后相乘可得钩子回来后会不会与屠夫当前面向不符
	private bool isHookRotate = false;        //钩子在旋转
	private bool isHookMove = false;        //钩子在移动
	private Quaternion targetRotation;
	//private Rigidbody m_Rigidbody;

	private Vector3 m_moveDirection = Vector3.zero;

	// Use this for initialization
	void Start()
	{
		m_player = GetComponent<CharacterController>();
		m_animator = GetComponent<Animator>();      // 获得当前角色的动作组
		//m_Rigidbody = GetComponent<Rigidbody>();
		m_hook = GameObject.Find("Hook");
	}

	// Update is called once per frame
	void Update()
	{
		float horizon = Input.GetAxis("Horizontal");
		float vertical = Input.GetAxis("Vertical");
		PlayerMove(horizon, vertical);
		// 把当前速度传给动作控制器
		m_animator.SetFloat("curSpeed", m_player.velocity.magnitude);

		//按空格扔钩子
		if (Input.GetButtonDown("Jump")) 
		{
			useHook();
		}

		hookRotate ();
		hookTranslate ();
		log = m_hook.transform.rotation.eulerAngles;
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
	//        // Move the character    按键时赋予一个新的速度向量
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

	//屠夫移动
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

	//屠夫转向
	private void Flip()
	{
		// Switch the way the player is labelled as facing.
		m_faceRight = !m_faceRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	//使用钩子
	private void useHook()
	{
		if (isHookMove || !isHookRotate) {
			pullHook ();
			return;
		}
		if (isHookRotate) {
			pushHook ();
			return;
		}
	}

	//旋转钩子
	private void hookRotate()
	{
		//按住鼠标左键旋转钩子
		/*
        float x;
        float y;
        if (Input.GetMouseButton(0)) {
            x = Input.GetAxis("Mouse X") * Time.deltaTime * m_hookRotateSpeed;
            y = Input.GetAxis("Mouse Y") * Time.deltaTime * m_hookRotateSpeed;

            log = log + "x:" + x + ",y:" + y;
        }
        else {
            x = y = 0;
        }
*/
		//m_hook.transform.Rotate (new Vector3 (0, 0, x));        //按鼠标增量转

		if(isHookRotate){
			m_hook.transform.Rotate(Vector3.back * Time.deltaTime * m_hookRotateSpeed);        //绕Y轴 旋转 
		}
	}

	//钩子朝当前方向移动
	private void hookTranslate()
	{
		if (isHookMove) {
			if (m_hook.transform.IsChildOf (m_player.transform)) {
				m_player.transform.DetachChildren ();        //解除父子关系
				m_hook.transform.localScale = new Vector3(0.6f,0.6f,0.6f);        //有时候大小会被变动，不知道啥原因，这里hard code掉
			}
			m_hook.transform.Translate (hookAngle * Time.deltaTime * m_hookTranslateSpeed);        //朝当前方向飞出去
		}
	}

	//扔出钩子
	private void pushHook()
	{
		//自定义角度
		//targetRotation = Quaternion.Euler(0,0,310.0f);
		// 直接设置旋转角度 
		//m_hook.transform.rotation = targetRotation;
		if (!isHookMove) {
			hookFaceFlag = (m_faceRight ? 1 : -1);        //记录扔出钩子时屠夫的朝向
			// 停止摇钩子，并让钩子开始移动 
			isHookRotate = false;
			isHookMove = true;
		}
		
	}

	//收回钩子开始转
	private void pullHook() {
		if (isHookMove || !isHookRotate) {
			m_hook.transform.parent = m_player.transform;
			m_hook.transform.localScale = new Vector3 (0.6f, 0.6f, 0.6f);        //有时候大小会被变动，不知道啥原因，这里hard code掉
			//让钩子的朝向与屠夫一致
			hookFaceFlag = hookFaceFlag * (m_faceRight ? 1 : -1);        //判断是否钩子离开前后屠夫转过向
			if (hookFaceFlag < 0) {        //改变钩子的朝向
				Vector3 theScale = m_hook.transform.localScale;
				theScale.x *= -1;
				m_hook.transform.localScale = theScale;
			}
			m_hook.transform.position = m_player.transform.position + new Vector3 (0.31f, 1f, -0.1f);        //控制钩子回来
			//开始摇
			isHookRotate = true;
			isHookMove = false;
		}
	}

	//按钮控制是否摇动钩子
	void OnGUI(){
		if(GUI.Button(new Rect(Screen.width - 110,10,100,50),"转起来【空格】")){        //钩回
			pullHook();
		}
		if(GUI.Button(new Rect(Screen.width - 110,70,100,50),"扔出去【空格】")){        //钩出
			pushHook();
		}
	}
}
