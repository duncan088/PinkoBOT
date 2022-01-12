using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using Nethereum.Web3;
using Nethereum.Contracts;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;
using Nethereum.Uniswap.Contracts.UniswapV2Factory;
using Nethereum.Uniswap.Contracts.UniswapV2Pair;
using Nethereum.Uniswap.Contracts.UniswapV2Pair.ContractDefinition;
using Nethereum.Uniswap.Contracts.UniswapV2Router02;
using Nethereum.Util;
using Nethereum.Web3.Accounts;
using Nethereum.Uniswap.Contracts.UniswapV2Router02.ContractDefinition;

namespace BotWpf
{
    public class CustomLabel : Label
    {
        public event EventHandler ContentChanged;

        protected override void OnContentChanged(object oldContent, object newContent)
        {
            base.OnContentChanged(oldContent, newContent);

            if (ContentChanged != null)
                ContentChanged(this, EventArgs.Empty);
        }
    }
    internal class BotHandler
    {
     public static string bnbcontrac = "0xbb4CdB9CBd36B01bD1cBaEBF2De08d9173bc095c";
       public static string busdcontrac = "0xe9e7cea3dedca5984780bafc599bd69add087d56";
      //  public static string bnbcontrac = "0xae13d989dac2f0debff460ac112a837c89baa7cd";
       // public static string busdcontrac= "0x7ef95a0fee0dd31b22626fa2e10ee6a223f8a684";
        public static  string usdtContract= "0x55d398326f99059fF775485246999027B3197955";
    //    public static string pancakeSwapFactoryAddress = "0xcA143Ce32Fe78f1f7019d7d551a6402fC5350c73";
    
