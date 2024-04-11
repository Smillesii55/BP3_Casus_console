using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BP3_Casus_console.Users.Friends
{
    public class UserRelationship
    {
        public int UserId1 { get; set; }
        public int UserId2 { get; set; }

        public RelationshipType Relationship { get; set; }
        public enum RelationshipType
        {
            Friend,
            Blocked
        }
    }
}
