using System.Collections.Generic;

namespace SecclShared.Models
{
    public class ClientResponse
    {
        public List<Client> Data { get; set; }
        public Meta Meta { get; set; }
    }

    public class Meta
    {
        public int Count { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
