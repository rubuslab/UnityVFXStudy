using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class ShootFireball : MonoBehaviour
{
    private bool _initialized = false;
    public float m_fireballSpeed = 6.0f;

    public GameObject m_fireballTemplate = null;
    public GameObject m_target = null;

    private GameObject m_fireball = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_initialized && m_fireball)
            m_fireball.transform.Translate(m_fireballSpeed * Time.deltaTime, 0, 0);  // 沿着local x方向

        if (m_fireball)
        {
            float distance = (m_target.transform.position - m_fireball.transform.position).magnitude;
            if (distance < 0.1)
            {
                Destroy(m_fireball);
                m_fireball = null;
                _initialized = false;
            }
        }
        
    }

    private void OnGUI()
    {
        GUI.skin.FindStyle("button").fontSize = 40;
        GUILayout.Space(100);
        if (GUILayout.Button("Shoot a fireball"))
        {
            m_fireball = Instantiate(m_fireballTemplate);
            InitFireball(m_fireball, m_target.transform.position);
        }
    }

    private void InitFireball(GameObject fireball, Vector2 targetPos, float lifeTime = 0)
    {
        Vector2 direction = (targetPos - (Vector2)fireball.transform.position).normalized;

        //m_fireballTemplate.transform.position = startPos;
        //Invoke(nameof(DestroyBall), lifeTime);

        _initialized = true;

        float rotationZ = Mathf.Atan2(direction.y, direction.x);
        fireball.transform.rotation = Quaternion.Euler(0, 0, rotationZ * Mathf.Rad2Deg);
    }

    //private void DestroyBall()
    //{
    //    Destroy(m_fireball);
    //}
}
