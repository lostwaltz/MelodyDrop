using System;
using UnityEngine;

public class TabEntitySpawner : MonoBehaviour
{
    [SerializeField] private TabEntity tabEntity;
    
    public bool CanSpawn {get; private set;} = true;
    
    public bool SpawnEntity(int entityKey, Color color)
    {
        if(false == CanSpawn) return false;
        
        TabEntity entity = Instantiate(tabEntity);
            
        entity.ChangeColor(color);
        
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
