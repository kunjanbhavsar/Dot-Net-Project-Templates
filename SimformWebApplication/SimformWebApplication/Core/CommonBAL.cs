using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BarberShopDALBAL.BAL
{
    public class CommonBAL
    {
        /// <summary>
        /// Author : Brijesh Mandaliya
        /// Usage : Insert object with passed optional required parameter like insertDate, isDeleted
        /// </summary>
        public T Insert<T>(T obj) where T : class
        {
            try
            {
                // Uncomment code and replace BarberShopEntities to your project entityName... And Comment return null line at the and of methods.
                //using (var DataContext = new BarberShopEntities())
                //{
                //    var os = DataContext.CreateObjectSet<T>();

                //    System.Reflection.PropertyInfo[] properties = typeof(T).GetProperties();

                //    foreach (var property in properties)
                //    {
                //        if (property.Name == "insertDate")
                //            property.SetValue(obj, DateTime.Now.ToUniversalTime(), null);
                //        else if (property.Name == "isDeleted")
                //            property.SetValue(obj, false, null);
                //    }

                //    os.AddObject(obj);
                //    DataContext.SaveChanges();

                //    return obj;
                //}
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Author : Brijesh Mandaliya
        /// Usage : Update object with passed required parameter. Don't pass updateDate
        /// </summary>
        public T Update<T>(T obj) where T : class
        {
            try
            {
                // Uncomment code and replace BarberShopEntities to your project entityName... And Comment return null line at the and of methods.
                //using (var DataContext = new BarberShopEntities())
                //{
                //    var os = DataContext.CreateObjectSet<T>();

                //    string primaryKeyName = os.EntitySet.ElementType.KeyMembers[0].ToString();
                //    object primaryKeyValue = obj.GetType().GetProperty(primaryKeyName).GetValue(obj, null); 

                //    System.Reflection.PropertyInfo[] properties = typeof(T).GetProperties();

                //    foreach (var property in properties)
                //    {
                //        if (property.Name == "updateDate")
                //            property.SetValue(obj, DateTime.Now.ToUniversalTime(), null);
                //    }
                //    var contextObj = os.Where("it." + primaryKeyName + " = " + primaryKeyValue).FirstOrDefault();
                    
                //    os.Attach(contextObj);
                //    os.ApplyCurrentValues(obj);

                //    DataContext.SaveChanges();

                //    return obj;
                //}
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Author : Brijesh Mandaliya
        /// Usage : soft delete object pass key value
        /// </summary>
        public T SoftDelete<T>(int primaryKeyValue) where T : class
        {
            try
            {
                // Uncomment code and replace BarberShopEntities to your project entityName and also passed primaryKeyId value... And Comment return null line at the and of methods.
                //using (var DataContext = new BarberShopEntities())
                //{
                //    var os = DataContext.CreateObjectSet<T>();

                //    string primaryKeyName = os.EntitySet.ElementType.KeyMembers[0].ToString();

                //    var contextObj = os.Where("it." + primaryKeyName + " = " + primaryKeyValue + " && " + " it.isDeleted = " + false).SingleOrDefault();

                //    if (contextObj != null)
                //    {
                //        System.Reflection.PropertyInfo[] properties = typeof(T).GetProperties();

                //        foreach (var property in properties)
                //        {
                //            if (property.Name == "deleteDate")
                //                property.SetValue(contextObj, DateTime.Now.ToUniversalTime(), null);

                //            else if (property.Name == "isDeleted")
                //                property.SetValue(contextObj, true, null);
                //        }
                //        DataContext.SaveChanges();
                //    }
                //    return contextObj;
                //}
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Author : Brijesh Mandaliya
        /// Usage : Delete object with passed entity object 
        /// </summary>
        public void HardDelete<T>(T obj) where T : class
        {
            try
            {
                // Uncomment code and replace BarberShopEntities to your project entityName... And Comment return null line at the and of methods.
                //using (var DataContext = new BarberShopEntities())
                //{
                //    var os = DataContext.CreateObjectSet<T>();

                //    os.DeleteObject(obj);

                //    DataContext.SaveChanges();
                //}
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Author : Brijesh Mandaliya
        /// Usage : soft delete object pass key name any value
        /// </summary>
        public void HardDelete<T>(int primaryKeyValue) where T : class
        {
            try
            {
                // Uncomment code and replace BarberShopEntities to your project entityName and also passed primaryKeyId value... And Comment return null line at the and of methods.
                //using (var DataContext = new BarberShopEntities())
                //{
                //    var os = DataContext.CreateObjectSet<T>();

                //    string primaryKeyName = os.EntitySet.ElementType.KeyMembers[0].ToString();

                //    var contextObj = os.Where("it." + primaryKeyName + " = " + primaryKeyValue + " && " + " it.isDeleted = " + false).SingleOrDefault();

                //    if (contextObj != null)
                //    {
                //        os.DeleteObject(contextObj);
                //        DataContext.SaveChanges();
                //    }
                //}
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Author : Brijesh Mandaliya
        /// Usage : Get unique object or table primary key based object
        /// </summary>
        public static T GetUniqueObject<T>(int primaryKeyValue) where T : class
        {
            try
            {
                // Uncomment code and replace BarberShopEntities to your project entityName and also pass Primary key value ... And Comment return null line at the and of methods.
                //using (var DataContext = new BarberShopEntities())
                //{
                //    var os = DataContext.CreateObjectSet<T>();

                //    string primaryKeyName = os.EntitySet.ElementType.KeyMembers[0].ToString();

                //    var contextObj = os.Where("it." + primaryKeyName + " = " + primaryKeyValue + " && " + " it.isDeleted = " + false).SingleOrDefault();

                //    return contextObj;
                //}
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Author : Brijesh Mandaliya
        /// Usage : Get all objects which passed entity
        /// </summary>
        public static IEnumerable<T> GetAllObjects<T>() where T : class
        {
            try
            {
                // Uncomment code and replace BarberShopEntities to your project entityName... And Comment return null line at the and of methods.
                //using (var DataContext = new BarberShopEntities())
                //{

                //    var _objectSet = DataContext.CreateObjectSet<T>();

                //    var contextObj = _objectSet.Where(" it.isDeleted = " + false);

                //    return contextObj.AsEnumerable();
                //}
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        ///// <summary>
        ///// Author : Brijesh Mandaliya
        ///// Usage : Get first or default object with passed search criteria
        ///// </summary>
        //public static T FirstOrDefault123<T>(Func<T, bool> predicate) where T : class
        //{

        //    try
        //    {
        //        using (var DataContext = new BarberShopEntities())
        //        {

        //            var _objectSet = DataContext.CreateObjectSet<T>();

        //            //return _objectSet.First<T>(predicate);

        //            var Obj = _objectSet.Where("it.isDeleted = " + false).FirstOrDefault<T>(predicate);

        //            return Obj;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
    }
}