   //  public static string panacakSwapRouter = "0x10ED43C718714eb63d5aA57B78B54704E256024E";
       // public static string panacakSwapRouter = "0x9ac64cc6e4415144c455bd8e4837fea55603e5c3";
        public static async Task<BigDecimal> TokenValueTask(string token, string bnb)
        {
            decimal priceUsd = 0;
    
            var web3 = new Web3(Properties.Settings.Default.BSCNODE);
            int precision = 10000;

            var contractABI = "[ { \"anonymous\": false, \"inputs\": [ { \"indexed\": true, \"internalType\": \"address\", \"name\": \"owner\", \"type\": \"address\" }, { \"indexed\": true, \"internalType\": \"address\", \"name\": \"spender\", \"type\": \"address\" }, { \"indexed\": false, \"internalType\": \"uint256\", \"name\": \"value\", \"type\": \"uint256\" } ], \"name\": \"Approval\", \"type\": \"event\" }, { \"anonymous\": false, \"inputs\": [ { \"indexed\": true, \"internalType\": \"address\", \"name\": \"from\", \"type\": \"address\" }, { \"indexed\": true, \"internalType\": \"address\", \"name\": \"to\", \"type\": \"address\" }, { \"indexed\": false, \"internalType\": \"uint256\", \"name\": \"value\", \"type\": \"uint256\" } ], \"name\": \"Transfer\", \"type\": \"event\" }, { \"constant\": true, \"inputs\": [ { \"internalType\": \"address\", \"name\": \"_owner\", \"type\": \"address\" }, { \"internalType\": \"address\", \"name\": \"spender\", \"type\": \"address\" } ], \"name\": \"allowance\", \"outputs\": [ { \"internalType\": \"uint256\", \"name\": \"\", \"type\": \"uint256\" } ], \"payable\": false, \"stateMutability\": \"view\", \"type\": \"function\" }, { \"constant\": false, \"inputs\": [ { \"internalType\": \"address\", \"name\": \"spender\", \"type\": \"address\" }, { \"internalType\": \"uint256\", \"name\": \"amount\", \"type\": \"uint256\" } ], \"name\": \"approve\", \"outputs\": [ { \"internalType\": \"bool\", \"name\": \"\", \"type\": \"bool\" } ], \"payable\": false, \"stateMutability\": \"nonpayable\", \"type\": \"function\" }, { \"constant\": true, \"inputs\": [ { \"internalType\": \"address\", \"name\": \"account\", \"type\": \"address\" } ], \"name\": \"balanceOf\", \"outputs\": [ { \"internalType\": \"uint256\", \"name\": \"\", \"type\": \"uint256\" } ], \"payable\": false, \"stateMutability\": \"view\", \"type\": \"function\" }, { \"constant\": true, \"inputs\": [], \"name\": \"decimals\", \"outputs\": [ { \"internalType\": \"uint256\", \"name\": \"\", \"type\": \"uint256\" } ], \"payable\": false, \"stateMutability\": \"view\", \"type\": \"function\" }, { \"constant\": true, \"inputs\": [], \"name\": \"getOwner\", \"outputs\": [ { \"internalType\": \"address\", \"name\": \"\", \"type\": \"address\" } ], \"payable\": false, \"stateMutability\": \"view\", \"type\": \"function\" }, { \"constant\": true, \"inputs\": [], \"name\": \"name\", \"outputs\": [ { \"internalType\": \"string\", \"name\": \"\", \"type\": \"string\" } ], \"payable\": false, \"stateMutability\": \"view\", \"type\": \"function\" }, { \"constant\": true, \"inputs\": [], \"name\": \"symbol\", \"outputs\": [ { \"internalType\": \"string\", \"name\": \"\", \"type\": \"string\" } ], \"payable\": false, \"stateMutability\": \"view\", \"type\": \"function\" }, { \"constant\": true, \"inputs\": [], \"name\": \"totalSupply\", \"outputs\": [ { \"internalType\": \"uint256\", \"name\": \"\", \"type\": \"uint256\" } ], \"payable\": false, \"stateMutability\": \"view\", \"type\": \"function\" }, { \"constant\": false, \"inputs\": [ { \"internalType\": \"address\", \"name\": \"recipient\", \"type\": \"address\" }, { \"internalType\": \"uint256\", \"name\": \"amount\", \"type\": \"uint256\" } ], \"name\": \"transfer\", \"outputs\": [ { \"internalType\": \"bool\", \"name\": \"\", \"type\": \"bool\" } ], \"payable\": false, \"stateMutability\": \"nonpayable\", \"type\": \"function\" }, { \"constant\": false, \"inputs\": [ { \"internalType\": \"address\", \"name\": \"sender\", \"type\": \"address\" }, { \"internalType\": \"address\", \"name\": \"recipient\", \"type\": \"address\" }, { \"internalType\": \"uint256\", \"name\": \"amount\", \"type\": \"uint256\" } ], \"name\": \"transferFrom\", \"outputs\": [ { \"internalType\": \"bool\", \"name\": \"\", \"type\": \"bool\" } ], \"payable\": false, \"stateMutability\": \"nonpayable\", \"type\": \"function\" } ]";



            BigDecimal ratio = 0;
            int decimales = 18;
            int decimals2 = 0;
            var contract = web3.Eth.GetContract(contractABI, token);
            var contract2 = web3.Eth.GetContract(contractABI, bnb);


            UniswapV2Router02Service pkswap = new UniswapV2Router02Service(web3, MainWindow.currentRouter);
            try
            {
                {
                    decimales = await contract.GetFunction("decimals").CallAsync<int>();
                    decimals2 = await contract2.GetFunction("decimals").CallAsync<int>();
                    ratio = BigDecimal.Pow(10, decimals2 - decimales);
                    var Path =new List<string>() {token, bnb };
                    var GetAmountsOut = await pkswap.GetAmountsOutQueryAsync(precision, Path, null);
                    
                    priceUsd = (decimal)(GetAmountsOut.Last() / ratio / precision);
                }
           
            
            }
            catch (Exception e)
            {
               await Application.Current.Dispatcher.BeginInvoke(new Action(() => {
                    MainWindow.Instance.Consola1.WriteOutput(Environment.NewLine + "No price for token "+DateTime.Now.TimeOfDay, Colors.Red);
                }), DispatcherPriority.Render);
            }
            return priceUsd;
        }
        public static async Task<BigInteger> SlippageTask(string from, string to,string amount)
        {
            BigInteger priceUsd = 0;

            var web3 = new Web3(Properties.Settings.Default.BSCNODE);
            int precision = 10000000;

            var contractABI = "[ { \"anonymous\": false, \"inputs\": [ { \"indexed\": true, \"internalType\": \"address\", \"name\": \"owner\", \"type\": \"address\" }, { \"indexed\": true, \"internalType\": \"address\", \"name\": \"spender\", \"type\": \"address\" }, { \"indexed\": false, \"internalType\": \"uint256\", \"name\": \"value\", \"type\": \"uint256\" } ], \"name\": \"Approval\", \"type\": \"event\" }, { \"anonymous\": false, \"inputs\": [ { \"indexed\": true, \"internalType\": \"address\", \"name\": \"from\", \"type\": \"address\" }, { \"indexed\": true, \"internalType\": \"address\", \"name\": \"to\", \"type\": \"address\" }, { \"indexed\": false, \"internalType\": \"uint256\", \"name\": \"value\", \"type\": \"uint256\" } ], \"name\": \"Transfer\", \"type\": \"event\" }, { \"constant\": true, \"inputs\": [ { \"internalType\": \"address\", \"name\": \"_owner\", \"type\": \"address\" }, { \"internalType\": \"address\", \"name\": \"spender\", \"type\": \"address\" } ], \"name\": \"allowance\", \"outputs\": [ { \"internalType\": \"uint256\", \"name\": \"\", \"type\": \"uint256\" } ], \"payable\": false, \"stateMutability\": \"view\", \"type\": \"function\" }, { \"constant\": false, \"inputs\": [ { \"internalType\": \"address\", \"name\": \"spender\", \"type\": \"address\" }, { \"internalType\": \"uint256\", \"name\": \"amount\", \"type\": \"uint256\" } ], \"name\": \"approve\", \"outputs\": [ { \"internalType\": \"bool\", \"name\": \"\", \"type\": \"bool\" } ], \"payable\": false, \"stateMutability\": \"nonpayable\", \"type\": \"function\" }, { \"constant\": true, \"inputs\": [ { \"internalType\": \"address\", \"name\": \"account\", \"type\": \"address\" } ], \"name\": \"balanceOf\", \"outputs\": [ { \"internalType\": \"uint256\", \"name\": \"\", \"type\": \"uint256\" } ], \"payable\": false, \"stateMutability\": \"view\", \"type\": \"function\" }, { \"constant\": true, \"inputs\": [], \"name\": \"decimals\", \"outputs\": [ { \"internalType\": \"uint256\", \"name\": \"\", \"type\": \"uint256\" } ], \"payable\": false, \"stateMutability\": \"view\", \"type\": \"function\" }, { \"constant\": true, \"inputs\": [], \"name\": \"getOwner\", \"outputs\": [ { \"internalType\": \"address\", \"name\": \"\", \"type\": \"address\" } ], \"payable\": false, \"stateMutability\": \"view\", \"type\": \"function\" }, { \"constant\": true, \"inputs\": [], \"name\": \"name\", \"outputs\": [ { \"internalType\": \"string\", \"name\": \"\", \"type\": \"string\" } ], \"payable\": false, \"stateMutability\": \"view\", \"type\": \"function\" }, { \"constant\": true, \"inputs\": [], \"name\": \"symbol\", \"outputs\": [ { \"internalType\": \"string\", \"name\": \"\", \"type\": \"string\" } ], \"payable\": false, \"stateMutability\": \"view\", \"type\": \"function\" }, { \"constant\": true, \"inputs\": [], \"name\": \"totalSupply\", \"outputs\": [ { \"internalType\": \"uint256\", \"name\": \"\", \"type\": \"uint256\" } ], \"payable\": false, \"stateMutability\": \"view\", \"type\": \"function\" }, { \"constant\": false, \"inputs\": [ { \"internalType\": \"address\", \"name\": \"recipient\", \"type\": \"address\" }, { \"internalType\": \"uint256\", \"name\": \"amount\", \"type\": \"uint256\" } ], \"name\": \"transfer\", \"outputs\": [ { \"internalType\": \"bool\", \"name\": \"\", \"type\": \"bool\" } ], \"payable\": false, \"stateMutability\": \"nonpayable\", \"type\": \"function\" }, { \"constant\": false, \"inputs\": [ { \"internalType\": \"address\", \"name\": \"sender\", \"type\": \"address\" }, { \"internalType\": \"address\", \"name\": \"recipient\", \"type\": \"address\" }, { \"internalType\": \"uint256\", \"name\": \"amount\", \"type\": \"uint256\" } ], \"name\": \"transferFrom\", \"outputs\": [ { \"internalType\": \"bool\", \"name\": \"\", \"type\": \"bool\" } ], \"payable\": false, \"stateMutability\": \"nonpayable\", \"type\": \"function\" } ]";



            BigDecimal ratio = 0;
            int decimales = 18;
            int decimals2 = 0;
            var contract = web3.Eth.GetContract(contractABI, from);
            var contract2 = web3.Eth.GetContract(contractABI, to);


            UniswapV2Router02Service pkswap = new UniswapV2Router02Service(web3, MainWindow.currentRouter);
            try
            {
                {
                    var Path = new List<string>();
                    decimales = await contract.GetFunction("decimals").CallAsync<int>();
                    decimals2 = await contract2.GetFunction("decimals").CallAsync<int>();
                    ratio = BigDecimal.Pow(10, decimals2 - decimales);
                  
                         Path = new List<string>() { from, to };

             
                
                     
                    var GetAmountsOut = await pkswap.GetAmountsOutQueryAsync(Web3.Convert.ToWei(amount,UnitConversion.EthUnit.Ether), Path, null);

                    priceUsd = (GetAmountsOut.Last());
                }


            }
            catch (Exception e)
            {
                await Application.Current.Dispatcher.BeginInvoke(new Action(() => {
                    MainWindow.Instance.Consola1.WriteOutput(Environment.NewLine + "Slippage failed may not have price yet", Colors.Red);
                }), DispatcherPriority.Render);
            }
            return priceUsd;
        }
        public static async Task<TokenSearchResult> GetPairTask(string token, string pair, CancellationToken ct)
        { var result = new TokenSearchResult();

            

            var web3 = new Web3(Properties.Settings.Default.BSCNODE);

            string routerABI =
                "[{\"inputs\":[{\"internalType\":\"address\",\"name\":\"_factory\",\"type\":\"address\"},{\"internalType\":\"address\",\"name\":\"_WETH\",\"type\":\"address\"}],\"stateMutability\":\"nonpayable\",\"type\":\"constructor\"},{\"inputs\":[],\"name\":\"WETH\",\"outputs\":[{\"internalType\":\"address\",\"name\":\"\",\"type\":\"address\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"tokenA\",\"type\":\"address\"},{\"internalType\":\"address\",\"name\":\"tokenB\",\"type\":\"address\"},{\"internalType\":\"uint256\",\"name\":\"amountADesired\",\"type\":\"uint256\"},{\"internalType\":\"uint256\",\"name\":\"amountBDesired\",\"type\":\"uint256\"},{\"internalType\":\"uint256\",\"name\":\"amountAMin\",\"type\":\"uint256\"},{\"internalType\":\"uint256\",\"name\":\"amountBMin\",\"type\":\"uint256\"},{\"internalType\":\"address\",\"name\":\"to\",\"type\":\"address\"},{\"internalType\":\"uint256\",\"name\":\"deadline\",\"type\":\"uint256\"}],\"name\":\"addLiquidity\",\"outputs\":[{\"internalType\":\"uint256\",\"name\":\"amountA\",\"type\":\"uint256\"},{\"internalType\":\"uint256\",\"name\":\"amountB\",\"type\":\"uint256\"},{\"internalType\":\"uint256\",\"name\":\"liquidity\",\"type\":\"uint256\"}],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"token\",\"type\":\"address\"},{\"internalType\":\"uint256\",\"name\":\"amountTokenDesired\",\"type\":\"uint256\"},{\"internalType\":\"uint256\",\"name\":\"amountTokenMin\",\"type\":\"uint256\"},{\"internalType\":\"uint256\",\"name\":\"amountETHMin\",\"type\":\"uint256\"},{\"internalType\":\"address\",\"name\":\"to\",\"type\":\"address\"},{\"internalType\":\"uint256\",\"name\":\"deadline\",\"type\":\"uint256\"}],\"name\":\"addLiquidityETH\",\"outputs\":[{\"internalType\":\"uint256\",\"name\":\"amountToken\",\"type\":\"uint256\"},{\"internalType\":\"uint256\",\"name\":\"amountETH\",\"type\":\"uint256\"},{\"internalType\":\"uint256\",\"name\":\"liquidity\",\"type\":\"uint256\"}],\"stateMutability\":\"payable\",\"type\":\"function\"},{\"inputs\":[],\"name\":\"factory\",\"outputs\":[{\"internalType\":\"address\",\"name\":\"\",\"type\":\"address\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"uint256\",\"name\":\"amountOut\",\"type\":\"uint256\"},{\"internalType\":\"uint256\",\"name\":\"reserveIn\",\"type\":\"uint256\"},{\"internalType\":\"uint256\",\"name\":\"reserveOut\",\"type\":\"uint256\"}],\"name\":\"getAmountIn\",\"outputs\":[{\"internalType\":\"uint256\",\"name\":\"amountIn\",\"type\":\"uint256\"}],\"stateMutability\":\"pure\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"uint256\",\"name\":\"amountIn\",\"type\":\"uint256\"},{\"internalType\":\"uint256\",\"name\":\"reserveIn\",\"type\":\"uint256\"},{\"internalType\":\"uint256\",\"name\":\"reserveOut\",\"type\":\"uint256\"}],\"name\":\"getAmountOut\",\"outputs\":[{\"internalType\":\"uint256\",\"name\":\"amountOut\",\"type\":\"uint256\"}],\"stateMutability\":\"pure\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"uint256\",\"name\":\"amountOut\",\"type\":\"uint256\"},{\"internalType\":\"address[]\",\"name\":\"path\",\"type\":\"address[]\"}],\"name\":\"getAmountsIn\",\"outputs\":[{\"internalType\":\"uint256[]\",\"name\":\"amounts\",\"type\":\"uint256[]\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"uint256\",\"name\":\"amountIn\",\"type\":\"uint256\"},{\"internalType\":\"address[]\",\"name\":\"path\",\"type\":\"address[]\"}],\"name\":\"getAmountsOut\",\"outputs\":[{\"internalType\":\"uint256[]\",\"name\":\"amounts\",\"type\":\"uint256[]\"}],\"stateMutability\":\"view\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"uint256\",\"name\":\"amountA\",\"type\":\"uint256\"},{\"internalType\":\"uint256\",\"name\":\"reserveA\",\"type\":\"uint256\"},{\"internalType\":\"uint256\",\"name\":\"reserveB\",\"type\":\"uint256\"}],\"name\":\"quote\",\"outputs\":[{\"internalType\":\"uint256\",\"name\":\"amountB\",\"type\":\"uint256\"}],\"stateMutability\":\"pure\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"tokenA\",\"type\":\"address\"},{\"internalType\":\"address\",\"name\":\"tokenB\",\"type\":\"address\"},{\"internalType\":\"uint256\",\"name\":\"liquidity\",\"type\":\"uint256\"},{\"internalType\":\"uint256\",\"name\":\"amountAMin\",\"type\":\"uint256\"},{\"internalType\":\"uint256\",\"name\":\"amountBMin\",\"type\":\"uint256\"},{\"internalType\":\"address\",\"name\":\"to\",\"type\":\"address\"},{\"internalType\":\"uint256\",\"name\":\"deadline\",\"type\":\"uint256\"}],\"name\":\"removeLiquidity\",\"outputs\":[{\"internalType\":\"uint256\",\"name\":\"amountA\",\"type\":\"uint256\"},{\"internalType\":\"uint256\",\"name\":\"amountB\",\"type\":\"uint256\"}],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"token\",\"type\":\"address\"},{\"internalType\":\"uint256\",\"name\":\"liquidity\",\"type\":\"uint256\"},{\"internalType\":\"uint256\",\"name\":\"amountTokenMin\",\"type\":\"uint256\"},{\"internalType\":\"uint256\",\"name\":\"amountETHMin\",\"type\":\"uint256\"},{\"internalType\":\"address\",\"name\":\"to\",\"type\":\"address\"},{\"internalType\":\"uint256\",\"name\":\"deadline\",\"type\":\"uint256\"}],\"name\":\"removeLiquidityETH\",\"outputs\":[{\"internalType\":\"uint256\",\"name\":\"amountToken\",\"type\":\"uint256\"},{\"internalType\":\"uint256\",\"name\":\"amountETH\",\"type\":\"uint256\"}],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"token\",\"type\":\"address\"},{\"internalType\":\"uint256\",\"name\":\"liquidity\",\"type\":\"uint256\"},{\"internalType\":\"uint256\",\"name\":\"amountTokenMin\",\"type\":\"uint256\"},{\"internalType\":\"uint256\",\"name\":\"amountETHMin\",\"type\":\"uint256\"},{\"internalType\":\"address\",\"name\":\"to\",\"type\":\"address\"},{\"internalType\":\"uint256\",\"name\":\"deadline\",\"type\":\"uint256\"}],\"name\":\"removeLiquidityETHSupportingFeeOnTransferTokens\",\"outputs\":[{\"internalType\":\"uint256\",\"name\":\"amountETH\",\"type\":\"uint256\"}],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"token\",\"type\":\"address\"},{\"internalType\":\"uint256\",\"name\":\"liquidity\",\"type\":\"uint256\"},{\"internalType\":\"uint256\",\"name\":\"amountTokenMin\",\"type\":\"uint256\"},{\"internalType\":\"uint256\",\"name\":\"amountETHMin\",\"type\":\"uint256\"},{\"internalType\":\"address\",\"name\":\"to\",\"type\":\"address\"},{\"internalType\":\"uint256\",\"name\":\"deadline\",\"type\":\"uint256\"},{\"internalType\":\"bool\",\"name\":\"approveMax\",\"type\":\"bool\"},{\"internalType\":\"uint8\",\"name\":\"v\",\"type\":\"uint8\"},{\"internalType\":\"bytes32\",\"name\":\"r\",\"type\":\"bytes32\"},{\"internalType\":\"bytes32\",\"name\":\"s\",\"type\":\"bytes32\"}],\"name\":\"removeLiquidityETHWithPermit\",\"outputs\":[{\"internalType\":\"uint256\",\"name\":\"amountToken\",\"type\":\"uint256\"},{\"internalType\":\"uint256\",\"name\":\"amountETH\",\"type\":\"uint256\"}],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"token\",\"type\":\"address\"},{\"internalType\":\"uint256\",\"name\":\"liquidity\",\"type\":\"uint256\"},{\"internalType\":\"uint256\",\"name\":\"amountTokenMin\",\"type\":\"uint256\"},{\"internalType\":\"uint256\",\"name\":\"amountETHMin\",\"type\":\"uint256\"},{\"internalType\":\"address\",\"name\":\"to\",\"type\":\"address\"},{\"internalType\":\"uint256\",\"name\":\"deadline\",\"type\":\"uint256\"},{\"internalType\":\"bool\",\"name\":\"approveMax\",\"type\":\"bool\"},{\"internalType\":\"uint8\",\"name\":\"v\",\"type\":\"uint8\"},{\"internalType\":\"bytes32\",\"name\":\"r\",\"type\":\"bytes32\"},{\"internalType\":\"bytes32\",\"name\":\"s\",\"type\":\"bytes32\"}],\"name\":\"removeLiquidityETHWithPermitSupportingFeeOnTransferTokens\",\"outputs\":[{\"internalType\":\"uint256\",\"name\":\"amountETH\",\"type\":\"uint256\"}],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"address\",\"name\":\"tokenA\",\"type\":\"address\"},{\"internalType\":\"address\",\"name\":\"tokenB\",\"type\":\"address\"},{\"internalType\":\"uint256\",\"name\":\"liquidity\",\"type\":\"uint256\"},{\"internalType\":\"uint256\",\"name\":\"amountAMin\",\"type\":\"uint256\"},{\"internalType\":\"uint256\",\"name\":\"amountBMin\",\"type\":\"uint256\"},{\"internalType\":\"address\",\"name\":\"to\",\"type\":\"address\"},{\"internalType\":\"uint256\",\"name\":\"deadline\",\"type\":\"uint256\"},{\"internalType\":\"bool\",\"name\":\"approveMax\",\"type\":\"bool\"},{\"internalType\":\"uint8\",\"name\":\"v\",\"type\":\"uint8\"},{\"internalType\":\"bytes32\",\"name\":\"r\",\"type\":\"bytes32\"},{\"internalType\":\"bytes32\",\"name\":\"s\",\"type\":\"bytes32\"}],\"name\":\"removeLiquidityWithPermit\",\"outputs\":[{\"internalType\":\"uint256\",\"name\":\"amountA\",\"type\":\"uint256\"},{\"internalType\":\"uint256\",\"name\":\"amountB\",\"type\":\"uint256\"}],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"uint256\",\"name\":\"amountOut\",\"type\":\"uint256\"},{\"internalType\":\"address[]\",\"name\":\"path\",\"type\":\"address[]\"},{\"internalType\":\"address\",\"name\":\"to\",\"type\":\"address\"},{\"internalType\":\"uint256\",\"name\":\"deadline\",\"type\":\"uint256\"}],\"name\":\"swapETHForExactTokens\",\"outputs\":[{\"internalType\":\"uint256[]\",\"name\":\"amounts\",\"type\":\"uint256[]\"}],\"stateMutability\":\"payable\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"uint256\",\"name\":\"amountOutMin\",\"type\":\"uint256\"},{\"internalType\":\"address[]\",\"name\":\"path\",\"type\":\"address[]\"},{\"internalType\":\"address\",\"name\":\"to\",\"type\":\"address\"},{\"internalType\":\"uint256\",\"name\":\"deadline\",\"type\":\"uint256\"}],\"name\":\"swapExactETHForTokens\",\"outputs\":[{\"internalType\":\"uint256[]\",\"name\":\"amounts\",\"type\":\"uint256[]\"}],\"stateMutability\":\"payable\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"uint256\",\"name\":\"amountOutMin\",\"type\":\"uint256\"},{\"internalType\":\"address[]\",\"name\":\"path\",\"type\":\"address[]\"},{\"internalType\":\"address\",\"name\":\"to\",\"type\":\"address\"},{\"internalType\":\"uint256\",\"name\":\"deadline\",\"type\":\"uint256\"}],\"name\":\"swapExactETHForTokensSupportingFeeOnTransferTokens\",\"outputs\":[],\"stateMutability\":\"payable\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"uint256\",\"name\":\"amountIn\",\"type\":\"uint256\"},{\"internalType\":\"uint256\",\"name\":\"amountOutMin\",\"type\":\"uint256\"},{\"internalType\":\"address[]\",\"name\":\"path\",\"type\":\"address[]\"},{\"internalType\":\"address\",\"name\":\"to\",\"type\":\"address\"},{\"internalType\":\"uint256\",\"name\":\"deadline\",\"type\":\"uint256\"}],\"name\":\"swapExactTokensForETH\",\"outputs\":[{\"internalType\":\"uint256[]\",\"name\":\"amounts\",\"type\":\"uint256[]\"}],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"uint256\",\"name\":\"amountIn\",\"type\":\"uint256\"},{\"internalType\":\"uint256\",\"name\":\"amountOutMin\",\"type\":\"uint256\"},{\"internalType\":\"address[]\",\"name\":\"path\",\"type\":\"address[]\"},{\"internalType\":\"address\",\"name\":\"to\",\"type\":\"address\"},{\"internalType\":\"uint256\",\"name\":\"deadline\",\"type\":\"uint256\"}],\"name\":\"swapExactTokensForETHSupportingFeeOnTransferTokens\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"uint256\",\"name\":\"amountIn\",\"type\":\"uint256\"},{\"internalType\":\"uint256\",\"name\":\"amountOutMin\",\"type\":\"uint256\"},{\"internalType\":\"address[]\",\"name\":\"path\",\"type\":\"address[]\"},{\"internalType\":\"address\",\"name\":\"to\",\"type\":\"address\"},{\"internalType\":\"uint256\",\"name\":\"deadline\",\"type\":\"uint256\"}],\"name\":\"swapExactTokensForTokens\",\"outputs\":[{\"internalType\":\"uint256[]\",\"name\":\"amounts\",\"type\":\"uint256[]\"}],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"uint256\",\"name\":\"amountIn\",\"type\":\"uint256\"},{\"internalType\":\"uint256\",\"name\":\"amountOutMin\",\"type\":\"uint256\"},{\"internalType\":\"address[]\",\"name\":\"path\",\"type\":\"address[]\"},{\"internalType\":\"address\",\"name\":\"to\",\"type\":\"address\"},{\"internalType\":\"uint256\",\"name\":\"deadline\",\"type\":\"uint256\"}],\"name\":\"swapExactTokensForTokensSupportingFeeOnTransferTokens\",\"outputs\":[],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"uint256\",\"name\":\"amountOut\",\"type\":\"uint256\"},{\"internalType\":\"uint256\",\"name\":\"amountInMax\",\"type\":\"uint256\"},{\"internalType\":\"address[]\",\"name\":\"path\",\"type\":\"address[]\"},{\"internalType\":\"address\",\"name\":\"to\",\"type\":\"address\"},{\"internalType\":\"uint256\",\"name\":\"deadline\",\"type\":\"uint256\"}],\"name\":\"swapTokensForExactETH\",\"outputs\":[{\"internalType\":\"uint256[]\",\"name\":\"amounts\",\"type\":\"uint256[]\"}],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"inputs\":[{\"internalType\":\"uint256\",\"name\":\"amountOut\",\"type\":\"uint256\"},{\"internalType\":\"uint256\",\"name\":\"amountInMax\",\"type\":\"uint256\"},{\"internalType\":\"address[]\",\"name\":\"path\",\"type\":\"address[]\"},{\"internalType\":\"address\",\"name\":\"to\",\"type\":\"address\"},{\"internalType\":\"uint256\",\"name\":\"deadline\",\"type\":\"uint256\"}],\"name\":\"swapTokensForExactTokens\",\"outputs\":[{\"internalType\":\"uint256[]\",\"name\":\"amounts\",\"type\":\"uint256[]\"}],\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"stateMutability\":\"payable\",\"type\":\"receive\"}]";
            string factoryABI =
                "[{\"inputs\":[{\"internalType\":\"address\",\"name\":\"_feeToSetter\",\"type\":\"address\"}],\"payable\":false,\"stateMutability\":\"nonpayable\",\"type\":\"constructor\"},{\"anonymous\":false,\"inputs\":[{\"indexed\":true,\"internalType\":\"address\",\"name\":\"token0\",\"type\":\"address\"},{\"indexed\":true,\"internalType\":\"address\",\"name\":\"token1\",\"type\":\"address\"},{\"indexed\":false,\"internalType\":\"address\",\"name\":\"pair\",\"type\":\"address\"},{\"indexed\":false,\"internalType\":\"uint256\",\"name\":\"\",\"type\":\"uint256\"}],\"name\":\"PairCreated\",\"type\":\"event\"},{\"constant\":true,\"inputs\":[],\"name\":\"INIT_CODE_PAIR_HASH\",\"outputs\":[{\"internalType\":\"bytes32\",\"name\":\"\",\"type\":\"bytes32\"}],\"payable\":false,\"stateMutability\":\"view\",\"type\":\"function\"},{\"constant\":true,\"inputs\":[{\"internalType\":\"uint256\",\"name\":\"\",\"type\":\"uint256\"}],\"name\":\"allPairs\",\"outputs\":[{\"internalType\":\"address\",\"name\":\"\",\"type\":\"address\"}],\"payable\":false,\"stateMutability\":\"view\",\"type\":\"function\"},{\"constant\":true,\"inputs\":[],\"name\":\"allPairsLength\",\"outputs\":[{\"internalType\":\"uint256\",\"name\":\"\",\"type\":\"uint256\"}],\"payable\":false,\"stateMutability\":\"view\",\"type\":\"function\"},{\"constant\":false,\"inputs\":[{\"internalType\":\"address\",\"name\":\"tokenA\",\"type\":\"address\"},{\"internalType\":\"address\",\"name\":\"tokenB\",\"type\":\"address\"}],\"name\":\"createPair\",\"outputs\":[{\"internalType\":\"address\",\"name\":\"pair\",\"type\":\"address\"}],\"payable\":false,\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"constant\":true,\"inputs\":[],\"name\":\"feeTo\",\"outputs\":[{\"internalType\":\"address\",\"name\":\"\",\"type\":\"address\"}],\"payable\":false,\"stateMutability\":\"view\",\"type\":\"function\"},{\"constant\":true,\"inputs\":[],\"name\":\"feeToSetter\",\"outputs\":[{\"internalType\":\"address\",\"name\":\"\",\"type\":\"address\"}],\"payable\":false,\"stateMutability\":\"view\",\"type\":\"function\"},{\"constant\":true,\"inputs\":[{\"internalType\":\"address\",\"name\":\"\",\"type\":\"address\"},{\"internalType\":\"address\",\"name\":\"\",\"type\":\"address\"}],\"name\":\"getPair\",\"outputs\":[{\"internalType\":\"address\",\"name\":\"\",\"type\":\"address\"}],\"payable\":false,\"stateMutability\":\"view\",\"type\":\"function\"},{\"constant\":false,\"inputs\":[{\"internalType\":\"address\",\"name\":\"_feeTo\",\"type\":\"address\"}],\"name\":\"setFeeTo\",\"outputs\":[],\"payable\":false,\"stateMutability\":\"nonpayable\",\"type\":\"function\"},{\"constant\":false,\"inputs\":[{\"internalType\":\"address\",\"name\":\"_feeToSetter\",\"type\":\"address\"}],\"name\":\"setFeeToSetter\",\"outputs\":[],\"payable\":false,\"stateMutability\":\"nonpayable\",\"type\":\"function\"}]";

            

            var service = new UniswapV2FactoryService(web3, MainWindow.currentFactory);

            try
            {
                while (true)
                {
                    var pairContractAddress = await service.GetPairQueryAsync(token, pair);
                result.tokenPair = pair;
                result.pairAddress=pairContractAddress;
                if (pairContractAddress == "0x0000000000000000000000000000000000000000")
                {
                    result.isFound = false;
                }
                else
                {
                    result.isFound = true;
                }

                result.pairP = 1;
                if (result.isFound)
                {
                    return result;
                }
                await Application.Current.Dispatcher.BeginInvoke(new Action(() => {
                    MainWindow.Instance.Consola1.WriteOutput(Environment.NewLine + "No pairs yet", Colors.Red);
                }), DispatcherPriority.Render);
                    Task.Delay(500);
                }
                
            }
            catch (Exception e)
            {
              await  Application.Current.Dispatcher.BeginInvoke(new Action(() => {
                    MainWindow.Instance.Consola1.WriteOutput(Environment.NewLine + e.Message+Environment.NewLine+"Contact support", Colors.Red);
                }), DispatcherPriority.Render);
                return result;
            }
            

        }
        
