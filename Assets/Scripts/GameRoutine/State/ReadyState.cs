using System.Collections;
using Engine;
using UnityEngine;

public class ReadyState : GameCycleBaseState
{
    private bool _isReady = false;

    public override void OnEnter()
    {
        base.OnEnter();
        
        GameManager.Instance.StartCoroutine(ReadyDelayRoutine());
    }

    public override void OnExit()
    {
        base.OnExit();
        
        _isReady = false;
    }

    private IEnumerator ReadyDelayRoutine()
    {
        yield return new CustomWaitForSec(3f, TimeType.Default);
        _isReady = true;
    }
    
    public FuncPredicate IsReady()
    {
        return new FuncPredicate(() => _isReady);
    }
}
