using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MasterData : MonoBehaviour {

	static MasterData instance = null;

	public static MasterData Instance {
		get {
			if (instance == null) {
				GameObject obj = GameObject.Find ("MasterData");
				if (obj == null) {
					obj = new GameObject("MasterData");
					//Debug.LogError ("Not Exist AtlasManager!!");
				}
				instance = obj.GetComponent<MasterData> ();
				if (instance == null) {
					//Debug.LogError ("Not Exist AtlasManager Script!!");
					instance = obj.AddComponent<MasterData>() as MasterData;
				}
				instance.initialize ();
			}
			return instance;
		}
	}

	private void initialize(){
		DontDestroyOnLoad(gameObject);
		return;
	}

	private List<CsvMonsterParam> m_monsterDataList = new List<CsvMonsterParam>();
	static public List<CsvMonsterParam> MonsterList {
		get{
			if (0 == Instance.m_monsterDataList.Count) {
				var csvMonster = new CsvMonster ();
				csvMonster.Load ();
				foreach (CsvMonsterParam csv_data in csvMonster.All) {
					Instance.m_monsterDataList.Add (csv_data);
				}
			}
			return Instance.m_monsterDataList;
		}
	}
	static public CsvMonsterParam Monster( int _iMonsterId ){
		foreach (CsvMonsterParam monster in MonsterList) {
			if (monster.monster_id == _iMonsterId) {
				return monster;
			}
		}
		return new CsvMonsterParam ();
	}


}












