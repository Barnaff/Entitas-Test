﻿using UnityEngine;
using System.Collections;
using Entitas;
using System.Collections.Generic;
using NetherWars.Parsing;
using NetherWars;

public class GameplaySimulation : MonoBehaviour {

    const int LAYOUT_CARD_WIDTH = 100;

    private bool isSelectingTarget;

    private Entity attackingCard;

    private List<Entity> _validTargets;

    private string _inputString = "";

    private Interpertor _interpertor;

    void OnEnable()
    {
        _interpertor = new Interpertor();

        GameplayActions.RunTargetTest();
    }

    void OnGUI()
    {
        Entity currentPlayer = Pools.pool.activePlayerEntity;

        GUILayout.BeginVertical(); // 1

        GUILayout.BeginHorizontal("Box"); // 2

        if (currentPlayer != null)
        {
            GUILayout.Label("Current Player: " + currentPlayer.player.Id + " : " + currentPlayer.player.Name);
        }
        

        if (isSelectingTarget)
        {
            if (GUILayout.Button("Cancel Attack"))
            {
                isSelectingTarget = false;
                attackingCard = null;
                _validTargets.Clear();
            }
        }
        else
        {
            if (GUILayout.Button("End Turn"))
            {
                EndTurn();
            }
        }
      

        GUILayout.EndHorizontal(); // -2

        Entity[] players = Pools.pool.GetEntities(Matcher.Player);

        Entity[] cardsInHand = Pools.pool.GetEntities(Matcher.AllOf(Matcher.Hand, Matcher.Card, Matcher.Controller));

        Entity[] cardsInBattlefield = Pools.pool.GetEntities(Matcher.AllOf(Matcher.Battlefield, Matcher.Card, Matcher.Controller));

        foreach (Entity playerEntity in players)
        {
            if (playerEntity == currentPlayer)
            {
                GUI.skin.label.fontStyle = FontStyle.Bold;
                GUI.color = Color.green;
            }

            GUILayout.BeginVertical("Box");  // 3

            GUILayout.BeginHorizontal();

            GUILayout.Label(playerEntity.player.Name + " Hand");

            if (playerEntity.hasDamage)
            {

                GUILayout.Label("health: " +  "/<color=red>" + GameplayActions.GetHealth(playerEntity) + "</color>");
            }
            else
            {
                GUILayout.Label("Health: "  + GameplayActions.GetHealth(playerEntity));
            }

            GUILayout.Label("Mana: " + playerEntity.manaPool.CurrentMana + "/" + playerEntity.manaPool.MaxMana);

            if (_validTargets != null && _validTargets.Contains(playerEntity))
            {
                if (GUILayout.Button("Target"))
                {
                    SelectAttackingTarget(playerEntity);
                }
            }

            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal(); // 4

            for (int i = 0; i < cardsInHand.Length; i++)
            {
                if (cardsInHand[i].controller.Id == playerEntity.player.Id)
                {
                    if (cardsInHand[i].isPlayable)
                    {
                        GUI.color = Color.green;
                    }
                    else
                    {
                        GUI.color = Color.white;
                    }

                    GUILayout.BeginVertical("Box", GUILayout.Width(LAYOUT_CARD_WIDTH));

                    GUILayout.Label(cardsInHand[i].card.CardName);

                    GUILayout.Label("Cost " + cardsInHand[i].manaCost.Value);

                    GUILayout.Label(cardsInHand[i].strength.Value + "/" + cardsInHand[i].health.Value);

                    if (currentPlayer == playerEntity)
                    {
                        if (GUILayout.Button("Cast"))
                        {
                            CastCard(cardsInHand[i]);
                        }

                        if (GUILayout.Button("Resource"))
                        {
                            PlayAsResource(cardsInHand[i]);
                        }
                    }

                    GUILayout.EndVertical();
                }

                GUI.skin.label.fontStyle = FontStyle.Normal;
                GUI.color = Color.white;
            }

            GUILayout.EndHorizontal(); // -4

            GUILayout.BeginHorizontal(); // 5

            for (int i = 0; i < cardsInBattlefield.Length; i++)
            {
                if (cardsInBattlefield[i].controller.Id == playerEntity.player.Id)
                {
                    if (cardsInBattlefield[i].isSummoningSickness)
                    {
                        GUI.color = Color.magenta;
                    }
                    else
                    {
                        GUI.color = Color.white;
                    }

                    GUILayout.BeginVertical("Box" , GUILayout.Width(LAYOUT_CARD_WIDTH));

                    GUILayout.Label(cardsInBattlefield[i].card.CardName);

                    if (cardsInBattlefield[i].hasDamage)
                    {
                        
                        GUILayout.Label(cardsInBattlefield[i].strength.Value + "/<color=red>" + GameplayActions.GetHealth(cardsInBattlefield[i]) + "</color>");
                    }
                    else
                    {
                        GUILayout.Label(cardsInBattlefield[i].strength.Value + "/" + GameplayActions.GetHealth(cardsInBattlefield[i]));
                    }

                    

                    if (cardsInBattlefield[i].controller.Id == playerEntity.player.Id && currentPlayer == playerEntity)
                    {
                        if (!cardsInBattlefield[i].isSummoningSickness)
                        {
                            if (GUILayout.Button("Attack"))
                            {
                                AttackWithCard(cardsInBattlefield[i]);
                            }
                        }
                    }

                    if (_validTargets != null && _validTargets.Contains(cardsInBattlefield[i]))
                    {
                        if (GUILayout.Button("Target"))
                        {
                            SelectAttackingTarget(cardsInBattlefield[i]);
                        }
                    }


                    GUILayout.EndVertical();

                }
            }

            GUILayout.EndHorizontal();  // -5

            GUILayout.EndVertical();  // -3

        }


        // Interperter

        GUILayout.BeginVertical("Box");

        _inputString = GUILayout.TextArea(_inputString);

        if (GUILayout.Button("Execute"))
        {
            ExecuteString(_inputString);
        }

        GUILayout.EndHorizontal();


        GUILayout.EndVertical(); // -1

    }


