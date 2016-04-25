using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DataManager : DataManagerBase <DataManager>{

	public readonly string SPREAD_SHEET = "14HLMZIPB21WrMf80gqsJw-R1BKMLFJt8haok36uSI5o";
	public readonly string SPREAD_SHEET_CONFIG_SHEET = "od6";

	public override void Initialize ()
	{
		base.Initialize ();

		Application.targetFrameRate = 60;
		QualitySettings.vSyncCount = 0;

		DontDestroyOnLoad(gameObject);

		if (PlayerPrefs.HasKey (DefineOld.USER_WIDTH) == false) {
			PlayerPrefs.SetInt (DefineOld.USER_WIDTH, DefineOld.DEFUALT_USER_WIDTH);
			PlayerPrefs.SetInt (DefineOld.USER_HEIGHT, DefineOld.DEFUALT_USER_WIDTH);
		}

		//int iWidth = PlayerPrefs.GetInt (DefineOld.USER_WIDTH);
		//int iHeight= PlayerPrefs.GetInt (DefineOld.USER_HEIGHT);

		//m_tDataUser.Initialize (iWidth,iHeight);
		AllLoad();
		foreach( CsvLocalNotificationData data in csv_localNotification ){
			LocalNotificationManager.Instance.Add (data);
		}
		return;
	}

	public void AllLoad(){
		kvs_data.Load( DataKvs.FILE_NAME );
		m_csvItem.Load ();
		m_csvMonster.Load ();
		dataMonster.Load (DataMonster.FILENAME);
		dataStaff.Load (DataStaff.FILENAME);
		//Debug.LogError ("here");
		m_dataItem.Load (DataItem.FILENAME);

		m_csvItemDetail.Load ();

		m_csvWork.Load ();
		dataWork.Load (DataWork.FILENAME);

		m_csvStaff.Load ();
		m_csvLevel.Load ();
		m_csvTime.Load ();
		m_csvWord.Load ();
		m_csvTutorial.Load ();
		m_csvLocalNotification.Load ();
	}

	public void DataSave(){
		#if !UNITY_EDITOR
		Debug.LogError ("save");
		#endif
		m_dataKvs.Save (DataKvs.FILE_NAME);
		dataMonster.Save (DataMonster.FILENAME);
		dataStaff.Save (DataStaff.FILENAME);
		m_dataItem.Save (DataItem.FILENAME);
		dataWork.Save (DataWork.FILENAME);

		m_csvItem.Save (CsvItem.FilePath);
		m_csvMonster.Save (CsvMonster.FilePath);
		m_csvStaff.Save (CsvStaffData.FilePath);


	}


	public void DummyCall(){
		return;
	}

	private DataUser m_tDataUser = new DataUser ();
	static public DataUser user{
		get{
			return Instance.m_tDataUser;
		}
		set {
			Instance.m_tDataUser = value;
		}
	}

	static public List<CsvItemParam> itemMaster {
		get{ return Instance.m_csvItem.list; }
	}
	static public CsvItemParam GetItemMaster( int _iItemId ){
		foreach( CsvItemParam data in itemMaster ){
			if (data.item_id == _iItemId) {
				return data;
			}
		}
		return new CsvItemParam ();
	}

	#region For CSV
	public CsvItem m_csvItem = new CsvItem();
	static public List<CsvItemParam> csv_item {
		get{ 
			return Instance.m_csvItem.All;
		}
	}
	static public CsvItemParam GetItem( int _iItemId ){
		foreach (CsvItemParam data in csv_item) {
			if (_iItemId == data.item_id) {
				return data;
			}
		}
		return new CsvItemParam ();
	}

	public DataItem m_dataItem = new DataItem();

	public List<DataItemParam> m_ItemDataList {
		get{ 
			return m_dataItem.Select (" status != 0 ");
		}
	}


	public CsvMonster m_csvMonster = new CsvMonster();
	static public List<CsvMonsterParam> csv_monster {
		get{ 
			return Instance.m_csvMonster.All;
		}
	}
	static public CsvMonsterParam GetMonster( int _iMonsterId ){
		foreach (CsvMonsterParam data in csv_monster) {
			if (_iMonsterId == data.monster_id) {
				return data;
			}
		}
		return new CsvMonsterParam ();
	}
	public CsvItemDetal m_csvItemDetail = new CsvItemDetal();
	static public List<CsvItemDetailData> csv_item_detail {
		get{ 
			return Instance.m_csvItemDetail.All;
		}
	}
	static public CsvItemDetailData GetItemDetail( int _iItemId , int _iLevel ){
		foreach (CsvItemDetailData data in csv_item_detail) {
			if (data.item_id == _iItemId && data.level == _iLevel) {
				return data;
			}
		}
		return new CsvItemDetailData ();
	}

	public CsvWork m_csvWork = new CsvWork();
	static public List<CsvWorkParam> csv_work {
		get{ 
			return Instance.m_csvWork.All;
		}
	}
	static public CsvWorkParam GetWork( int _iWorkId ){
		foreach (CsvWorkParam work in csv_work) {
			if (work.work_id == _iWorkId) {
				return work;
			}
		}
		Debug.LogError ("ignore staff_id:" + _iWorkId.ToString ());
		return new CsvWorkParam ();
	}
	public DataWork dataWork = new DataWork ();
	public DataMonster dataMonster = new DataMonster ();
	public DataStaff dataStaff= new DataStaff();


	public CsvStaffData m_csvStaff = new CsvStaffData();
	static public List<CsvStaffParam> csv_staff {
		get{ 
			return Instance.m_csvStaff.All;
		}
	}
	static public CsvStaffParam GetStaff( int _iStaffId ){
		foreach (CsvStaffParam staff in csv_staff) {
			if (staff.staff_id == _iStaffId) {
				return staff;
			}
		}
		Debug.LogError ("ignore staff_id:" + _iStaffId.ToString ());
		return new CsvStaffParam ();
	}
	public CsvLevel m_csvLevel = new CsvLevel();
	static public List<CsvLevelData> csv_level {
		get{ 
			return Instance.m_csvLevel.All;
		}
	}
	public CsvTime m_csvTime = new CsvTime();
	static public List<CsvTimeData> csv_time {
		get{ 
			return Instance.m_csvTime.All;
		}
	}
	public CsvWord m_csvWord = new CsvWord();
	static public List<CsvWordData> csv_word {
		get{ 
			return Instance.m_csvWord.All;
		}
	}
	public string GetWord( string _strKey ){
		foreach (CsvWordData data in csv_word) {
			if (_strKey.Equals (data.key) == true) {
				return data.word;
			}
		}
		return "-------";
	}

	public CsvTutorial m_csvTutorial = new CsvTutorial();
	static public List<CsvTutorialData> csv_tutorial {
		get{ 
			return Instance.m_csvTutorial.All;
		}
	}

	public CsvLocalNotification m_csvLocalNotification = new CsvLocalNotification();
	static public List<CsvLocalNotificationData> csv_localNotification {
		get{ 
			return Instance.m_csvLocalNotification.All;
		}
	}

	#endregion
	public float m_fInterval;
	public const float EDITOR_SAVE_INTERVAL = 10.0f;

	void OnApplicationPause(bool pauseStatus) {
		///Debug.LogError ("here");
		if (pauseStatus) {
			DataSave ();

		} else {
		}
	}
	#if UNITY_EDITOR
	void Update(){

		m_fInterval += Time.deltaTime;

		if (EDITOR_SAVE_INTERVAL < m_fInterval) {
			m_fInterval -= EDITOR_SAVE_INTERVAL;
			DataSave ();
		}

	}
	#endif

}



















