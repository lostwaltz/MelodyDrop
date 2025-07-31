using Engine;
using UnityEngine;

public class GameInteractionHandler : Singleton<GameInteractionHandler>
{
    private TabEntity _selectedEntity;

    protected override void Awake()
    {
        base.Awake();
        
        InitializeSingleton();
    }

    public void SelectCube(TabEntity clickedEntity)
    {
        if (_selectedEntity != null)
        {
            SwapEntity(clickedEntity);
            return;
        }
        
        _selectedEntity = clickedEntity;

        if (!_selectedEntity.CheckMatch()) return;
        
        _selectedEntity.OnMatchSuccess();
        _selectedEntity = null;
    }

    private void SwapEntity(TabEntity clickedEntity)
    {
        (clickedEntity.Spawner, _selectedEntity.Spawner) = 
            (_selectedEntity.Spawner, clickedEntity.Spawner);
        
        (clickedEntity.transform.position, _selectedEntity.transform.position) = 
            (_selectedEntity.transform.position, clickedEntity.transform.position);

        if (clickedEntity.CheckMatch())
            clickedEntity.OnMatchSuccess();

        if (_selectedEntity.CheckMatch())
            _selectedEntity.OnMatchSuccess();
        
        _selectedEntity = null;
    }
}
