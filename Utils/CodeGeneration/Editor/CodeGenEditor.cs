using UnityEditor;

public class CodeGenEditor : Editor
{
	//  [MenuItem("Tools/Code generation/Check AudioIds")]
	//  public static void CheckAudioIds()
	//  {
	//    StringBuilder result = new StringBuilder();
	//    Array ids = Enum.GetValues(typeof(AudioId));

	//    foreach (AudioId id in Enum.GetValues(typeof(AudioId)))
	//    {
	//      string path = id.GetDesc();
	//      AudioClip clip = Resources.Load<AudioClip>(path);
	//      if (clip == null)
	//        result.AppendFormat("AudioClip at path {0} is not found\n", id.GetDesc());
	//    }

	//    if (result.Length > 0)
	//      Debug.LogError(result);
	//    else
	//      Debug.Log("Test passed.");
	//  }

	//  [MenuItem("Tools/Code generation/Add Selected Audio")]
	//  public static void AddSelectedAudio()
	//  {
	//    string audioIdStart =
	//@"public enum AudioId
	//{";
	//    string audioIdEnd =
	//@"
	//}";

	//    string generateClass =
	//      audioIdStart
	//      + "{CODE_GEN}"
	//      + audioIdEnd;

	//    string content = File.ReadAllText("Assets/Scripts/Data/AudioId.cs");
	//    content = content.Replace(audioIdStart, String.Empty);
	//    content = content.Replace(audioIdEnd, String.Empty);




	//    List<Object> selectedAudioClips = Selection.GetFiltered(typeof(AudioClip), SelectionMode.Assets).ToList();
	//    List<string> paths = new List<string>();
	//    foreach (Object clip in selectedAudioClips)
	//    {
	//      string path = AssetDatabase.GetAssetPath(clip);
	//      paths.Add(path);
	//    }

	//    StringBuilder generatedLines = new StringBuilder();
	//    foreach (var file in paths)
	//    {
	//      if (file.EndsWith("meta")) continue;

	//      string name = Path.GetFileNameWithoutExtension(file);
	//      string path = Path.GetDirectoryName(file).Replace("\\", "/").Replace("Assets/Resources/", "");
	//      string folder = path.Split('/').Last();

	//      generatedLines.AppendFormat("  [Description(\"{0}\")]\n", path + "/" + name);
	//      generatedLines.AppendFormat("  {0},\n", name + folder);
	//    }

	//    content += generatedLines;
	//    generateClass = generateClass.Replace("{CODE_GEN}", content);

	//    string codeGenFolder = "Assets/Scripts/Data";
	//    string codeGenFile = "AudioId.cs";
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

	//  [MenuItem("Tools/Code generation/Generate AudioIds")]
	//  public static void GenerateAudioIds()
	//  {
	//    string generateClass = @"
	//public enum AudioId
	//{
	//{CODE_GEN}
	//}";

	//    StringBuilder generateLines = new StringBuilder();
	//    string[] files = Directory.GetFiles("Assets/Resources/Audio", "*.*", SearchOption.AllDirectories);
	//    foreach (var file in files)
	//    {
	//      if (!file.EndsWith("meta"))
	//      {
	//        string name = Path.GetFileNameWithoutExtension(file);
	//        string path = Path.GetDirectoryName(file).Replace("\\", "/").Replace("Assets/Resources/", "");
	//        string folder = path.Split('/').Last();

	//        generateLines.AppendFormat("  [Description(\"{0}\")]\n", path + "/" + name);
	//        generateLines.AppendFormat("  {0},\n", name + folder);
	//      }
	//    }
	//    generateClass = generateClass.Replace("{CODE_GEN}", generateLines.ToString());
	//    string codeGenFolder = "Assets/Scripts/Data";
	//    string codeGenFile = "AudioId.cs";
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
