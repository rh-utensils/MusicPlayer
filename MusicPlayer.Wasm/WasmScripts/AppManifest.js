var UnoAppManifest = {
    splashScreenImage: "Assets/logo.svg",
    splashScreenColor:
        window.matchMedia &&
            window.matchMedia("(prefers-color-scheme: dark)").matches
            ? "#212121"
            : "#FFFFFF",
    displayName: "RH Music",
};