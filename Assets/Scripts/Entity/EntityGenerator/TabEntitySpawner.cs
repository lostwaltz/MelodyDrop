using Engine;
using UnityEngine;

public class TabEntitySpawner : MonoBehaviour
{
    [SerializeField] private TabEntity tabEntity;
    
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
        
        CanSpawn = false;
        
        return true; 
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("TabEntity"))
            CanSpawn = true;
    }
}