        public static async Task<TokenSearchResult> GetNewPairs(string token, bool bnb, bool route, CancellationToken ct)
        {
            var web3 = new Web3(Properties.Settings.Default.BSCNODE);
            var result = new TokenSearchResult();
            TokenSearchResult tokenResult = new TokenSearchResult();
            var service = new UniswapV2FactoryService(web3, MainWindow.currentFactory);
            var pairContractAddress = "";
            try
            {
                while (true)
                {
                    if (ct.IsCancellationRequested)
                    {
                        ct.ThrowIfCancellationRequested();
                    }
                    if (route)
                    {
                        pairContractAddress = await service.GetPairQueryAsync(token, usdtContract);

                        if (pairContractAddress != "0x0000000000000000000000000000000000000000")
                        {
                            result.pairAddressUSDT = pairContractAddress;
                            result.tokenPair = usdtContract;
                            result.isFound = true;
                            result.pairP = 1;
                            await Application.Current.Dispatcher.BeginInvoke(new Action(() => {
                                MainWindow.Instance.Consola1.WriteOutput(Environment.NewLine + "Token Found Pair: USDT", Colors.Green);
                            }), DispatcherPriority.Render);
                        }
                        else
                        {
                            result.pairAddressUSDT = "0x0000000000000000000000000000000000000000";
                        }
                        pairContractAddress = await service.GetPairQueryAsync(token, busdcontrac);
                        if (pairContractAddress != "0x0000000000000000000000000000000000000000")
                        {
                            result.pairAddressBUSD = pairContractAddress;
                            result.tokenPair = busdcontrac;
                            result.isFound = true;
                            result.pairP = 1;
                            await Application.Current.Dispatcher.BeginInvoke(new Action(() => {
                                MainWindow.Instance.Consola1.WriteOutput(Environment.NewLine + "Token Found Pair: BUSD", Colors.Green);
                            }), DispatcherPriority.Render);

                        }
                        else
                        {
                            result.pairAddressBUSD = "0x0000000000000000000000000000000000000000";
                        }
                        pairContractAddress = await service.GetPairQueryAsync(token, bnbcontrac);
                        if (pairContractAddress != "0x0000000000000000000000000000000000000000")
                        {
                            result.pairAddress = pairContractAddress;
                            result.tokenPair = bnbcontrac;
                            result.isFound = true;
                            result.pairP = 1;
                            await Application.Current.Dispatcher.BeginInvoke(new Action(() => {
                                MainWindow.Instance.Consola1.WriteOutput(Environment.NewLine + "Token Found Pair: BNB", Colors.Green);
                            }), DispatcherPriority.Render);
                            
                        }
                        else
                        {
                            result.pairAddress = "0x0000000000000000000000000000000000000000";
                        }

                        if (result.isFound)
                        {
                            return result;
                        }
                    }
                    else
                    {
                        if (bnb)
                        {
                            pairContractAddress = await service.GetPairQueryAsync(token, bnbcontrac);
                            if (pairContractAddress != "0x0000000000000000000000000000000000000000")
                            {
                                result.pairAddress = pairContractAddress;
                                result.tokenPair = bnbcontrac;
                                result.isFound = true;
                                result.pairP = 1;
                                await Application.Current.Dispatcher.BeginInvoke(new Action(() => {
                                    MainWindow.Instance.Consola1.WriteOutput(Environment.NewLine + "Token Found Pair: BNB", Colors.Green);
                                }), DispatcherPriority.Render);
                                return result;
                            }
                        }
                        else
                        {
                            pairContractAddress = await service.GetPairQueryAsync(token, busdcontrac);
                            if (pairContractAddress != "0x0000000000000000000000000000000000000000")
                            {
                                result.pairAddress = pairContractAddress;
                                result.tokenPair = busdcontrac;
                                result.isFound = true;
                                result.pairP = 1;
                                await Application.Current.Dispatcher.BeginInvoke(new Action(() => {
                                    MainWindow.Instance.Consola1.WriteOutput(Environment.NewLine + "Token Found Pair: BUSD", Colors.Green);
                                }), DispatcherPriority.Render);
                                return result;
                            }
                        }
                    }
                   
                    await Application.Current.Dispatcher.BeginInvoke(new Action(() => {
                        MainWindow.Instance.Consola1.WriteOutput(Environment.NewLine + "Not found, retrying Time:" + DateTime.Now, Colors.Red);
                    }), DispatcherPriority.Render);
                    await Task.Delay(500);
                }


            }
            catch (Exception e)
            {
              await  Application.Current.Dispatcher.BeginInvoke(new Action(() => {
                    MainWindow.Instance.Consola1.WriteOutput(Environment.NewLine + e.Message + Environment.NewLine + "Contact support", Colors.Red);
                }), DispatcherPriority.Render);
                return result;
            }

        }

