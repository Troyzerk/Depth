using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Job", menuName = "Content/NewJob")]
public class Job : ScriptableObject
{
    public string jobName; 
    public string jobDescription;
    public Ability autoAttack;
    public List<Ability> abilities;
    public int jobDamageModification;
    public int jobDefenceModification;
}