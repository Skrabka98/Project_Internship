﻿using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Warehouse.admin
{
    class AddPermissions
    {
        private warehouseDatabaseEntities1 context = new warehouseDatabaseEntities1();
        private List<CheckBox> checkBoxesList;
        private Permissions permissions;
        private UserPermissions userPermissions = new UserPermissions();
        private List<UserPermissions> userPermissionsList = new List<UserPermissions>();
        public AddPermissions(List<CheckBox> checkBoxesList)
        {
            this.checkBoxesList = checkBoxesList;

        }
        public void permission(int idUsers)
        {
            string checkBoxName;

            if (idUsers != 0)
            {
                for (int i = 0; i < checkBoxesList.Count; i++)
                {
                    if (checkBoxesList[i].Checked == true)
                    {
                        checkBoxName = checkBoxesList[i].Text.Trim();
                        addPermision(checkBoxName, idUsers);
                    }

                }
            }
            

        }

        private void addPermision(string checkBoxName, int userId)
        {
            permissions = context.Permissions.First(c => c.permissionName == checkBoxName);
            userPermissions.permissionsID = permissions.permissionID;
            userPermissions.userID = userId;
            context.UserPermissions.Add(userPermissions);
            context.SaveChanges();
        }


        public void deletePermissions(int idUsers)
        {
            try
            {
                userPermissionsList = context.UserPermissions.ToList();
                foreach (UserPermissions userPermissions in userPermissionsList)
                {
                    var userPermissionsExist = context.UserPermissions.FirstOrDefault(c => c.userID == idUsers);
                    if (userPermissionsExist != null)
                    {       
                                context.UserPermissions.Remove(userPermissionsExist);
                                context.SaveChanges();
                    }

                }
            }
            catch (System.InvalidOperationException e)
            {

            }
        }
    }
}
