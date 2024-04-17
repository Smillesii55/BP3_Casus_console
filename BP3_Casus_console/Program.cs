﻿using BP3_Casus_console.Events.Service;
using BP3_Casus_console.Events;
using BP3_Casus_console.Users;
using BP3_Casus_console.Users.Friends;
using BP3_Casus_console.Users.Service;
using System.Text;
using static BP3_Casus_console.Users.Friends.FriendRequest;

UserService userService = UserService.Instance;
FriendService friendService = FriendService.Instance;

User CurrentUser = Login();
User Login()
{
    
    User? loggedInUser = null;

    while (loggedInUser == null)
    {
        loggedInUser = null;

        while (loggedInUser == null)
        {
            Console.Clear();
            Console.WriteLine("Login.");
            Console.WriteLine();
            Console.Write("Username: ");
            string username = Console.ReadLine();
            
            Console.Write("password: ");
            string password = Console.ReadLine();
            loggedInUser = userService.Login(username, password);

            if (loggedInUser == null)
            {
                Console.WriteLine();
                Console.WriteLine("Press any key to try again.");
                Console.ReadKey();
            }
        }
    }

    return loggedInUser;
}

while (true)
{
    Console.Clear();
    Console.WriteLine("Welcome, " + CurrentUser.FirstName + "!");
    Console.WriteLine();
    Console.WriteLine("1. View profile");
    Console.WriteLine("2. Edit profile");
    Console.WriteLine("3. View friends");
    Console.WriteLine("4. View friend requests");
    Console.WriteLine("5. Add friend");
    Console.WriteLine("6. Remove friend");
    Console.WriteLine("7. View events");
    Console.WriteLine("8. Participate in event");
    Console.WriteLine("9. Exit");
    Console.WriteLine();
    Console.Write("Select an option: ");
    string input = Console.ReadLine();

    switch (input)
    {
        case "1":
            ViewProfile();
            break;
        case "2":
            EditProfile();
            break;
        case "3":
            ViewFriends();
            break;
        case "4":
            ViewFriendRequest();
            break;
        case "5":
            AddFriend();
            break;
        case "6":
            RemoveFriend();
            break;
        case "7":
            ViewEvents();
            break;
        case "8":
            ParticipateInEvent();
            break;
        case "9":
            return;
        default:
            Console.WriteLine("Invalid input. Press any key to try again.");
            Console.ReadKey();
            break;
    }
}
void ViewProfile()
{
    UserService userService = UserService.Instance;
    User userProfile = userService.GetUserProfileById(CurrentUser.ID);

    Console.Clear();
    Console.WriteLine("Profile");
    Console.WriteLine();
    Console.WriteLine("Username: " + userProfile.Username);
    Console.WriteLine("Email: " + userProfile.Email);
    Console.WriteLine("First name: " + userProfile.FirstName);
    Console.WriteLine("Last name: " + userProfile.LastName);
    Console.WriteLine("Date of birth: " + userProfile.DateOfBirth.ToString("yyyy-MM-dd"));
    Console.WriteLine("User type: " + userProfile.Type);
    Console.WriteLine("General level: " + (userProfile as Participant)?.GeneralLevel);
    Console.WriteLine("General experience: " + (userProfile as Participant)?.GeneralExperience);
    Console.WriteLine("Expertise: " + (userProfile as Coach)?.Expertise);
    Console.WriteLine();
    Console.WriteLine("Press any key to return.");
    Console.ReadKey();
}
void EditProfile()
{
    UserService userService = UserService.Instance;
    Console.Clear();
    Console.WriteLine("Edit profile");
    Console.WriteLine();
    Console.Write("Email: ");
    CurrentUser.Email = Console.ReadLine();
    Console.Write("First name: ");
    CurrentUser.FirstName = Console.ReadLine();
    Console.Write("Last name: ");
    CurrentUser.LastName = Console.ReadLine();
    Console.Write("Date of birth (yyyy-MM-dd): ");
    CurrentUser.DateOfBirth = DateTime.Parse(Console.ReadLine());

    userService.UpdateUserProfile(CurrentUser);

    Console.WriteLine();
    Console.WriteLine("Profile updated. Press any key to return.");
    Console.ReadKey();
}
void ViewFriends()
{
    FriendService friendService = FriendService.Instance;
    List<User> friends = friendService.GetFriendsList(CurrentUser.ID);

    Console.Clear();
    Console.WriteLine("Friends");
    Console.WriteLine();

    if (friends.Count == 0)
    {
        Console.WriteLine("You have no friends.");
    }
    else
    {
        foreach (User friend in friends)
        {
            Console.WriteLine(friend.FirstName + " " + friend.LastName);
        }
    }

    Console.WriteLine();
    Console.WriteLine("Press any key to return.");
    Console.ReadKey();
}
void ViewFriendRequest()
{
    FriendService friendService = FriendService.Instance;
    UserService userService = UserService.Instance;
    Console.Clear();

    Console.WriteLine("Friendrequest list");
    List<User> friends = friendService.GetFriendsList(CurrentUser.ID);

    Console.WriteLine("Choose the friendrequest you want to accept or decline: ");
    int requestID = Convert.ToInt32(Console.ReadLine());

    Console.WriteLine("1: Accept");
    Console.WriteLine("2: Decline");
    string answer = Console.ReadLine();

    if (answer == "1")
    {
        //Werkt. Er staat in de Database Accepted
        friendService.AcceptFriendRequest(CurrentUser.ID);
    }
    else if (answer == "2")
    {
        friendService.DeclineFriendRequest(CurrentUser.ID);
    }

    //Make sure you can see the name of the reciever en sender instead of the ID.

    Console.WriteLine(friends);


    Console.ReadKey();
}
void AddFriend()
{
    FriendService friendService = FriendService.Instance;
    UserService userService = UserService.Instance;

    Console.Clear();
    Console.WriteLine("Add friend");
    Console.WriteLine();
    Console.Write("Enter the username of the friend you want to add: ");
    string friendUsername = Console.ReadLine();

    User? friend = userService.GetUserProdileByUsername(friendUsername);
    friendService.GetId(friendUsername);

    if (friend == null)
    {
        Console.WriteLine("User not found.");
    }
    else if (friend.ID == CurrentUser.ID)
    {
        Console.WriteLine("You cannot add yourself as a friend.");
    }
    else if (friendService.GetFriendsList(CurrentUser.ID).Contains(friend))
    {
        Console.WriteLine("You are already friends with this user.");
    }
    else
    {
        friendService.SendFriendRequest(CurrentUser.ID, friend.ID);
        Console.WriteLine("Friend added.");
    }

    Console.WriteLine();
    Console.WriteLine("Press any key to return.");
    Console.ReadKey();
}
void RemoveFriend()
{
    FriendService friendService = FriendService.Instance;
    UserService userService = UserService.Instance;

    Console.Clear();
    Console.WriteLine("Remove friend");
    Console.WriteLine();
    Console.Write("Enter the username of the friend you want to remove: ");
    string friendUsername = Console.ReadLine();

    User? friend = userService.GetUserProdileByUsername(friendUsername);

    if (friend == null)
    {
        Console.WriteLine("User not found.");
    }
    else if (friend.ID == CurrentUser.ID)
    {
        Console.WriteLine("You cannot remove yourself as a friend.");
    }
    else if (!friendService.GetFriendsList(CurrentUser.ID).Contains(friend))
    {
        Console.WriteLine("You are not friends with this user.");
    }
    else
    {
        friendService.DeclineFriendRequest(friend.ID);
        Console.WriteLine("Friend removed.");
    }

    Console.WriteLine();
    Console.WriteLine("Press any key to return.");
    Console.ReadKey();
}
void ViewEvents()
{
    EventService eventService = EventService.Instance;
    //List<Event>? events = eventService.GetEvents();

    Console.Clear();
    Console.WriteLine("Events");
    Console.WriteLine();
    /*
    if (events.Count == 0)
    {
        Console.WriteLine("There are no events.");
    }
    else
    {
        foreach (Event @event in events)
        {
            Console.WriteLine(@event.Name);
        }
    }
    */

    Console.WriteLine();
    Console.WriteLine("Press any key to return.");
    Console.ReadKey();
}
void ParticipateInEvent()
{
    EventService eventService = EventService.Instance;
    UserService userService = UserService.Instance;

    Console.Clear();
    Console.WriteLine("Participate in event");
    Console.WriteLine();
    Console.Write("Enter the name of the event you want to participate in: ");
    string eventName = Console.ReadLine();
    /*
    Event? @event = eventService.GetEvents().FirstOrDefault(e => e.Name == eventName);

    if (@event == null)
    {
        Console.WriteLine("Event not found.");
    }
    else
    {
        Participant participant = (Participant)userService.GetUserProfileById(CurrentUser.ID);
        participant.ParticipateInEvent(@event);
        Console.WriteLine("Participation confirmed.");
    }

    Console.WriteLine();
    Console.WriteLine("Press any key to return.");
    Console.ReadKey();
    */
}