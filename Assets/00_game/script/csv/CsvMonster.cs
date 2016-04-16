using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

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


	public int monster_id { get; set; }
	public string name { get; set; }
	public string description_cell { get; set; }
	public int cost { get; set; }
	public int fill { get; set; }
	public int dust { get; set; }
	public int coin { get; set; }
	public int ticket { get; set; }
	public int revenew_coin { get; set; }
	public int revenew_exp { get; set; }
	public int revenew_interval { get; set; }
	public int open_work_id { get; set; }
	public string description_book { get; set; }
	public int size { get; set; }
	public int rare { get; set; }
	public int status { get{ return m_status;} set{m_status = value; } }





}



public class CsvMonster : CsvData<CsvMonsterParam> {

	private static readonly string FilePath = "csv/monster";
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


