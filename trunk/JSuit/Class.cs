using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Globalization;

namespace JSuit.ja
{
    [DataContract(Namespace = "http://jeebook.com/")]
    class Exception
    {
        [DataMember(Name = "success")]
        public bool success { get; set; }

        [DataMember(Name = "code")]
        public int code { get; set; }

        [DataMember(Name = "message")]
        public string message { get; set; }
    }

    [DataContract(Namespace = "http://jeebook.com/album")]
    class Album
    {
        [DataMember(Name = "success")]
        public bool success { get; set; }

        [DataMember(Name = "code")]
        public int code { get; set; }

        [DataMember(Name = "message")]
        public string message { get; set; }
    }

    [DataContract(Namespace = "http://jeebook.com/album")]
    class CMD_albums
    {
        [DataMember(Name = "success")]
        public bool success { get; set; }

        [DataMember(Name = "total")]
        public int code { get; set; }

        [DataMember(Name = "albums")]
        public Album[] albums { get; set; }
    }
}
