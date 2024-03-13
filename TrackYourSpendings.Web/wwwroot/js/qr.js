window.addEventListener("load", () => {
    const uri = document.getElementById("qrCodeData").getAttribute('data-url');
    try {
        new QRCode(document.getElementById("qrCode"),
            {
                text: uri,
                width: 150,
                height: 150
            });
    } catch (ex){
        console.error(ex.message);
    }
});