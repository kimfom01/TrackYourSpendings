"use strict";

const populateModal = (transaction, action) => {
    const id = document.getElementById(`${action}TransactionId`);
    const name = document.getElementById(`${action}TransactionName`);
    const description = document.getElementById(`${action}TransactionDescription`);
    const cost = document.getElementById(`${action}TransactionCost`);
    const categoryId = document.getElementById(`${action}TransactionCategoryId`);
    const walletId = document.getElementById(`${action}TransactionWalletId`);

    id.value = transaction.id;
    name.value = transaction.name;
    description.value = transaction.description;
    cost.value = transaction.cost;
    categoryId.value = transaction.categoryId;
    walletId.value = transaction.walletId;
}


const extractTransaction = (e) => {
    e.preventDefault();

    const row = e.target.closest("tr");

    const transactionRow = row.children;

    return {
        id: transactionRow[0].innerText,
        name: transactionRow[1].innerText,
        description: transactionRow[2].innerText,
        cost: transactionRow[4].innerText,
        categoryId: transactionRow[5].innerText,
        walletId: transactionRow[7].innerText,
    };
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

const viewButtons = document.querySelectorAll("#viewButtons");

viewButtons.forEach(btn => btn.addEventListener("click", (e) => {

    const transaction = extractTransaction(e);

    populateModal(transaction, "view");
}));