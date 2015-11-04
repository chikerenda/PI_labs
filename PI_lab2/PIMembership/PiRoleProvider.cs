using System;
using System.Web.Security;

namespace PI_lab2.PIMembership
{
    public class PiRoleProvider : RoleProvider
    {
        public override string[] GetRolesForUser(string nickname)
        {
            //var client = new MembershipServiceClient();
            //return client.GetRolesForUser(nickname);
        }

        public override void CreateRole(string roleName)
        {
            //var client = new MembershipServiceClient();
            //client.CreateRole(roleName);
        }

        public override bool IsUserInRole(string nickname, string roleName)
        {
            //var client = new MembershipServiceClient();
            //return client.IsUserInRole(nickname, roleName);
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
