let uri = '/addWallet'
    
addWallet = async (e) => {
    e.preventDefault();
    
    // const form = document.querySelector('form');
    console.log(e.target);

    await new Promise(res => setTimeout(res, 60000));

    const wallet = new FormData(form);

    console.log(JSON.stringify(wallet));

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

    // window.location.href = uri;
}