using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BP3_Casus_console.Users.Friends
{
    public class FriendRequest
    {
        public int RequestId { get; set; }
        public int SenderUserId { get; set; }
        public int ReceiverUserId { get; set; }
        public DateTime RequestDate { get; set; }
        public FriendRequestStatus Status { get; set; }
        public enum FriendRequestStatus
        {
            Pending,
            Accepted,
            Declined
        }

        public FriendRequest(int requestId, int senderUserId, int receiverUserId, DateTime requestDate, FriendRequestStatus status)
        {
            RequestId = requestId;
            SenderUserId = senderUserId;
            ReceiverUserId = receiverUserId;
            RequestDate = requestDate;
            Status = status;

        }
    }
}
