using UnityEngine;
using System.Collections;

namespace Common
{
    public class CoroutineManager : MonoBehaviour
    {
        public static CoroutineManager m_instance;
        public static CoroutineManager instance
        {
            get
            {
                if (m_instance == null)
                {
                    GameObject go = new GameObject("CoroutineManager");
                    CoroutineManager script = go.AddComponent<CoroutineManager>();
                    m_instance = script;
                }
                return m_instance;
            }
        }

        void Start()
        {
            DontDestroyOnLoad(gameObject);
        }

        public void DoCoroutine(IEnumerator routine)
        {
            StartCoroutine(routine);
        }

        void OnDestroy()
        {
            m_instance = null;
        }
    }
}