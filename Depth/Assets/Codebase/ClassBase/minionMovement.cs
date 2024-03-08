using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore.Text;
using static UnityEngine.RuleTile.TilingRuleOutput;

/*
 * Handles movement, detection and destruction of NPC characters in the battle scene.
 * 
 */
public static class BattleBehaviour
{
    [Tooltip("Returns the closest game character from a ListOfTargetCharacter of self object")]
    public static GameObject FriendlyToEnemy(GameObject SelfObject)
    {
        float closestDistance = Mathf.Infinity;
        List<Character> enemyMinionList = PersistentManager.instance.enemyParty.characters;

        GameObject targetMinion = null;

        // this foreach loop checks enemy distances and then selects closest.

        foreach (Character attacker in enemyMinionList)
        {
            GameObject attackerGameObject = GameObject.Find(attacker.name);
            

            if (attackerGameObject != null)
            {
                float enemyDistance = Vector2.Distance(attackerGameObject.transform.position, SelfObject.transform.position);


                if (enemyDistance < closestDistance)
                {
                    closestDistance = enemyDistance;      
                    targetMinion = GameObject.Find(attacker.name);
                }
            }

        }
        return targetMinion;
    }

    public static GameObject EnemyToFriendly(GameObject SelfObject)
    {
        float closestDistance = Mathf.Infinity;
        Character mainCharacter = PersistentManager.instance.playerCharacter;

        List<Character> playerMinionList = PlayerData.instance.playerParty.characters;
        if (!playerMinionList.Contains(mainCharacter))
        {
            playerMinionList.Add(mainCharacter);
        }

        GameObject targetMinion = null;

        // this foreach loop checks enemy distances and then selects closest.

        foreach (Character attacker in playerMinionList)
        {
            GameObject attackerGameObject = GameObject.Find(attacker.name);


            if (attackerGameObject != null)
            {
                float enemyDistance = Vector2.Distance(attackerGameObject.transform.position, SelfObject.transform.position);


                if (enemyDistance < closestDistance)
                {
                    closestDistance = enemyDistance;
                    targetMinion = GameObject.Find(attacker.name);
                }
            }

        }
        return targetMinion;
    }

    public static bool TheWinner()
    {
        List<Character> enemyMinionList = PersistentManager.instance.enemyParty.characters;
        List<Character> playerMinionList = PlayerData.instance.playerParty.characters;
        int deathCount = 0;

        foreach (Character attacker in playerMinionList)
        {
            if (attacker.status == CharacterStatus.Dead)
            {
                deathCount++;
                if (enemyMinionList.Count == deathCount)
                {
                    return true;
                }

            }
        }
        foreach (Character attacker in enemyMinionList)
        {
            if (attacker.status == CharacterStatus.Dead)
            {
                deathCount++;
                if (enemyMinionList.Count == deathCount)
                {
                    return true;
                }

            }
        }
        return false;
    }
}