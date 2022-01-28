
/*

The basics of the site are done. There is 4 main functions implemented, each in their own little HTML box/div. 
    - Authorize metamask, and display your address, your ETH balance, and COOL balance
    - Check the balance of any address
    - Transfer COOL tokens to addresses
    - Call the mintFreeTokens() function from the smart contract to mint COOL tokens.
The implementations of these shows the basics on how you would create a website to interact with the ethereum blockchain.

Things that could be added to make it all more smooth and finished etc:
    - What happens when switching accounts? What happens when more than one account is connected? Update the account infos, make sure it all works
    - If metamask is not logged in, display so, and disable lots of the functionality - include a button to then login to metamask
    - Better logging - when a transfer transaction is made or a mint transaction, display the data better, like the tx hash, details etc - display somewhere nice maybe
    - Better error handiling - what if not connected? What if invalid eth address? What if can't mint any more tokens? What if don't have the amount of tokens to transfer?
                - display error messages, make sure nothing 'bad' happens
    - Fix the MM transfer display bug - when sending COOL tokens, the MM dialog displays an incorrect amount of tokens being txd, e.g. sending 5k tokens has like "0.000000005 cool"
    - When minting tokens, update the tokens minted label. Update the user info label? 
    - Implement ERC721 token and NFT functions.
*/

// Web3 object
web3 = null;

// Connected account details
accountAddress = null;
connected = false;

// ERC-20 COOL token contract details
erc20contractAbi = null; // set by reading the artifact json
const erc20contractAddress = "0x2122B68Af3f32803f45130aEB027cc4B4c861Dc3"; // update when deployed
var erc20contract;

// On page load, check if web3 is injected (metamask or others), if so, ask for accounts, if not, tell user to connect
window.addEventListener('load', async function() {
    
    await initializeMetamask();
    await initializeErc20Contract(); // TODO: this should only be called if provider was set...
    await updateUserInfo();

    addEventListeners();
    

});

// Add event listeners for HTML elements. Function called on window load.
function addEventListeners() {

    // Add balance check button listener
    const checkAddressButton = document.getElementById("checkBalanceButtonInput")
    checkAddressButton.addEventListener('click', async function(event) {
        var addressToCheck = document.getElementById("checkBalanceTextInput").value;
        checkAddressBalances(addressToCheck);
        // TODO: Deal with invalid/blank ethereum address
    });

    // Add transfer button listener
    const transferButton = document.getElementById("transferButtonInput");
    transferButton.addEventListener('click', async function(event) {
        var addressToTransfer = document.getElementById("transferAddressTextInput").value;
        var amountToTransfer = document.getElementById("transferAmountTextInput").value;

        transferToken(addressToTransfer, amountToTransfer);
    });

    // Add mint button listener
    const mintButton = document.getElementById("mintButtonInput");
    mintButton.addEventListener('click', async function(event) {
        mintTokens();
    });

}

// TODO: If metamask is not installed/logged in, web3 instance never set, nothing really works
//          Do something about that I guess..
async function initializeMetamask() {

    if(window.ethereum) {
        // Metamask is installed        
        // Request account access
        try {
            accountAddress = (await window.ethereum.request( { method: "eth_requestAccounts" } ))[0];
            connected = true;
            
            web3 = new Web3(window.ethereum);
        }
        catch (error) {
            // User denied account access
            accountAddress = null;
            connected = false;
        }
    }
    else {
        // Metamask not installed
        accountAddress = null;
        connected = false;
    }

    console.log("Connected: " + connected);
    console.log("Account address: " + accountAddress);

}

function initializeErc20Contract() {
    // Read the artifcats json to get the contract abi. Only do this if MM connected, and thus, web3 object set.
    if(connected) {
        $.getJSON('MyToken.json', function(data) {
            erc20contractAbi = data.abi;
            erc20contract = new web3.eth.Contract(erc20contractAbi, erc20contractAddress);
        });
    }
}

// Gets the users ETH + token balance, and updates the HTML divs to display users address and balance
async function updateUserInfo() {
    address = "METAMASK NOT CONNECTED";
    balance = "METAMASK NOT CONNECTED";
    balanceToken = "METAMASK NOT CONNECTED";

    if(connected) {
        address = accountAddress; 
        balance = (await getEtherBalance(address)).toString() + " ETH";
        balanceToken = (await getTokenBalance(address)).toString() + " COOL";
    }

    // Set HTML elements
    address = "Your address: " + address;
    balance = "Your balance: " + balance;
    balanceToken = "Token balance: " + balanceToken;
    document.getElementById("infoAddress").textContent = address;
    document.getElementById("infoBalance").textContent = balance;
    document.getElementById("infoTokenBalance").textContent = balanceToken;

}

async function checkAddressBalances(address) {
    var etherBalance = "Balance: ";
    var tokenBalance = "Token balance: ";

    etherBalance = etherBalance + (await getEtherBalance(address)).toString() + " ETH";
    tokenBalance = tokenBalance + (await getTokenBalance(address)).toString() + " COOL";

    // Set HTML elements
    document.getElementById("checkedBalanceEthLabel").textContent = etherBalance;
    document.getElementById("checkedBalanceTokenLabel").textContent = tokenBalance;
}

// Get the ether balance of an address - returns the value in Ether
async function getEtherBalance(address) {
    if(!connected) return;
    var balance = await web3.eth.getBalance(address);
    return web3.utils.fromWei(balance);
}

// Get the COOL token balance of an address
async function getTokenBalance(address) {
    if(!connected) return;
    var balance = await erc20contract.methods.balanceOf(address).call();
    return balance;
}

// Make the MM send transaction dialog pop up to send COOL tokens from sender to toAddress
async function transferToken(toAddress, amount) {
    if(!connected) return;
    
    var contractData = erc20contract.methods.transfer(toAddress, amount).encodeABI();

    var txParams = {
        from: accountAddress,
        to: erc20contractAddress,
        data: contractData
    };

    ethereum.request({
        method: 'eth_sendTransaction',
        params: [txParams]
    }).then((txHash) => console.log(txHash))
    .catch((error) => console.error);

    // The metamask pop up is a bit weird for the amount of tokens being transferred, think to do with the decimals, but it transfers correctly.
}

// Call the mintFreeTokens method from the smart contract to mint some free COOL
async function mintTokens() {
    await erc20contract.methods.mintFreeTokens()
    .send({from: accountAddress})
    .on('transactionHash', function(hash) { console.log(hash); })
}