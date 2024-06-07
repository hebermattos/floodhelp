using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;

namespace Floodhelp.Contracts.FloodHelp.ContractDefinition
{
    public partial class HelpRequest : HelpRequestBase { }

    public class HelpRequestBase 
    {
        [Parameter("uint256", "id", 1)]
        public virtual BigInteger Id { get; set; }
        [Parameter("string", "title", 2)]
        public virtual string Title { get; set; }
        [Parameter("string", "description", 3)]
        public virtual string Description { get; set; }
        [Parameter("string", "contact", 4)]
        public virtual string Contact { get; set; }
        [Parameter("uint256", "timestamp", 5)]
        public virtual BigInteger Timestamp { get; set; }
        [Parameter("address", "author", 6)]
        public virtual string Author { get; set; }
        [Parameter("uint256", "goal", 7)]
        public virtual BigInteger Goal { get; set; }
        [Parameter("uint256", "balance", 8)]
        public virtual BigInteger Balance { get; set; }
        [Parameter("bool", "open", 9)]
        public virtual bool Open { get; set; }
    }
}
