using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bt1
{
    internal class Globals
    {
        public static int GlobalUserId { get; private set; }
        public static string GlobalUserName { get; private set; }
        public static string GlobalRole { get ; private set; }
        public static string GlobalEmail { get; private set; }


        public static void SetGlobalUserInfo(int UserID,string Username,string Role, string email)
        {
            GlobalUserId = UserID;
            GlobalUserName = Username;  
            GlobalRole = Role;
            GlobalEmail = email;
        }
        public static void ClearSession()
        {
            GlobalUserId = 0;
            GlobalUserName = null;
            GlobalRole = null;
            GlobalEmail = null;

        }
    }
}
