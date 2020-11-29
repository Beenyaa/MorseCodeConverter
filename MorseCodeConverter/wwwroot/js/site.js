// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {
    $("#englishToMorse").click(function () {
        fetch("/api/EnglishToMorse/" + $("#EnglishTextIn")[0].value)
            .then(res => { return res.json() })
            .then(data => { $("#MorseCodeOut")[0].value = (data.translatedText) })
            .catch(error => console.log(error))
    })

    $("#morseToEnglish").click(function () {
        fetch("/api/MorseToEnglish/" + $("#MorseCodeIn")[0].value)
            .then(res => { return res.json() })
            .then(data => { $("#EnglishTextOut")[0].value = (data.translatedText) })
            .catch(error => console.log(error))
    })
})