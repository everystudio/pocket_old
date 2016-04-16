using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using System;

using Prime31;

public class InitialMain : MonoBehaviour {

	public enum STEP
	{
		NONE				= 0,
		DATAMANAGER_SETUP	,
		SOUND_LOAD			,
		REVIEW				,

		IDLE				,
		DB_SETUP			,
		INPUT_WAIT			,

		DB_BACKUP_NOEXIST	,
		DB_BACKUP_CHECK		,
		DB_BACKUP			,
		END					,
		MAX					,
	}
	public STEP m_eStep;
	public STEP m_eStepPre;

	public GameObject m_goRoot;
	public GameObject m_goStartButton;
	public ButtonBase m_btnStart;
	public ButtonBase m_btnBackup;
	public CtrlOjisanCheck m_ojisanCheck;
	public UITexture m_texBack;
	public UtilSwitchSprite m_SwitchSpriteBack;

	public ButtonBase m_btnTutorialReset;
	public ButtonBase m_btnCacheClear;

	public CtrlReviewWindow m_reviewWindow;

	public CtrlLoading m_csLoading;
	[SerializeField]
	private GameObject m_posDisplay;
	#region DB関係
	CsvKvs m_dbKvs{
		get{ 
			return DataManager.Instance.kvs;
		}
	}
	DataWork m_dbWork {
		get{
			return DataManager.Instance.dataWork;
		}
	}
	DBItem m_dbItem;
	DBItemMaster m_dbItemMaster;
	DBMonster m_dbMonster;
	CsvMonster m_dbMonsterMaster{
		get{
			return DataManager.Instance.m_csvMonster;
		}
	}
	#endregion



	// Use this for initialization
	void Start () {

		Application.targetFrameRate = 60;
		QualitySettings.vSyncCount = 0;

		m_eStep = STEP.IDLE;
		m_eStep = STEP.DATAMANAGER_SETUP;
		m_eStepPre = STEP.MAX;

		//m_SwitchSpriteBack.SetSprite ("garalley_003");
		m_SwitchSpriteBack.SetSprite ("texture/back/bg001.png");
		//m_SwitchSpriteBack.SetSprite ("tutorial777");

		SoundManager.Instance.PlayBGM ("farming" , "https://s3-ap-northeast-1.amazonaws.com/every-studio/app/sound/bgm");

		#if UNITY_ANDROID
		GoogleIAB.enableLogging (true);
		string key = "your public key from the Android developer portal here";
		key = "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAsqFXrg2t62dru/VFQYyxd2m1kORBbrAGxDxiSAkh3ybaXJtJWNcej/YAxKx7Orrtfq+pU965U2FnU3K54xddts2UGCI9O6TSU0AoKbwFYj+okfF21firsEqZd4aYtVYQ471flWj3ZEG9u2YpIzjGykUQadsxO4Y/OcRbdUn9289Mc0JAbdepmN9yRnvgBJWKZF/c0mBrM4ISfF5TVip2Tp+BXACqblOb+TQZjOB0OeVPxYpdy5k3eJTcQuwiLmYxgpEBL3tIT7grxVROgk8YYncncaZR7Q/wWlsFgFTNMRaF2bPI8apLiA7eIyKv5zbmhbE7YLBXUvkuoHbAqDQrLQIDAQAB";
		key = "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAoNpjSDejTWrxkCnuj5BQ8ozItBVBS2OhgRga4D2zgG42rKy/9C5nb32NDIl+N9xaVh2eMRDVdR9Hzznp0DIE3Xs89le26pzht5dK4/9s01qsVHmuEtecAcXp6ItCieayYSTn9oMgDwd5LWJMQf8+w5vm1qo6Vlo2vh0Lm70DGqisp3pee+6K+Zb+UfPrcvv9tmo3zCpq9EyiPaitw58nSWJYzDuLHzubUj5qeH5OwcAXi/scEkJrD5dJKmkmUgnDTQ2xSP/UAmtN8qAUULej3iOlQCqVIGlSRqL5kA9Qo9fKUX9PU0hcFz6vnuNj9SN3dk/ocAIvvujFKsQjNHvNHQIDAQAB";

		Debug.Log( key );
		//下はテスト用
		//key = "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEArGLKSb92Imt43S40ArCXfTmQ31c+pFQTM0Dza3j/Tn4cqjwccjQ/jej68GgVyGXGC2gT/EtbcVVA+bHugXmyv73lGBgmQlzBL41WYTKolO8Z6pVWTeHBtsT7RcHKukoKiONZ7NiQ9P5t6CCPBB2sXQOp1y3ryVbv01xXlM+hB6HkkKxrT6lIjTbtiVXCHAJvqPexPbqVIfGjBaXH/oHKxEBxYDaa6PTUsU3OP3MTx63ycTEnEMsQlA1W6ZuTFIa5Jd3cVlfQI7uovEzAbIlUfwcnxVOUWASiYe81eQiD1BMl+JeCRhfd5e8D4n0LOA12rHm1F3fC9ZoIEjpNB+BRhwIDAQAB";
		GoogleIAB.init( key );
		#endif


	}
	
