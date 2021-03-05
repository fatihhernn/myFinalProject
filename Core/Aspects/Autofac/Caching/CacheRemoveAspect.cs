using Core.CrossCuttingConcerns.Caching;
using Core.Utilities.Interceptos;
using Core.Utilities.IoC;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Castle.DynamicProxy;

namespace Core.Aspects.Autofac.Caching
{
    //veriyi manupile edenen metoda CacheRemoveAspect uygularız
    public class CacheRemoveAspect : MethodInterception
    {
        private string _pattern;
        private ICacheManager _cacheManager;

        public CacheRemoveAspect(string pattern)
        {
            _pattern = pattern;
            _cacheManager = ServiceTool.ServiceProvider.GetService<ICacheManager>();
        }
        //add işlemi hata verecek yeni metod ekleyemezse bunu çalıştırma. Success olduğunda ekle.
        protected override void OnSuccess(IInvocation invocation)
        {
            //pattern'a göre cache silme işlemi yapıyor
            _cacheManager.RemoveByPattern(_pattern);
        }
    }
}
