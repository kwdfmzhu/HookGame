using UnityEngine;
using System.Collections;

public class MainPlayer : MonoBehaviour {
    CharacterController m_player;       // 主角

    public float m_moveSpeed;           // 移动速度

    // Use this for initialization
    void Start () {
        m_player = GetComponent<CharacterController>();
    }
	
	// Update is called once per frame
	void Update () {
        //向上  
        if (Input.GetAxis("Vertical") > 0)
        {
            m_player.transform.Translate(Vector3.up * Time.deltaTime * m_moveSpeed);
        }
        //向下  
        if (Input.GetAxis("Vertical") < 0)
        {
            m_player.transform.Translate(Vector3.down * Time.deltaTime * m_moveSpeed);
        }
        //向左  
        if (Input.GetAxis("Horizontal") > 0)
        {
            m_player.transform.Translate(Vector3.right * Time.deltaTime * m_moveSpeed);
        }
        //向右  
        if (Input.GetAxis("Horizontal") < 0)
        {
            m_player.transform.Translate(Vector3.left * Time.deltaTime * m_moveSpeed);
        }
    }
}
