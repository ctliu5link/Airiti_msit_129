namespace SchemaNote_11087.Models
{

    public class TablePropertyEditModel
    {
        public string TableName { get; set; } //資料表名稱
        public string ColumnName { get; set; } //欄位名稱
        public string Item { get; set; } //欄位擴充屬性'名稱'
        public string Value { get; set; } //欄位擴充屬性'值'
        public string TableMSDescription { get; set; }//資料表擴充屬性'名稱'
        public string TableRemark { get; set; }//資料表擴充屬性'值'

    }
}
