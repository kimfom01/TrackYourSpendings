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

const updateTransaction = (transaction) => {
    const updateId = document.getElementById("updateTransactionId");
    const updateName = document.getElementById("updateTransactionName");
    const updateDescription = document.getElementById("updateTransactionDescription");
    const updateCost = document.getElementById("updateTransactionCost");
    const updateMonth = document.getElementById("updateTransactionMonth");
    const updateCategoryId = document.getElementById("updateTransactionCategoryId");
    const updateWalletId = document.getElementById("updateTransactionWalletId");
    updateId.value = transaction.id;
    updateName.value = transaction.name;
    updateDescription.value = transaction.description;
    updateCost.value = transaction.cost;
    updateMonth.value = transaction.month;
    updateCategoryId.value = transaction.categoryId;
    updateWalletId.value = transaction.walletId;
}

const buttons = document.querySelectorAll("table button");

buttons.forEach(btn => btn.addEventListener("click", (e) => {
    e.preventDefault();
    
    const row = e.target.closest("tr");
    
    const transactionRow = row.children;
    
    const transaction = {
        id: transactionRow[0].innerText,
        name: transactionRow[1].innerText,
        description: transactionRow[2].innerText,
        cost: transactionRow[5].innerText,
        month: months[transactionRow[6].innerText],
        categoryId: transactionRow[7].innerText,
        walletId: transactionRow[9].innerText,
    };

    updateTransaction(transaction)
}))

const months = {
    "January": 1,
    "February": 2,
    "March": 3,
    "April": 4,
    "May": 5,
    "June": 6,
    "July": 7,
    "August": 8,
    "September": 9,
    "October": 10,
    "November": 11,
    "December": 12
}