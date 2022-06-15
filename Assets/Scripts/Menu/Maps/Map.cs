using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "Create Mapdatat", menuName = "Map")]
public class Map: ScriptableObject {

	[Header("Properties")]
	public new string name;
	public Sprite img;
	public string info;
	public string scene;
}
