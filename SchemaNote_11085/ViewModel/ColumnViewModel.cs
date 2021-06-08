using SchemaNote_A11085.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SchemaNote_A11085.ViewModel
{
    public class ColumnViewModel
    {

        private Combine iv_airititable = null;
        public Combine schairitable  { get { return iv_airititable; } }

        public ColumnViewModel(Combine A)
        {
            iv_airititable =A;
        }
        public ColumnViewModel()
        {
            iv_airititable = new Combine();
        }
        //[DisplayName("欄位名稱")]
        //public string Column_Name { get { return iv_airititable.a.Column_Name; } set { iv_airititable.a.Column_Name = value; } }
        //[DisplayName("欄位說明")]
        //public string Column_Description { get { return iv_airititable.a.Column_Description; } set { iv_airititable.a.Column_Description = value; } }
        //[DisplayName("主鍵")]
        //public int Column_PK { get { return iv_airititable.a.Column_PK; } set { iv_airititable.a.Column_PK = value; } }
        //[DisplayName("資料型態")]
        //public string Column_Type { get { return iv_airititable.a.Column_Type; } set { iv_airititable.a.Column_Type = value; } }
        //[DisplayName("不為Null")]
        //public int Column_IsNullable { get { return iv_airititable.a.Column_IsNullable; } set { iv_airititable.a.Column_IsNullable = value; } }
        //[DisplayName("預設值")]
        //public string Column_Default { get { return iv_airititable.a.Column_Default; } set { iv_airititable.a.Column_Default = value; } }
        //[DisplayName("備註")]
        //public string Column_Remark { get { return iv_airititable.a.Column_Remark; } set { iv_airititable.a.Column_Remark = value; } }

        //public string TableName { get { return iv_airititable.b.TableName; } set { iv_airititable.b.TableName = value; } }
        //[DisplayName("表欄位說明")]
        //public string Main_UserTable { get { return iv_airititable.b.Main_UserTable; } set { iv_airititable.b.Main_UserTable = value; } }      
        //public string DescriptionName { get { return iv_airititable.b.DescriptionName; } set { iv_airititable.b.DescriptionName = value; } }
        //public string Object_CreateDay { get { return iv_airititable.b.Object_CreateDay; } set { iv_airititable.b.Object_CreateDay = value; } }
        //public string Object_UpdateDay { get { return iv_airititable.b.Object_UpdateDay; } set { iv_airititable.b.Object_UpdateDay = value; } }
        //public string TotalCount { get { return iv_airititable.b.TotalCount; } set { iv_airititable.b.TotalCount = value; } }
        //[DisplayName("表備註")]
        //public string Remark { get { return iv_airititable.b.Remark; } set { iv_airititable.b.Remark = value; } }

        //public string Account { get { return iv_airititable.Account; } set { iv_airititable.Account = value; } }

        //public string Account_ETDS { get { return iv_airititable.Account_ETDS; } set { iv_airititable.Account_ETDS = value; } }

        //public string Account_SchInfo { get { return iv_airititable.Account_SchInfo; } set { iv_airititable.Account_SchInfo = value; } }

        //public string Account_SchInfoRefuse { get { return iv_airititable.Account_SchInfoRefuse; } set { iv_airititable.Account_SchInfoRefuse = value; } }

        //public string AccountRefuse { get { return iv_airititable.AccountRefuse; } set { iv_airititable.AccountRefuse = value; } }

        //private AiritiTable iv_airitiTablet = null;
        //public AiritiTable airitiTable { get { return iv_airitiTable; } }
        //public CAccountViewModel(AiritiTable a)
        //{
        //    iv_airitiTable = a;
        //}
        //public CAccountViewModel()
        //{
        //    iv_airitiTable = new AiritiTable();
        //}

        //public string TABLE_NAME { get { return iv_airitiTable.TABLE_NAME; } set { iv_airitiTable.TABLE_NAME = value; } }


        //[Required(ErrorMessage = "")]
        //[DisplayName("")]
        //public string AccountId 
        //{
        //    get { return iv_account.AccountId; }
        //    set { iv_account.AccountId = value; }
        //}

        //[Required(ErrorMessage = "")]
        //[DisplayName("")]
        //public string AccountPassword
        //{
        //    get { return iv_account.AccountPassword; }
        //    set { iv_account.AccountPassword = value; }
        //}
        //public string ContactEmail
        //{
        //    get { return iv_account.ContactEmail; }
        //    set { iv_account.ContactEmail = value; }
        //}
        //public string CustomerId
        //{
        //    get { return iv_account.CustomerId; }
        //    set { iv_account.CustomerId = value; }
        //}
        //public string CustomerFrom
        //{
        //    get { return iv_account.CustomerId; }
        //    set { iv_account.CustomerId = value; }
        //}
        //public string RegisterDate
        //{
        //    get { return iv_account.RegisterDate; }
        //    set { iv_account.RegisterDate = value; }
        //}
        //public string AccountType
        //{
        //    get { return iv_account.AccountType; }
        //    set { iv_account.AccountType = value; }
        //}
        //public string AccountStatus
        //{
        //    get { return iv_account.AccountStatus; }
        //    set { iv_account.AccountStatus = value; }
        //}
        //public string AccountSdate
        //{
        //    get { return iv_account.AccountSdate; }
        //    set { iv_account.AccountSdate = value; }
        //}
        //public string AccountEdate
        //{
        //    get { return iv_account.AccountEdate; }
        //    set { iv_account.AccountEdate = value; }
        //}
        //public string RegCustomerId
        //{
        //    get { return iv_account.RegCustomerId; }
        //    set { iv_account.RegCustomerId = value; }
        //}
        //public string VerifyId
        //{
        //    get { return iv_account.VerifyId; }
        //    set { iv_account.VerifyId = value; }
        //}
        //public string AppsyncStatus
        //{
        //    get { return iv_account.AppsyncStatus; }
        //    set { iv_account.AppsyncStatus = value; }
        //}
        //public int? GracePeriod
        //{
        //    get { return iv_account.GracePeriod; }
        //    set { iv_account.GracePeriod = value; }
        //}
        //public string Sort
        //{
        //    get { return iv_account.Sort; }
        //    set { iv_account.Sort = value; }
        //}
    }

    
}
