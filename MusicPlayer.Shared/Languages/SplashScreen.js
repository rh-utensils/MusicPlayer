var userLang = navigator.language || navigator.userLanguage;
userLang = userLang.substr(0, 2).toLowerCase();

if (userLang === "de") {
    lang = {
        loading: "App wird geladen",
        offline:
            "Eine Verbindung zum Server konnte nicht hergestellt werden, das Laden der Seite kann etwas länger dauern als üblich!",
        error:
            "Es tut uns leid! Beim Laden vom MusicPlayer ist ein Fehler aufgetreten. Bitte versuchen Sie es später erneut!"
    };
} else {
    lang = {
        loading: "App is loading",
        offline:
            "A connection to the server could not be established, the loading of the page could take a little longer than usual!",
        error: "Sorry! There was an error loading the MusicPlayer, please try again later!"
    };
}