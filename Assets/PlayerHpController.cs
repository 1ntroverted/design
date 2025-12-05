using UnityEngine;
using UnityEngine.Rendering;

using UnityEngine.Rendering.Universal;

public class PlayerHpController : MonoBehaviour
{

    public float PlayerHp { get; private set; }
    [SerializeField] private float maxHp;
    [SerializeField] private float hpDisappearSpeed;

    [SerializeField] Volume volume;

    Vignette vignette;
    FilmGrain filmGrain;

    void Awake()
    {
        PlayerHp = maxHp;
        vignette = GetVolume<Vignette>();
        filmGrain = GetVolume<FilmGrain>();
    }




    // Update is called once per frame
    void Update()
    {
        PlayerHp -= Time.deltaTime * hpDisappearSpeed;
        
        PlayerHp = Mathf.Clamp(PlayerHp, 0, maxHp);
        vignette.intensity.value = Mathf.Clamp01(1 - (PlayerHp / maxHp));
        filmGrain.intensity.value = Mathf.Clamp01(1 - (PlayerHp / maxHp));
    }

    void RecoverHealth(float value)
    {
        PlayerHp += value;
    }
    
    T GetVolume<T>() where T : VolumeComponent
    {
        
        if (volume.profile.TryGet<T>(out var v))
        {
            return v;
        }
        else
        {

            Debug.LogError(typeof(T) + " not found in Volume Profile!");
            return null;
        }
    }
    
    
}
