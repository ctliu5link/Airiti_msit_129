using SchemaNote_11084.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchemaNote_11084.ViewModel
{
    public class CResultViewModel
    {
        private TResult iv_result = null;
        public CResultViewModel(TResult r)
        {
            iv_result = r;
        }
    }
}
