using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Interceptos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Aspects.Autofac.Validation
{
    public class ValidationAspect : MethodInterception//aspect => metodun başında veya sonunda çalışacak şey - ezeceğin metoda bağlı
    {
        
        private Type _validatorType;
        public ValidationAspect(Type validatorType) //bana bir validator type ver diyoruz => o tipi atribute de typeOf(ProductValidator) olarak ver.
        {

            //defensive coding => type of un içine başka class yazmasın diye kotrol et
            if (!typeof(IValidator).IsAssignableFrom(validatorType)) //typeOf(ProductValidator) attributunde başka class vermemek için yapıyoruz bunu
            {
                throw new Exception("Bu bir doğrulama sınıfı değildir");
            }

            _validatorType = validatorType;
        }
        protected override void OnBefore(IInvocation invocation)
        {
            //type of içinde ProductValidator geliyor fakat newlenmeden geliyor. onu newlemek için reflection yapıyorum => new ProductValidator() 
            var validator = (IValidator)Activator.CreateInstance(_validatorType); 

            //ProductValidator base'indeki(abstractValidator<Product>) argümanı getir => Product tipini yakaladım
            var entityType = _validatorType.BaseType.GetGenericArguments()[0];

            //aspect'in metodu (Add()) içerisindeki parametreleri dolaş, eğer yakarıdaki kodda yakaladığım tip, metodtaki parametreye eş ise Validate et..
            var entities = invocation.Arguments.Where(t => t.GetType() == entityType);

            foreach (var entity in entities)
            {
                ValidationTool.Validate(validator, entity);
            }
        }
    }
}
