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

const populateModal = (transaction, action) => {
    const id = document.getElementById(`${action}TransactionId`);
    const name = document.getElementById(`${action}TransactionName`);
    const description = document.getElementById(`${action}TransactionDescription`);
    const cost = document.getElementById(`${action}TransactionCost`);
    const month = document.getElementById(`${action}TransactionMonth`);
    const categoryId = document.getElementById(`${action}TransactionCategoryId`);
    const walletId = document.getElementById(`${action}TransactionWalletId`);
    id.value = transaction.id;
    name.value = transaction.name;
    description.value = transaction.description;
    cost.value = transaction.cost;
    month.value = transaction.month;
    categoryId.value = transaction.categoryId;
    walletId.value = transaction.walletId;
}

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

const extractTransaction = (e) => {
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

    return transaction;
}

const editButtons = document.querySelectorAll("#editButtons");

editButtons.forEach(btn => btn.addEventListener("click", (e) => {

    const transaction = extractTransaction(e);

    populateModal(transaction, "update");
}));

const deleteButtons = document.querySelectorAll("#deleteButtons");

deleteButtons.forEach(btn => btn.addEventListener("click", (e) => {
    const transaction = extractTransaction(e);

    populateModal(transaction, "delete");
}))