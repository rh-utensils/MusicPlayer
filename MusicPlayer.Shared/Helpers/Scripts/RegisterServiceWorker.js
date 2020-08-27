if ("serviceWorker" in navigator) {
    navigator.serviceWorker.register("./workbox.js").then(function(registration) {
            console.debug("[ServiceWorker] Registration successful with scope: ", registration.scope);
        },
        function(err) {
            console.debug("[ServiceWorker] Registration failed: ", err);
        });
} else console.debug("[ServiceWorker] Not supported in Browser");