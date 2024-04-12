using BP3_Casus_console.Users.Friends;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BP3_Casus_console.Users.Service
{
    public class FriendService
    {
        private FriendService()
        {
        }

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
            // Create a new FriendRequest object
            // Set its properties (sender, receiver, status = Pending, etc.)
            // Save the friend request to the database
        }

        public void AcceptFriendRequest(int requestId)
        {
            // Retrieve the request by requestId
            // Change its status to Accepted
            // Create a new UserRelationship and save it
            // Save changes to the database
        }

        public void DeclineFriendRequest(int requestId)
        {
            // Retrieve the request by requestId
            // Change its status to Declined
            // Save changes to the database
        }

        public List<User> GetFriendsList(int userId)
        {
            // Retrieve and return a list of User objects representing the friends of the specified user
            return new List<User>(); // Placeholder
        }

        // Additional methods can include RemoveFriend, GetFriendRequests, etc.
       /* public class FriendService
        {
            private static FriendService instance;
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

            private List<FriendRequest> friendRequests = new List<FriendRequest>();

            public void SendFriendRequest(User sender, User receiver)
            {
                FriendRequest request = new FriendRequest(friendRequests.Count + 1, sender.UserId, receiver.UserId, DateTime.Now, FriendRequest.FriendRequestStatus.Pending);
                friendRequests.Add(request);
            }

            public void AcceptFriendRequest(FriendRequest request)
            {
                request.Status = FriendRequest.FriendRequestStatus.Accepted;
            }

            public void DeclineFriendRequest(FriendRequest request)
            {
                request.Status = FriendRequest.FriendRequestStatus.Declined;
            }

            public List<FriendRequest> GetFriendRequests(User user)
            {
                return friendRequests.Where(r => r.ReceiverUserId == user.UserId).ToList();
            }

            public List<User> GetFriends(User user)
            {
                List<User> friends = new List<User>();
                foreach (FriendRequest request in friendRequests)
                {
                    if (request.Status == FriendRequest.FriendRequestStatus.Accepted)
                    {
                        if (request.SenderUserId == user.UserId)
                        {
                            friends.Add(UserService.Instance.GetUserById(request.ReceiverUserId));
                        }
                        else if (request.ReceiverUserId == user.UserId)
                        {
                            friends.Add(UserService.Instance.GetUserById(request.SenderUserId));
                        }
                    }
                }
                return friends; */
            }
        }
    }
}
}
