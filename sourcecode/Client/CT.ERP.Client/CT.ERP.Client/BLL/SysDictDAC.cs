using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PES.DataModel;
using CT.ERP.Client.Entity;

namespace CT.ERP.Client.BLL
{
    public class SysDictDAC : BaseDAL<SysDictEntity>
    {
        public SysDictEntity Select(string dictype, string dictvalue)
        {

            Spec<SysDictEntity> where = new Spec<SysDictEntity>();
            where.And(p => p.dictype == dictype);
            where.And(p => p.dictvalue == dictvalue);

            return base.DMSelect(where.Exp);
        }

        public int Add(SysDictEntity entity)
        {
            return base.DMInsert(entity);
        }

        public List<SysDictEntity> SelectList(string dictype)
        {
            return base.DMSelectList(50, p => p.dictype == dictype,p=>p.dicid.Asc());
        }

    }
}
