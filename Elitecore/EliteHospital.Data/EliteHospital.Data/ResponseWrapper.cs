using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EliteHospital.Data
{
    public class ResponseWrapper<T>
    {
        public Result<T> Result { get; set; }
    }

    public class Result<T>: ResponseData
    {
        public List<T> Data { get; set; }
        
    }

    public class ResponseData
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}
