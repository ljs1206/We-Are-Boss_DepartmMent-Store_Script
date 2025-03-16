// using System.Linq;
// using Sirenix.OdinInspector.Editor;
// using Sirenix.Utilities;
// using Sirenix.Utilities.Editor;
// using UnityEditor;
// using UnityEngine;
// public class StaffGeneratorWindow : OdinMenuEditorWindow
// {
//     private readonly string _staffSODirectory = "Assets/00.Work/LJS/Editor/TestSO";
//     private readonly string _staffSOTableDirectory = "Assets/00.Work/LJS/Editor/TestSO/Table";
//     private Editor _cachedEditor;

//     [MenuItem("Generator/StaffWindow")]
//     public static void OpenWindow(){
//         var window = GetWindow<StaffGeneratorWindow>();
//         window.Show();
//         window.minSize = new Vector2(800f, 550f);
//         window.position = GUIHelper.GetEditorWindowRect().AlignCenter(800f, 550f);
//     }

//     protected override OdinMenuTree BuildMenuTree(){

//         // _staffSOTableDirectory = AssetDatabase.LoadAssetAtPath<>
//         var tree = new OdinMenuTree();
//         var itemList = tree.AddAllAssetsAtPath("Item Datas", _staffSODirectory, typeof(StaffSO))
//             .AddIcons<StaffSO>(x => x.StaffTextur);
        
//         tree.SortMenuItemsByName();
//         foreach(var item in itemList){
//             if(item == null || item.Value == null) continue;
//             item.Name = (item.Value as StaffSO).StaffName;
//         }

//         return tree;
//     }

//     protected override void OnBeginDrawEditors()
//     {
//         var selected = this.MenuTree.Selection.FirstOrDefault();
//         StaffSO selStaff = (selected.Value as StaffSO);
//         SirenixEditorGUI.BeginHorizontalToolbar();
//         {
//             if(selected != null){
//                 GUILayout.Label(selected.Name);

//                 Editor.CreateCachedEditor(
//                 (Object)selected.Value, null, ref _cachedEditor);
//             }
            
//             if(SirenixEditorGUI.ToolbarButton(new GUIContent("Create Staff"))){
                
//                 CreateWindow<CreateStaffWindow>().Show();

//                 // #region SetPrefabCompo
//                 // GameObject prefab = new GameObject();
//                 // prefab.
//                 // #endregion
//                 // PrefabUtility.SaveAsPrefabAsset(prefab, _staffPrefabDirectory);
//             }
//             if(SirenixEditorGUI.ToolbarButton(new GUIContent("Delete Staff"))){
//                 // #region SetPrefabCompo
//                 // GameObject prefab = new GameObject();
//                 // #endregion
//                 // PrefabUtility.SaveAsPrefabAsset(prefab,_staffPrefabDirectory);
//                 GameObject obj = null;
//                 if(OdinPrefabUtility.OpenPrefabStage($"Assets/00.Work/LJS/Test/Prefab/{selected}.prefab",  obj)){
//                     Destroy(obj);
//                 }
//             }
//             if (SirenixEditorGUI.ToolbarButton(new GUIContent("Save")))
//             {
//                 // foreach (var item in _enemyTable.enemySOs)
//                 // {
//                 //     if(selEnemy.enemyInfo.name == item.enemyInfo.name)
//                 //     {
//                 //         AssetDatabase.LoadAssetAtPath<EnemySO>($"{_enemyDirectory}/{item.name}.asset")
//                 //             .name = selEnemy.enemyInfo.name;
//                 //         //item.name = selEnemy.enemyInfo.name;
//                 //     }
//                 // }
//             }
//             selected.Name = (selected.Value as StaffSO).StaffName;
//         }
//         SirenixEditorGUI.EndHorizontalToolbar();

//         base.OnBeginDrawEditors();
//     }
// }
