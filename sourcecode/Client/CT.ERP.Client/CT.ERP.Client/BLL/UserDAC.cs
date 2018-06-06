using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PES.DataModel;
using CT.ERP.Client.Entity;

namespace CT.ERP.Client.BLL
{
    public class UserDAC : BaseDAL<User>
    {

        public int ChangePwd(int userId, string sPwd)
        {
            string strSql="update ct_user set password='" + sPwd + "' where userid=" + userId;
            return DMContext.TSqlCommand(strSql).ExecuteNonQuery();
        }
        
        public int Delete(int userId)
        {
            return base.DMDelete(p => p.userid == userId);
        }

        public int Add(User user)
        {
            return base.DMInsert(user);
        }

        public int Update(User user) 
        {
            return base.DMUpdate(user, p => p.userid == user.userid);
        }

        public List<User> SelectAll()
        {
            return base.DMSelectAll();
        }

        public User Select(string loginId,int userId)
        {
            if(userId==0)
                return base.DMSelect(p => p.loginid == loginId);
            else
            {
                Spec<User> where = new Spec<User>();
                where.And(p => p.loginid == loginId);
                where.And(p => p.userid!=userId);
                return base.DMSelect(where.Exp);
            }
        }

        public User Select(int userId)
        {
            return base.DMSelect(p => p.userid == userId);
        }
        

    }
}
