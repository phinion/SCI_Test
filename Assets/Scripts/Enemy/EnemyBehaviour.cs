using System.Collections;
using UnityEngine;

public abstract class EnemyBehaviour: ScriptableObject
{
    protected EnemyCharacter self;

    protected EnemyCharacter.WalkOffDelegate walkOffDelegate;

    public virtual void Setup(EnemyCharacter _self)
    {
        self = _self;
    }
    
    public void SetWalkOffDelegate(EnemyCharacter.WalkOffDelegate walkOffDel)
    {
        walkOffDelegate = walkOffDel;
    }

    public abstract void AIBehaviour();
    public abstract IEnumerator GeneralAILoop();
}
