using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RAbbitTest
{
    public record EventMessage
    {
        public EventMessage()
        {
            Id = Guid.NewGuid();
            CreationDate = DateTime.UtcNow;
        }

        [JsonInclude]
        public Guid Id { get; set; }

        [JsonInclude]
        public DateTime CreationDate { get; set; }
    }

    public record TestEventMessage: EventMessage
    {
        public TestEventMessage() : base()
        {
           
        }

        [JsonInclude]
        public string Data { get; set; }

    }
}
