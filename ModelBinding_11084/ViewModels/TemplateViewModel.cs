using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ModelBinding_11084.ViewModels
{
    public class TemplateViewModel
    {
        /// <summary>
        /// 繫結類型
        /// </summary>
        public string bindingType { get; set; }
        /// <summary>
        /// JSON value
        /// </summary>
        public HtmlString objJSON { get; set; }
        /// <summary>
        /// Action參數
        /// </summary>
        public string actionParameter { get; set; }
        /// <summary>
        /// 返回類型
        /// </summary>
        public string returnType { get; set; }
        /// <summary>
        /// 傳給前端的 List<TemplateViewModel>
        /// </summary>
        public static List<TemplateViewModel> lisTemplate = new List<TemplateViewModel>();
        /// <summary>
        /// 建構式
        /// </summary>
        /// <param name="_bindingType"></param>
        /// <param name="_actionParameter"></param>
        /// <param name="_object"></param>
        /// <param name="_returnType"></param>
        public TemplateViewModel(string _bindingType, string _actionParameter, object _object, string _returnType)
        {
            bindingType = _bindingType;
            actionParameter = _actionParameter;
            objJSON = new HtmlString(JsonConvert.SerializeObject(_object));            
            returnType = _returnType;

            lisTemplate.Add(this);
        }
        
    }
}