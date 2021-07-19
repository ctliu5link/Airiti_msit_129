using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchemaNote_11083.Models
{
  public static class SQLCommand
  {
    public static string schemaCommand()
    {
      return @"select st.name as Table_Name " +
                    ",ic.TABLE_SCHEMA as Table_Schema " +
                    ",st.create_date as Create_Date " +
                    ",st.modify_date as Modify_Date " +
                    ",sp.rows as Total_Rows " +
                    ",sc.name as Column_Name " +
                    ",case when ISNULL(ik.COLUMN_NAME,'') = '' then '' " +
                    "else 'Y' " +
                    "end PK " +
                    ",ic.IS_NULLABLE as IS_Nullable " +
                    ",ic.DATA_TYPE + case " +
                    "when ISNULL(ic.CHARACTER_MAXIMUM_LENGTH,'')='' then '' " +
                    "else '(' + cast(ic.CHARACTER_MAXIMUM_LENGTH as varchar) + ')' " +
                    "end Data_Type " +
                    ",ISNULL(ic.COLUMN_DEFAULT,'') as Column_Default " +
                    ",ISNULL(epcd.value,'') as Description " +
                    ",ISNULL(epcr.value,'') as REMARK " +
                    ",ISNULL(eptd.value,'') as Table_Description " +
                    ",ISNULL(eptr.value,'') as Table_REMARK " +
              "from sys.tables st " +
              "inner join sys.columns sc " +
              "on st.object_id = sc.object_id " +
              "inner join sys.partitions sp " +
              "on st.object_id = sp.object_id " +
              "and sp.index_id in (0,1) " +
              "left join INFORMATION_SCHEMA.COLUMNS ic " +
              "on ic.TABLE_NAME = st.name " +
              "and ic.COLUMN_NAME = sc.name " +
              "left join sys.extended_properties epcd " +
              "on epcd.major_id = st.object_id " +
              "and epcd.minor_id = sc.column_id " +
              "and epcd.name ='MS_Description' " +
              "left join sys.extended_properties epcr " +
              "on epcr.major_id = st.object_id " +
              "and epcr.minor_id = sc.column_id " +
              "and epcr.name ='REMARK' " +
              "left join sys.extended_properties eptd " +
              "on eptd.major_id = st.object_id " +
              "and eptd.minor_id = 0 " +
              "and eptd.name ='MS_Description' " +
              "left join sys.extended_properties eptr " +
              "on eptr.major_id = st.object_id " +
              "and eptr.minor_id = 0 " +
              "and eptr.name ='REMARK' " +
              "left join INFORMATION_SCHEMA.KEY_COLUMN_USAGE ik " +
              "on st.name = ik.TABLE_NAME " +
              "and sc.name = ik.COLUMN_NAME " +
              "and left(ik.CONSTRAINT_NAME,2)='PK' " +
              "order by st.name,sc.column_id " +
              "select st.name as Table_Name " +
                    ",count(st.name) as Column_qty " +
              "from sys.tables st " +
              "inner join INFORMATION_SCHEMA.COLUMNS ic " +
              "on st.name = ic.TABLE_NAME " +
              "group by st.name";
    }


    public static string dapper_getTableColumns()
    {
      return "select sc.name as Column_Name " +
                    ",case when ISNULL(ik.COLUMN_NAME,'') = '' then 0 " +
                    "else 1 " +
                    "end PK " +
                    ",case when ic.IS_NULLABLE ='YES' then 1 " +
                    "else 0 " +
                    "end IS_Nullable " +
                    ",ic.DATA_TYPE + case " +
                    "when ISNULL(ic.CHARACTER_MAXIMUM_LENGTH,'')='' then '' " +
                    "else '(' + cast(ic.CHARACTER_MAXIMUM_LENGTH as varchar) + ')' "+
                    "end Data_Type " +
                    ",ISNULL(ic.COLUMN_DEFAULT,'') as Column_Default " +
                    ",ISNULL(epcd.value,'') as Description " +
                    ",ISNULL(epcr.value,'') as REMARK " +
             "from sys.tables st " +
             "inner join sys.columns sc " +
             "on st.object_id = sc.object_id " +
             "left join INFORMATION_SCHEMA.COLUMNS ic " +
             "on ic.TABLE_NAME = st.name " +
             "and ic.COLUMN_NAME = sc.name " +
             "left join sys.extended_properties epcd " +
             "on epcd.major_id = st.object_id " +
             "and epcd.minor_id = sc.column_id " +
             "and epcd.name ='MS_Description' " +
             "left join sys.extended_properties epcr " +
             "on epcr.major_id = st.object_id " +
             "and epcr.minor_id = sc.column_id " +
             "and epcr.name ='REMARK' " +
             "left join INFORMATION_SCHEMA.KEY_COLUMN_USAGE ik " +
             "on st.name = ik.TABLE_NAME " +
             "and sc.name = ik.COLUMN_NAME " +
             "and left(ik.CONSTRAINT_NAME,2)='PK' " +
             "where st.name = @Table_Name " +
             "order by sc.column_id";
    }
    public static string dapper_getAllTables()
    {
      return "select st.name as Table_Name " +
                    ",ist.TABLE_SCHEMA as Table_Schema " +
                    ",st.create_date as Create_Date " +
                    ",st.modify_date as Modify_Date " +
                    ",sp.rows as Total_Rows " +
                    ",ISNULL(eptd.value,'') as Table_Description " +
                    ",ISNULL(eptr.value,'') as Table_REMARK " +
             "from sys.tables st " +
             "inner join INFORMATION_SCHEMA.TABLES ist " +
             "on st.name = ist.TABLE_NAME " +
             "inner join sys.partitions sp " +
             "on st.object_id = sp.object_id " +
             "and sp.index_id in (0,1) " +
             "left join sys.extended_properties eptd " +
             "on eptd.major_id = st.object_id " +
             "and eptd.minor_id = 0 " +
             "and eptd.name ='MS_Description' " +
             "left join sys.extended_properties eptr " +
             "on eptr.major_id = st.object_id " +
             "and eptr.minor_id = 0 " +
             "and eptr.name ='REMARK' " +
             "order by st.name";
    }
  }
}
