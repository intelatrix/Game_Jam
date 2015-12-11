using UnityEngine;
using System.Collections;


public class Player_Bear : MonoSingleton<Player_Bear>
{

    BasicBull TargetedBull = null;
    BearState CurrentBearState = BearState.BEAR_NONE;
    public float ConstLerpTime = 0.5f;
    float LerpTimeLeft = 0;

    enum BearState
    {
        BEAR_NONE,
        BEAR_ATTACK,
        BEAR_MISS,
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        BearUpdate();
    }

    void BearUpdate()
    {
        switch (CurrentBearState)
        {
            case BearState.BEAR_ATTACK:
                BearAttackUpdate();
                break;
        }
    }

    void BearAttackUpdate()
    {
    	LerpTimeLeft += TimeManager.Instance.GetGameDeltaTime();
		transform.position = Vector3.Lerp(new Vector3(transform.position.x,transform.position.y,0), new Vector3(TargetedBull.transform.position.x,transform.position.y,0), LerpTimeLeft/ConstLerpTime);

		if(transform.position == new Vector3(TargetedBull.transform.position.x,transform.position.y,0))
    	{
    		CurrentBearState = BearState.BEAR_NONE;
    		GameSceneManager.Instance.BullGetPunched(TargetedBull);
    		TargetedBull = null;
    	}
    }

    public void SetBearAttack(BasicBull AttackThisBull)
    {
    	TargetedBull = AttackThisBull;
    	CurrentBearState = BearState.BEAR_ATTACK;
    	LerpTimeLeft = 0;
    }

    public void SetBearMiss(bool IfFacingRight)
    {
    }
}
