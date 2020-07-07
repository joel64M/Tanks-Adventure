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
        Rect headerSectionRect;
        Rect spawnObjRect;
        Rect addPrefabsButonRect;
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
            window.minSize = new Vector2(400, 200);
            window.Show();
        }
        private void OnEnable()
        {
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

        private void OnGUI()
        {
            DrawLayouts();
            DrawHeader();
            DrawButtons();
          
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
        #endregion

        #region Custom Methods
        void InitTextures()
        {
            headerSectionTexture = new Texture2D(1, 1);
            headerSectionTexture.SetPixel(0, 0, Color.gray);
            headerSectionTexture.Apply();
          //or
          //  headerSectionTexture = Resources.Load<Texture2D>("grass");
        }

        void DrawLayouts()
        {
            headerSectionRect.x = 0;
            headerSectionRect.y = 0;
            headerSectionRect.width = Screen.width;
            headerSectionRect.height = 30f;
            GUI.DrawTexture(headerSectionRect, headerSectionTexture);

            spawnObjRect.x =0;
            spawnObjRect.y = 30f;
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
        }

        void DrawButtons()
        {

        }
        #endregion

    }
}
