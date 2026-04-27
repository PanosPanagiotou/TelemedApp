window.modal = {
    registerEscape: function (dotnet) {
        document.addEventListener("keydown", e => {
            if (e.key === "Escape") {
                dotnet.invokeMethodAsync("CloseFromJs");
            }
        });
    }
};