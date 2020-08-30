using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Linq;
namespace NameSpaceName {

    public class LevelEditorWindow : EditorWindow
    {

        #region Variables
        Texture2D headerSectionTexture;
        Texture2D parentSectionTexture;

        Rect headerSectionRect;
        Rect parentSectionRect;
        Rect spawnObjRect;
        Rect addPrefabsButonRect;

        GameObject parentGameobject = null;
        bool isSnap = false;
        int snapMultiple = 5;
        float rotateMultiple = 45f;

        List<GameObject[]> prefabObjs = new List<GameObject[]>();
        GUISkin skin;
        List<string> prefabPaths = new List<string>();
        GameObject currentPrefab;

        #endregion

        #region Builtin Methods

        [MenuItem("Editor/LevelEditor")]
        static void ShowWindow() // custom method
        {
            LevelEditorWindow window = GetWindow<LevelEditorWindow>("Level Editor");
            window.minSize = new Vector2(300, 200);
            window.Show();
        }

        
        private void OnEnable()
        {
            SceneView.duringSceneGui += this.OnSceneGUI;

            InitTextures();
            skin = Resources.Load<GUISkin>("GuiSkins/LevelEditorSkin");
         
            if (PlayerPrefs.GetInt("PathCount") > 0)
            {
                for (int i = 0; i < PlayerPrefs.GetInt("PathCount"); i++)
                {
                    string str = PlayerPrefs.GetString("Path" + (i).ToString());
                    Debug.Log("Loading as ... path" + (i).ToString() + " :" + str);
                    prefabPaths.Add(str);
                    GameObject[] gos = Resources.LoadAll<GameObject>(prefabPaths[i]);
                    prefabObjs.Add(gos);
                }
            }
        }
        void OnDisable() { SceneView.duringSceneGui -= this.OnSceneGUI; }

        private void OnGUI()
        {
            DrawLayouts();
            DrawHeader();
            DrawButtons();
           // PlacePrefabV();
            
            GUILayout.BeginArea(spawnObjRect);

            if (prefabObjs.Count >0 )
            {
                for (int j = 0; j < prefabObjs.Count; j++)
                {

                    GUILayout.BeginHorizontal();
                    float columns = 2f;
                    float tempWidth = Screen.width / columns;

                    GUILayout.Label(prefabPaths[j], skin.GetStyle("Name"));
                    //{

                   // }

                    if (GUILayout.Button("Delete", GUILayout.Width(tempWidth)))
                    {
                       // Debug.Log("************"+prefabObjs.Count);
                        prefabPaths.RemoveAt(j);
                        prefabObjs.RemoveAt(j);
                     //   Debug.Log("************" + prefabObjs.Count);

                        PlayerPrefs.DeleteKey("Path" + j.ToString());
                     //   List<GameObject[]> tempList = prefabObjs;
                       // prefabObjs = tempList;
                       // List<string> tempList2 = prefabPaths;
                       // prefabPaths = tempList2;
                      //  Debug.Log(PlayerPrefs.GetString("Path0"));
                       // Debug.Log(PlayerPrefs.GetString("Path1"));
                        foreach (var item in prefabPaths)
                        {
                         //   Debug.Log(item);
                        }
                        ///
                        ///  have to order  the list and save it again 
                        //
                        for (int i = 0; i < prefabPaths.Count; i++)
                        {
                            PlayerPrefs.SetString("Path" + i.ToString(), prefabPaths[i]);
                        }
                        PlayerPrefs.SetInt("PathCount", prefabPaths.Count);
                        PlayerPrefs.Save();
                        // Debug.Log("path count after deleting" + PlayerPrefs.GetInt("PathCount"));

                        break;
                    }
                    GUILayout.EndHorizontal();

                    GUILayout.BeginHorizontal();

                   // for (int i = 0; i < prefabObjs.Count; i++)
                   /// {
                    int elementsInThisRow = 0;

                    for (int k = 0; k < prefabObjs[j].Length; k++)
                        {
                        elementsInThisRow++;

                        Texture prefabTexture = AssetPreview.GetAssetPreview(prefabObjs[j][k]);
                            if(currentPrefab == prefabObjs[j][k])
                            {
                                if (GUILayout.Button(prefabTexture, GUILayout.MaxWidth(70), GUILayout.MaxHeight(70)))
                                {
                                    currentPrefab = prefabObjs[j][k]; // redundant sicne its already assigned
                                    EditorWindow.FocusWindowIfItsOpen<SceneView>();
                                }
                            }
                            else
                            {
                                if (GUILayout.Button(prefabTexture, GUILayout.MaxWidth(50), GUILayout.MaxHeight(50)))
                                {
                                    currentPrefab = prefabObjs[j][k];
                                    EditorWindow.FocusWindowIfItsOpen<SceneView>();
                                }
                            }
                        if (elementsInThisRow>=( Screen.width -20)/ 50)
                        {
                            elementsInThisRow = 0;
                            GUILayout.EndHorizontal();
                            GUILayout.BeginHorizontal();
                        }
                    }
                   // }

                    /*

                    if (prefabObjs.Count > 0)
                    {
                        for (int i = 0; i < prefabObjs[j].Length; i++)
                        {
                            Texture prefabTexture = AssetPreview.GetAssetPreview(prefabObjs[j][i]);
                            if (GUILayout.Button(prefabTexture, GUILayout.MaxWidth(50), GUILayout.MaxHeight(50)))
                            {
                                EditorWindow.FocusWindowIfItsOpen<SceneView>();
                            }
                        }
                    }
                    */
                    GUILayout.EndHorizontal();
                    GUILayout.Space(5);

                }
            }
            GUILayout.Space(5);
            if (GUILayout.Button("Add Prefabs"))
            {
                string str  = EditorUtility.OpenFolderPanel("Select Path", "Assets", "what does this do lol ?");
                DirectoryInfo dir = new DirectoryInfo(str);
                DirectoryInfo[] info = dir.GetDirectories("*.*");
                int count = dir.GetDirectories().Length;
               // Debug.Log(count);
                if (count == 0)
                {
                    string st = (Path.GetFileName(Path.GetFullPath(dir.ToString()).TrimEnd(Path.DirectorySeparatorChar)));
                    prefabPaths.Add(st);
                    GameObject[] gos = Resources.LoadAll<GameObject>(st);
                    if(gos.Length==0)
                    {
                        return;
                    }
                    prefabObjs.Add(gos);
                  //  Debug.Log("Saving as ...path" + (prefabObjs.Count).ToString() + " :" + st);
                    PlayerPrefs.SetString("Path" + (prefabObjs.Count-1).ToString(), st);
                    PlayerPrefs.SetInt("PathCount", PlayerPrefs.GetInt("PathCount") + 1);
                    PlayerPrefs.Save();
                }
                else
                {
                    for (int i = 0; i < count; i++)
                    {
                        string st = (Path.GetFileName(Path.GetFullPath(info[i].ToString()).TrimEnd(Path.DirectorySeparatorChar)));
                        prefabPaths.Add(st);
                        GameObject[] gos = Resources.LoadAll<GameObject>(st);
                        if (gos.Length == 0)
                        {
                            return;
                        }
                        prefabObjs.Add(gos);

                        Debug.Log("Saving as ...path"+(  prefabObjs.Count-1).ToString() + " :" + st);
                        PlayerPrefs.SetString("Path" +(prefabObjs.Count-1).ToString(), st);

                      //  Debug.Log(PlayerPrefs.GetString("Path0"));
                      //  Debug.Log(PlayerPrefs.GetString("Path1"));
                        PlayerPrefs.SetInt("PathCount", PlayerPrefs.GetInt("PathCount") + 1);
                        PlayerPrefs.Save();
                    }
                }
            }

            GUILayout.EndArea();

        }
        void OnSceneGUI(SceneView sceneView)
        {
            PlacePrefab();
            RotateThatPrefab();
          
        }
      
    
        #endregion

