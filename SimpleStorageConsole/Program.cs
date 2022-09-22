using EthereumSmartContracts.Contracts.SimpleStorage;
using EthereumSmartContracts.Contracts.SimpleStorage.ContractDefinition;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using System;
using System.Threading.Tasks;

namespace SimpleStorageConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Demo().Wait();
        }

        static async Task Demo()
        {
            try
            {
                // Setup using the Nethereum public test chain
                var url = "http://127.0.0.1:8547";
                var privateKey = "0x079aa9e55d65109a321960f8d0b5bf365502dcc3fcbd5bd97a2a59bf2d52c35f";
                var account = new Account(privateKey);
                var web3 = new Web3(account, url);

                web3.TransactionManager.UseLegacyAsDefault = true;
                Console.WriteLine("Deploying...");
                var deployment = new SimpleStorageDeployment();
                var res = await SimpleStorageService.DeployContractAsync(web3, deployment);
                Console.WriteLine("Deployed at: " + res);
                // var receipt = await SimpleStorageService.DeployContractAsync(web3, deployment);
                // var service = new SimpleStorageService(web3, receipt.ContractAddress);
                // Console.WriteLine($"Contract Deployment Tx Status: {receipt.Status.Value}");
                // Console.WriteLine($"Contract Address: {service.ContractHandler.ContractAddress}");
                // Console.WriteLine("");

                // Console.WriteLine("Sending a transaction to the function set()...");
                // var receiptForSetFunctionCall = await service.SetRequestAndWaitForReceiptAsync(new SetFunction() { X = 42, Gas = 400000 });
                // Console.WriteLine($"Finished storing an int: Tx Hash: {receiptForSetFunctionCall.TransactionHash}");
                // Console.WriteLine($"Finished storing an int: Tx Status: {receiptForSetFunctionCall.Status.Value}");
                // Console.WriteLine("");

                // Console.WriteLine("Calling the function get()...");
                // var intValueFromGetFunctionCall = await service.GetQueryAsync();
                // Console.WriteLine($"Int value: {intValueFromGetFunctionCall} (expecting value 42)");
                // Console.WriteLine("");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            Console.WriteLine("Finished");
            Console.ReadLine();
        }

        static async Task Demo2()
        {
            try
            {
                // Setup
                var url = "https://rinkeby.infura.io/v3/7238211010344719ad14a89db874158c";
                var web3 = new Web3(url);
                // An already-deployed SimpleStorage.sol contract on Rinkeby:
                var contractAddress = "0xb52Fe7D1E04fbf47918Ad8d868103F03Da6ec4fE";
                var service = new SimpleStorageService(web3, contractAddress);

                // Get the stored value
                var currentStoredValue = await service.GetQueryAsync();
                Console.WriteLine($"Contract has value stored: {currentStoredValue}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            Console.WriteLine("Finished");
            Console.ReadLine();
        }
    }
}