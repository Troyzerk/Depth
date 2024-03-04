using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewGoalDescriptor", menuName = "Questing/Goals/Goal Descriptor")]
public class GoalDiscriptor : ScriptableObject
{
    public GoalType type;
    public string goalName;
    public string goalDescription;
}
