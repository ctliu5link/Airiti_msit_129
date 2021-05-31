using SchemaNote_11083.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchemaNote_11083.ViewModels
{
    //public class TtableSchema 
    //{
    //    public TtableSchema(Ttable[] p_t)
    //    {
    //        this.Ttable = p_t;
    //    }
    //    public Ttable[] Ttable { get; set; }
    //}

    public class TtableSchema<T> //傳到cshtml用的viewmodel;泛型
    {
        public TtableSchema(T p_t)
        {
            this.Ttable = p_t;
        }
        public T Ttable { get; set; }
    }

    //public class TtableSchema_dapper //傳到cshtml用的viewmodel
    //{
    //    public TtableSchema_dapper(IEnumerable<Ttable_dapper> p_t)
    //    {
    //        this.Ttable = p_t;
    //    }
    //    public IEnumerable<Ttable_dapper> Ttable { get; set; }
    //}
}
