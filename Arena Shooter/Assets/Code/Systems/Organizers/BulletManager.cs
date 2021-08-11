using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    
    #region Singleton
    private static BulletManager _instance;
    public static BulletManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<BulletManager>();
            return _instance;
        }

    }
    #endregion
}