        #region Custom Methods
        void InitTextures()
        {
            headerSectionTexture = new Texture2D(1, 1);
            headerSectionTexture.SetPixel(0, 0, Color.red);
            headerSectionTexture.Apply();

            parentSectionTexture = new Texture2D(1, 1);
            parentSectionTexture.SetPixel(0, 0, Color.yellow);
            parentSectionTexture.Apply();
            //or
            //  headerSectionTexture = Resources.Load<Texture2D>("grass");
        }

        void DrawLayouts()
        {
            headerSectionRect.x = 0;
            headerSectionRect.y = 0;
            headerSectionRect.width = Screen.width ;
            headerSectionRect.height = 30f;
            GUI.DrawTexture(headerSectionRect, headerSectionTexture);

            parentSectionRect.x = 0;
            parentSectionRect.y = 35;
            parentSectionRect.width = Screen.width;
            parentSectionRect.height = 60f;
            GUI.DrawTexture(parentSectionRect, parentSectionTexture);

            spawnObjRect.x =0;
            spawnObjRect.y = 100f;
            spawnObjRect.width = Screen.width;
            spawnObjRect.height = 600f;
            GUI.DrawTexture(spawnObjRect, new Texture2D(1, 1));


          //  addPrefabsButonRect.x = 0f;
          //  addPrefabsButonRect.y = 0f;
           // addPrefabsButonRect.width = 90f;
           // addPrefabsButonRect.height = 50f;
           // GUI.DrawTexture(addPrefabsButonRect, new Texture2D(1, 1));

        }
        void DrawHeader()
        {
            GUILayout.BeginArea(headerSectionRect);
            GUILayout.Label("Noob Level Editor",skin.GetStyle("Header1"));
            GUILayout.EndArea();

            GUILayout.BeginArea(parentSectionRect);

                GUILayout.BeginHorizontal();
                    GUILayout.Label("Parent of spawned gameobjects");
                    parentGameobject = (GameObject)EditorGUILayout.ObjectField(parentGameobject, typeof(GameObject), true);
                GUILayout.EndHorizontal();

                    if (GUILayout.Button("Create Empty Parent GameObject"))
                    {
                        GameObject go = new GameObject("Parent");
                        parentGameobject = go;
                    }

                GUILayout.BeginHorizontal();
                    GUILayout.Label("Snap", GUILayout.Width(35f));
                    isSnap = EditorGUILayout.Toggle(isSnap, GUILayout.Width(15f));
                    GUILayout.Label("Snap Multiple", GUILayout.Width(80f));
                    snapMultiple = EditorGUILayout.IntField( snapMultiple,GUILayout.Width(35f));
                GUILayout.EndHorizontal();

            GUILayout.EndArea();



        }

