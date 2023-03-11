﻿namespace API_Chat.Model
{
    public class Notifications
    {
        public int Id { get; set; }
        public string MessageText { get; set; } = string.Empty;
        public string ToWhom { get; set; } = string.Empty;
        public string FromWhom { get; set; } = string.Empty;

        public int ContactsId { get; set; }
        public Contacts Contacts { get; set; }


    }
}