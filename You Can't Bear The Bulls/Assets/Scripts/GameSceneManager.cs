using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameSceneManager : MonoSingleton<GameSceneManager>
{ 
    List<BasicBull> ListOfBullLeft = new List<BasicBull>();
    List<BasicBull> ListOfBullRight = new List<BasicBull>();
    List<BasicBull> ListofAllBulls = new List<BasicBull>();

    Camera MainCamera;

    void Start()
    {
        MainCamera = Camera.main;
    }

    void Update()
    {
        GameUpdate();
    }
    
    void GameUpdate()
    {
        NormalGameUpdate();
    }

    void NormalGameUpdate()
    {
        ArrowKeysPressed CurrentKeyPressed = ControlsUpdate();
        List<BasicBull> TempList = null;
        bool? IfPressRight = null;
        switch (CurrentKeyPressed)
        {
            case ArrowKeysPressed.KEYS_LEFT:
                TempList = ListOfBullLeft;
                IfPressRight = false;
                break;
            case ArrowKeysPressed.KEYS_RIGHT:
                TempList = ListOfBullRight;
                IfPressRight = true;
                break;
        }

        if (TempList == null)
        {
            if (IfPressRight != null)
            {
                Player_Bear.Instance.SetBearMiss((bool)IfPressRight);
            }
        }
        else if (TempList.Count > 0)
        {
			BearPunchBull(TempList);
        }
    }

    void BearPunchBull(List<BasicBull> TempList)
    {
		BasicBull TempBull = TempList[0];

		Player_Bear.Instance.SetBearAttack(TempBull);

		switch(TempBull.BullType)
    	{
    		case BasicBull.TypeOfBull.BULL_BABY:
				BabyBull TempBabyBull = (BabyBull)TempBull;
    			TempBabyBull.GetHit();
    			break;
    	}
    }

    enum ArrowKeysPressed
    {
        KEYS_NONE,
        KEYS_LEFT,
        KEYS_RIGHT,
        KEYS_UP,
        KEYS_DOWN
    }

    ArrowKeysPressed ControlsUpdate()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
            return ArrowKeysPressed.KEYS_LEFT;

        else if (Input.GetKey(KeyCode.RightArrow))
            return ArrowKeysPressed.KEYS_LEFT;

        else if (Input.GetKey(KeyCode.UpArrow))
            return ArrowKeysPressed.KEYS_UP;

        else if (Input.GetKey(KeyCode.DownArrow))
            return ArrowKeysPressed.KEYS_DOWN;

        else
            return ArrowKeysPressed.KEYS_NONE;
    }

    public void AddBullInsideList(BasicBull EnteringBull)
    {
        if (!EnteringBull.IfFacingRight)
            ListOfBullRight.Add(EnteringBull);
        else
            ListOfBullLeft.Add(EnteringBull);
    }

    public void BullGetPunched(BasicBull TargetBull)
    {
		List<BasicBull> TempList = null;
		if(TargetBull.IfFacingRight)
           	 TempList = ListOfBullLeft;
        else
            TempList = ListOfBullRight;

        TempList.RemoveAt(0);

        Destroy(TargetBull.gameObject);
    }
}
