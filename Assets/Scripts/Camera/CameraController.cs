using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CameraController : MonoBehaviour
{
    public LayerMask normalMask;
    public LayerMask shadowMask;
    public UnityEvent<Filter> OnChangedFilter;
    private Camera camera;
    [SerializeField] private AudioClip swapSfx;
    
    public void Init()
    {
        camera = Camera.main;
        HideMask("ShadowRealm");
        GameManager.Instance.currentFilter = Filter.Normal;
    }

    public void ShowMask(string _layerName)
    {
        camera.cullingMask |= 1 << LayerMask.NameToLayer(_layerName);
    }
    
    public void HideMask(string _layerName)
    {
        camera.cullingMask &=  ~(1 << LayerMask.NameToLayer(_layerName));
    }
    
    public void SwapFilter()
    {
        GameManager.Instance.currentFilter = GameManager.Instance.currentFilter == Filter.Normal ? Filter.Ghost : Filter.Normal;
        
        SoundManager.Instance.PlaySFX(swapSfx);
        
        camera.cullingMask ^= 1 << LayerMask.NameToLayer("NormalRealm");
        camera.cullingMask ^= 1 << LayerMask.NameToLayer("ShadowRealm");
        
        OnChangedFilter?.Invoke(GameManager.Instance.currentFilter);
    }

    public void FilterOn()
    {
        GameManager.Instance.currentFilter = Filter.Ghost;
        
        ShowMask("NormalRealm");
        HideMask("ShadowRealm");
    }

    public void FilterOff()
    {
        GameManager.Instance.currentFilter = Filter.Normal;
        
        ShowMask("ShadowRealm");
        HideMask("NormalRealm");
    }
}
