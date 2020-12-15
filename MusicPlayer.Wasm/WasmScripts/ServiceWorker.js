if ("serviceWorker" in navigator) {
    window.addEventListener("load",
        () => {
            navigator.serviceWorker
                .register("./workbox.js", { scope: "./" })
                .then(() => console.log("SERVICE_WORKER: Registered"))
                .catch((error) =>
                    console.warn("SERVICE_WORKER: Registration failed: ", error)
                );
        });
}