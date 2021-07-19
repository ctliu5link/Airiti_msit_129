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
        public string Topic { get; set; }
        public HtmlString Obj_JSON { get; set; }
        public string ReturnType { get; set; }
        public string Para { get; set; }
    }
}