        void DrawButtons()
        {

        }


        Vector3 snappedRotationValue;
        Vector3[] snappedRotationAmount = { new Vector3(1.5f, 0, -1.5f) , new Vector3(1.5f,0,1.5f), new Vector3(-1.5f, 0, 1.5f), new Vector3(-1.5f, 0, -1.5f), new Vector3(0, 0,0) };
        float[] snappedRotationAngle = {45f, -45, 45, -45,0};
        int snapRotationIndex = 0;
        GameObject currentSpawnedGO;

        GameObject selectedGO;
        Vector3 defaultPosition;
    
        void RotateThatPrefab()
        {
            if (Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.F)
            {
                Vector3 mousePos = Event.current.mousePosition;
                float ppp = EditorGUIUtility.pixelsPerPoint;
                mousePos.y = Camera.current.pixelHeight - mousePos.y * ppp;
                //  mousePos.x *= ppp;

                Ray ray = Camera.current.ScreenPointToRay(mousePos);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit,5000,~LayerMask.GetMask("Ground")))
                {
                    Debug.Log(hit.transform.gameObject.name);
                    if(selectedGO!=hit.transform.gameObject)
                    defaultPosition = hit.transform.position;
                    selectedGO = hit.transform.gameObject;


                    snappedRotationValue = new Vector3(Mathf.Abs(defaultPosition.x), 0, Mathf.Abs(defaultPosition.z));
                    Debug.Log(snappedRotationValue);
                    float signZ = Mathf.Sign(defaultPosition.z);
                    float signX = Mathf.Sign(defaultPosition.x);
                    Debug.Log(signX + " " + signZ);
                    snappedRotationValue += snappedRotationAmount[snapRotationIndex];
                    Debug.Log(snappedRotationValue);
                    selectedGO.transform.position = new Vector3(snappedRotationValue.x * signX, 0, snappedRotationValue.z * signZ);

                    selectedGO.transform.eulerAngles = new Vector3(0, snappedRotationAngle[snapRotationIndex], 0);
                    snapRotationIndex++;
                    if (snapRotationIndex >= 5)
                    {
                        snapRotationIndex = 0;
                    }
                }
            
            }
        }
        void PlacePrefab()
        {
            Handles.BeginGUI();
            GUILayout.Box("Level Editor Mode");
            if (currentPrefab == null)
            {
                GUILayout.Box("No prefab selected!");
            }
            Handles.EndGUI();


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
                    Vector3 roundedPos = new Vector3(5 * Mathf.Round(hit.point.x / 5), 5 * Mathf.Round(hit.point.y / 5), 5 * Mathf.Round(hit.point.z / 5));
                    RaycastHit[] sphereHits =  (Physics.SphereCastAll(roundedPos, 1.5f, ray.direction));
                    bool samePrefabUnder = false;
                    foreach (var objs in sphereHits)
                    {

                        if(objs.transform.gameObject.layer == currentPrefab.gameObject.layer)
                        {
                            samePrefabUnder = true;
                            /*
                            Debug.Log("same exists " + objs.transform.gameObject.name + " = " + currentPrefab.gameObject.name);
                            Debug.DrawLine(objs.transform.position, roundedPos,Color.red,1f);
                         GameObject go =  GameObject.CreatePrimitive(PrimitiveType.Sphere);
                            go.transform.position = roundedPos;
                            go.transform.localScale =( Vector3.one * 3f);
                            */
                        }
                        else
                        {
                         //   Debug.Log("no same"+ objs.transform.gameObject.name + " = " + currentPrefab.gameObject.name);

                        }
                    }

                    if (!samePrefabUnder)
                    {
                        Spawn(roundedPos);
                    }
               

                }
            }
        }
      
        void Spawn(Vector3 _spawnPosition)
        {
            if (currentPrefab != null)
            {
               // if (levelMap.parent == null)
                {
                  //  GameObject gg = new GameObject("Environment");
               //     levelMap.parent = gg.transform;
                }
             GameObject tempGo =  (GameObject) PrefabUtility.InstantiatePrefab(currentPrefab);
                currentSpawnedGO = tempGo;
                tempGo.transform.position = _spawnPosition;
                if(parentGameobject!=null)
                    tempGo.transform.SetParent(parentGameobject.transform);
                // currentPrefab = goo;
                //  Selection.activeGameObject = goo;
                tempGo.name = currentPrefab.name ;
            }
        }
        #endregion

    }
}
