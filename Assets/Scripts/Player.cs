using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    int _lifeCount;
    [SerializeField] GUISkin basicSkin;
    public int LifeCount
    {
        get
        {
            return _lifeCount;
        }
    }
    void Awake()
    {
        _lifeCount = 10;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnGUI()
    {
        GUI.skin = basicSkin;
        GUI.Box(new Rect(0, 0, 100, 50), _lifeCount.ToString());
    }
}
