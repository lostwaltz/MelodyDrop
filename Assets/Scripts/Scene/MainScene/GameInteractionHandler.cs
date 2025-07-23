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
        _selectedEntity = clickedEntity;
        
        Debug.Log($"Selected entity: {_selectedEntity}");
    }

    // private void OnPanelClicked(IInteractable interactable)
    // {
    //     if (interactable is not GlowFrame frame || _selectedEntity == null) return;
    //
    //     Vector3 targetPosition = frame.transform.position;
    //     
    //     _selectedEntity.transform.position = new Vector3(targetPosition.x, _selectedEntity.transform.position.y, targetPosition.z);
    //     
    //     //var result = JudgeMatch(_selectedCube, panel);
    //     
    //     // if (result)
    //     //     _selectedEntity.DropFastTo(panel);
    //     // else
    //     //     _selectedEntity.DropSlowTo(panel);
    //     
    //     _selectedEntity = null;
    // }
}
