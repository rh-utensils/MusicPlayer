console.log("RH_MUSIC: Initializing error handling");

window.addEventListener("error", (event) => handleError(event));
window.addEventListener("unhandledrejection", (event) => handleError(event));

function handleError(event) {
    var reloadCounter = getCookie("reloadCounter");

    if (reloadCounter >= 5) {
        window.addEventListener("DOMContentLoaded", () => displayErrorAlert());
        displayErrorAlert();

        function displayErrorAlert() {
            var elements = document.getElementsByClassName("box");
            for (var i = 0; i < elements.length; ++i)
                elements[i].style.visibility = "collapse";

            document.getElementById("errorbox-error").style.visibility = "visible";
        }

        setCookie("reloadCounter", "", 0);
    } else {
        console.warn(
            "caught an error in: '" +
            event.filename +
            "', reason: '" +
            event.message +
            "'"
        );
        console.warn(
            "prevent page from further loading, reload page in 2 seconds ..."
        );

        if (reloadCounter == "") setCookie("reloadCounter", "1", 30);
        else
            setCookie("reloadCounter", (parseInt(reloadCounter) + 1).toString(), 30);

        setTimeout(() => window.location.reload(), 2000);
    }

    window.stop();
}

function setCookie(cName, cValue, exMinutes) {
    var d = new Date();
    d.setMinutes(d.getMinutes() + exMinutes);
    var expires = "expires=" + d.toUTCString();
    document.cookie = cName + "=" + cValue + ";" + expires + ";path=/";
}

function getCookie(cName) {
    var name = cName + "=";
    var decodedCookie = decodeURIComponent(document.cookie);
    var ca = decodedCookie.split(";");
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == " ") {
            c = c.substring(1);
        }
        if (c.indexOf(name) == 0) {
            return c.substring(name.length, c.length);
        }
    }
    return "";
}