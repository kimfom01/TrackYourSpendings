let wallet;

const getActiveWallet = async () => {
    const response = await fetch(`/Reports/wallets/d-active`);

    return await response.json();
}

const getWallet = async () => {
    const walletId = document.getElementById("walletId").value;

    const response = await fetch(`/Reports/wallets/q?walletId=${walletId}`);

    const wallet = await response.json();

    console.table(wallet);

    return wallet;
}

const getWallets = async () => {
    const response = await fetch("/Reports/wallets");

    return await response.json();
}

window.addEventListener('load', async () => {
    try {
        wallet = await getActiveWallet();
        const container = document.getElementById("chart_div");

        container && wallet && google.charts.load('current', {'packages': ['corechart']});
        container && wallet && google.charts.setOnLoadCallback(drawChart);

        const wallets = await getWallets();

        const walletList = document.getElementById("walletId");
        walletList && wallet && walletList.appendChild(new Option(wallet.name, wallet.id, false, true))
        walletList && wallets.map((wall, idx) => {
            walletList.appendChild(new Option(wall.name, wall.id));
        })
    } catch (ex) {
        console.log(ex)
    }
})

const loadReportBtn = document.getElementById("load-report-btn");
loadReportBtn && loadReportBtn.addEventListener("click", async () => {
    wallet = await getWallet();
    await drawChart();
})

async function drawChart() {
    const transactionsGroups = Object.groupBy(wallet.transactions, tr => tr.category.name);

    const data = new google.visualization.DataTable();
    data.addColumn('string', 'Category');
    data.addColumn('number', 'Month');

    const rows = [];
    for (const [key] of Object.entries(transactionsGroups)) {
        rows.push([key, transactionsGroups[key].length])
    }
    
    data.addRows(rows)

    const options = {
        'title': `${wallet.name}`,
        'width': 400,
        'height': 300
    };

    const chart = new google.visualization.PieChart(document.getElementById('chart_div'));
    chart.draw(data, options);
}