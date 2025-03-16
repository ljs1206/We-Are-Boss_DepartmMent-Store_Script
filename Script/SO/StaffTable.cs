using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Staff/Table")]
public class StaffTableSO : ScriptableObject
{
    public List<StaffSO> staffList = new();
}
