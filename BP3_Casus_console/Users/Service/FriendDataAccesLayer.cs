using BP3_Casus_console.Users.Friends;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BP3_Casus_console.Users.Friends.FriendRequest;
using static BP3_Casus_console.Users.Friends.UserRelationship;

namespace BP3_Casus_console.Users.Service
{
    public class FriendDataAccesLayer
    {
        private string connectionString = "Data Source=.;Initial Catalog=BP3Casus;Integrated Security=True;Encrypt=False";

        private FriendDataAccesLayer()
        {
        }

        private static FriendDataAccesLayer? instance = null;
        public static FriendDataAccesLayer Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new FriendDataAccesLayer();
                }
                return instance;
            }
        }
        public List<FriendRequest> FriendRequestList(int userID)
        {
            List<FriendRequest> friends = new List<FriendRequest>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM FriendRequests WHERE RecieverUserID IN (SELECT UserId FROM Users WHERE UserId = @UserID)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserID", userID);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string statusString = reader["status"].ToString();
                            FriendRequestStatus status = (FriendRequestStatus)Enum.Parse(typeof(FriendRequestStatus), statusString);
                            FriendRequest @friend = new FriendRequest(0, (int)reader["SenderUserId"], (int)reader["RecieverUserID"], (DateTime)reader["Date"], status);
                            @friend.RequestId = (int)reader["ID"];
                            friends.Add(@friend);

                        }
                    }
                }
            }
            return friends;
        }

        public List<UserRelationship> FriendsList(int userID)
        {
            List<UserRelationship> friendsList = new List<UserRelationship>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM UserRelations WHERE UserID IN (SELECT UserId FROM Users WHERE UserId = @UserID)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserID", userID);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string typeString = reader["Type"].ToString();
                            RelationshipType type = (RelationshipType)Enum.Parse(typeof(RelationshipType), typeString);
                            UserRelationship @friend = new UserRelationship((int)reader["UserID"], (int)reader["User2ID"], type);
                            @friend.UserId1 = (int)reader["ID"];
                            friendsList.Add(@friend);

                        }
                    }
                }
            }
            return friendsList;
            
        }

        public void InsertUserRelation(UserRelationship userRelationship)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "INSERT INTO UserRelations (UserID, User2ID, Type) VALUES (@UserID, @User2ID, @Type)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserID", userRelationship.UserId1);
                    command.Parameters.AddWithValue("@User2ID", userRelationship.UserId2);
                    command.Parameters.AddWithValue("@Type", userRelationship.Relationship);

                    command.ExecuteNonQuery();


                }
            }
        }

        public void UpdateRequestStatus(FriendRequest friendRequest)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "UPDATE FriendRequests SET Status = @Status WHERE ID = @ID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID", friendRequest.RequestId);
                    command.Parameters.AddWithValue("@Status", friendRequest.Status.ToString());

                    command.ExecuteNonQuery();
                }
            }
        }

        public void InsertRequest(FriendRequest friendRequest)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "INSERT INTO FriendRequests (SenderUserID, RecieverUserID, Date, Status) VALUES (@SenderUserID, @RecieverUserID, @Date, @Status)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@SenderUserID", friendRequest.SenderUserId);
                    command.Parameters.AddWithValue("@RecieverUserID", friendRequest.ReceiverUserId);
                    command.Parameters.AddWithValue("@Date", friendRequest.RequestDate);
                    command.Parameters.AddWithValue("@Status", friendRequest.Status);

                    command.ExecuteNonQuery();


                }
            }
        }
    }
}
