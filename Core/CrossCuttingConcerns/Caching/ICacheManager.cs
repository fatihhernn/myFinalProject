using System;
using System.Collections.Generic;
using System.Text;

namespace Core.CrossCuttingConcerns.Caching
{
    //redis - elastic-search log gibi cache araçları kullanabiliriz
    public interface ICacheManager
    {
        //farklı veri tipleri döndür, hangi tipte istediğimizi isteyeceğiz
        T Get<T>(string key); 

        object Get(string key);

        void Add(string key,object value, int duration);

        //cache eklerken bunu cacheden mi veritabanından mı getirelim?? cache de var mı???
        bool IsAdd(string key); 

        //uçurma işlemleri
        void Remove(string key);

        //içinde get olanları uçur gibi
        void RemoveByPattern(string pattern);
    }
}
