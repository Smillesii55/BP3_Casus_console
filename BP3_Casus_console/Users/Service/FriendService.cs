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

        }

        public void DeclineFriendRequest(int requestId)
        {

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