    #region Actions

    private void EndTurn()
    {
        Pools.pool.ReplaceTurnPhase(NetherWars.TurnPhase.eTurnPhase.End);
    }

    private void PlayAsResource(Entity card)
    {
        if (Pools.pool.activePlayerEntity.isPlayedResource)
        {
            Debug.LogWarning("Player already played resource this turn!");
        }
        else
        {
            card.isHand = false;
            card.isResource = true;

            Pools.pool.activePlayerEntity.isPlayedResource = true;
        }
    }

    private void CastCard(Entity card)
    {
        if (card.isPlayable)
        {
            GameplayActions.ChangeCardZone(card, eZoneType.Battlefield);

            card.isPlayable = false;

            Entity player = Pools.pool.activePlayerEntity;

            player.ReplaceManaPool(player.manaPool.CurrentMana - card.manaCost.Value, player.manaPool.MaxMana);
        }
    }

    private void AttackWithCard(Entity card)
    {
        if (!card.isSummoningSickness && !card.isTapped)
        {
            attackingCard = card;
            isSelectingTarget = true;

            _validTargets = GameplayActions.GeValidTargetsForAttacking(attackingCard);
        }
        else
        {
            Debug.LogWarning("This card cannot attack now!");
        }
    }

    private void SelectAttackingTarget(Entity card)
    {
        isSelectingTarget = false;

        ResolveCombat(attackingCard, card);
    }

    private void ResolveCombat(Entity attacker, Entity target)
    {
        Debug.Log(attacker + " attacks " + target);

        if (attacker.hasStrength)
        {
            if (target.hasDealDamage)
            {
                target.ReplaceDealDamage(attacker.strength.Value, true, attacker);
            }
            else
            {
                target.AddDealDamage(attacker.strength.Value, true, attacker);
            }
        }
       

        if (target.hasStrength)
        {
            if (attacker.hasDamage)
            {
                attacker.ReplaceDealDamage(target.strength.Value, true, target);
            }
            else
            {
                attacker.AddDealDamage(target.strength.Value, true, target);
            }
        }
       


        attacker.isTapped = true;

        attackingCard = null;

        _validTargets.Clear();
    }

    #endregion



    #region Processor

    private void ExecuteString(string inputString)
    {
        _interpertor.Execute(inputString);


    }

    #endregion
}