        public static async Task<string> GetNameTask(string token)
        {
            var nameFunction = "";
            var contractABI =
                      @"[{""inputs"":[],""payable"":false,""stateMutability"":""nonpayable"",""type"":""constructor""},{""anonymous"":false,""inputs"":[{""indexed"":true,""internalType"":""address"",""name"":""owner"",""type"":""address""},{""indexed"":true,""internalType"":""address"",""name"":""spender"",""type"":""address""},{""indexed"":false,""internalType"":""uint256"",""name"":""value"",""type"":""uint256""}],""name"":""Approval"",""type"":""event""},{""anonymous"":false,""inputs"":[{""indexed"":true,""internalType"":""address"",""name"":""sender"",""type"":""address""},{""indexed"":false,""internalType"":""uint256"",""name"":""amount0"",""type"":""uint256""},{""indexed"":false,""internalType"":""uint256"",""name"":""amount1"",""type"":""uint256""},{""indexed"":true,""internalType"":""address"",""name"":""to"",""type"":""address""}],""name"":""Burn"",""type"":""event""},{""anonymous"":false,""inputs"":[{""indexed"":true,""internalType"":""address"",""name"":""sender"",""type"":""address""},{""indexed"":false,""internalType"":""uint256"",""name"":""amount0"",""type"":""uint256""},{""indexed"":false,""internalType"":""uint256"",""name"":""amount1"",""type"":""uint256""}],""name"":""Mint"",""type"":""event""},{""anonymous"":false,""inputs"":[{""indexed"":true,""internalType"":""address"",""name"":""sender"",""type"":""address""},{""indexed"":false,""internalType"":""uint256"",""name"":""amount0In"",""type"":""uint256""},{""indexed"":false,""internalType"":""uint256"",""name"":""amount1In"",""type"":""uint256""},{""indexed"":false,""internalType"":""uint256"",""name"":""amount0Out"",""type"":""uint256""},{""indexed"":false,""internalType"":""uint256"",""name"":""amount1Out"",""type"":""uint256""},{""indexed"":true,""internalType"":""address"",""name"":""to"",""type"":""address""}],""name"":""Swap"",""type"":""event""},{""anonymous"":false,""inputs"":[{""indexed"":false,""internalType"":""uint112"",""name"":""reserve0"",""type"":""uint112""},{""indexed"":false,""internalType"":""uint112"",""name"":""reserve1"",""type"":""uint112""}],""name"":""Sync"",""type"":""event""},{""anonymous"":false,""inputs"":[{""indexed"":true,""internalType"":""address"",""name"":""from"",""type"":""address""},{""indexed"":true,""internalType"":""address"",""name"":""to"",""type"":""address""},{""indexed"":false,""internalType"":""uint256"",""name"":""value"",""type"":""uint256""}],""name"":""Transfer"",""type"":""event""},{""constant"":true,""inputs"":[],""name"":""DOMAIN_SEPARATOR"",""outputs"":[{""internalType"":""bytes32"",""name"":"""",""type"":""bytes32""}],""payable"":false,""stateMutability"":""view"",""type"":""function""},{""constant"":true,""inputs"":[],""name"":""MINIMUM_LIQUIDITY"",""outputs"":[{""internalType"":""uint256"",""name"":"""",""type"":""uint256""}],""payable"":false,""stateMutability"":""view"",""type"":""function""},{""constant"":true,""inputs"":[],""name"":""PERMIT_TYPEHASH"",""outputs"":[{""internalType"":""bytes32"",""name"":"""",""type"":""bytes32""}],""payable"":false,""stateMutability"":""view"",""type"":""function""},{""constant"":true,""inputs"":[{""internalType"":""address"",""name"":"""",""type"":""address""},{""internalType"":""address"",""name"":"""",""type"":""address""}],""name"":""allowance"",""outputs"":[{""internalType"":""uint256"",""name"":"""",""type"":""uint256""}],""payable"":false,""stateMutability"":""view"",""type"":""function""},{""constant"":false,""inputs"":[{""internalType"":""address"",""name"":""spender"",""type"":""address""},{""internalType"":""uint256"",""name"":""value"",""type"":""uint256""}],""name"":""approve"",""outputs"":[{""internalType"":""bool"",""name"":"""",""type"":""bool""}],""payable"":false,""stateMutability"":""nonpayable"",""type"":""function""},{""constant"":true,""inputs"":[{""internalType"":""address"",""name"":"""",""type"":""address""}],""name"":""balanceOf"",""outputs"":[{""internalType"":""uint256"",""name"":"""",""type"":""uint256""}],""payable"":false,""stateMutability"":""view"",""type"":""function""},{""constant"":false,""inputs"":[{""internalType"":""address"",""name"":""to"",""type"":""address""}],""name"":""burn"",""outputs"":[{""internalType"":""uint256"",""name"":""amount0"",""type"":""uint256""},{""internalType"":""uint256"",""name"":""amount1"",""type"":""uint256""}],""payable"":false,""stateMutability"":""nonpayable"",""type"":""function""},{""constant"":true,""inputs"":[],""name"":""decimals"",""outputs"":[{""internalType"":""uint8"",""name"":"""",""type"":""uint8""}],""payable"":false,""stateMutability"":""view"",""type"":""function""},{""constant"":true,""inputs"":[],""name"":""factory"",""outputs"":[{""internalType"":""address"",""name"":"""",""type"":""address""}],""payable"":false,""stateMutability"":""view"",""type"":""function""},{""constant"":true,""inputs"":[],""name"":""getReserves"",""outputs"":[{""internalType"":""uint112"",""name"":""_reserve0"",""type"":""uint112""},{""internalType"":""uint112"",""name"":""_reserve1"",""type"":""uint112""},{""internalType"":""uint32"",""name"":""_blockTimestampLast"",""type"":""uint32""}],""payable"":false,""stateMutability"":""view"",""type"":""function""},{""constant"":false,""inputs"":[{""internalType"":""address"",""name"":""_token0"",""type"":""address""},{""internalType"":""address"",""name"":""_token1"",""type"":""address""}],""name"":""initialize"",""outputs"":[],""payable"":false,""stateMutability"":""nonpayable"",""type"":""function""},{""constant"":true,""inputs"":[],""name"":""kLast"",""outputs"":[{""internalType"":""uint256"",""name"":"""",""type"":""uint256""}],""payable"":false,""stateMutability"":""view"",""type"":""function""},{""constant"":false,""inputs"":[{""internalType"":""address"",""name"":""to"",""type"":""address""}],""name"":""mint"",""outputs"":[{""internalType"":""uint256"",""name"":""liquidity"",""type"":""uint256""}],""payable"":false,""stateMutability"":""nonpayable"",""type"":""function""},{""constant"":true,""inputs"":[],""name"":""name"",""outputs"":[{""internalType"":""string"",""name"":"""",""type"":""string""}],""payable"":false,""stateMutability"":""view"",""type"":""function""},{""constant"":true,""inputs"":[{""internalType"":""address"",""name"":"""",""type"":""address""}],""name"":""nonces"",""outputs"":[{""internalType"":""uint256"",""name"":"""",""type"":""uint256""}],""payable"":false,""stateMutability"":""view"",""type"":""function""},{""constant"":false,""inputs"":[{""internalType"":""address"",""name"":""owner"",""type"":""address""},{""internalType"":""address"",""name"":""spender"",""type"":""address""},{""internalType"":""uint256"",""name"":""value"",""type"":""uint256""},{""internalType"":""uint256"",""name"":""deadline"",""type"":""uint256""},{""internalType"":""uint8"",""name"":""v"",""type"":""uint8""},{""internalType"":""bytes32"",""name"":""r"",""type"":""bytes32""},{""internalType"":""bytes32"",""name"":""s"",""type"":""bytes32""}],""name"":""permit"",""outputs"":[],""payable"":false,""stateMutability"":""nonpayable"",""type"":""function""},{""constant"":true,""inputs"":[],""name"":""price0CumulativeLast"",""outputs"":[{""internalType"":""uint256"",""name"":"""",""type"":""uint256""}],""payable"":false,""stateMutability"":""view"",""type"":""function""},{""constant"":true,""inputs"":[],""name"":""price1CumulativeLast"",""outputs"":[{""internalType"":""uint256"",""name"":"""",""type"":""uint256""}],""payable"":false,""stateMutability"":""view"",""type"":""function""},{""constant"":false,""inputs"":[{""internalType"":""address"",""name"":""to"",""type"":""address""}],""name"":""skim"",""outputs"":[],""payable"":false,""stateMutability"":""nonpayable"",""type"":""function""},{""constant"":false,""inputs"":[{""internalType"":""uint256"",""name"":""amount0Out"",""type"":""uint256""},{""internalType"":""uint256"",""name"":""amount1Out"",""type"":""uint256""},{""internalType"":""address"",""name"":""to"",""type"":""address""},{""internalType"":""bytes"",""name"":""data"",""type"":""bytes""}],""name"":""swap"",""outputs"":[],""payable"":false,""stateMutability"":""nonpayable"",""type"":""function""},{""constant"":true,""inputs"":[],""name"":""symbol"",""outputs"":[{""internalType"":""string"",""name"":"""",""type"":""string""}],""payable"":false,""stateMutability"":""view"",""type"":""function""},{""constant"":false,""inputs"":[],""name"":""sync"",""outputs"":[],""payable"":false,""stateMutability"":""nonpayable"",""type"":""function""},{""constant"":true,""inputs"":[],""name"":""token0"",""outputs"":[{""internalType"":""address"",""name"":"""",""type"":""address""}],""payable"":false,""stateMutability"":""view"",""type"":""function""},{""constant"":true,""inputs"":[],""name"":""token1"",""outputs"":[{""internalType"":""address"",""name"":"""",""type"":""address""}],""payable"":false,""stateMutability"":""view"",""type"":""function""},{""constant"":true,""inputs"":[],""name"":""totalSupply"",""outputs"":[{""internalType"":""uint256"",""name"":"""",""type"":""uint256""}],""payable"":false,""stateMutability"":""view"",""type"":""function""},{""constant"":false,""inputs"":[{""internalType"":""address"",""name"":""to"",""type"":""address""},{""internalType"":""uint256"",""name"":""value"",""type"":""uint256""}],""name"":""transfer"",""outputs"":[{""internalType"":""bool"",""name"":"""",""type"":""bool""}],""payable"":false,""stateMutability"":""nonpayable"",""type"":""function""},{""constant"":false,""inputs"":[{""internalType"":""address"",""name"":""from"",""type"":""address""},{""internalType"":""address"",""name"":""to"",""type"":""address""},{""internalType"":""uint256"",""name"":""value"",""type"":""uint256""}],""name"":""transferFrom"",""outputs"":[{""internalType"":""bool"",""name"":"""",""type"":""bool""}],""payable"":false,""stateMutability"":""nonpayable"",""type"":""function""}]";

            var web3 = new Web3(Properties.Settings.Default.BSCNODE);
            var contract = web3.Eth.GetContract(contractABI, token);
            try
            {
                 nameFunction = await contract.GetFunction("symbol").CallAsync<string>();
                 if (nameFunction == null)
                 {
                     nameFunction = "Token";
                 }
            }
            catch (Exception e)
            {
                
            }
            
            return nameFunction;
        }
        


         public static async Task<decimal> GetAccountBalance()
         {

             double value = 0;
             var web3 = new Web3(Properties.Settings.Default.BSCNODE);
             var balance =
                 await web3.Eth.GetBalance.SendRequestAsync(MainWindow.wallet);
             value = (double) balance.Value;
             return  Web3.Convert.FromWei(balance.Value);
   

         }
        public static async Task<TxResult> DeBNBaToken(string cantidad, decimal minimo, List<string> tokens, string gas)
        {
            try
            {
                var url = Properties.Settings.Default.BSCNODE;
                var myWallet = MainWindow.wallet;
                var privateKey = Properties.Settings.Default.PK;// Properties.Settings.Default.PK;
                var account = new Account(privateKey, 56);
                var web3 = new Web3(account, url);
                var camtidadBUSD = Web3.Convert.ToWei(cantidad,UnitConversion.EthUnit.Ether);// Web3.Convert.ToWei(decimal.Parse(cantidad));//CANTIDAD DE BUSD A INTERCAMBIAR - 20$
                var camtidadToken = Web3.Convert.ToWei(minimo,UnitConversion.EthUnit.Ether);//MINIMO DE TOKEN META A RECIBIR - 5 META
                var uniswapV2Router02Service = new UniswapV2Router02Service(web3, MainWindow.currentRouter);
                var deadline = DateTimeOffset.Now.AddMinutes(15).ToUnixTimeSeconds();
                var swapEthForExactTokens = new Nethereum.Uniswap.Contracts.UniswapV2Router02.ContractDefinition.SwapExactETHForTokensSupportingFeeOnTransferTokensFunction()
                {
                    AmountOutMin = camtidadToken,
                    Path = tokens,
                    Deadline = deadline,
                    To = myWallet,
                    AmountToSend = camtidadBUSD,
                    GasPrice = Web3.Convert.ToWei(gas, UnitConversion.EthUnit.Gwei),
                    Gas = BigInteger.Parse("300000")
                };

                var swapReceipt = await uniswapV2Router02Service.SwapExactETHForTokensSupportingFeeOnTransferTokensRequestAndWaitForReceiptAsync(swapEthForExactTokens);
                var swapLog = swapReceipt.Logs.DecodeAllEvents<SwapEventDTO>();
                var resultado = new TxResult();
                var result = await Task.Run(async () => { return swapReceipt.Status.HexValue;}) ;
                  if (result == "0x1")
                  {
                      decimal temp =0;
                      resultado = new TxResult();
                      
                      resultado.txHash = swapReceipt.TransactionHash;
                      resultado.Time=DateTime.Now;
                      
                      resultado.result = "Success";
                      if (swapLog.Count == 1)
                      { temp = Web3.Convert.FromWei(swapLog[0].Event.Amount0Out);
                         
                          resultado.value = temp.ToString();
                          temp = Web3.Convert.FromWei(swapLog[0].Event.Amount1In);
                          resultado.ValueSpend = temp.ToString();
                          await Application.Current.Dispatcher.BeginInvoke(new Action(() => {
                          MainWindow.Instance.Consola1.WriteOutput(Environment.NewLine + "Success Tx: " + swapReceipt.TransactionHash + "Value: " + Web3.Convert.FromWei(swapLog[0].Event.Amount1In).ToString(), Colors.Green);
                      }), DispatcherPriority.Render);
                      }
                      else
                      {
                          if (swapLog.Count == 2)
                          {
                              temp = Web3.Convert.FromWei(swapLog[1].Event.Amount1Out);

                              resultado.value = temp.ToString();
                              temp = Web3.Convert.FromWei(swapLog[0].Event.Amount1In);
                              resultado.ValueSpend = temp.ToString();
                            await Application.Current.Dispatcher.BeginInvoke(new Action(() => {
                                  MainWindow.Instance.Consola1.WriteOutput(Environment.NewLine + "Success Tx: " + swapReceipt.TransactionHash + "Value: " + Web3.Convert.FromWei(swapLog[0].Event.Amount1In).ToString(), Colors.Green);
                              }), DispatcherPriority.Render);
                        }
                          else
                          {
                              await Application.Current.Dispatcher.BeginInvoke(new Action(() => {
                              MainWindow.Instance.Consola1.WriteOutput(Environment.NewLine + "Success Tx: " + swapReceipt.TransactionHash + "Value: "  , Colors.Green);
                          }), DispatcherPriority.Render);
                          }
                          
                    }
                    return resultado;

                  }
                  else
                  {
       
                    resultado = new TxResult();
                    resultado.value = "0";
                    resultado.txHash = swapReceipt.TransactionHash;
                    resultado.Time = DateTime.Now;
                    resultado.ValueSpend = "0";
                    resultado.result = "Failed";
                    await Application.Current.Dispatcher.BeginInvoke(new Action(() => {
                        MainWindow.Instance.Consola1.WriteOutput(Environment.NewLine + "Failed Tx: " + swapReceipt.TransactionHash , Colors.Red);
                    }), DispatcherPriority.Render);
                    return resultado;
                }

            }
            catch (Exception ex)
            {
               await Application.Current.Dispatcher.BeginInvoke(new Action(() => {
                    MainWindow.Instance.Consola1.WriteOutput(Environment.NewLine + ex.Message + Environment.NewLine + "Contact support", Colors.Red);
                }), DispatcherPriority.Render);
                return
                    new TxResult(); 
            }
            
        }

        public static async Task<decimal> GetReservesTask(string pairAddress, int pair, CancellationToken ct)
        {
            
            var web3 = new Web3(Properties.Settings.Default.BSCNODE);

            UniswapV2PairService uniservices = new UniswapV2PairService(web3, pairAddress);
   
           decimal reserve0 = 0;
           try
            {
                if(pairAddress!= "0x0000000000000000000000000000000000000000")
                while (reserve0 < 100 )
                {
                    if (ct.IsCancellationRequested)
                    {
                        ct.ThrowIfCancellationRequested();
                    }
                    var result = await uniservices.GetReservesQueryAsync();
                    string token0 = await uniservices.Token0QueryAsync();
                    if (token0 == usdtContract || token0==bnbcontrac || token0==busdcontrac)
                    {
                        reserve0 = Web3.Convert.FromWei(result.Reserve0, UnitConversion.EthUnit.Ether);
                    }
                    else
                    {
                        reserve0 = Web3.Convert.FromWei(result.Reserve1, UnitConversion.EthUnit.Ether);
                    }

                    await Application.Current.Dispatcher.BeginInvoke(new Action(() => {
                        MainWindow.Instance.Consola1.WriteOutput(Environment.NewLine+"Getting liquidity: " + reserve0, Colors.Red);
                    }), DispatcherPriority.Render);
                    await Task.Delay(300);
                }
                return reserve0;
            }
            catch (Exception e)
            {
                await Application.Current.Dispatcher.BeginInvoke(new Action(() => {
                    MainWindow.Instance.Consola1.WriteOutput(Environment.NewLine +e.Message+ "Contact Support", Colors.Red);
                }), DispatcherPriority.Render);
                return 0;
            }
        }
        public static async Task<PairReserve> GetReservesRouteTask(TokenSearchResult pairAddress, CancellationToken ct)
        {
            decimal reserve0 = 0;
            decimal reserve1 = 0;
            decimal reserve2 = 0;
            string token0 = "";
            var web3 = new Web3(Properties.Settings.Default.BSCNODE);

            UniswapV2PairService uniservices = new UniswapV2PairService(web3, pairAddress.pairAddress);
            try
            {
                while (true)
                {
                    if (ct.IsCancellationRequested)
                    {
                        return new PairReserve();
                    }

                    if (pairAddress.pairAddress != "0x0000000000000000000000000000000000000000")
                    {
                        uniservices = new UniswapV2PairService(web3, pairAddress.pairAddress);
                        var result = await uniservices.GetReservesQueryAsync();
                     token0 = await uniservices.Token0QueryAsync();
                     reserve0 = Web3.Convert.FromWei(token0 == bnbcontrac ? result.Reserve0 : result.Reserve1, UnitConversion.EthUnit.Ether);
                    }
                    
                    if (pairAddress.pairAddressBUSD != "0x0000000000000000000000000000000000000000")
                    {
                        uniservices = new UniswapV2PairService(web3, pairAddress.pairAddressBUSD);
                        var result = await uniservices.GetReservesQueryAsync();
                     token0 = await uniservices.Token0QueryAsync();
                     reserve1 = Web3.Convert.FromWei(token0 == busdcontrac ? result.Reserve0 : result.Reserve1, UnitConversion.EthUnit.Ether);
                    }
                    
                     
                    if (pairAddress.pairAddressUSDT != "0x0000000000000000000000000000000000000000")
                    {
                        uniservices = new UniswapV2PairService(web3, pairAddress.pairAddressUSDT);
                   var result = await uniservices.GetReservesQueryAsync();
                     token0 = await uniservices.Token0QueryAsync();
                     reserve2 = Web3.Convert.FromWei(token0 == usdtContract ? result.Reserve0 : result.Reserve1, UnitConversion.EthUnit.Ether);
                    }
                    

                    if (reserve0 > 100)
                    {
                        return new PairReserve(){Pair = bnbcontrac, Reserver = reserve0};
                    }
                    else
                    {

                        if (reserve1 > 100)
                        {
                            return new PairReserve() { Pair = busdcontrac, Reserver = reserve1 };
                        }
                        else
                        {
                            if (reserve2 > 100)
                            {
                                return new PairReserve() { Pair = usdtContract, Reserver = reserve2 };
                            }
                        }
                    }

                    Task.Delay(200);

                }
            }
            catch (Exception e)
            {
                await Application.Current.Dispatcher.BeginInvoke(new Action(() => {
                    MainWindow.Instance.Consola1.WriteOutput(Environment.NewLine + e.Message + "Contact Support", Colors.Red);
                }), DispatcherPriority.Render);
                return new PairReserve();
            }
        }
        public static async Task<TxResult> AproveTask(string token, string gas)
        {
            try
            {
                TxResult result = new TxResult();
                var account = new Account(Properties.Settings.Default.PK, 56);
                var web3 = new Web3(account, Properties.Settings.Default.BSCNODE);
                var contract = web3.Eth.GetContractTransactionHandler<ApproveFunction>();
                var approve = new ApproveFunction()
                {
                    Spender = MainWindow.currentRouter,
                    Value = BigInteger.Pow(2, 256) - 1,
                    Gas = 50000,
                    GasPrice = Web3.Convert.ToWei(gas, UnitConversion.EthUnit.Gwei)
                };
                var result2 = await contract.SendRequestAndWaitForReceiptAsync(token, approve);

                var status = await Task.Run(async () => { return result2.Status.HexValue; });
                if (status == "0x1")
                {
                    result.result = "Success";
                    result.txHash = result2.TransactionHash;
                    result.Time = DateTime.Now;
                    await Application.Current.Dispatcher.BeginInvoke(new Action(() => {
                        MainWindow.Instance.Consola1.WriteOutput(Environment.NewLine + "Success Tx: " + result2.TransactionHash, Colors.Green);
                    }), DispatcherPriority.Render);
                }
                else
                {
                    result.result = "Failed";
                    result.txHash = result2.TransactionHash;
                    result.Time = DateTime.Now;
                    await Application.Current.Dispatcher.BeginInvoke(new Action(() => {
                        MainWindow.Instance.Consola1.WriteOutput(Environment.NewLine + "Failed Tx: " + result2.TransactionHash, Colors.Red);
                    }), DispatcherPriority.Render);
                }
                return result;
            }
            catch (Exception e)
            {
                await Application.Current.Dispatcher.BeginInvoke(new Action(() => {
                    MainWindow.Instance.Consola1.WriteOutput(Environment.NewLine + e.Message + Environment.NewLine + "Contact support", Colors.Red);
                }), DispatcherPriority.Render);
                return new TxResult();
            }
           
        }

        public static async Task<TxResult> DeTokenABNB(string cantidad, decimal minimo, List<string> tokens, string gas)
        {
            try
            {
                var url = Properties.Settings.Default.BSCNODE;
                var myWallet = MainWindow.wallet;
                var privateKey = Properties.Settings.Default.PK;
                var account = new Account(privateKey, 56);
                var web3 = new Web3(account, url);
                var uniswapV2Router02Service = new UniswapV2Router02Service(web3, MainWindow.currentRouter);
                var camtidadBUSD = Web3.Convert.ToWei(cantidad,UnitConversion.EthUnit.Ether);//CANTIDAD DE BUSD A INTERCAMBIAR - 20$
                var camtidadToken = Web3.Convert.ToWei(minimo, UnitConversion.EthUnit.Ether);//MINIMO DE TOKEN META A RECIBIR - 5 META
                var swapDTO = new SwapExactTokensForETHSupportingFeeOnTransferTokensFunction()
                {
                    AmountIn = camtidadBUSD,
                    AmountOutMin = camtidadToken, //MINIMO DE TOKENS A RECIBIR
                    Path = tokens,
                    To = myWallet,
                    Deadline = ((DateTimeOffset)DateTime.Now).ToUnixTimeSeconds() + 260,
                    GasPrice = Web3.Convert.ToWei(gas, UnitConversion.EthUnit.Gwei),
                    Gas = 300000
                };
                var swapReceipt = await uniswapV2Router02Service.SwapExactTokensForETHSupportingFeeOnTransferTokensRequestAndWaitForReceiptAsync(swapDTO);
                var swapLog = swapReceipt.Logs.DecodeAllEvents<SwapEventDTO>();
                var resultado = new TxResult();
                var result = await Task.Run(async () => { return swapReceipt.Status.HexValue; });
                if (result == "0x1")
                {
                    decimal temp = 0;
                    resultado = new TxResult();
                   
                    resultado.txHash = swapReceipt.TransactionHash;
                    resultado.Time = DateTime.Now;
                    
                    resultado.result = "Success";
                    if (swapLog.Count == 1)
                    {
                         temp = Web3.Convert.FromWei(swapLog[0].Event.Amount1Out);
                        resultado.value = temp.ToString();
                        temp = Web3.Convert.FromWei(swapLog[0].Event.Amount0In);
                        resultado.ValueSpend = temp.ToString();
                        await Application.Current.Dispatcher.BeginInvoke(new Action(() => {
                            MainWindow.Instance.Consola1.WriteOutput(Environment.NewLine + "Success Tx: " + swapReceipt.TransactionHash + "Value: " + Web3.Convert.FromWei(swapLog[0].Event.Amount1In).ToString(), Colors.Green);
                        }), DispatcherPriority.Render);
                        return resultado;
                    }
                    else
                    {
                        if (swapLog.Count == 2)
                        {
                            temp = Web3.Convert.FromWei(swapLog[1].Event.Amount1Out);
                            resultado.value = temp.ToString();
                            temp = Web3.Convert.FromWei(swapLog[0].Event.Amount1In);
                            resultado.ValueSpend = temp.ToString();
                            await Application.Current.Dispatcher.BeginInvoke(new Action(() => {
                                MainWindow.Instance.Consola1.WriteOutput(Environment.NewLine + "Success Tx: " + swapReceipt.TransactionHash + "Value: " + Web3.Convert.FromWei(swapLog[1].Event.Amount1Out).ToString(), Colors.Green);
                            }), DispatcherPriority.Render);
                            return resultado;
                        }
                        else
                        {
                            await Application.Current.Dispatcher.BeginInvoke(new Action(() => {
                                MainWindow.Instance.Consola1.WriteOutput(Environment.NewLine + "Success Tx: " + swapReceipt.TransactionHash + "Value: ", Colors.Green);
                            }), DispatcherPriority.Render);
                            return resultado;
                        }
                    }
                   
                }
                else
                {
                    resultado = new TxResult();
                    resultado.txHash = swapReceipt.TransactionHash;
                    resultado.Time = DateTime.Now;
                    resultado.result = "Failed";
                    if (swapLog.Count > 0)
                    {
                        resultado.value = swapLog[0].Event.Amount1In.ToString("c");
                        resultado.ValueSpend = swapLog[0].Event.Amount0Out.ToString("c");
                    }
                    await Application.Current.Dispatcher.BeginInvoke(new Action(() => {
                        MainWindow.Instance.Consola1.WriteOutput(Environment.NewLine + "Failed Tx: " + swapReceipt.TransactionHash , Colors.Red);
                    }), DispatcherPriority.Render);
                    return resultado;
                }
            }
            catch (Exception ex)
            {
               await Application.Current.Dispatcher.BeginInvoke(new Action(() => {
                    MainWindow.Instance.Consola1.WriteOutput(Environment.NewLine + ex.Message + Environment.NewLine + "Contact support", Colors.Red);
                }), DispatcherPriority.Render);
                return new TxResult();
            }

            
        }
        public static async Task<decimal> TokenBalanceAsync(string token)
           {
               var web3 = new Web3(Properties.Settings.Default.BSCNODE);

               if (token.Length != 42)
                   return 0;
               var contractABI =
                   @"[{""inputs"":[],""payable"":false,""stateMutability"":""nonpayable"",""type"":""constructor""},{""anonymous"":false,""inputs"":[{""indexed"":true,""internalType"":""address"",""name"":""owner"",""type"":""address""},{""indexed"":true,""internalType"":""address"",""name"":""spender"",""type"":""address""},{""indexed"":false,""internalType"":""uint256"",""name"":""value"",""type"":""uint256""}],""name"":""Approval"",""type"":""event""},{""anonymous"":false,""inputs"":[{""indexed"":true,""internalType"":""address"",""name"":""sender"",""type"":""address""},{""indexed"":false,""internalType"":""uint256"",""name"":""amount0"",""type"":""uint256""},{""indexed"":false,""internalType"":""uint256"",""name"":""amount1"",""type"":""uint256""},{""indexed"":true,""internalType"":""address"",""name"":""to"",""type"":""address""}],""name"":""Burn"",""type"":""event""},{""anonymous"":false,""inputs"":[{""indexed"":true,""internalType"":""address"",""name"":""sender"",""type"":""address""},{""indexed"":false,""internalType"":""uint256"",""name"":""amount0"",""type"":""uint256""},{""indexed"":false,""internalType"":""uint256"",""name"":""amount1"",""type"":""uint256""}],""name"":""Mint"",""type"":""event""},{""anonymous"":false,""inputs"":[{""indexed"":true,""internalType"":""address"",""name"":""sender"",""type"":""address""},{""indexed"":false,""internalType"":""uint256"",""name"":""amount0In"",""type"":""uint256""},{""indexed"":false,""internalType"":""uint256"",""name"":""amount1In"",""type"":""uint256""},{""indexed"":false,""internalType"":""uint256"",""name"":""amount0Out"",""type"":""uint256""},{""indexed"":false,""internalType"":""uint256"",""name"":""amount1Out"",""type"":""uint256""},{""indexed"":true,""internalType"":""address"",""name"":""to"",""type"":""address""}],""name"":""Swap"",""type"":""event""},{""anonymous"":false,""inputs"":[{""indexed"":false,""internalType"":""uint112"",""name"":""reserve0"",""type"":""uint112""},{""indexed"":false,""internalType"":""uint112"",""name"":""reserve1"",""type"":""uint112""}],""name"":""Sync"",""type"":""event""},{""anonymous"":false,""inputs"":[{""indexed"":true,""internalType"":""address"",""name"":""from"",""type"":""address""},{""indexed"":true,""internalType"":""address"",""name"":""to"",""type"":""address""},{""indexed"":false,""internalType"":""uint256"",""name"":""value"",""type"":""uint256""}],""name"":""Transfer"",""type"":""event""},{""constant"":true,""inputs"":[],""name"":""DOMAIN_SEPARATOR"",""outputs"":[{""internalType"":""bytes32"",""name"":"""",""type"":""bytes32""}],""payable"":false,""stateMutability"":""view"",""type"":""function""},{""constant"":true,""inputs"":[],""name"":""MINIMUM_LIQUIDITY"",""outputs"":[{""internalType"":""uint256"",""name"":"""",""type"":""uint256""}],""payable"":false,""stateMutability"":""view"",""type"":""function""},{""constant"":true,""inputs"":[],""name"":""PERMIT_TYPEHASH"",""outputs"":[{""internalType"":""bytes32"",""name"":"""",""type"":""bytes32""}],""payable"":false,""stateMutability"":""view"",""type"":""function""},{""constant"":true,""inputs"":[{""internalType"":""address"",""name"":"""",""type"":""address""},{""internalType"":""address"",""name"":"""",""type"":""address""}],""name"":""allowance"",""outputs"":[{""internalType"":""uint256"",""name"":"""",""type"":""uint256""}],""payable"":false,""stateMutability"":""view"",""type"":""function""},{""constant"":false,""inputs"":[{""internalType"":""address"",""name"":""spender"",""type"":""address""},{""internalType"":""uint256"",""name"":""value"",""type"":""uint256""}],""name"":""approve"",""outputs"":[{""internalType"":""bool"",""name"":"""",""type"":""bool""}],""payable"":false,""stateMutability"":""nonpayable"",""type"":""function""},{""constant"":true,""inputs"":[{""internalType"":""address"",""name"":"""",""type"":""address""}],""name"":""balanceOf"",""outputs"":[{""internalType"":""uint256"",""name"":"""",""type"":""uint256""}],""payable"":false,""stateMutability"":""view"",""type"":""function""},{""constant"":false,""inputs"":[{""internalType"":""address"",""name"":""to"",""type"":""address""}],""name"":""burn"",""outputs"":[{""internalType"":""uint256"",""name"":""amount0"",""type"":""uint256""},{""internalType"":""uint256"",""name"":""amount1"",""type"":""uint256""}],""payable"":false,""stateMutability"":""nonpayable"",""type"":""function""},{""constant"":true,""inputs"":[],""name"":""decimals"",""outputs"":[{""internalType"":""uint8"",""name"":"""",""type"":""uint8""}],""payable"":false,""stateMutability"":""view"",""type"":""function""},{""constant"":true,""inputs"":[],""name"":""factory"",""outputs"":[{""internalType"":""address"",""name"":"""",""type"":""address""}],""payable"":false,""stateMutability"":""view"",""type"":""function""},{""constant"":true,""inputs"":[],""name"":""getReserves"",""outputs"":[{""internalType"":""uint112"",""name"":""_reserve0"",""type"":""uint112""},{""internalType"":""uint112"",""name"":""_reserve1"",""type"":""uint112""},{""internalType"":""uint32"",""name"":""_blockTimestampLast"",""type"":""uint32""}],""payable"":false,""stateMutability"":""view"",""type"":""function""},{""constant"":false,""inputs"":[{""internalType"":""address"",""name"":""_token0"",""type"":""address""},{""internalType"":""address"",""name"":""_token1"",""type"":""address""}],""name"":""initialize"",""outputs"":[],""payable"":false,""stateMutability"":""nonpayable"",""type"":""function""},{""constant"":true,""inputs"":[],""name"":""kLast"",""outputs"":[{""internalType"":""uint256"",""name"":"""",""type"":""uint256""}],""payable"":false,""stateMutability"":""view"",""type"":""function""},{""constant"":false,""inputs"":[{""internalType"":""address"",""name"":""to"",""type"":""address""}],""name"":""mint"",""outputs"":[{""internalType"":""uint256"",""name"":""liquidity"",""type"":""uint256""}],""payable"":false,""stateMutability"":""nonpayable"",""type"":""function""},{""constant"":true,""inputs"":[],""name"":""name"",""outputs"":[{""internalType"":""string"",""name"":"""",""type"":""string""}],""payable"":false,""stateMutability"":""view"",""type"":""function""},{""constant"":true,""inputs"":[{""internalType"":""address"",""name"":"""",""type"":""address""}],""name"":""nonces"",""outputs"":[{""internalType"":""uint256"",""name"":"""",""type"":""uint256""}],""payable"":false,""stateMutability"":""view"",""type"":""function""},{""constant"":false,""inputs"":[{""internalType"":""address"",""name"":""owner"",""type"":""address""},{""internalType"":""address"",""name"":""spender"",""type"":""address""},{""internalType"":""uint256"",""name"":""value"",""type"":""uint256""},{""internalType"":""uint256"",""name"":""deadline"",""type"":""uint256""},{""internalType"":""uint8"",""name"":""v"",""type"":""uint8""},{""internalType"":""bytes32"",""name"":""r"",""type"":""bytes32""},{""internalType"":""bytes32"",""name"":""s"",""type"":""bytes32""}],""name"":""permit"",""outputs"":[],""payable"":false,""stateMutability"":""nonpayable"",""type"":""function""},{""constant"":true,""inputs"":[],""name"":""price0CumulativeLast"",""outputs"":[{""internalType"":""uint256"",""name"":"""",""type"":""uint256""}],""payable"":false,""stateMutability"":""view"",""type"":""function""},{""constant"":true,""inputs"":[],""name"":""price1CumulativeLast"",""outputs"":[{""internalType"":""uint256"",""name"":"""",""type"":""uint256""}],""payable"":false,""stateMutability"":""view"",""type"":""function""},{""constant"":false,""inputs"":[{""internalType"":""address"",""name"":""to"",""type"":""address""}],""name"":""skim"",""outputs"":[],""payable"":false,""stateMutability"":""nonpayable"",""type"":""function""},{""constant"":false,""inputs"":[{""internalType"":""uint256"",""name"":""amount0Out"",""type"":""uint256""},{""internalType"":""uint256"",""name"":""amount1Out"",""type"":""uint256""},{""internalType"":""address"",""name"":""to"",""type"":""address""},{""internalType"":""bytes"",""name"":""data"",""type"":""bytes""}],""name"":""swap"",""outputs"":[],""payable"":false,""stateMutability"":""nonpayable"",""type"":""function""},{""constant"":true,""inputs"":[],""name"":""symbol"",""outputs"":[{""internalType"":""string"",""name"":"""",""type"":""string""}],""payable"":false,""stateMutability"":""view"",""type"":""function""},{""constant"":false,""inputs"":[],""name"":""sync"",""outputs"":[],""payable"":false,""stateMutability"":""nonpayable"",""type"":""function""},{""constant"":true,""inputs"":[],""name"":""token0"",""outputs"":[{""internalType"":""address"",""name"":"""",""type"":""address""}],""payable"":false,""stateMutability"":""view"",""type"":""function""},{""constant"":true,""inputs"":[],""name"":""token1"",""outputs"":[{""internalType"":""address"",""name"":"""",""type"":""address""}],""payable"":false,""stateMutability"":""view"",""type"":""function""},{""constant"":true,""inputs"":[],""name"":""totalSupply"",""outputs"":[{""internalType"":""uint256"",""name"":"""",""type"":""uint256""}],""payable"":false,""stateMutability"":""view"",""type"":""function""},{""constant"":false,""inputs"":[{""internalType"":""address"",""name"":""to"",""type"":""address""},{""internalType"":""uint256"",""name"":""value"",""type"":""uint256""}],""name"":""transfer"",""outputs"":[{""internalType"":""bool"",""name"":"""",""type"":""bool""}],""payable"":false,""stateMutability"":""nonpayable"",""type"":""function""},{""constant"":false,""inputs"":[{""internalType"":""address"",""name"":""from"",""type"":""address""},{""internalType"":""address"",""name"":""to"",""type"":""address""},{""internalType"":""uint256"",""name"":""value"",""type"":""uint256""}],""name"":""transferFrom"",""outputs"":[{""internalType"":""bool"",""name"":"""",""type"":""bool""}],""payable"":false,""stateMutability"":""nonpayable"",""type"":""function""}]";
               try
               {
                   var contract4 = web3.Eth.GetContract(contractABI, token);
                   var balanceFunction = contract4.GetFunction("balanceOf");
                   var balance4 = await balanceFunction.CallAsync<BigInteger>(MainWindow.wallet);
                   var decimales = await contract4.GetFunction("decimals").CallAsync<int>();
                   if(decimales==18)
                return Web3.Convert
                    .FromWei(
                        balance4,UnitConversion.EthUnit.Ether);
                   else
                   {
                    return Web3.Convert
                        .FromWei(
                            balance4, UnitConversion.EthUnit.Mwei);
                }
               }
               catch (Exception ex)
               {
                   return 0;
               }
           }
       
    }
    internal static class NativeMethods
    {
        // See http://msdn.microsoft.com/en-us/library/ms649021%28v=vs.85%29.aspx
        public const int WM_CLIPBOARDUPDATE = 0x031D;
        public static IntPtr HWND_MESSAGE = new IntPtr(-3);