	// Update is called once per frame
	void Update () {

		bool bInit = false;
		if (m_eStepPre != m_eStep) {
			m_eStepPre  = m_eStep;
			bInit = true;
		}

		switch (m_eStep) {

		case STEP.DATAMANAGER_SETUP:
			if (bInit) {
				/*
				GameObject pref = PrefabManager.Instance.PrefabLoadInstance ("test");
				paramtest script = pref.GetComponent<paramtest> ();
				Debug.Log (script.list.Count);
				script.list.Add (new DataItem ());
				*/
			}


			if (SpriteManager.Instance.IsIdle ()) {
				m_goRoot.SetActive (true);
				m_btnStart.gameObject.SetActive (false);
				m_eStep = STEP.SOUND_LOAD;
			}
			if (m_csLoading != null) {
				m_csLoading.ViewPercent (0.0f);
			}
			break;
		case STEP.SOUND_LOAD:
			/*
			if (bInit) {
				foreach (MasterAudioCSV data in DataManager.master_audio_list) {
					if (data.audio_type != 1) {
						SoundManager.Instance.AddLoadFile (data.filename);
					}
				}
			}
			if (m_csLoading != null) {
				m_csLoading.ViewPercent ( 0.0f );
			}
			if (SoundManager.Instance.IsIdle ()) {
				m_btnStart.gameObject.SetActive (true);
				m_eStep = STEP.IDLE;

				if (ReviewManager.Instance.IsReadyReview ()) {
					m_eStep = STEP.REVIEW;
				}
			}
			*/
			m_btnStart.gameObject.SetActive (true);
			m_eStep = STEP.IDLE;

			if (ReviewManager.Instance.IsReadyReview ()) {
				m_eStep = STEP.REVIEW;
			}


			break;

		case STEP.REVIEW:
			if (bInit) {
				GameObject obj = PrefabManager.Instance.MakeObject ("prefab/CtrlReviewWindow", m_goRoot.transform.parent.gameObject);
				m_reviewWindow = obj.GetComponent<CtrlReviewWindow> ();
				m_reviewWindow.Initialize ();

				m_goRoot.SetActive (false);

			}
			if (m_reviewWindow.IsEnd ()) {
				m_goRoot.SetActive (true);
				Destroy (m_reviewWindow.gameObject);;
				m_eStep = STEP.IDLE;
			}
			break;

		case STEP.IDLE:
			if (bInit) {
				m_btnStart.TriggerClear ();
				m_btnBackup.TriggerClear ();
			}
			if (m_btnStart.ButtonPushed) {
				m_eStep = STEP.DB_SETUP;
				SoundManager.Instance.PlaySE ("se_cleanup");
			} else if (m_btnBackup.ButtonPushed) {

				string backupDB = System.IO.Path.Combine (Application.persistentDataPath, DefineOld.DB_NAME_DOUBTSUEN_BK );
				if (System.IO.File.Exists (backupDB) == false ) {
					m_eStep = STEP.DB_BACKUP_NOEXIST;
				} else {
					m_eStep = STEP.DB_BACKUP_CHECK;
				}
			} else {
			}

			break;

		case STEP.DB_SETUP:
			if (bInit) {

				m_dbItem = new DBItem (DefineOld.DB_TABLE_ASYNC_ITEM);
				m_dbItemMaster = new DBItemMaster (DefineOld.DB_TABLE_ASYNC_ITEM_MASTER);
				//m_dbWork = new DBWork (DefineOld.DB_TABLE_ASYNC_WORK);
				m_dbMonster = new DBMonster (DefineOld.DB_TABLE_ASYNC_MONSTER);
				//DataMonster = new DBMonsterMaster (DefineOld.DB_TABLE_ASYNC_MONSTER_MASTER);
				/*
				m_dbItem.Open (DefineOld.DB_NAME_DOUBTSUEN, DefineOld.DB_FILE_DIRECTORY, "");
				m_dbItemMaster.Open (DefineOld.DB_NAME_DOUBTSUEN, DefineOld.DB_FILE_DIRECTORY, "");
				m_dbWork.Open (DefineOld.DB_NAME_DOUBTSUEN, DefineOld.DB_FILE_DIRECTORY, "");
				m_dbMonster.Open (DefineOld.DB_NAME_DOUBTSUEN, DefineOld.DB_FILE_DIRECTORY, "");
				DataMonster.Open (DefineOld.DB_NAME_DOUBTSUEN, DefineOld.DB_FILE_DIRECTORY, "");
				m_tkKvsOpen = m_dbKvs.Open (DefineOld.DB_NAME_DOUBTSUEN, DefineOld.DB_FILE_DIRECTORY, ""); // DefineOld.DB_PASSWORD
				*/
			}
			if (true) {
				//if (m_tkKvsOpen.Completed) {

				List<DataItemParam> data_item_list =  DataManager.Instance.m_dataItem.All;
				// 最初しか通らない
				if (data_item_list.Count == 0) {
					Debug.LogError ("here");
					m_dbKvs.WriteInt (DefineOld.USER_SYAKKIN,300000000);
					m_dbKvs.WriteInt (DefineOld.USER_TICKET,5);
					m_dbKvs.WriteInt (DefineOld.USER_SYOJIKIN,10000);
					m_dbKvs.Save (CsvKvs.FILE_NAME);
					var skitMasterTable = new MasterTableMapChip ();
					skitMasterTable.Load ();
					var csvItem = new CsvItem ();
					csvItem.Load ();
					foreach (MapChipCSV csvMapChip in skitMasterTable.All) {
						DataItemParam data = new DataItemParam (csvMapChip , csvItem );
						DataManager.Instance.m_dataItem.list.Add (data);
					}
				}

				List<DataWorkParam> data_work_list = m_dbWork.All;
				if (data_work_list.Count == 0) {
					var csvWork = new CsvWork ();
					csvWork.Load ();
					foreach (CsvWorkParam csv_work_data in csvWork.All) {
						DataWorkParam data = new DataWorkParam (csv_work_data);
						// 最初に出現していいのはappear_work_id== 0とlevel<=1のものだけ
						if (data.appear_work_id == 0 && data.level <= 1 ) {
							data.status = 1;
						}
						m_dbWork.list.Add(data);
					}
					m_dbWork.Save (DataWork.FILENAME);
				}

				List<DataMonsterParam> data_monster_list = DataManager.Instance.dataMonster.list;
				//Debug.LogError( string.Format( "data_monster_list.Count:{0}" , data_monster_list.Count ));
				if (data_monster_list.Count == 0) {
					DataMonsterParam monster = new DataMonsterParam ();
					monster.monster_serial = 1;
					monster.monster_id = 1;
					monster.item_serial = 12;
					monster.condition = (int)DefineOld.Monster.CONDITION.FINE;
					monster.collect_time = TimeManager.StrNow ();

					string strHungry = TimeManager.StrGetTime (-1 * 60 * 30);
					monster.meal_time = strHungry;
					monster.clean_time = strHungry;
					m_dbMonster.Replace (monster);
				}

				/*
				List<CsvMonsterParam> data_monster_master_list = DataManager.Instance.m_csvMonster.list;
				if (data_monster_master_list.Count == 0) {
					var csvMonsterMaster = new CsvMonster ();
					csvMonsterMaster.Load ();
					foreach (CsvMonsterData csv_monster_master_data in csvMonsterMaster.All) {
						DataMonster.Replace (csv_monster_master_data);
					}
				}
				*/

				/*
				//マスターデータの生成用ですが、状況的にこれはおこらないようにする
				//List<DataItemMaster> data_item_master = m_dbItemMaster.SelectAll ();
				List<CsvItemParam> data_item_master = DataManager.Instance.m_csvItem.list;
				//Debug.LogError (string.Format ("count:{0}", data_item_master.Count));
				if (data_item_master.Count == 0) {
					foreach (CsvItemParam csv_item_data in csvItem.All) {
						DataItemMaster data = new DataItemMaster (csv_item_data);
						if (data.open_item_id == 0) {
							data.status = 1;
						}
						m_dbItemMaster.Replace (data);
					}
				}
				*/
				m_eStep = STEP.INPUT_WAIT;
			} else {
				//Debug.Log ("wait");
			}

			break;

		case STEP.INPUT_WAIT:
			if (bInit) {
				m_btnStart.TriggerClear ();
			}
			if (true) {

				// とりあえず全部調べる
				List<DataWorkParam> cleared_work_list = m_dbWork.Select ( string.Format(" status = {0} " , (int)DefineOld.Work.STATUS.CLEARD ));
				foreach (DataWorkParam work in cleared_work_list) {
					List<CsvMonsterParam> list_monster = m_dbMonsterMaster.Select ( string.Format(" status = 0 and open_work_id = {0} " , work.work_id ));
					foreach (CsvMonsterParam data_monster_master in list_monster) {
						Dictionary< string , string > monster_master_dict = new Dictionary< string , string > ();
						monster_master_dict.Add ("status", "1");

						m_dbMonsterMaster.Update (monster_master_dict , string.Format( "monster_id = {0}" , data_monster_master.monster_id ) );
					}

				}
				m_btnStart.TriggerClear ();
				SceneManager.LoadScene ("park_main");
				//Application.LoadLevel ("park_main");
			}
			break;

		case STEP.DB_BACKUP_NOEXIST:
			if (bInit) {
				GameObject objOjisan = PrefabManager.Instance.MakeObject ("prefab/PrefOjisanCheck", m_posDisplay);
				m_ojisanCheck = objOjisan.GetComponent<CtrlOjisanCheck> ();
				m_ojisanCheck.Initialize ("バックアップファイルが\n存在しません", true);
			}
			if (m_ojisanCheck.IsYes ()) {
				Destroy (m_ojisanCheck.gameObject);
				m_eStep = STEP.IDLE;
			}
			break;
		case STEP.DB_BACKUP_CHECK:
			if (bInit) {
				GameObject objOjisan = PrefabManager.Instance.MakeObject ("prefab/PrefOjisanCheck", m_posDisplay );
				m_ojisanCheck = objOjisan.GetComponent<CtrlOjisanCheck> ();
				m_ojisanCheck.Initialize ("自動保存されたデータ\nを利用して\nバックアップを行います\n\nよろしいですか");
			}
			if (m_ojisanCheck.IsYes ()) {
				//SoundManager.Instance.PlaySE (SoundName.BUTTON_PUSH);
				Destroy (m_ojisanCheck.gameObject);
				m_eStep = STEP.DB_BACKUP;
			} else if (m_ojisanCheck.IsNo ()) {
				//SoundManager.Instance.PlaySE (SoundName.BUTTON_PUSH);
				Destroy (m_ojisanCheck.gameObject);
				m_eStep = STEP.IDLE;
			} else {
			}
			break;
		case STEP.DB_BACKUP:
			if (bInit) {
				GameObject objOjisan = PrefabManager.Instance.MakeObject ("prefab/PrefOjisanCheck", m_posDisplay);
				m_ojisanCheck = objOjisan.GetComponent<CtrlOjisanCheck> ();
				m_ojisanCheck.Initialize ("完了しました\nゲームをスタートしてください", true);

				string sourceDB = System.IO.Path.Combine (Application.persistentDataPath, DefineOld.DB_NAME_DOUBTSUEN );
				string dummyDB = System.IO.Path.Combine (Application.persistentDataPath, DefineOld.DB_NAME_DOUBTSUEN + "." + TimeManager.StrGetTime() );
				string backupDB = System.IO.Path.Combine (Application.persistentDataPath, DefineOld.DB_NAME_DOUBTSUEN_BK );
				string backup2DB = System.IO.Path.Combine (Application.persistentDataPath, DefineOld.DB_NAME_DOUBTSUEN_BK2 );
				if (System.IO.File.Exists (sourceDB)) {
					System.IO.File.Copy (sourceDB, dummyDB, true);
				}
				if (System.IO.File.Exists (backupDB)) {
					System.IO.File.Copy (backupDB, sourceDB, true);
				}
			}
			if (m_ojisanCheck.IsYes ()) {
				Destroy (m_ojisanCheck.gameObject);
				m_eStep = STEP.IDLE;
			}
			break;

		default:
			break;
		}

		if (m_btnTutorialReset.ButtonPushed) {
			PlayerPrefs.DeleteAll ();

			string full_path = System.IO.Path.Combine (Application.persistentDataPath , DefineOld.DB_NAME_DOUBTSUEN );
			System.IO.File.Delete( full_path );

			m_btnTutorialReset.TriggerClear ();
		}
		if (m_btnCacheClear.ButtonPushed) {
			Caching.CleanCache();
			m_btnCacheClear.TriggerClear ();
		}
	
	}
}

























