using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtractChild : MonoBehaviour
{
    [SerializeField] bool trackChild;
    [SerializeField] List<GameObject> child;

    public List<GameObject> Child { get => child; set => child = value; }

    private void Awake()
    {
        if (trackChild)
        {
            child = new List<GameObject>();
            for(int i = transform.childCount-1; i >= 0; i--)
            {
                Transform t = transform.GetChild(i);
                Child.Add(t.gameObject);
                t.SetParent(null);
            }
        }
        else
        {
            for (int i = transform.childCount - 1; i >= 0; i--)
            {
                Transform t = transform.GetChild(i); 
                t.SetParent(null);
            }
        }
    }
}
