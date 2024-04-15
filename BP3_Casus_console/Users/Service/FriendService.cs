using BP3_Casus_console.Users.Friends;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BP3_Casus_console.Users.Friends.FriendRequest;

namespace BP3_Casus_console.Users.Service
{
    public class FriendService
    {
        UserDataAccesLayer UserDataAccesLayer = UserDataAccesLayer.Instance;

        private FriendService()
        {
        }

        List<FriendRequest> friendRequestList = new List<FriendRequest>();
        List<UserRelationship> userRelationships = new List<UserRelationship>();

        private static FriendService? instance = null;
        public static FriendService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new FriendService();
                }
                return instance;
            }
        }

        public void SendFriendRequest(int senderUserId, int receiverUserId)
        {
            //List<FriendRequest> sendFriendRequest = new List<FriendRequest>();

            //Blackbox.Ai
            DateTime requestDate = DateTime.Now;
            FriendRequestStatus status = FriendRequestStatus.Pending;

            FriendRequest friendRequest = new FriendRequest(0, senderUserId, receiverUserId, requestDate, status);

            UserDataAccesLayer.InsertRequest(friendRequest);
            friendRequestList.Add(friendRequest);

            // Create a new FriendRequest object
            // Set its properties (sender, receiver, status = Pending, etc.)
            // Save the friend request to the database
        }

        public void AcceptFriendRequest(int requestId)
        {
            DateTime requestDate = DateTime.Now;
            FriendRequestStatus status = FriendRequestStatus.Accepted;

            FriendRequest friendRequest = new FriendRequest(requestId, 0, 0, requestDate, status);
            friendRequestList.Add(friendRequest); 

            UserRelationship userRelationship = new UserRelationship();
            userRelationships.Add(userRelationship);

            UserDataAccesLayer.UpdateRequestStatus(friendRequest);

            // Retrieve the request by requestId
            // Change its status to Accepted
            // Create a new UserRelationship and save it
            // Save changes to the database
        }

        public void DeclineFriendRequest(int requestId)
        {
            //List<FriendRequest> declineFriendRequest = new List<FriendRequest>();
            DateTime requestDate = DateTime.Now;
            FriendRequestStatus status = FriendRequestStatus.Declined;

            FriendRequest friendRequest = new FriendRequest(requestId, 0, 0, requestDate, status);
            friendRequestList.Add(friendRequest);

            UserDataAccesLayer.UpdateRequestStatus(friendRequest);
            // Retrieve the request by requestId
            // Change its status to Declined
            // Save changes to the database
        }

        public List<User> GetFriendsList(int userId)
        {
            
            // Retrieve and return a list of User objects representing the friends of the specified user
            return new List<User>(); // Placeholder
        }

        public void GetId(string username)
        {
            UserDataAccesLayer.GetIdByUsername(username);
        }
    }
}
