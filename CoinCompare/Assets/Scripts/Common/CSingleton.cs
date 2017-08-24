using System;

public class CSingleton<TYPE>
{
    private static object m_lockObject;
    private static TYPE m_singleton;

    static CSingleton()
    {
        CSingleton<TYPE>.m_singleton = default(TYPE);
        CSingleton<TYPE>.m_lockObject = new object();
    }

    public static TYPE Singleton
    {
        get
        {
            if(CSingleton<TYPE>.m_singleton==null)
            {
                lock(CSingleton<TYPE>.m_lockObject)
                {
                    if(CSingleton<TYPE>.m_singleton==null)
                    {
                        CSingleton<TYPE>.m_singleton = (TYPE)Activator.CreateInstance(typeof(TYPE), true);
                    }
                }
            }
            return CSingleton<TYPE>.m_singleton;
        }
    }
}