        // See http://msdn.microsoft.com/en-us/library/ms632599%28VS.85%29.aspx#message_only
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool AddClipboardFormatListener(IntPtr hwnd);
    }
    public class ClipboardManager
    {
        public event EventHandler ClipboardChanged;

        public ClipboardManager(Window windowSource)
        {
            HwndSource source = PresentationSource.FromVisual(windowSource) as HwndSource;
            if (source == null)
            {
                throw new ArgumentException(
                    "Window source MUST be initialized first, such as in the Window's OnSourceInitialized handler."
                    , nameof(windowSource));
            }

            source.AddHook(WndProc);

            // get window handle for interop
            IntPtr windowHandle = new WindowInteropHelper(windowSource).Handle;

            // register for clipboard events
            NativeMethods.AddClipboardFormatListener(windowHandle);
        }

        private void OnClipboardChanged()
        {
            ClipboardChanged?.Invoke(this, EventArgs.Empty);
        }

        private static readonly IntPtr WndProcSuccess = IntPtr.Zero;

        private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            if (msg == NativeMethods.WM_CLIPBOARDUPDATE)
            {
                OnClipboardChanged();
                handled = true;
            }

            return WndProcSuccess;
        }
    }

    public class PairReserve
    {
        public PairReserve()
        {
            Pair = "";
            Reserver = 0;
        }

        public string Pair { get; set; }
        public decimal Reserver { get; set; }
    }
}
