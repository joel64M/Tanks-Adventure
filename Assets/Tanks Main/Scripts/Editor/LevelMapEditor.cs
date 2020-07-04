using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
namespace NameSpaceName {

    [CustomEditor(typeof(LevelMap))]
    public class LevelMapEditor : Editor
    {

        #region Variables
        LevelMap levelMap;
        GameObject currentPrefab;
        int currentPrefabIndex;
    #endregion

        #region Builtin Methods


        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            if (levelMap == null)
            {
                levelMap = FindObjectOfType<LevelMap>();
            }
            #region List
            
            
            GUILayout.Space(10f);
            for (int j = 0; j < levelMap.pList.Count; j++)
            {
                GUILayout.Label("  " + levelMap.pList[j].name);

                //string what = "Show";
                if (GUILayout.Button("Show/Hide", GUILayout.MaxWidth(90), GUILayout.MaxHeight(20)))
                {

                    EditorPrefs.SetBool("BOOLS" + j, !EditorPrefs.GetBool("BOOLS" + j));
                }
                GUILayout.BeginHorizontal();
                if (levelMap.pList[j].prefab != null && EditorPrefs.GetBool("BOOLS" + j))
                {
                    int elementsInThisRow = 0;
                    for (int i = 0; i < levelMap.pList[j].prefab.Length; i++)
                    {
                        elementsInThisRow++;
                        Texture prefabTexture = AssetPreview.GetAssetPreview(levelMap.pList[j].prefab[i]);
                        if(currentPrefab == levelMap.pList[j].prefab[i])
                        {
                        
                          
                            if (GUILayout.Button(prefabTexture, GUILayout.MaxWidth(80), GUILayout.MaxHeight(80)))
                            {
                                currentPrefab = levelMap.pList[j].prefab[i];
                                currentPrefabIndex = j;//go.GetComponent<Map>().pList[j].prefab.Length;
                                EditorWindow.FocusWindowIfItsOpen<SceneView>();
                            }
                        }
                        else
                        {
                            if (GUILayout.Button(prefabTexture, GUILayout.MaxWidth(60), GUILayout.MaxHeight(60)))
                            {

                                currentPrefab = levelMap.pList[j].prefab[i];
                                currentPrefabIndex = j;//go.GetComponent<Map>().pList[j].prefab.Length;
                                EditorWindow.FocusWindowIfItsOpen<SceneView>();
                            }
                        }
                      
                        //move to next row after creating a certain number of buttons so it doesn't overflow horizontally
                        if (elementsInThisRow > Screen.width / 100)
                        {
                            elementsInThisRow = 0;
                            GUILayout.EndHorizontal();
                            GUILayout.BeginHorizontal();
                        }
                    }
                }
                GUILayout.EndHorizontal();
            }

            #endregion
         
        }

        void OnSceneGUI()
        {
            /*
            Handles.BeginGUI();
            GUILayout.Box("Level Editor Mode");
            if (currentPrefab == null)
            {
                GUILayout.Box("No prefab selected!");
            }
            Handles.EndGUI();

    */

            if (Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.V)
            {

                Vector3 mousePos = Event.current.mousePosition;
                float ppp = EditorGUIUtility.pixelsPerPoint;
              mousePos.y = Camera.current.pixelHeight - mousePos.y * ppp;
             //  mousePos.x *= ppp;

                Ray ray = Camera.current.ScreenPointToRay(mousePos);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    levelMap.temp.transform.position = hit.point;
                    Vector3 roundedPos = new Vector3(5 * Mathf.Round(hit.point.x / 5), 5 * Mathf.Round(hit.point.y / 5), 5 * Mathf.Round(hit.point.z / 5));
                    Spawn(roundedPos);
                  //  Debug.Log(hit.point);

                   // if (!hit.transform.gameObject.CompareTag("Ball"))
                    {
                       // if (!hit.transform.gameObject.CompareTag("Ground"))
                        {
                           // spawnPosition = hit.transform.position;//   new Vector3(Mathf.Round(hit.point.x), Mathf.Round(hit.point.y), Mathf.Round(hit.point.z));
                            //DestroyImmediate(hit.transform.gameObject);

                            //    spawnPosition = new Vector3(Mathf.Round(hit.point.x), Mathf.Round(hit.point.y ), Mathf.Round(hit.point.z));

                            // if (!hit.transform.gameObject.CompareTag(selectedPrefab.gameObject.tag))
                            {
                            //    Spawn(spawnPosition);

                            }
                        }
                    }

                }
            }
        }
        #endregion

        #region Custom Methods
        void Spawn(Vector3 _spawnPosition)
        {
            if (currentPrefab != null)
            {
                if (levelMap.parent == null)
                {
                    GameObject gg = new GameObject("Environment");
                    levelMap.parent = gg.transform;
                }

                GameObject goo = (GameObject)Instantiate(currentPrefab, new Vector3(_spawnPosition.x, _spawnPosition.y, _spawnPosition.z), Quaternion.identity, levelMap.parent);
                currentPrefab = goo;
              //  Selection.activeGameObject = goo;
                goo.name = currentPrefab.name;
            }
        }
        #endregion

    }
    }
