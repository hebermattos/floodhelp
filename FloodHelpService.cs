using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Web3;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Contracts.CQS;
using Nethereum.Contracts.ContractHandlers;
using Nethereum.Contracts;
using System.Threading;
using Floodhelp.Contracts.FloodHelp.ContractDefinition;

namespace Floodhelp.Contracts.FloodHelp
{
    public partial class FloodHelpService
    {
        public static Task<TransactionReceipt> DeployContractAndWaitForReceiptAsync(Nethereum.Web3.Web3 web3, FloodHelpDeployment floodHelpDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            return web3.Eth.GetContractDeploymentHandler<FloodHelpDeployment>().SendRequestAndWaitForReceiptAsync(floodHelpDeployment, cancellationTokenSource);
        }

        public static Task<string> DeployContractAsync(Nethereum.Web3.Web3 web3, FloodHelpDeployment floodHelpDeployment)
        {
            return web3.Eth.GetContractDeploymentHandler<FloodHelpDeployment>().SendRequestAsync(floodHelpDeployment);
        }

        public static async Task<FloodHelpService> DeployContractAndGetServiceAsync(Nethereum.Web3.Web3 web3, FloodHelpDeployment floodHelpDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            var receipt = await DeployContractAndWaitForReceiptAsync(web3, floodHelpDeployment, cancellationTokenSource);
            return new FloodHelpService(web3, receipt.ContractAddress);
        }

        protected Nethereum.Web3.IWeb3 Web3{ get; }

        public ContractHandler ContractHandler { get; }

        public FloodHelpService(Nethereum.Web3.Web3 web3, string contractAddress)
        {
            Web3 = web3;
            ContractHandler = web3.Eth.GetContractHandler(contractAddress);
        }

        public FloodHelpService(Nethereum.Web3.IWeb3 web3, string contractAddress)
        {
            Web3 = web3;
            ContractHandler = web3.Eth.GetContractHandler(contractAddress);
        }

        public Task<string> CloseHelpRequestRequestAsync(CloseHelpRequestFunction closeHelpRequestFunction)
        {
             return ContractHandler.SendRequestAsync(closeHelpRequestFunction);
        }

        public Task<TransactionReceipt> CloseHelpRequestRequestAndWaitForReceiptAsync(CloseHelpRequestFunction closeHelpRequestFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(closeHelpRequestFunction, cancellationToken);
        }

        public Task<string> CloseHelpRequestRequestAsync(BigInteger id)
        {
            var closeHelpRequestFunction = new CloseHelpRequestFunction();
                closeHelpRequestFunction.Id = id;
            
             return ContractHandler.SendRequestAsync(closeHelpRequestFunction);
        }

        public Task<TransactionReceipt> CloseHelpRequestRequestAndWaitForReceiptAsync(BigInteger id, CancellationTokenSource cancellationToken = null)
        {
            var closeHelpRequestFunction = new CloseHelpRequestFunction();
                closeHelpRequestFunction.Id = id;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(closeHelpRequestFunction, cancellationToken);
        }

        public Task<string> DonateRequestAsync(DonateFunction donateFunction)
        {
             return ContractHandler.SendRequestAsync(donateFunction);
        }

        public Task<TransactionReceipt> DonateRequestAndWaitForReceiptAsync(DonateFunction donateFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(donateFunction, cancellationToken);
        }

        public Task<string> DonateRequestAsync(BigInteger id)
        {
            var donateFunction = new DonateFunction();
                donateFunction.Id = id;
            
             return ContractHandler.SendRequestAsync(donateFunction);
        }

        public Task<TransactionReceipt> DonateRequestAndWaitForReceiptAsync(BigInteger id, CancellationTokenSource cancellationToken = null)
        {
            var donateFunction = new DonateFunction();
                donateFunction.Id = id;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(donateFunction, cancellationToken);
        }

        public Task<GetHelpRequestsOutputDTO> GetHelpRequestsQueryAsync(GetHelpRequestsFunction getHelpRequestsFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<GetHelpRequestsFunction, GetHelpRequestsOutputDTO>(getHelpRequestsFunction, blockParameter);
        }

        public Task<GetHelpRequestsOutputDTO> GetHelpRequestsQueryAsync(BigInteger page, BigInteger pageSize, BlockParameter blockParameter = null)
        {
            var getHelpRequestsFunction = new GetHelpRequestsFunction();
                getHelpRequestsFunction.Page = page;
                getHelpRequestsFunction.PageSize = pageSize;
            
            return ContractHandler.QueryDeserializingToObjectAsync<GetHelpRequestsFunction, GetHelpRequestsOutputDTO>(getHelpRequestsFunction, blockParameter);
        }

        public Task<HelpRequestsOutputDTO> HelpRequestsQueryAsync(HelpRequestsFunction helpRequestsFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<HelpRequestsFunction, HelpRequestsOutputDTO>(helpRequestsFunction, blockParameter);
        }

        public Task<HelpRequestsOutputDTO> HelpRequestsQueryAsync(BigInteger returnValue1, BlockParameter blockParameter = null)
        {
            var helpRequestsFunction = new HelpRequestsFunction();
                helpRequestsFunction.ReturnValue1 = returnValue1;
            
            return ContractHandler.QueryDeserializingToObjectAsync<HelpRequestsFunction, HelpRequestsOutputDTO>(helpRequestsFunction, blockParameter);
        }

        public Task<BigInteger> LastIdQueryAsync(LastIdFunction lastIdFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<LastIdFunction, BigInteger>(lastIdFunction, blockParameter);
        }

        
        public Task<BigInteger> LastIdQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<LastIdFunction, BigInteger>(null, blockParameter);
        }

        public Task<string> OpenHelpRequestRequestAsync(OpenHelpRequestFunction openHelpRequestFunction)
        {
             return ContractHandler.SendRequestAsync(openHelpRequestFunction);
        }

        public Task<TransactionReceipt> OpenHelpRequestRequestAndWaitForReceiptAsync(OpenHelpRequestFunction openHelpRequestFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(openHelpRequestFunction, cancellationToken);
        }

        public Task<string> OpenHelpRequestRequestAsync(string title, string description, string contact, BigInteger goal)
        {
            var openHelpRequestFunction = new OpenHelpRequestFunction();
                openHelpRequestFunction.Title = title;
                openHelpRequestFunction.Description = description;
                openHelpRequestFunction.Contact = contact;
                openHelpRequestFunction.Goal = goal;
            
             return ContractHandler.SendRequestAsync(openHelpRequestFunction);
        }

        public Task<TransactionReceipt> OpenHelpRequestRequestAndWaitForReceiptAsync(string title, string description, string contact, BigInteger goal, CancellationTokenSource cancellationToken = null)
        {
            var openHelpRequestFunction = new OpenHelpRequestFunction();
                openHelpRequestFunction.Title = title;
                openHelpRequestFunction.Description = description;
                openHelpRequestFunction.Contact = contact;
                openHelpRequestFunction.Goal = goal;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(openHelpRequestFunction, cancellationToken);
        }
    }
}
