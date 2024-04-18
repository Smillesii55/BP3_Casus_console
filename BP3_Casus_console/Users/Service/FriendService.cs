using BP3_Casus_console.Events.Service;
using BP3_Casus_console.Events;
using BP3_Casus_console.Users.Friends;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using BP3_Casus_console.Users.Service;
using static BP3_Casus_console.Users.Friends.UserRelationship;
using static BP3_Casus_console.Users.Friends.FriendRequest;


namespace BP3_Casus_console.Users.Service
{
    public class FriendService
    {
        FriendDataAccesLayer FriendDataAccesLayer = FriendDataAccesLayer.Instance;
        UserService userService = UserService.Instance;

        private FriendService()
        {
        }

        List<FriendRequest> friendRequestList = new List<FriendRequest>();

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
            DateTime requestDate = DateTime.Now;
            FriendRequest.FriendRequestStatus status = FriendRequest.FriendRequestStatus.Pending;

            FriendRequest friendRequest = new FriendRequest(0, senderUserId, receiverUserId, requestDate, status);

            FriendDataAccesLayer.InsertRequest(friendRequest);
            friendRequestList.Add(friendRequest);
        }

        public void AcceptFriendRequest(int requestId)
        {
            DateTime requestDate = DateTime.Now;
            FriendRequest.FriendRequestStatus status = FriendRequest.FriendRequestStatus.Accepted;

            FriendRequest friendRequest = new FriendRequest(requestId, 0, 0, requestDate, status);
            friendRequestList.Add(friendRequest);

            RelationshipType relationship = RelationshipType.Friend;

            UserRelationship userRelationship = new UserRelationship(0, 0, relationship);

            FriendDataAccesLayer.UpdateRequestStatus(friendRequest);
            FriendDataAccesLayer.InsertUserRelation(userRelationship);

        }

        public void DeclineFriendRequest(int requestId)
        {
            DateTime requestDate = DateTime.Now;
            FriendRequest.FriendRequestStatus status = FriendRequest.FriendRequestStatus.Declined;

            FriendRequest friendRequest = new FriendRequest(requestId, 0, 0, requestDate, status);

            FriendDataAccesLayer.UpdateRequestStatus(friendRequest);
            FriendDataAccesLayer.RemoveRequest(friendRequest);
        }

        public List<User> GetFriendRequestList(int userID)
        {
            List<User> friendRequestList = new List<User>();

            List<FriendRequest> requests = FriendDataAccesLayer.FriendRequestList(userID);

            foreach (FriendRequest request in requests)
            {
                User user = userService.GetUserProfileById(request.SenderUserId);
                friendRequestList.Add(user);
            }

            foreach (FriendRequest friend in requests)
            {
                Console.WriteLine("Friend Request Details:");
                Console.WriteLine("Request ID: " + friend.RequestId);
                Console.WriteLine("Sender ID: " + friend.SenderUserId);
                Console.WriteLine("Receiver ID: " + friend.ReceiverUserId);
                Console.WriteLine("Request Date: " + friend.RequestDate);
                Console.WriteLine("Status: " + friend.Status);
                Console.WriteLine();
            }

            return friendRequestList;
        }

        public List<User> GetFriendsList(int userID)
        {
            List<User> friendsList = new List<User>();

            List<UserRelationship> friends = FriendDataAccesLayer.FriendsList(userID);

            foreach (UserRelationship friend in friends)
            {
                User user = userService.GetUserProfileById(friend.UserId2);
                friendsList.Add(user);
            }

            foreach (UserRelationship friend in friends)
            {
                Console.WriteLine("Friend Details:");
                Console.WriteLine("Sender ID: " + friend.UserId1);
                Console.WriteLine("Receiver ID: " + friend.UserId2);
                Console.WriteLine("Relationship: " + friend.Relationship);
            }

            return friendsList;
        }

        public void GetId(string username)
        {
        }

        public void UpdateState(int userId1, int userId2, RelationshipType type)
        {

        }
    }
}
