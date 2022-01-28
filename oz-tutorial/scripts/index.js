// scripts/index.js
module.exports = async function main (callback) {
  try {
    
	// Set up a Truffle contract, representing our deployed Box instance
	const Box = artifacts.require('Box');
	const box = await Box.deployed();

	// Send a transaction to store() a new value in the Box
	await box.store(31);

	// Check stored value
	const value = await box.retrieve();
	console.log('Box value is', value.toString());

    callback(0);
  } catch (error) {
    console.error(error);
    callback(1);
  }
};