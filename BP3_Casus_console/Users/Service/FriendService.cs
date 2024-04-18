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
        FriendDataAccesLayer UserDataAccesLayer = FriendDataAccesLayer.Instance;

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

            UserDataAccesLayer.InsertRequest(friendRequest);
            friendRequestList.Add(friendRequest);
        }

        public void AcceptFriendRequest(int requestId)
        {

        }

        public void DeclineFriendRequest(int requestId)
        {

        }

        public List<User> GetFriendsList(int userID)
        {

            return new List<User>();

        }

        public List<User> FriendsList(int userID)
        {
            return new List<User>();
        }

        public void GetId(string username)
        {
        }

        public void UpdateState(int userId1, int userId2, RelationshipType type)
        {

        }
    }
}
