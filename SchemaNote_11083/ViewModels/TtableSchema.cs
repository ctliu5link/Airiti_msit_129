using SchemaNote_11083.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchemaNote_11083.ViewModels
{

  public class TtableSchema<T> //傳到cshtml用的viewmodel;泛型
  {
    private bool isNull;
    private T _ttable;

    public TtableSchema()
    {
      this.isNull = true;
    }
    public TtableSchema(T p_t,string Method)
    {
      this._ttable = p_t;
      this.isNull = false;
      this.Method = Method;
    }
    public T Ttable
    {
      get { return this._ttable; }
      set
      {
        this._ttable = value;
        this.isNull = value == null ? true : false;
      }
    }

    public string Method { get; set; }

    public bool IsNull { get { return this.isNull; } }
  }
}
