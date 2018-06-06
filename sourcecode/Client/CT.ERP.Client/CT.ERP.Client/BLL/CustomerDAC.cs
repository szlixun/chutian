using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PES.DataModel;
using CT.ERP.Client.Entity;

namespace CT.ERP.Client.BLL
{
    public class CustomerDAC : BaseDAL<CustomerEntity>
    {
        public int Add(CustomerEntity entity)
        {
            return base.DMInsert(entity);
        }

        public List<CustomerEntity> SelectAll() 
        {
            return base.DMSelectList(100, null, p => p.customer.Asc());
        }

        public CustomerEntity Select(string customer) 
        {
            return base.DMSelect(p => p.customer == customer);
        }

        public int Update(CustomerEntity entity)
        {
            return base.DMUpdate(entity, p => p.cid == entity.cid);
        }
    }
}
