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

            // go = Resources.LoadAll<GameObject>("props");

            /*
            for (int i = 0; i < prefabObjs.Count; i++)
            {
                for (int j = 0; j < prefabObjs[i].Length; j++)
                {
                    Debug.Log(prefabObjs[i][j].name);

                }
            }
            */
            if (PlayerPrefs.GetInt("PathCount") > 0)
            {
                for (int i = 0; i < PlayerPrefs.GetInt("PathCount"); i++)
                {
                 prefabPaths.Add(PlayerPrefs.GetString("Path" + (i+1).ToString()));
                    GameObject[] goz = Resources.LoadAll<GameObject>(prefabPaths[i]);
                    prefabObjs.Add(goz);
                }
            }


        }
        string tempp;
        private void OnGUI()
        {
            DrawLayouts();
            DrawHeader();
            DrawButtons();
            GUILayout.BeginArea(addPrefabsButonRect);
         
         
            GUILayout.EndArea();
            GUILayout.BeginArea(spawnObjRect);

            if (prefabObjs.Count >0 )
            {


                for (int j = 0; j < prefabObjs.Count; j++)
                {
                    if (GUILayout.Button("Delete",GUILayout.Width(50f)))
                    {
                        Debug.Log(PlayerPrefs.GetInt("PathCount"));
                        prefabPaths.RemoveAt(j);
                        prefabObjs.RemoveAt(j);
                        PlayerPrefs.DeleteKey("Path" + j.ToString());
                        PlayerPrefs.SetInt("PathCount", prefabPaths.Count);
                        Debug.Log(PlayerPrefs.GetInt("PathCount"));
                        List<GameObject[]> ddd = prefabObjs;
                        prefabObjs = ddd;
                        break;
                    }
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
                }


            }

            if (GUILayout.Button("Add Prefabs"))
            {
                 tempp  = EditorUtility.OpenFolderPanel("Select Path", "Assets", "hey");
                DirectoryInfo dir = new DirectoryInfo(tempp);
              
                DirectoryInfo[] info = dir.GetDirectories("*.*");
                int count = dir.GetDirectories().Length;
                if (count == 0)
                {
                    string st = (Path.GetFileName(Path.GetFullPath(dir.ToString()).TrimEnd(Path.DirectorySeparatorChar)));
                    prefabPaths.Add(st);
                    GameObject[] goz = Resources.LoadAll<GameObject>(st);
                    prefabObjs.Add(goz);
                    PlayerPrefs.SetString("Path" + prefabPaths.Count, st);
                    PlayerPrefs.SetInt("PathCount", PlayerPrefs.GetInt("PathCount") + 1);
                }
                else
                {
                    for (int i = 0; i < count; i++)
                    {

                        //Debug.Log("Found Directory: " + Path.GetFileName( Path.GetFullPath(info[i].ToString()).TrimEnd(Path.DirectorySeparatorChar)));
                        string st = (Path.GetFileName(Path.GetFullPath(info[i].ToString()).TrimEnd(Path.DirectorySeparatorChar)));
                        prefabPaths.Add(st);
                        GameObject[] goz = Resources.LoadAll<GameObject>(st);
                        prefabObjs.Add(goz);
                        PlayerPrefs.SetString("Path" + prefabPaths.Count, st);
                        PlayerPrefs.SetInt("PathCount", PlayerPrefs.GetInt("PathCount") + 1);

                        //  Debug.Log(prefabObjs[i]);

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
           headerSectionTexture.SetPixel(0, 0, Color.grey);
            headerSectionTexture.Apply();
          //or
          //  headerSectionTexture = Resources.Load<Texture2D>("grass");
        }

        void DrawLayouts()
        {
            headerSectionRect.x = 0;
            headerSectionRect.y = 0;
            headerSectionRect.width = Screen.width;
            headerSectionRect.height = 50f;
            GUI.DrawTexture(headerSectionRect, headerSectionTexture);

            spawnObjRect.x =0;
            spawnObjRect.y = 50f;
            spawnObjRect.width = Screen.width;
            spawnObjRect.height = 600f;
            GUI.DrawTexture(spawnObjRect, new Texture2D(1, 1));


            addPrefabsButonRect.x = 0f;
            addPrefabsButonRect.y = 0f;
            addPrefabsButonRect.width = 90f;
            addPrefabsButonRect.height = 50f;
            GUI.DrawTexture(addPrefabsButonRect, new Texture2D(1, 1));

        }
        void DrawHeader()
        {
            GUILayout.BeginArea(headerSectionRect);
            GUILayout.Label("Hello Cello",skin.GetStyle("Header1"));
            GUILayout.EndArea();
        }

        void DrawButtons()
        {

        }
        #endregion

    }
}
