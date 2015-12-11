﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameSceneManager : MonoSingleton<GameSceneManager>
{ 
    List<BasicBull> ListOfBullLeft = new List<BasicBull>();
    List<BasicBull> ListOfBullRight = new List<BasicBull>();
    List<BasicBull> ListofAllBulls = new List<BasicBull>();

    bool IfMiss = false;
    public float ConstMissTime = 1.0f;
  
    float MissTimeLeft = 0f;

    enum GameState
    {
    	GAME_NORMAL,
    	GAME_MOTHER_QTE,
    	GAME_FATHER_QTE
    };

    GameState CurrentState = GameState.GAME_NORMAL;

    void Start()
    {
  
    }

    void Update()
    {
        GameUpdate();
    }
    
    void GameUpdate()
    {
    	switch(CurrentState)
    	{
    	case GameState.GAME_NORMAL:
        	NormalGameUpdate();
        	break;
        case GameState.GAME_MOTHER_QTE:
        	break;
        case GameState.GAME_FATHER_QTE:
       		break;
        }
    }

    void NormalGameUpdate()
    {
	    if(!IfMiss)
	    {
	        ArrowKeysPressed CurrentKeyPressed = NormalControlsUpdate();
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
	           
	        }
	        else 
	        {
				if (TempList.Count > 0)
	       		{
					BearPunchBull(TempList);
		        }
		        else
		        {
	        		Player_Bear.Instance.SetBearMiss((bool)IfPressRight);
					MissTimeLeft =  ConstMissTime;
					IfMiss = true;
	       		}
	        }
        }
        else
        {
			MissTimeLeft -= TimeManager.Instance.GetGameDeltaTime();
			if(MissTimeLeft <=0)
				IfMiss = false;

			//Player_Bear.Instance.StopMissing();
        }
    }

    void MotherGameUpdate()
    {
	
    }

    ArrowKeysPressed MotherControlUpdate()
    {
		if (Input.GetKeyDown(KeyCode.LeftArrow))
            return ArrowKeysPressed.KEYS_LEFT;

        else if (Input.GetKeyDown(KeyCode.RightArrow))
            return ArrowKeysPressed.KEYS_RIGHT;

		else if (Input.GetKeyDown(KeyCode.UpArrow))
            return ArrowKeysPressed.KEYS_UP;

		else if (Input.GetKeyDown(KeyCode.DownArrow))
            return ArrowKeysPressed.KEYS_DOWN;

        else
            return ArrowKeysPressed.KEYS_NONE;
    }

    void BearPunchBull(List<BasicBull> TempList)
    {
		BasicBull TempBull = TempList[0];

		switch(TempBull.BullType)
    	{
    		case BasicBull.TypeOfBull.BULL_BABY:
				Player_Bear.Instance.SetBearAttackBaby(TempBull);
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

    ArrowKeysPressed NormalControlsUpdate()
    {
		if (Input.GetKeyDown(KeyCode.LeftArrow))
            return ArrowKeysPressed.KEYS_LEFT;

        else if (Input.GetKeyDown(KeyCode.RightArrow))
            return ArrowKeysPressed.KEYS_RIGHT;
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
