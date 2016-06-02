using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using TemplateDesigner.Models;

namespace TemplateDesigner.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IUserService" in both code and config file together.
    [ServiceContract]
    public interface IUserService
    {
        [OperationContract]
        bool IsUserLogined(int Mode);
        [OperationContract]
        bool UserLogin(string UserName, string UserPassword, int Mode);
        [OperationContract]
        List<UserImages> GetUserImages(int Mode);
      
    }
}
