var userLang = navigator.language || navigator.userLanguage;
userLang = userLang.substr(0, 2).toLowerCase();

if (userLang === "de") {
    lang = {
        loading: "App wird geladen"
    };
} else {
    lang = {
        loading: "App is loading"
    };
}