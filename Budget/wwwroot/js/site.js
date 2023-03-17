"use strict";

const addWallet = async () => {
    let uri = '/Home/AddWallet'
    
    const name = document.getElementById('walletName').value;
    const income = document.getElementById('walletIncome').value;
    
    const wallet = {
        name,
        income
    }

    fetch(uri, {
        method: 'post',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(wallet)
    })
        .then(res => console.log(res.status))
        .catch(err => console.error(err));
};

const addTransaction = async () => {
    const uri = '/Home/AddTransaction';
    
    const name = document.getElementById("transactionName").value;
    const description = document.getElementById("transactionDescription").value;
    const month = document.getElementById("transactionMonth").value;
    const cost = document.getElementById("transactionCost").value;
    const categoryId = document.getElementById("categoryId").value;
    
    const transaction = {
        name,
        description,
        month,
        cost,
        categoryId
    }

    fetch(uri, {
        method: 'post',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(transaction)
    })
        .then(res => console.log(res.status))
        .catch(err => console.error(err));
};
