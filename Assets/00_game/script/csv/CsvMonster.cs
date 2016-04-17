using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

[System.Serializable]
public class CsvMonsterParam : CsvDataParam
{
	public int m_monster_id;
	public string m_name;
	public string m_description_cell;
	public int m_cost;
	public int m_fill;
	public int m_dust;
	public int m_coin;
	public int m_ticket;
	public int m_revenew_coin;
	public int m_revenew_exp;
	public int m_revenew_interval;
	public int m_open_work_id;
	public string m_description_book;
	public int m_size;
	public int m_rare;
	public int m_status;


	public int monster_id { get{ return m_monster_id; } set{m_monster_id= value;} }
	public string name { get{ return m_name; } set{m_name= value;} }
	public string description_cell { get{ return m_description_cell; } set{m_description_cell= value;} }
	public int cost { get{ return m_cost; } set{m_cost= value;} }
	public int fill { get{ return m_fill; } set{m_fill= value;} }
	public int dust { get{ return m_dust; } set{m_dust= value;} }
	public int coin { get{ return m_coin; } set{m_coin= value;} }
	public int ticket { get{ return m_ticket; } set{m_ticket= value;} }
	public int revenew_coin { get{ return m_revenew_coin; } set{m_revenew_coin= value;} }
	public int revenew_exp { get{ return m_revenew_exp; } set{m_revenew_exp= value;} }
	public int revenew_interval { get{ return m_revenew_interval; } set{m_revenew_interval= value;} }
	public int open_work_id { get{ return m_open_work_id; } set{m_open_work_id= value;} }
	public string description_book { get{ return m_description_book; } set{m_description_book= value;} }
	public int size { get{ return m_size; } set{m_size= value;} }
	public int rare { get{ return m_rare; } set{m_rare= value;} }
	public int status { get{ return m_status;} set{m_status = value; } }





}


[System.Serializable]
public class CsvMonster : CsvData<CsvMonsterParam> {

	public const string FilePath = "csv/monster";
	public void Load() {
		Load (FilePath);
	}

	public CsvMonsterParam Select( int _iMonsterId ){
		return SelectOne (string.Format (" monster_id = {0} ", _iMonsterId));
	}

	public int Update( int _iMonsterId , Dictionary< string , string >  _dictUpdate ){
		return base.Update (_dictUpdate, string.Format (" monster_id = {0} ", _iMonsterId));
	}




}


