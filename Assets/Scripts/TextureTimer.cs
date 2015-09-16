using UnityEngine;
using System.Collections;

public class TextureTimer : MonoBehaviour
{
    public float scrollSpeed = 0.5F;
    public Renderer rend;
    public float timer = 0;
    public Vector2 offset;
    void Start()
    {
        rend = GetComponent<Renderer>();
        offset = Vector2.zero;
    }
    void Update()
    {
        timer++;
       if (timer >= 120)
       {
           offset.x = Random.Range(0.1f, 0.9f);
           offset.y = Random.Range(0.1f, 0.0f);
           timer = 0;
       }
        rend.material.mainTextureOffset = new Vector2(offset.x, offset.y);
        Debug.Log(timer);
        Debug.Log(offset);
    }
    //void FixedUpdate()
    //{
    //    rend.material.mainTextureOffset = new Vector2(offset.x, offset.y);
    //
    //}
}

//using UnityEngine;
//using System.Collections;
//
//public class TextureTimer : MonoBehaviour
//{
//    [SerializeField]
//    private Renderer rend;
//
//	void Start ()
//    {
//        m_Timer = 0;
//        m_RandomOffset = Vector2.zero;
//        rend = GetComponent<Renderer>();
//        //m_PropertyName = m_Renderer.material.GetTexture(0);
//	}
//
//	void Update ()
//    {
//        m_Timer++;
//        if (m_Timer > 100)
//        {
//            m_RandomOffset = new Vector2(Random.Range(0, 20), 
//                                         Random.Range(0, 20));
//            //rend.material.SetTextureOffset("_MainTex", m_RandomOffset);
//            rend.material.mainTextureOffset = new Vector2(m_RandomOffset.x, 
//                                                          m_RandomOffset.y);
//
//            Debug.Log(m_RandomOffset);
//            //Debug.Log(m_Timer);
//            m_Timer = 0;
//        }
//	}
//
//    // private variables
//    float m_Timer;
//    Vector2 m_RandomOffset;
//    string m_PropertyName;
//}
