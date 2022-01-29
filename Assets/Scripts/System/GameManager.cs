using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Filter{ Normal, Ghost}
public class GameManager : Singleton<GameManager>
{
    public Filter currentFilter;
    
    [SerializeField] private PlayerController player;

    public PlayerController Player => player;

    public override void Init()
    {
        base.Init();
        player = FindObjectOfType<PlayerController>();
        
    }

    
}
