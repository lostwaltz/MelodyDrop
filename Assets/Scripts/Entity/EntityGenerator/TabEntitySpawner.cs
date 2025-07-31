using Engine;
using UnityEngine;
using UnityEngine.Serialization;

public class TabEntitySpawner : MonoBehaviour
{
    [SerializeField] private TabEntity tabEntity;
    [SerializeField] private GlowFrame glowFrame;
    
    public GlowFrame GlowFrame => glowFrame;

    public bool CanSpawn {get; private set;} = true;
    
    public bool SpawnEntity(int entityKey, int colorKey)
    {
        if(false == CanSpawn) return false;
        
        TabEntity entity = Instantiate(tabEntity);
        
        ShortCut.Get<DataManager>().ColorDataMap.TryGetValue(colorKey, out ColorData colorData);

        if (colorData != null) 
            entity.ChangeColor(colorData);

        entity.gameObject.transform.position = transform.position;
            
        entity.gameObject.SetActive(true);
        
        entity.Spawner = this;
        
        CanSpawn = false;
        
        return true; 
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("TabEntity"))
            CanSpawn = true;
    }
}
