self.importScripts("https://storage.googleapis.com/workbox-cdn/releases/5.1.2/workbox-sw.js");

workbox.loadModule("workbox-strategies");
workbox.loadModule("workbox-routing");

workbox.routing.registerRoute(
    new RegExp("/.*"),
    new workbox.strategies.NetworkFirst()
);