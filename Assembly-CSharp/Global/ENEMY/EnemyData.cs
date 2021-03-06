﻿using System;
using UnityEngine;

public class EnemyData : MonoBehaviour
{
	private void Awake()
	{
		this.Load();
		EnemyData.Main = this;
	}

	private void Load()
	{
		TextAsset textAsset = Resources.Load(this.stageDataPath, typeof(TextAsset)) as TextAsset;
		Byte[] bytes = textAsset.bytes;
		Int32 miniGameArg = (Int32)FF9StateSystem.Common.FF9.miniGameArg;
		Char c = (Char)bytes[miniGameArg * 2];
		Int32 num = (Int32)c;
		c = (Char)bytes[miniGameArg * 2 + 1];
		Int32 wise = (Int32)c;
		this.cardLevel = num;
		this.Wise = wise;
		TextAsset textAsset2 = Resources.Load(this.cardLevelDataPath, typeof(TextAsset)) as TextAsset;
		this.enemyData = textAsset2.bytes;
	}

	public Int32 GetCardID()
	{
		Int32 num = UnityEngine.Random.Range(0, 256);
		Int32 num2 = 15;
		for (Int32 i = (Int32)this.probability.Length - 1; i >= 0; i--)
		{
			if (num > this.probability[i])
			{
				break;
			}
			num2 = i;
		}
		return (Int32)this.enemyData[this.cardLevel * 16 + num2];
	}

	public void Initialize(Hand hand)
	{
		hand.Clear();
		for (Int32 i = 0; i < 5; i++)
		{
			QuadMistCard quadMistCard = CardPool.CreateQuadMistCard(this.GetCardID());
			quadMistCard.side = 1;
			hand.Add(quadMistCard);
		}
	}

	public static void Setup(Hand hand)
	{
		EnemyData.Main.Initialize(hand);
	}

	public static void RestorePlayerLostCard(Hand hand, Int32 cardArrayIndex, QuadMistCard lostCard)
	{
		hand.ReplaceCard(cardArrayIndex, lostCard);
	}

	public static EnemyData Main;

	public Int32 Wise;

	private String cardLevelDataPath = "EmbeddedAsset/QuadMist/MINIGAME_CARD_LEVEL_ADDRESS";

	private String stageDataPath = "EmbeddedAsset/QuadMist/MINIGAME_STAGE_ADDRESS";

	private Int32 cardLevel;

	private Int32[] probability = new Int32[]
	{
		20,
		40,
		60,
		80,
		100,
		120,
		140,
		160,
		180,
		200,
		220,
		240,
		252,
		254,
		255
	};

	private Byte[] enemyData;
}
