using DevExpress.DataAccess.Web;
using System;
using System.Collections.Generic;

namespace DXWebApplication1.Services
{
    public class ObjectDataSourceWizardCustomTypeProvider : IObjectDataSourceWizardTypeProvider
    {
        public IEnumerable<Type> GetAvailableTypes(string context)
        {
            return new[] {
                typeof(Employees.DataSource),
                typeof(Country.DataSource)
            };
        }
    }
}

//using DevExpress.DataAccess.Web;
//using System;
//using System.Collections.Generic;

//namespace DXWebApplication1.Services {
//    public class ObjectDataSourceWizardCustomTypeProvider : IObjectDataSourceWizardTypeProvider {
//        public IEnumerable<Type> GetAvailableTypes(string context) {

//            //return new[] { typeof(Employees.DataSource) };

//            if (context == "Employee")
//                return new[] { typeof(Employees.DataSource) };
//            else if (context == "CountryData")
//                return new[] { typeof(CountryData.DataSource) };
//            else
//                return null;
//        }

//        //public IEnumerable<Type> GetAvailableTypes(string context){
//        //    return new[] { typeof(CountryData.DataSource) };
//        //}
//    }
//}