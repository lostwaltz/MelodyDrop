using Engine;
using UnityEngine;

public class GameInteractionHandler : ManualMonoBehaviour
{
    private TabEntity _selectedEntity;

    public override void ManualAwake()
    {
        base.ManualAwake();
        
        ShortCut.Get<InteractionManager>().Subscribe(InteractionType.Cube, OnCubeClicked);
        ShortCut.Get<InteractionManager>().Subscribe(InteractionType.GlowPanel, OnPanelClicked);
    }
    private void OnCubeClicked(IInteractable interactable)
    {
        if (interactable is not TabEntity entity) return;
        
        _selectedEntity = entity;
        _selectedEntity.Interact();
        
        Debug.Log($"Selected entity: {_selectedEntity}");
    }

    private void OnPanelClicked(IInteractable interactable)
    {
        if (interactable is not GlowFrame frame || _selectedEntity == null) return;

        Vector3 targetPosition = frame.transform.position;
        
        _selectedEntity.transform.position = new Vector3(targetPosition.x, _selectedEntity.transform.position.y, targetPosition.z);
        
        //var result = JudgeMatch(_selectedCube, panel);
        
        // if (result)
        //     _selectedEntity.DropFastTo(panel);
        // else
        //     _selectedEntity.DropSlowTo(panel);
        
        _selectedEntity = null;
    }
}
