using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore.Text;
using static UnityEngine.RuleTile.TilingRuleOutput;

/*
 * Handles movement, detection and destruction of NPC characters in the battle scene.
 */
public static class BattleBehaviour
{
    [Tooltip("Returns the closest game character from a ListOfTargetCharacter of self object")]
    public static GameObject FindClosestEnemy(List<Character> ListOfTargetCharacter,GameObject SelfObject)
    {
        float closestDistance = Mathf.Infinity;

        GameObject targetMinion = null;

        // this foreach loop checks enemy distances and then selects closest.

        foreach (Character attacker in ListOfTargetCharacter)
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
}