if (window.matchMedia && window.matchMedia("(prefers-color-scheme: dark)").matches) {
    var UnoAppManifest = {
        splashScreenImage: "Assets/logo.svg",
        splashScreenColor: "#212121",
        displayName: "RH Music"
    };
} else {
    var UnoAppManifest = {
        splashScreenImage: "Assets/logo.svg",
        splashScreenColor: "#FFFFFF",
        displayName: "RH Music"
    };
}