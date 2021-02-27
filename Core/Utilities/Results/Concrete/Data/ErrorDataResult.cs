using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    public class ErrorDataResult<T>:DataResult<T>
    {
        //data -message - true 
        public ErrorDataResult(T data, string message) : base(data, false, message)
        {

        }

        //data - true
        public ErrorDataResult(T data) : base(data, false)
        {

        }

        //data default olarak dönmek - sadece message geçebilirz
        public ErrorDataResult(string message) : base(default, false, message)
        {

        }
        public ErrorDataResult() : base(default, false)
        {

        }
    }
}
