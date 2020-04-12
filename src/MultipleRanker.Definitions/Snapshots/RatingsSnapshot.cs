using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace MultipleRanker.Definitions.Snapshots
{
    [DataContract]
    public class RatingsSnapshot
    {
        [DataMember]
        public Guid RatingId { get; set; }

        [DataMember]
        public Guid RatingBoardId { get; set; }

        [DataMember]
        public ICollection<RatingSnapshot> RatingSnapshots 
            = new ICollection<RatingSnapshot>();
    }
}
