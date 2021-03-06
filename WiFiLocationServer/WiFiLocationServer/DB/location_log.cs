﻿using System;
using System.Data;
using System.Text;
using System.Collections.Generic;
using System.Data.SqlClient;
using WiFiLocationServer.Models;

namespace WiFiLocationServer.DB
{
    /// <summary>
    /// 数据访问类:手机型号表
    /// </summary>
    public class location_log
    {
        #region 公用方法=================================
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from tb_location_log");
            strSql.Append(" where id=@id ");
            SqlParameter[] parameters = {
                    new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Models.location_log model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into tb_location_log(actual_coord,location_coord,room_id,mobile_id,location_algorithm,location_time,flag,mobile_mac)");
            strSql.Append(" values (@actual_coord,@location_coord,@room_id,@mobile_id,@location_algorithm,@location_time,@flag,@mobile_mac)");
            SqlParameter[] parameters = {
                    new SqlParameter("@actual_coord", SqlDbType.VarChar,10),
                    new SqlParameter("@location_coord", SqlDbType.VarChar, 10),
                    new SqlParameter("@room_id",SqlDbType.Int),
                    new SqlParameter("@mobile_id",SqlDbType.Int),
                    new SqlParameter("@location_algorithm", SqlDbType.Int),
                    new SqlParameter("@location_time", SqlDbType.VarChar,50),
                    new SqlParameter("@flag",SqlDbType.Int),
                    new SqlParameter("@mobile_mac",SqlDbType.VarChar,20)
                                        };
            parameters[0].Value = model.actual_coord;
            parameters[1].Value = model.location_coord;
            parameters[2].Value = model.room_id;
            parameters[3].Value = model.mobile_id;
            parameters[4].Value = model.location_algorithm;
            parameters[5].Value = model.location_time;
            parameters[6].Value = model.flag;
            parameters[7].Value = model.mobile_mac;

            object obj = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            model.id = Convert.ToInt32(obj);

            return model.id;
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Models.location_log model)
        {
         
            return true;
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from tb_location_log ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
                    new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;

            int rowsAffected = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rowsAffected > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Models.location_log GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 id,model_name");
            strSql.Append(" from tb_location_log");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
                    new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;

            Models.location_log model = new Models.location_log();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return DataRowToModel(ds.Tables[0].Rows[0]);
            }
            else
            {
                return null;
            }
        }

        public DataSet GetlocationLogList(int room,int mobile,int algorithm)
        {
            string where1 = "room_id = " + room + " and mobile_id = " + mobile + " and location_algorithm = " + algorithm + " and memory is null and flag = 4";
            string where2 = "room_id = " + room + " and mobile_id = " + mobile;
            string sql = "select actual_coord,location_coord from tb_location_log where " + where1;
            sql += ";select coord from tb_wifi_fingerprint  where id in (select coord_id from tb_wifi_rssi where " + where2 + ")";
            return DbHelperSQL.Query(sql);
        }
        #endregion



        #region 私有方法=================================
        ///<summary>
        /// 将对象转换为实体
        /// </summary>
        private Models.location_log DataRowToModel(DataRow row)
        {
            Models.location_log model = new Models.location_log();
            if (row != null)
            {
                if (row["id"] != null && row["id"].ToString() != "")
                {
                    model.id = int.Parse(row["id"].ToString());
                }
             
            }
            return model;
        }
        #endregion


    }
}