using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    public class SuccessDataResult<T>:DataResult<T>
    {
        //data -message - true 
        public SuccessDataResult(T data, string message):base(data,true,message)
        {

        }

        //data - true
        public SuccessDataResult(T data):base(data,true)
        {

        }

        //data default olarak dönmek - sadece message geçebilirz
        public SuccessDataResult(string message):base(default,true,message)
        {

        }
        public SuccessDataResult():base(default,true)
        {

        }
    }
}
