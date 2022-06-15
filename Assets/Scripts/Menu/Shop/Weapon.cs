using UnityEngine;

[CreateAssetMenu(fileName = "Create Weapon", menuName = "Weapon information")]
public class Weapon: ScriptableObject {

	[Header("Properties")]
	public new string name;
	public Sprite img;
	public int price;
	public GameObject prefab;
}
