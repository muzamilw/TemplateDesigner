using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TemplateDesigner.Models;


namespace TemplateDesigner.Services
{
    public class AppGlobalData
    {

        public AppGlobalData()
        {
            //
            // TODO: Add constructor logic here
            //
        }


        private Users _user;
        public Users user
        {
            get { return this._user; }
            set { this._user = value; }
        }

        private Int32 _CountryID = 0;
        public int CountryID
        {
            get { return _CountryID; }
            set { _CountryID = value; }
        }

        private bool _ISCooperateCustomer = false;
        public bool ISCooperateCustomer
        {
            get { return _ISCooperateCustomer; }
            set { _ISCooperateCustomer = value; }
        }

        private bool _ISNormalPaymentCustomer = false;
        public bool ISNormalPaymentCustomer
        {
            get { return _ISNormalPaymentCustomer; }
            set { _ISNormalPaymentCustomer = value; }
        }

        private int _WebUrlCustomerId = 0;
        public int WebUrlCustomerId
        {
            get { return _WebUrlCustomerId; }
            set { _WebUrlCustomerId = value; }
        }

        //private printdesignBLL.Currencies _Currencies;
        //public printdesignBLL.Currencies Currencies
        //{
        //    get { return this._Currencies; }
        //    set { this._Currencies = value; }
        //}
    }
}