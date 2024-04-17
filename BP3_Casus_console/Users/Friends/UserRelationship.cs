using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BP3_Casus_console.Users.Friends
{
    public class UserRelationship
    {
        public int ID { get; set; } 
        public int UserId1 { get; set; }
        public int UserId2 { get; set; }

        public RelationshipType Relationship { get; set; }
        public enum RelationshipType
        {
            Friend,
            Blocked
        }

        public UserRelationship(int userId1, int userId2, RelationshipType relationship)
        {
            UserId1 = userId1;  
            UserId2 = userId2;
            Relationship = relationship;

        }
    }
}
