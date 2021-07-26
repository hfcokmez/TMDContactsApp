using Core.Entities;
using System;
using System.Runtime.CompilerServices;

namespace Core.Utilities.Services
{
    public class ObjectBindHelper
    {
        public static T2 ObjectToObject<T1, T2>(T1 from, T2 to)
            where T1 : class, IEntity, new()
        where T2 : class, IEntity, new()
        {
            foreach (var property1 in to.GetType().GetProperties())
            {
                if (property1.GetValue(to, null) == null)
                {
                    foreach (var property2 in from.GetType().GetProperties())
                    {
                        if (String.Equals(property1.Name, property2.Name))
                        {
                            property1.SetValue(to, property2.GetValue(from, null));
                        }
                    }
                }
            }
            return to;
        }
    }
}
