const Web3 = require('web3')

//let web3 = new Web3('http://127.0.0.1:8545');

//web3.setProvider('http://127.0.0.1:8545');


web3 = new Web3("http://127.0.0.1:8545");

// Get the ether balance of main account on our testnet network - account 0, the one that deployed the contract
account = "0xEbDB99e414fE4A777ac323B99D1F8A243824C705";
web3.eth.getBalance(account, (err, wei) => {
    balance = web3.utils.fromWei(wei, 'ether');
    console.log(balance);
})

// To interact with the smart contract, need to create a contract object, supplying the contracts ABI and address on the network
// Get the ABI by reading from the build json file
const fs = require('fs');
const contract = JSON.parse(fs.readFileSync('C:/Users/User/Desktop/solidity-subcurrency-example/build/contracts/BeastCoin.json', 'utf8'));

contractAddress = "0x22ffd518df34Bf91a8e3C084A6e1199cA5898bc6";

coinContract = new web3.eth.Contract(contract.abi, contractAddress);

// Contract object set up - interact with it

// want to be able to - get balance of an address, call the mint function, call the send function

// Getting the balance of an address, can be called from any address, however something like mint or send will need to be from a particular address
// Might need to use metamask?