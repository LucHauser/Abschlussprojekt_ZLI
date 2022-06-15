using UnityEngine;
using TMPro;

public class Quest : MonoBehaviour {

	[Header("Type")]
	public bool hasToKill;

	[Header("Kills")]
	public int killsForNextLevel = 5;
	public int maxEnemy = 10;
	public bool hasCountText;
	public TextMeshProUGUI countText;

	public string quest;
	public GameObject objectsOfLevel;
	public bool loadNextLevelImmediately;

	[Header("Zoom")]
	public bool changeZoom;
	public float zoom;
}
