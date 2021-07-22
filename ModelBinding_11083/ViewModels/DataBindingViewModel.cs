using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace ModelBinding_11083.ViewModels
{
  public class DataBindingViewModel
  {
    public DataBindingViewModel(string p_topic, string p_returnType, string p_para, object p_object)
    {
      Topic = p_topic;
      Obj_JSON = new HtmlString(JsonConvert.SerializeObject(p_object));
      ReturnType = p_returnType;
      Para = p_para;
    }
    /// <summary>
    /// 不同傳入參數條件的主題名稱
    /// </summary>
    public string Topic { get; set; }
    /// <summary>
    /// 前端要傳回後端的物件json字串
    /// </summary>
    public HtmlString Obj_JSON { get; set; }
    /// <summary>
    /// 接到前端物件後傳回前端的資料型態
    /// 1. json, 2. text
    /// </summary>
    public string ReturnType { get; set; }
    /// <summary>
    /// 後端action的參數的內容
    /// </summary>
    public string Para { get; set; }
  }
}