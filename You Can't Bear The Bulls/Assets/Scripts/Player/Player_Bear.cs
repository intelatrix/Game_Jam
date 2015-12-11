using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player_Bear : MonoSingleton<Player_Bear>
{

    BasicBull TargetedBull = null;
    BearState CurrentBearState = BearState.BEAR_NONE;
    public float ConstLerpTime = 0.5f;
    float LerpTimeLeft = 0;

    public int ConstMaxCharge = 100;
    int CurrentCharge = 0;

    public Image ChargeBar;

    enum BearState
    {
        BEAR_NONE,
        BEAR_ATTACK_BABY,
		BEAR_ATTACK_MOTHER,
		BEAR_ATTACK_FATHER,
        BEAR_MISS
    }

    // Use this for initialization
    void Start()
    {
		ChargeBar.fillAmount = 0.5f;
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
			case BearState. BEAR_ATTACK_BABY:
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

    void IncreaseCharge(int AmountOfNewCharge)
    {
		CurrentCharge += AmountOfNewCharge;

    }

    public void SetBearAttackBaby(BasicBull AttackThisBull)
    {
    	TargetedBull = AttackThisBull;
    	CurrentBearState = BearState.BEAR_ATTACK_BABY;
    	LerpTimeLeft = 0;
    }

    public void SetBearMiss(bool IfFacingRight)
    {
    }
}
