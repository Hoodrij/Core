using System;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreateRootWindow : EditorWindow
{

	//  public static string GENERATOR_START = "#region Content";
	//  public static string GENERATOR_END = "#endregion";

	//  private string rootName = String.Empty;


	//  //[MenuItem("Tools/CREATE ROOT", false, -1000)]
	//  //public static void ShowWindow()
	//  //{
	//  //  EditorWindow.GetWindow(typeof(CreateRootWindow), false, "Create Root");
	//  //}

	//  protected void OnGUI()
	//  {
	//    EditorGUIUtility.labelWidth = 100f;

	//    GUILayout.BeginVertical();
	//    GUILayout.Space(8f);

	//    rootName = EditorGUILayout.TextField("Name:", rootName);
	//    rootName = rootName.Replace("Root", "");
	//    if (rootName.Length > 0)
	//      rootName = rootName.First().ToString().ToUpper() + rootName.Substring(1);

	//    if (GUI.changed)
	//    {
	//      EditorPrefs.SetString("NewRootName", rootName);
	//    }

	//    GUILayout.Space(5f);


	//    if (EditorApplication.isCompiling)
	//    {
	//      GUILayout.Label("Wait for compilation");
	//    }
	//    else
	//    {
	//      if (GUILayout.Button("Generate Classes"))
	//      {
	//        CreateRootClass();
	//        ModifyGameState();
	//        ModifyRoots();
	//      }
	//      if (GUILayout.Button("Generate Scene"))
	//      {
	//        CreateScene();
	//      }
	//    }

	//    GUILayout.EndVertical();
	//  }


	//  public static void CreateRootClass()
	//  {
	//    return;
	//    string rootName = EditorPrefs.GetString("NewRootName");

	//    string generateClass =
	//"public class " + rootName + @"Root : RootBase
	//{
	//  private void Awake()
	//  {
	//    base.Awake();
	//    Game." + rootName + @"Root = this;
	//  }

	//  public override void Unload()
	//  {
	//    Game." + rootName + @"Root = null;
	//  }
	//}";

	//    string codeGenFolder = "Assets/Scripts/Roots";
	//    string codeGenFile = rootName + "Root.cs";
	//    string codeGenFilePath = Path.Combine(codeGenFolder, codeGenFile);

	//    if (!Directory.Exists(codeGenFolder))
	//    {
	//      Directory.CreateDirectory(codeGenFolder);
	//    }
	//    if (File.Exists(codeGenFilePath))
	//    {
	//      File.Delete(codeGenFilePath);
	//    }

	//    File.WriteAllText(codeGenFilePath, generateClass);
	//    AssetDatabase.Refresh();
	//  }

	//  public static void CreateScene()
	//  {
	//    return;
	//    string rootName = EditorPrefs.GetString("NewRootName");

	//    EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);

	//    // Add _Starter
	//    GameObject starter = Utils.LoadAssetByName("_Starter");
	//    PrefabUtility.InstantiatePrefab(starter);

	//    // Add Root object
	//    GameObject rootTemplate = Utils.LoadAssetByName("RootTemplate");
	//    GameObject root = (GameObject) PrefabUtility.InstantiatePrefab(rootTemplate);
	//    root.AddComponent(Utils.GetType(rootName + "Root"));
	//    root.name = "_" + rootName + "Root";
	//    PrefabUtility.DisconnectPrefabInstance(root);

	//    EditorSceneManager.SaveScene(SceneManager.GetActiveScene(), string.Format("Assets/Scenes/{0}Scene.unity", rootName));
	//    AssetDatabase.Refresh();

	//    Utils.AddSceneToBuild(string.Format("Assets/Scenes/{0}Scene.unity", rootName));
	//  }

	//  public static void ModifyGameState()
	//  {
	//    return;
	//    string rootName = EditorPrefs.GetString("NewRootName");

	//    string generateClass = File.ReadAllText("Assets/Scripts/Data/GameState.cs");

	//    string oldContent = File.ReadAllText("Assets/Scripts/Data/GameState.cs");
	//    int pFrom = oldContent.IndexOf(GENERATOR_START);
	//    int pTo = oldContent.LastIndexOf(GENERATOR_END);
	//    oldContent = oldContent.Substring(pFrom, pTo - pFrom);

	//    string generatedString = "\t" + rootName + "," + System.Environment.NewLine;
	//    string newContent = oldContent + generatedString;

	//    generateClass = generateClass.Replace(oldContent, newContent);

	//    string codeGenFolder = "Assets/Scripts/Data";
	//    string codeGenFile = "GameState.cs";
	//    string codeGenFilePath = Path.Combine(codeGenFolder, codeGenFile);

	//    if (!Directory.Exists(codeGenFolder))
	//    {
	//      Directory.CreateDirectory(codeGenFolder);
	//    }
	//    if (File.Exists(codeGenFilePath))
	//    {
	//      File.Delete(codeGenFilePath);
	//    }

	//    File.WriteAllText(codeGenFilePath, generateClass);
	//    AssetDatabase.Refresh();
	//  }

	//  public static void ModifyRoots()
	//  {
	//    return;
	//    string rootName = EditorPrefs.GetString("NewRootName");

	//    string generateClass = File.ReadAllText("Assets/Scripts/Data/cs");

	//    string oldContent = File.ReadAllText("Assets/Scripts/Data/cs");
	//    int pFrom = oldContent.IndexOf(GENERATOR_START);
	//    int pTo = oldContent.LastIndexOf(GENERATOR_END);
	//    oldContent = oldContent.Substring(pFrom, pTo - pFrom);

	//    string generatedString = "\t" + "public " + rootName + "Root " + rootName + "Root { set; get; }" + System.Environment.NewLine;
	//    string newContent = oldContent + generatedString;

	//    generateClass = generateClass.Replace(oldContent, newContent);

	//    string codeGenFolder = "Assets/Scripts/Data";
	//    string codeGenFile = "cs";
	//    string codeGenFilePath = Path.Combine(codeGenFolder, codeGenFile);

	//    if (!Directory.Exists(codeGenFolder))
	//    {
	//      Directory.CreateDirectory(codeGenFolder);
	//    }
	//    if (File.Exists(codeGenFilePath))
	//    {
	//      File.Delete(codeGenFilePath);
	//    }
	//    File.WriteAllText(codeGenFilePath, generateClass);
	//    AssetDatabase.Refresh();
	//  }
}