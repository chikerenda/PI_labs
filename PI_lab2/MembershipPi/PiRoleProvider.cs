using System;
using System.Web.Security;

namespace PI_lab2.MembershipPi
{
    public class PiRoleProvider : RoleProvider
    {
        public override string[] GetRolesForUser(string login)
        {
            var role = DataManager.GetUserRole(login);
            return role == null ? null : new[] {role};
        }

        public override void CreateRole(string roleName)
        {
            DataManager.CreateRole(roleName);
        }

        public override bool IsUserInRole(string login, string roleName)
        {
            return DataManager.UserInRole(login, roleName);
        }
        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}
