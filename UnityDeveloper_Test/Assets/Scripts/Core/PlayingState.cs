using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayingState : IGameState
{
	private readonly GameManager gameManager;

	public PlayingState(GameManager gameManager)
	{
		this.gameManager = gameManager;
	}

	public void Enter()
	{
		Debug.Log("Playing State Started.");
	}

	public void Update()
	{

	}

	public void Exit()
	{

	}
}
